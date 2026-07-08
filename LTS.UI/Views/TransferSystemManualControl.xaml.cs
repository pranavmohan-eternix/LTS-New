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

        private void BtnMoveHome_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveHome();
        }

        private void BtnMoveLoadPort_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveLoadPort();
        }

        private void BtnMoveChamber_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MoveChamber();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Stop();
        }
    }
}