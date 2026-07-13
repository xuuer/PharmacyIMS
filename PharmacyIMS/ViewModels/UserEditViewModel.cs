using PharmacyIMS.Data;
using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class UserEditViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly bool _isEdit;

        public User User { get; set; }

        public List<UserRole> Roles { get; } = new()
        {
            UserRole.Admin,
            UserRole.Manager,
            UserRole.Purchaser,
            UserRole.Salesperson
        };

        public string PlainPassword { get; set; } = "";

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public string WindowTitle => _isEdit ? "编辑用户" : "新增用户";

        public UserEditViewModel(Window window, User? user = null)
        {
            _window = window;
            _isEdit = user != null;

            if (user != null)
            {
                User = new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    RealName = user.RealName,
                    Role = user.Role,
                    Phone = user.Phone,
                    IsActive = user.IsActive,
                    CreateTime = user.CreateTime
                };
            }
            else
            {
                User = new User
                {
                    Role = UserRole.Salesperson,
                    IsActive = true
                };
            }

            SaveCommand = new RelayCommand(DoSave, CanSave);
            CancelCommand = new RelayCommand(DoCancel);
        }

        private bool CanSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(User.Username)
                && !string.IsNullOrWhiteSpace(User.RealName)
                && (_isEdit || !string.IsNullOrWhiteSpace(PlainPassword));
        }

        private void DoSave(object? parameter)
        {
            try
            {
                using var db = new AppDbContext();

                if (db.Users.Any(u => u.Username == User.Username && u.Id != User.Id))
                {
                    MessageBox.Show("用户名已存在！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(PlainPassword))
                {
                    User.PasswordHash = PasswordHelper.HashPassword(PlainPassword);
                }

                if (_isEdit)
                {
                    db.Users.Update(User);
                }
                else
                {
                    User.CreateTime = DateTime.Now;
                    db.Users.Add(User);
                }
                db.SaveChanges();
                _window.DialogResult = true;
                _window.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoCancel(object? parameter)
        {
            _window.DialogResult = false;
            _window.Close();
        }
    }
}
