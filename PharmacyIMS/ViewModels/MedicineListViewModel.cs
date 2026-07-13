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
    public class MedicineListViewModel : ViewModelBase
    {
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _medicineCount;
        public int MedicineCount
        {
            get => _medicineCount;
            set => SetProperty(ref _medicineCount, value);
        }

        private Medicine? _selectedMedicine;
        public Medicine? SelectedMedicine
        {
            get => _selectedMedicine;
            set => SetProperty(ref _selectedMedicine, value);
        }

        private MedicineCategory? _selectedCategory;
        public MedicineCategory? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (SetProperty(ref _selectedCategory, value))
                {
                    DoSearch(null);
                }
            }
        }

        private StatusOptions? _selectedType;
        public StatusOptions? SelectedType
        {
            get => _selectedType;
            set
            {
                if (SetProperty(ref _selectedType, value))
                {
                    DoSearch(null);
                }
            }
        }

        private List<Medicine> _allMedicines = new();
        public ObservableCollection<Medicine> Medicines { get; set; } = new();

        public List<MedicineCategory> Categories { get; private set; } = new();

        public List<StatusOptions> TypeList { get; } = new()
        {
            new StatusOptions { Text = "全部类型", Status = null },
            new StatusOptions { Text = "处方药", Status = PrescriptionType.Prescription },
            new StatusOptions { Text = "非处方药", Status = PrescriptionType.OTC },
            new StatusOptions { Text = "中药饮片", Status = PrescriptionType.TraditionalChinese },
            new StatusOptions { Text = "保健品", Status = PrescriptionType.HealthProduct },
        };

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public MedicineListViewModel()
        {
            SearchCommand = new RelayCommand(DoSearch);
            AddCommand = new RelayCommand(DoAdd);
            EditCommand = new RelayCommand(DoEdit, p => SelectedMedicine != null);
            DeleteCommand = new RelayCommand(DoDelete, p => SelectedMedicine != null);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                var categories = new List<MedicineCategory>() { new MedicineCategory { Id = 0, CategoryName = "全部分类" } };
                categories.AddRange(db.MedicineCategories.OrderBy(c => c.Id).ToList());
                Categories = categories;

                _selectedCategory = Categories[0];
                _selectedType = TypeList[0];

                _allMedicines = db.Medicines
                    .Include(m => m.Category)
                    .OrderBy(m => m.MedicineCode)
                    .ToList();

                RefreshList(_allMedicines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载药品数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<Medicine> query = _allMedicines;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(m => m.MedicineName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    m.MedicineCode.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    (m.GenericName != null && m.GenericName.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
            }

            if (SelectedCategory != null && SelectedCategory.Id != 0)
            {
                query = query.Where(m => m.CategoryId == SelectedCategory.Id);
            }

            var typeFilter = SelectedType?.Status as PrescriptionType?;
            if (typeFilter != null)
            {
                query = query.Where(m => m.PrescriptionType == typeFilter.Value);
            }

            RefreshList(query.ToList());
        }

        private void DoAdd(object? parameter)
        {
            var win = new MedicineEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new MedicineEditViewModel(win);
            if (win.ShowDialog() == true)
            {
                ReloadMedicines();
            }
        }

        private void RefreshList(List<Medicine> list)
        {
            Medicines.Clear();
            foreach (var medicine in list)
            {
                Medicines.Add(medicine);
            }
            MedicineCount = Medicines.Count;
        }

        private void DoEdit(object? parameter)
        {
            if (SelectedMedicine == null) return;

            var win = new MedicineEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new MedicineEditViewModel(win, SelectedMedicine);
            if (win.ShowDialog() == true)
            {
                ReloadMedicines();
            }
        }

        private void DoDelete(object? parameter)
        {
            if (SelectedMedicine == null) return;

            var result = MessageBox.Show($"确定要删除药品\"{SelectedMedicine.MedicineName}\"吗？", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                var hasOrders = db.PurchaseOrderDetails.Any(d => d.MedicineId == SelectedMedicine.Id)
                    || db.SaleOrderDetails.Any(d => d.MedicineId == SelectedMedicine.Id)
                    || db.PurchaseReturnOrderDetails.Any(d => d.MedicineId == SelectedMedicine.Id)
                    || db.SaleReturnOrderDetails.Any(d => d.MedicineId == SelectedMedicine.Id);
                if (hasOrders)
                {
                    MessageBox.Show("该药品已有采购或销售记录，无法删除！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var entity = db.Medicines.Find(SelectedMedicine.Id);
                if (entity != null)
                {
                    db.Medicines.Remove(entity);
                    db.SaveChanges();
                }
                ReloadMedicines();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReloadMedicines()
        {
            try
            {
                using var db = new AppDbContext();
                _allMedicines = db.Medicines.Include(m => m.Category).OrderBy(m => m.MedicineCode).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新药品数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DoSearch(null);
        }
    }
}
