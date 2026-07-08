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

        private void BtnMoveToLoadPort_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveToLoadPort();
        }

        private void BtnMoveToChamber_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveToChamber();
        }

        private void BtnPickFromCarrier_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PickFromCarrier();
        }

        private void BtnPlaceToChamber_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PlaceToChamber();
        }

        private void BtnPickFromChamber_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PickFromChamber();
        }

        private void BtnPlaceToCarrier_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PlaceToCarrier();
        }

        private void BtnMoveHome_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveHome();
        }
    }
}