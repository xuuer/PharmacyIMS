using PharmacyIMS.Data;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using PharmacyIMS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class SupplierListViewModel : ViewModelBase
    {
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private int _supplierCount;
        public int SupplierCount
        {
            get => _supplierCount;
            set => SetProperty(ref _supplierCount, value);
        }

        private Supplier? _selectedSupplier;
        public Supplier? SelectedSupplier
        {
            get => _selectedSupplier;
            set => SetProperty(ref _selectedSupplier, value);
        }

        private List<Supplier> _allSuppliers = new();
        public ObservableCollection<Supplier> Suppliers { get; set; } = new();

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public SupplierListViewModel()
        {
            SearchCommand = new RelayCommand(DoSearch);
            AddCommand = new RelayCommand(DoAdd);
            EditCommand = new RelayCommand(DoEdit, p => SelectedSupplier != null);
            DeleteCommand = new RelayCommand(DoDelete, p => SelectedSupplier != null);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using var db = new AppDbContext();
                _allSuppliers = db.Suppliers.OrderBy(s => s.Id).ToList();
                RefreshList(_allSuppliers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载供应商数据失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoSearch(object? parameter)
        {
            IEnumerable<Supplier> query = _allSuppliers;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                string keyword = SearchText.Trim();
                query = query.Where(s => s.SupplierName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                    (s.ContactPerson != null && s.ContactPerson.Contains(keyword, StringComparison.OrdinalIgnoreCase)));
            }

            RefreshList(query.ToList());
        }

        private void DoAdd(object? parameter)
        {
            var win = new SupplierEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new SupplierEditViewModel(win);
            if (win.ShowDialog() == true)
            {
                ReloadSuppliers();
            }
        }

        private void DoEdit(object? parameter)
        {
            if (SelectedSupplier == null) return;

            var win = new SupplierEditWindow();
            win.Owner = Application.Current.MainWindow;
            win.DataContext = new SupplierEditViewModel(win, SelectedSupplier);
            if (win.ShowDialog() == true)
            {
                ReloadSuppliers();
            }
        }

        private void DoDelete(object? parameter)
        {
            if (SelectedSupplier == null) return;

            var result = MessageBox.Show($"确定要删除供应商\"{SelectedSupplier.SupplierName}\"吗？", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            try
            {
                using var db = new AppDbContext();
                var hasOrders = db.PurchaseOrders.Any(o => o.SupplierId == SelectedSupplier.Id)
                    || db.PurchaseReturnOrders.Any(o => o.SupplierId == SelectedSupplier.Id);
                if (hasOrders)
                {
                    MessageBox.Show("该供应商已有采购或退货记录，无法删除！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var entity = db.Suppliers.Find(SelectedSupplier.Id);
                if (entity != null)
                {
                    db.Suppliers.Remove(entity);
                    db.SaveChanges();
                }
                ReloadSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshList(List<Supplier> list)
        {
            Suppliers.Clear();
            foreach (var supplier in list)
            {
                Suppliers.Add(supplier);
            }
            SupplierCount = Suppliers.Count;
        }

        private void ReloadSuppliers()
        {
            try
            {
                using var db = new AppDbContext();
                _allSuppliers = db.Suppliers.OrderBy(s => s.Id).ToList();
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
