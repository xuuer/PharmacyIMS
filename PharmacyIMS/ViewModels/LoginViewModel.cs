using PharmacyIMS.Data;
using PharmacyIMS.Helpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "";
        private string _password = "";
        private string _message = "";

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(DoLogin, CanLogin);
        }

        private bool CanLogin(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void DoLogin(object? parameter)
        {
            try
            {
                using var db = new AppDbContext();
                var user = db.Users.FirstOrDefault(u => u.Username == Username && u.IsActive);
                if (user != null && PasswordHelper.VerifyPassword(Password, user.PasswordHash))
                {
                    Session.Login(user);

                    var mainWindow = new MainWindow
                    {
                        DataContext = new MainViewModel(user)
                    };
                    mainWindow.Show();
                    Application.Current.MainWindow = mainWindow;

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is LoginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                else
                               {
                    Message = "用户名或者密码错误";
                }
            }
            catch (Exception ex)
            {
                Message = "登录失败；无法连接数据库，请联系管理员";
                MessageBox.Show(ex.Message, "数据库错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
