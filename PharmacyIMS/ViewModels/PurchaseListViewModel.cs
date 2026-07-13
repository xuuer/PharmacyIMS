using PharmacyIMS.Data;
using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using PharmacyIMS.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class PurchaseListViewModel : ViewModelBase
    {
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _orderCount;
        public int OrderCount
        {
            get => _orderCount;
            set => SetProperty(ref _orderCount, value);
        }

        private PurchaseOrder? _selectedOrder;
        public PurchaseOrder? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        private List<PurchaseOrder> _allOrders = new();
        public ObservableCollection<PurchaseOrder> Orders { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand DeleteCommand { get; }

        public PurchaseListViewModel()
        {
            SearchCommand = new RelayCommand(DoSearch);
            AddCommand = new RelayCommand(DoAdd);
            ViewCommand = new RelayCommand(DoView, p => SelectedOrder != null);
            DeleteCommand = new RelayCommand(DoDelete, p => SelectedOrder != null);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                _allOrders = db.PurchaseOrders
                    .Include(o => o.Supplier)
                    .OrderByDescending(o => o.PurchaseDate)
                    .ToList();
                RefreshList(_allOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载采购数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<PurchaseOrder> query = _allOrders;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(o => o.OrderNo.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    (o.Supplier != null && o.Supplier.SupplierName.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
            }

            RefreshList(query.ToList());
        }

        private void DoAdd(object? parameter)
        {
            var win = new PurchaseEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new PurchaseEditViewModel(win);
            if (win.ShowDialog() == true)
            {
                ReloadOrders();
            }
        }

        private void DoView(object? parameter)
        {
            if (SelectedOrder == null) return;

            var win = new PurchaseEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new PurchaseEditViewModel(win, SelectedOrder, isReadOnly: true);
            win.ShowDialog();
        }

        private void DoDelete(object? parameter)
        {
            if (SelectedOrder == null) return;

            var result = MessageBox.Show($"确定要删除采购单\"{SelectedOrder.OrderNo}\"吗？删除后将扣减相应库存。", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                // 扣减库存（删除入库相当于撤销入库）
                var details = db.PurchaseOrderDetails.Where(d => d.PurchaseOrderId == SelectedOrder.Id).ToList();
                foreach (var detail in details)
                {
                    var medicine = db.Medicines.Find(detail.MedicineId);
                    if (medicine != null)
                    {
                        medicine.StockQuantity -= detail.Quantity;
                        if (medicine.StockQuantity < 0) medicine.StockQuantity = 0;
                    }
                }

                var entity = db.PurchaseOrders.Find(SelectedOrder.Id);
                if (entity != null)
                {
                    db.PurchaseOrders.Remove(entity);
                }
                db.SaveChanges();
                ReloadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshList(List<PurchaseOrder> list)
        {
            Orders.Clear();
            foreach (var order in list)
            {
                Orders.Add(order);
            }
            OrderCount = Orders.Count;
        }

        private void ReloadOrders()
        {
            try
            {
                using var db = new AppDbContext();
                _allOrders = db.PurchaseOrders.Include(o => o.Supplier).OrderByDescending(o => o.PurchaseDate).ToList();
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
