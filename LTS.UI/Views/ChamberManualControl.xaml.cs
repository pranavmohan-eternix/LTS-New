using System.Windows;
using System.Windows.Controls;
using LTS.UI.ViewModels;

namespace LTS.UI.Views
{
    public partial class ChamberManualControl : UserControl
    {
        public ChamberManualControl()
        {
            InitializeComponent();
        }

        private ChamberViewModel? ViewModel =>
            DataContext as ChamberViewModel;

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.Initialize();
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PlaceMaterial();
        }

        private void BtnPick_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.PickMaterial();
        }

        private void BtnRunRecipe_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.RunRecipe();
        }
    }
}