using System.Windows;
using LTS.Core.Models;
using LTS.UI.Views;

namespace LTS.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var chamber = new Chamber("CH-01");

            var window = new MainWindow(chamber);

            window.Show();
        }
    }
}