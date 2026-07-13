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
    public class MedicineEditViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly bool _isEdit;

        public Medicine Medicine { get; set; }

        public List<MedicineCategory> Categories { get; private set; } = new();

        public List<PrescriptionType> PrescriptionTypes { get; } = new()
        {
            PrescriptionType.Prescription,
            PrescriptionType.OTC,
            PrescriptionType.TraditionalChinese,
            PrescriptionType.HealthProduct
        };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public string WindowTitle => _isEdit ? "编辑药品" : "新增药品";

        public MedicineEditViewModel(Window window, Medicine? medicine = null)
        {
            _window = window;
            _isEdit = medicine != null;

            if (medicine != null)
            {
                Medicine = new Medicine
                {
                    Id = medicine.Id,
                    MedicineCode = medicine.MedicineCode,
                    MedicineName = medicine.MedicineName,
                    GenericName = medicine.GenericName,
                    Specification = medicine.Specification,
                    DosageForm = medicine.DosageForm,
                    Manufacturer = medicine.Manufacturer,
                    ApprovalNo = medicine.ApprovalNo,
                    PrescriptionType = medicine.PrescriptionType,
                    PurchasePrice = medicine.PurchasePrice,
                    SalePrice = medicine.SalePrice,
                    StockQuantity = medicine.StockQuantity,
                    StockAlertLevel = medicine.StockAlertLevel,
                    ExpiryDate = medicine.ExpiryDate,
                    Remark = medicine.Remark,
                    CategoryId = medicine.CategoryId,
                    CreateTime = medicine.CreateTime
                };
            }
            else
            {
                Medicine = new Medicine
                {
                    MedicineCode = $"YP-{DateTime.Now:yyyyMMdd}{new Random().Next(100, 999)}",
                    PurchasePrice = 0,
                    SalePrice = 0,
                    StockQuantity = 0,
                    StockAlertLevel = 10,
                    PrescriptionType = PrescriptionType.OTC,
                    ExpiryDate = DateTime.Now.AddYears(2)
                };
            }

            SaveCommand = new RelayCommand(DoSave, CanSave);
            CancelCommand = new RelayCommand(DoCancel);

            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                using var db = new AppDbContext();
                Categories = db.MedicineCategories.OrderBy(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载分类失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Medicine.MedicineCode)
                && !string.IsNullOrWhiteSpace(Medicine.MedicineName)
                && Medicine.CategoryId > 0;
        }

        private void DoSave(object? parameter)
        {
            try
            {
                using var db = new AppDbContext();
                if (_isEdit)
                {
                    db.Medicines.Update(Medicine);
                }
                else
                {
                    Medicine.CreateTime = DateTime.Now;
                    db.Medicines.Add(Medicine);
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
