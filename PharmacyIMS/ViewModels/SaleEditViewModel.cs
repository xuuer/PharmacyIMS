using PharmacyIMS.Data;
using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class SaleEditViewModel : ViewModelBase
    {
        private readonly Window _window;
        private readonly bool _isReadOnly;

        private SaleOrderDetail? _selectedDetail;
        public SaleOrderDetail? SelectedDetail
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

        public SaleOrder Order { get; set; }

        public ObservableCollection<SaleOrderDetail> Details { get; set; } = new();

        public List<Medicine> Medicines { get; private set; } = new();

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddDetailCommand { get; }
        public ICommand RemoveDetailCommand { get; }

        public string WindowTitle => _isReadOnly ? "查看销售单" : "新增销售单";
        public bool IsReadOnly => _isReadOnly;

        public SaleEditViewModel(Window window, SaleOrder? order = null, bool isReadOnly = false)
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
                Order = new SaleOrder
                {
                    OrderNo = $"XS{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(10, 99)}",
                    SaleDate = DateTime.Now,
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

            LoadMedicines();
        }

        private void LoadDetails(int orderId)
        {
            try
            {
                using var db = new AppDbContext();
                var details = db.SaleOrderDetails
                    .Where(d => d.SaleOrderId == orderId)
                    .Include(d => d.Medicine)
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
            return !_isReadOnly && Details.Count > 0;
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
                // 检查库存是否足够（在同一DbContext中检查，保证事务一致性）
                foreach (var detail in Details)
                {
                    var medicine = db.Medicines.Find(detail.MedicineId);
                    if (medicine != null && medicine.StockQuantity < detail.Quantity)
                    {
                        MessageBox.Show($"药品【{medicine.MedicineName}】库存不足，当前库存 {medicine.StockQuantity}，需要 {detail.Quantity}",
                            "库存不足", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }
                Order.TotalAmount = Details.Sum(d => d.SubTotal);
                Order.OperatorName = Session.CurrentUser?.RealName ?? "admin";
                Order.CreateTime = DateTime.Now;
                db.SaleOrders.Add(Order);
                db.SaveChanges();

                foreach (var detail in Details)
                {
                    detail.SaleOrderId = Order.Id;
                    db.SaleOrderDetails.Add(detail);

                    // 扣减库存
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
                MessageBox.Show("没有可供选择的药品（库存为零）", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var medicine = Medicines[0];
            var detail = new SaleOrderDetail
            {
                MedicineId = medicine.Id,
                Medicine = medicine,
                Quantity = 1,
                UnitPrice = medicine.SalePrice,
                SubTotal = medicine.SalePrice
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
                        detail.UnitPrice = detail.Medicine.SalePrice;
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
