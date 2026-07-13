using PharmacyIMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyIMS.Views
{
    public partial class SaleEditWindow : Window
    {
        public SaleEditWindow()
        {
            InitializeComponent();
            SaleDetailsGrid.CellEditEnding += SaleDetailsGrid_CellEditEnding;
        }

        private void SaleDetailsGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is SaleEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }

        private void MedicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is SaleEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }
    }
}
