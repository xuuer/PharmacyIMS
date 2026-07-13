using PharmacyIMS.Data;
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
    public class SaleListViewModel : ViewModelBase
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

        private SaleOrder? _selectedOrder;
        public SaleOrder? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        private List<SaleOrder> _allOrders = new();
        public ObservableCollection<SaleOrder> Orders { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand DeleteCommand { get; }

        public SaleListViewModel()
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
                _allOrders = db.SaleOrders
                    .OrderByDescending(o => o.SaleDate)
                    .ToList();
                RefreshList(_allOrders);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载销售数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<SaleOrder> query = _allOrders;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(o => o.OrderNo.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    (o.CustomerName != null && o.CustomerName.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
            }

            RefreshList(query.ToList());
        }

        private void DoAdd(object? parameter)
        {
            var win = new SaleEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new SaleEditViewModel(win);
            if (win.ShowDialog() == true)
            {
                ReloadOrders();
            }
        }

        private void DoView(object? parameter)
        {
            if (SelectedOrder == null) return;

            var win = new SaleEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new SaleEditViewModel(win, SelectedOrder, isReadOnly: true);
            win.ShowDialog();
        }

        private void DoDelete(object? parameter)
        {
            if (SelectedOrder == null) return;

            var result = MessageBox.Show($"确定要删除销售单\"{SelectedOrder.OrderNo}\"吗？删除后将恢复相应库存。", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                // 恢复库存
                var details = db.SaleOrderDetails.Where(d => d.SaleOrderId == SelectedOrder.Id).ToList();
                foreach (var detail in details)
                {
                    var medicine = db.Medicines.Find(detail.MedicineId);
                    if (medicine != null)
                    {
                        medicine.StockQuantity += detail.Quantity;
                    }
                }

                var entity = db.SaleOrders.Find(SelectedOrder.Id);
                if (entity != null)
                {
                    db.SaleOrders.Remove(entity);
                }
                db.SaveChanges();
                ReloadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshList(List<SaleOrder> list)
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
                _allOrders = db.SaleOrders.OrderByDescending(o => o.SaleDate).ToList();
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
