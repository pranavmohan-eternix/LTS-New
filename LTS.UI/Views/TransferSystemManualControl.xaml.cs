using System.Windows;
using System.Windows.Controls;
using LTS.UI.ViewModels;

namespace LTS.UI.Views
{
    public partial class TransferSystemManualControl : UserControl
    {
        public TransferSystemManualControl()
        {
            InitializeComponent();
        }

        private TransferSystemViewModel? ViewModel =>
            DataContext as TransferSystemViewModel;

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel == null)
                return;

            var dialog = new LocationSelectionDialog(ViewModel, "Move")
            {
                Owner = Window.GetWindow(this)
            };

            if (dialog.ShowDialog() == true && dialog.SelectedIdentifier != null)
                ViewModel.MoveTo(dialog.SelectedIdentifier);
        }

        private void BtnPick_Click(object sender, RoutedEventArgs e)
        {
            // Picks from wherever the arm currently is — no popup needed
            ViewModel?.Pick(ViewModel.CurrentLocation);
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            // Places at wherever the arm currently is — no popup needed
            ViewModel?.Place(ViewModel.CurrentLocation);
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveHome();
        }
    }
}