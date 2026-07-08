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

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.LoadCarrier();
        }

        private void BtnUnload_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.UnloadCarrier();
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.MapCarrier();
        }
    }
}