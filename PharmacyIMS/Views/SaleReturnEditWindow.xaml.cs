using PharmacyIMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyIMS.Views
{
    public partial class SaleReturnEditWindow : Window
    {
        public SaleReturnEditWindow()
        {
            InitializeComponent();
            SaleReturnDetailsGrid.CellEditEnding += SaleReturnDetailsGrid_CellEditEnding;
        }

        private void SaleReturnDetailsGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is SaleReturnEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }

        private void MedicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is SaleReturnEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }
    }
}
