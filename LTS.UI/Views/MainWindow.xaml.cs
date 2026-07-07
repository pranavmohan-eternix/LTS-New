using LTS.Core.Models;
using LTS.UI.ViewModels;
using System.Windows;

namespace LTS.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly JobScreen _jobScreen;
        private readonly ManualScreen _manualScreen;

        public MainWindow(EquipmentViewModel equipmentVM)
        {
            InitializeComponent();

            _jobScreen = new JobScreen(equipmentVM);

            _manualScreen = new ManualScreen(equipmentVM);

            MainContent.Content = _jobScreen;
        }

        private void BtnJobs_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _jobScreen;
        }

        private void BtnManual_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = _manualScreen;
        }
    }
}