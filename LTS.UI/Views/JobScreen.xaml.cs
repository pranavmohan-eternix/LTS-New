using System.Windows.Controls;
using LTS.Core.Models;
using LTS.UI.ViewModels;

namespace LTS.UI.Views
{
    public partial class JobScreen : UserControl
    {
        public JobScreen(Chamber chamber)
        {
            InitializeComponent();

            ChamberControl.DataContext =
                new ChamberStatusControlVM(chamber);
        }
    }
}