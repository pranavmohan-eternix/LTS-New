using System.Windows;
using System.Windows.Controls;
using LTS.UI.ViewModels;

namespace LTS.UI.Views
{
    public partial class LocationSelectionDialog : Window
    {
        public string? SelectedIdentifier { get; private set; }

        private readonly string _currentLocation;

        public LocationSelectionDialog(TransferSystemViewModel viewModel, string actionName)
        {
            InitializeComponent();

            DataContext = viewModel;
            Title = $"{actionName} - Select Location";
            _currentLocation = viewModel.CurrentLocation;
        }

        private void RadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton &&
                radioButton.Tag is string identifier)
            {
                radioButton.IsEnabled = identifier != _currentLocation;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
                SelectedIdentifier = radioButton.Tag as string;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIdentifier == null)
            {
                MessageBox.Show(
                    "Please select a chamber or load port.",
                    "No Location Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}