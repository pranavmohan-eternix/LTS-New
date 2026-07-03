using LTS.Core.Models;
using System.Windows;

namespace LTS.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly JobScreen _jobScreen;
        private readonly ManualScreen _manualScreen;

        public MainWindow(Chamber chamber)
        {
            InitializeComponent();

            _jobScreen = new JobScreen(chamber);

            _manualScreen = new ManualScreen(chamber);

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