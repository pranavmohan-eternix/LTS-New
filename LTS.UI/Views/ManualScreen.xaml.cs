using LTS.Core.Models;
using LTS.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LTS.UI.Views
{
    public partial class ManualScreen : UserControl
    {
        private readonly ManualScreenVM _viewModel;

        public ManualScreen(Chamber chamber)
        {
            InitializeComponent();

            _viewModel = new ManualScreenVM(chamber);

            // This DataContext is for the buttons
            DataContext = _viewModel;

            ChamberList.Items.Add(chamber.Identifier);
            ChamberList.SelectedIndex = 0;

            
        }

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Initialize();
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PlaceMaterial();
        }

        private void BtnPick_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PickMaterial();
        }

        private void BtnRunRecipe_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RunRecipe();
        }
    }
}