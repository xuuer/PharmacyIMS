using PharmacyIMS.Data;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PharmacyIMS.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private int _totalMedicineCount;
        public int TotalMedicineCount
        {
            get => _totalMedicineCount;
            set => SetProperty(ref _totalMedicineCount, value);
        }

        private int _totalStockValue;
        public int TotalStockValue
        {
            get => _totalStockValue;
            set => SetProperty(ref _totalStockValue, value);
        }

        private int _todayPurchaseCount;
        public int TodayPurchaseCount
        {
            get => _todayPurchaseCount;
            set => SetProperty(ref _todayPurchaseCount, value);
        }

        private decimal _todayPurchaseAmount;
        public decimal TodayPurchaseAmount
        {
            get => _todayPurchaseAmount;
            set => SetProperty(ref _todayPurchaseAmount, value);
        }

        private int _todaySaleCount;
        public int TodaySaleCount
        {
            get => _todaySaleCount;
            set => SetProperty(ref _todaySaleCount, value);
        }

        private decimal _todaySaleAmount;
        public decimal TodaySaleAmount
        {
            get => _todaySaleAmount;
            set => SetProperty(ref _todaySaleAmount, value);
        }

        private int _lowStockCount;
        public int LowStockCount
        {
            get => _lowStockCount;
            set => SetProperty(ref _lowStockCount, value);
        }

        private int _nearExpiryCount;
        public int NearExpiryCount
        {
            get => _nearExpiryCount;
            set => SetProperty(ref _nearExpiryCount, value);
        }

        public List<Medicine> LowStockMedicines { get; set; } = new();
        public List<Medicine> NearExpiryMedicines { get; set; } = new();

        public DashboardViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                var medicines = db.Medicines.ToList();
                var today = DateTime.Now.Date;

                TotalMedicineCount = medicines.Count;
                TotalStockValue = (int)medicines.Sum(m => m.StockQuantity * m.PurchasePrice);

                var todayPurchases = db.PurchaseOrders.Where(o => o.PurchaseDate.Date == today).ToList();
                var todayPurchaseReturns = db.PurchaseReturnOrders.Where(o => o.ReturnDate.Date == today).ToList();
                TodayPurchaseCount = todayPurchases.Count;
                TodayPurchaseAmount = todayPurchases.Sum(o => o.TotalAmount) - todayPurchaseReturns.Sum(o => o.TotalAmount);

                var todaySales = db.SaleOrders.Where(o => o.SaleDate.Date == today).ToList();
                var todaySaleReturns = db.SaleReturnOrders.Where(o => o.ReturnDate.Date == today).ToList();
                TodaySaleCount = todaySales.Count;
                TodaySaleAmount = todaySales.Sum(o => o.TotalAmount) - todaySaleReturns.Sum(o => o.TotalAmount);

                LowStockCount = medicines.Count(m => m.StockQuantity <= m.StockAlertLevel);
                NearExpiryCount = medicines.Count(m => m.ExpiryDate.HasValue && m.ExpiryDate.Value <= DateTime.Now.AddMonths(3));

                LowStockMedicines = medicines.Where(m => m.StockQuantity <= m.StockAlertLevel).Take(5).ToList();
                NearExpiryMedicines = medicines.Where(m => m.ExpiryDate.HasValue && m.ExpiryDate.Value <= DateTime.Now.AddMonths(3)).Take(5).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载统计数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
