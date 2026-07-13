using PharmacyIMS.Data;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class SupplierEditViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly bool _isEdit;

        public Supplier Supplier { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public string WindowTitle => _isEdit ? "编辑供应商" : "新增供应商";

        public SupplierEditViewModel(Window window, Supplier? supplier = null)
        {
            _window = window;
            _isEdit = supplier != null;

            if (supplier != null)
            {
                Supplier = new Supplier
                {
                    Id = supplier.Id,
                    SupplierName = supplier.SupplierName,
                    ContactPerson = supplier.ContactPerson,
                    Phone = supplier.Phone,
                    Address = supplier.Address,
                    LicenseNo = supplier.LicenseNo,
                    IsActive = supplier.IsActive,
                    CreateTime = supplier.CreateTime
                };
            }
            else
            {
                Supplier = new Supplier
                {
                    IsActive = true
                };
            }

            SaveCommand = new RelayCommand(DoSave, CanSave);
            CancelCommand = new RelayCommand(DoCancel);
        }

        private bool CanSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Supplier.SupplierName);
        }

        private void DoSave(object? parameter)
        {
            try
            {
                using var db = new AppDbContext();
                if (_isEdit)
                {
                    db.Suppliers.Update(Supplier);
                }
                else
                {
                    Supplier.CreateTime = DateTime.Now;
                    db.Suppliers.Add(Supplier);
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
