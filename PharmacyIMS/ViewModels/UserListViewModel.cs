using PharmacyIMS.Data;
using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using PharmacyIMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class UserListViewModel : ViewModelBase
    {
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _userCount;
        public int UserCount
        {
            get => _userCount;
            set => SetProperty(ref _userCount, value);
        }

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        private List<User> _allUsers = new();
        public ObservableCollection<User> Users { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public UserListViewModel()
        {
            SearchCommand = new RelayCommand(DoSearch);
            AddCommand = new RelayCommand(DoAdd);
            EditCommand = new RelayCommand(DoEdit, p => SelectedUser != null);
            DeleteCommand = new RelayCommand(DoDelete, p => SelectedUser != null);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                _allUsers = db.Users.OrderBy(u => u.Id).ToList();
                RefreshList(_allUsers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载用户数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<User> query = _allUsers;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(u => u.Username.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    u.RealName.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            RefreshList(query.ToList());
        }

        private void DoAdd(object? parameter)
        {
            var win = new UserEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new UserEditViewModel(win);
            if (win.ShowDialog() == true)
            {
                ReloadUsers();
            }
        }

        private void DoEdit(object? parameter)
        {
            if (SelectedUser == null) return;

            var win = new UserEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new UserEditViewModel(win, SelectedUser);
            if (win.ShowDialog() == true)
            {
                ReloadUsers();
            }
        }

        private void DoDelete(object? parameter)
        {
            if (SelectedUser == null) return;

            if (SelectedUser.Username == "admin")
            {
                MessageBox.Show("不能删除系统管理员账号！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"确定要删除用户\"{SelectedUser.RealName}\"吗？", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                var entity = db.Users.Find(SelectedUser.Id);
                if (entity != null)
                {
                    db.Users.Remove(entity);
                    db.SaveChanges();
                }
                ReloadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshList(List<User> list)
        {
            Users.Clear();
            foreach (var user in list)
            {
                Users.Add(user);
            }
            UserCount = Users.Count;
        }

        private void ReloadUsers()
        {
            try
            {
                using var db = new AppDbContext();
                _allUsers = db.Users.OrderBy(u => u.Id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DoSearch(null);
        }
    }
}
