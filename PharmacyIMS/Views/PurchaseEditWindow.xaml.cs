using PharmacyIMS.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyIMS.Views
{
    public partial class PurchaseEditWindow : Window
    {
        public PurchaseEditWindow()
        {
            InitializeComponent();
            DetailsGrid.CellEditEnding += DetailsGrid_CellEditEnding;
        }

        private void DetailsGrid_CellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (DataContext is PurchaseEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }

        private void MedicineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is PurchaseEditViewModel vm && !vm.IsReadOnly)
            {
                Dispatcher.BeginInvoke(() => vm.RecalcDetails());
            }
        }
    }
}
