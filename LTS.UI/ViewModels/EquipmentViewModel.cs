using LTS.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LTS.UI.ViewModels;

public class EquipmentViewModel : INotifyPropertyChanged
{
    private EquipmentItemViewModel? _selectedItem;

    public Equipment Equipment { get; }

    public ObservableCollection<ChamberViewModel> Chambers { get; }

    public ObservableCollection<LoadPortViewModel> LoadPorts { get; }

    public TransferSystemViewModel TransferSystem { get; }

  
    public ObservableCollection<EquipmentItemViewModel> EquipmentItems { get; }


    public EquipmentItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != value)
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
    }

    public EquipmentViewModel(Equipment equipment)
    {
        Equipment = equipment;

        Chambers = new ObservableCollection<ChamberViewModel>(
            equipment.Chambers.Select(c => new ChamberViewModel(c)));

        LoadPorts = new ObservableCollection<LoadPortViewModel>(
            equipment.LoadPorts.Select(lp => new LoadPortViewModel(lp)));

        TransferSystem = new TransferSystemViewModel(
             equipment.TransferSystem, Chambers, LoadPorts);

        EquipmentItems = new ObservableCollection<EquipmentItemViewModel>();

        foreach (var chamber in Chambers)
            EquipmentItems.Add(chamber);

        foreach (var loadPort in LoadPorts)
            EquipmentItems.Add(loadPort);

        EquipmentItems.Add(TransferSystem);

        SelectedItem = EquipmentItems.FirstOrDefault();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}