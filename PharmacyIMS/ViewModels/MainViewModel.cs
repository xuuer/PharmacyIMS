using PharmacyIMS.Enums;
using PharmacyIMS.Helpers;
using PharmacyIMS.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace PharmacyIMS.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _currentUserName = "admin";
        private object? _currentView;
        private UserRole _currentRole;

        public string CurrentUserName
        {
            get => _currentUserName;
            set => SetProperty(ref _currentUserName, value);
        }

        public object? CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public UserRole CurrentRole
        {
            get => _currentRole;
            set => SetProperty(ref _currentRole, value);
        }

        // 权限控制：各菜单可见性
        public bool CanViewMedicine => true;
        public bool CanViewPurchase => CurrentRole == UserRole.Admin || CurrentRole == UserRole.Manager || CurrentRole == UserRole.Purchaser;
        public bool CanViewSale => CurrentRole == UserRole.Admin || CurrentRole == UserRole.Manager || CurrentRole == UserRole.Salesperson;
        public bool CanViewInventory => true;
        public bool CanViewSupplier => CurrentRole == UserRole.Admin || CurrentRole == UserRole.Manager || CurrentRole == UserRole.Purchaser;
        public bool CanViewDashboard => CurrentRole == UserRole.Admin || CurrentRole == UserRole.Manager;
        public bool CanViewUser => CurrentRole == UserRole.Admin;

        public ICommand NavCommand { get; }
        public ICommand ExitCommand { get; }

        public MainViewModel(User user)
        {
            CurrentUserName = user.RealName;
            CurrentRole = user.Role;
            CurrentView = $"欢迎 {user.RealName} 使用惠民药店药品进销存管理系统，请点击左侧菜单选择功能模块";
            NavCommand = new RelayCommand(DoNav);
            ExitCommand = new RelayCommand(DoExit);
        }

        private void DoNav(object? parameter)
        {
            string module = parameter?.ToString() ?? "";
            CurrentView = module switch
            {
                "Medicine" => new MedicineListViewModel(),
                "Purchase" => new PurchaseListViewModel(),
                "Sale" => new SaleListViewModel(),
                "Inventory" => new InventoryViewModel(),
                "Supplier" => new SupplierListViewModel(),
                "PurchaseReturn" => new PurchaseReturnListViewModel(),
                "SaleReturn" => new SaleReturnListViewModel(),
                "Dashboard" => new DashboardViewModel(),
                "User" => new UserListViewModel(),
                _ => "未知模块"
            };
        }

        private void DoExit(object? parameter)
        {
            var result = MessageBox.Show("确定要退出系统吗？", "退出确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Session.Logout();
                Application.Current.Shutdown();
            }
        }
    }
}
