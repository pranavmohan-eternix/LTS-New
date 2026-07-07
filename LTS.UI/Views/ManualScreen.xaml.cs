using LTS.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LTS.UI.Views
{
    public partial class ManualScreen : UserControl
    {
        private readonly EquipmentViewModel _equipmentVM;

        public ManualScreen(EquipmentViewModel equipmentVM)
        {
            InitializeComponent();

            _equipmentVM = equipmentVM;

            DataContext = _equipmentVM;
        }

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            _equipmentVM.SelectedChamber?.Initialize();
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            _equipmentVM.SelectedChamber?.PlaceMaterial();
        }

        private void BtnPick_Click(object sender, RoutedEventArgs e)
        {
            _equipmentVM.SelectedChamber?.PickMaterial();
        }

        private void BtnRunRecipe_Click(object sender, RoutedEventArgs e)
        {
            _equipmentVM.SelectedChamber?.RunRecipe();
        }
    }
}