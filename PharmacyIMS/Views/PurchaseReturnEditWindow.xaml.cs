using PharmacyIMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyIMS.Views
{
    public partial class PurchaseReturnEditWindow : Window
    {
        public PurchaseReturnEditWindow()
        {
            InitializeComponent();
            ReturnDetailsGrid.CellEditEnding += ReturnDetailsGrid_CellEditEnding;
        }

        private void ReturnDetailsGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is PurchaseReturnEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }

        private void MedicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PurchaseReturnEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }
    }
}
