using PharmacyIMS.Data;
using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class PurchaseReturnEditViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly bool _isReadOnly;

        private PurchaseReturnOrderDetail? _selectedDetail;
        public PurchaseReturnOrderDetail? SelectedDetail
        {
            get => _selectedDetail;
            set
            {
                if (SetProperty(ref _selectedDetail, value))
                {
                    (RemoveDetailCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public PurchaseReturnOrder Order { get; set; }
        public ObservableCollection<PurchaseReturnOrderDetail> Details { get; set; } = new();
        public List<Supplier> Suppliers { get; private set; } = new();
        public List<Medicine> Medicines { get; private set; } = new();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddDetailCommand { get; }
        public ICommand RemoveDetailCommand { get; }

        public string WindowTitle => _isReadOnly ? "查看采购退货单" : "新增采购退货单";
        public bool IsReadOnly => _isReadOnly;

        public PurchaseReturnEditViewModel(Window window, PurchaseReturnOrder? order = null, bool isReadOnly = false)
        {
            _window = window;
            _isReadOnly = isReadOnly;

            if (order != null)
            {
                Order = order;
                LoadDetails(order.Id);
            }
            else
            {
                Order = new PurchaseReturnOrder
                {
                    OrderNo = $"CGTH{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(10, 99)}",
                    ReturnDate = DateTime.Now,
                    Status = OrderStatus.Completed
                };
            }

            SaveCommand = new RelayCommand(DoSave, CanSave);
            CancelCommand = new RelayCommand(DoCancel);
            AddDetailCommand = new RelayCommand(DoAddDetail);
            RemoveDetailCommand = new RelayCommand(DoRemoveDetail, CanRemoveDetail);

            // 订阅集合变化，自动刷新保存按钮状态
            Details.CollectionChanged += (s, e) =>
            {
                (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
            };

            LoadSuppliers();
            LoadMedicines();
        }

        private void LoadDetails(int orderId)
        {
            try
            {
                using var db = new AppDbContext();
                var details = db.PurchaseReturnOrderDetails
                    .Where(d => d.PurchaseReturnOrderId == orderId)
                    .ToList();
                Details.Clear();
                foreach (var d in details)
                {
                    Details.Add(d);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载明细失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using var db = new AppDbContext();
                Suppliers = db.Suppliers.Where(s => s.IsActive).OrderBy(s => s.Id).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载供应商失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadMedicines()
        {
            try
            {
                using var db = new AppDbContext();
                Medicines = db.Medicines.Where(m => m.StockQuantity > 0).OrderBy(m => m.MedicineName).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载药品失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSave(object? parameter)
        {
            return !_isReadOnly && Order.SupplierId > 0 && Details.Count > 0;
        }

        private bool CanRemoveDetail(object? parameter)
        {
            return !_isReadOnly && SelectedDetail != null;
        }

        private void DoSave(object? parameter)
        {
            try
            {
                RecalcDetails();
                using var db = new AppDbContext();
                Order.TotalAmount = Details.Sum(d => d.SubTotal);
                Order.OperatorName = Session.CurrentUser?.RealName ?? "admin";
                Order.CreateTime = DateTime.Now;
                db.PurchaseReturnOrders.Add(Order);
                db.SaveChanges();

                foreach (var detail in Details)
                {
                    detail.PurchaseReturnOrderId = Order.Id;
                    db.PurchaseReturnOrderDetails.Add(detail);

                    var medicine = db.Medicines.Find(detail.MedicineId);
                    if (medicine != null)
                    {
                        medicine.StockQuantity -= detail.Quantity;
                        if (medicine.StockQuantity < 0) medicine.StockQuantity = 0;
                    }
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

        private void DoAddDetail(object? parameter)
        {
            if (Medicines.Count == 0)
            {
                MessageBox.Show("没有可供选择的药品", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var medicine = Medicines[0];
            var detail = new PurchaseReturnOrderDetail
            {
                MedicineId = medicine.Id,
                Medicine = medicine,
                Quantity = 1,
                UnitPrice = medicine.PurchasePrice,
                SubTotal = medicine.PurchasePrice
            };
            Details.Add(detail);
            SelectedDetail = detail;
            RecalcTotal();
        }

        private void DoRemoveDetail(object? parameter)
        {
            if (SelectedDetail == null) return;

            var result = MessageBox.Show($"确定要移除【{SelectedDetail.Medicine?.MedicineName ?? ""}】吗？", "移除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            Details.Remove(SelectedDetail);
            SelectedDetail = null;
            RecalcTotal();
        }

        public void RecalcDetails()
        {
            foreach (var detail in Details)
            {
                if (detail.Medicine == null || detail.Medicine.Id != detail.MedicineId)
                {
                    detail.Medicine = Medicines.FirstOrDefault(m => m.Id == detail.MedicineId);
                    if (detail.Medicine != null)
                    {
                        detail.UnitPrice = detail.Medicine.PurchasePrice;
                    }
                }
                detail.SubTotal = detail.Quantity * detail.UnitPrice;
            }
            RecalcTotal();
        }

        private void RecalcTotal()
        {
            Order.TotalAmount = Details.Sum(d => d.SubTotal);
            OnPropertyChanged(nameof(Order));
        }
    }
}
