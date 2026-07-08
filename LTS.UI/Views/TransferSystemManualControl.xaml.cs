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

        private void BtnPick_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Pick();
        }

        private void BtnMoveToChamber_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveToChamber();
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Place();
        }

        private void BtnMoveHome_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveHome();
        }
    }
}