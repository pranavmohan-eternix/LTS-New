using LTS.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LTS.UI.ViewModels;

public class EquipmentViewModel : INotifyPropertyChanged
{
    private ChamberViewModel? _selectedChamber;

    public Equipment Equipment { get; }

    public ObservableCollection<ChamberViewModel> Chambers { get; }

    public ChamberViewModel? SelectedChamber
    {
        get => _selectedChamber;
        set
        {
            if (_selectedChamber != value)
            {
                _selectedChamber = value;
                OnPropertyChanged();
            }
        }
    }

    public EquipmentViewModel(Equipment equipment)
    {
        Equipment = equipment;

        Chambers = new ObservableCollection<ChamberViewModel>(
            equipment.Chambers.Select(c => new ChamberViewModel(c)));

        SelectedChamber = Chambers.FirstOrDefault();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}