using LTS.Core.Models;
using LTS.UI.ViewModels;
using LTS.UI.Views;
using System.Windows;

namespace LTS.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var equipment = new Equipment();

            var equipmentVM = new EquipmentViewModel(equipment);

            var window = new MainWindow(equipmentVM);

            window.Show();
        }
    }
}