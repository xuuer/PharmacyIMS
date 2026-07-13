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
    public class InventoryViewModel : ViewModelBase
    {
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _totalMedicineCount;
        public int TotalMedicineCount
        {
            get => _totalMedicineCount;
            set => SetProperty(ref _totalMedicineCount, value);
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

        private bool _showLowStockOnly;
        public bool ShowLowStockOnly
        {
            get => _showLowStockOnly;
            set
            {
                if (SetProperty(ref _showLowStockOnly, value))
                {
                    DoSearch(null);
                }
            }
        }

        private bool _showNearExpiryOnly;
        public bool ShowNearExpiryOnly
        {
            get => _showNearExpiryOnly;
            set
            {
                if (SetProperty(ref _showNearExpiryOnly, value))
                {
                    DoSearch(null);
                }
            }
        }

        private List<Medicine> _allMedicines = new();
        public ObservableCollection<Medicine> Medicines { get; set; } = new();

        private Medicine? _selectedMedicine;
        public Medicine? SelectedMedicine
        {
            get => _selectedMedicine;
            set
            {
                if (SetProperty(ref _selectedMedicine, value))
                {
                    LoadBatches();
                }
            }
        }

        public ObservableCollection<MedicineBatch> Batches { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand RefreshCommand { get; }

        public InventoryViewModel()
        {
            SearchCommand = new RelayCommand(DoSearch);
            RefreshCommand = new RelayCommand(DoRefresh);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                _allMedicines = db.Medicines
                    .Include(m => m.Category)
                    .OrderBy(m => m.MedicineCode)
                    .ToList();

                TotalMedicineCount = _allMedicines.Count;
                LowStockCount = _allMedicines.Count(m => m.StockQuantity <= m.StockAlertLevel);
                NearExpiryCount = _allMedicines.Count(m => m.ExpiryDate.HasValue && m.ExpiryDate.Value <= DateTime.Now.AddMonths(3));

                RefreshList(_allMedicines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载库存数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<Medicine> query = _allMedicines;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(m => m.MedicineName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    m.MedicineCode.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            if (ShowLowStockOnly)
            {
                query = query.Where(m => m.StockQuantity <= m.StockAlertLevel);
            }

            if (ShowNearExpiryOnly)
            {
                query = query.Where(m => m.ExpiryDate.HasValue && m.ExpiryDate.Value <= DateTime.Now.AddMonths(3));
            }

            RefreshList(query.ToList());
        }

        private void DoRefresh(object? parameter)
        {
            ShowLowStockOnly = false;
            ShowNearExpiryOnly = false;
            SearchText = "";
            LoadData();
        }

        private void RefreshList(List<Medicine> list)
        {
            Medicines.Clear();
            foreach (var medicine in list)
            {
                Medicines.Add(medicine);
            }
        }

        private void LoadBatches()
        {
            Batches.Clear();
            if (SelectedMedicine == null) return;

            try
            {
                using var db = new AppDbContext();
                var batches = db.MedicineBatches
                    .Where(b => b.MedicineId == SelectedMedicine.Id)
                    .OrderBy(b => b.ExpiryDate)
                    .ToList();
                foreach (var batch in batches)
                {
                    Batches.Add(batch);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载批次数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
