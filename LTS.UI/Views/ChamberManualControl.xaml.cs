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

        private void BtnOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.OpenDoor();
        }

        private void BtnCloseDoor_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.CloseDoor();
        }

        private void BtnRunRecipe_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select Recipe File",
                Filter = "Recipe Files (*.txt;*.recipe)|*.txt;*.recipe|All Files (*.*)|*.*",
                CheckFileExists = true
            };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                ViewModel?.RunRecipe(dialog.FileName);
            }
        }

        private void BtnCancelRecipe_Click(object sender, RoutedEventArgs e)
        {
            ViewModel?.CancelRecipe();
        }
    }
}