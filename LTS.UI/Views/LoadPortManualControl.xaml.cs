using System.Windows;
using System.Windows.Controls;
using LTS.UI.ViewModels;

namespace LTS.UI.Views
{
    public partial class LoadPortManualControl : UserControl
    {
        public LoadPortManualControl()
        {
            InitializeComponent();
        }

        private LoadPortViewModel? ViewModel =>
            DataContext as LoadPortViewModel;

        private void BtnDock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Dock();
        }

        private void BtnClamp_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Clamp();
        }

        private void BtnOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.OpenDoor();
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MapCarrier();
        }

        private void BtnCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.CloseDoor();
        }

        private void BtnUnclamp_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Unclamp();
        }

        private void BtnUndock_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Undock();
        }
    }
}