using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class TransferSystemViewModel : EquipmentItemViewModel, INotifyPropertyChanged
{
    private readonly TransferSystem _transferSystem;

    public TransferSystemViewModel(
        TransferSystem transferSystem,
        ObservableCollection<ChamberViewModel> chambers,
        ObservableCollection<LoadPortViewModel> loadPorts)
        : base(transferSystem.Identifier)
    {
        _transferSystem = transferSystem;
        Chambers = chambers;
        LoadPorts = loadPorts;

        _transferSystem.StateChanged += OnStateChanged;
    }

    
    // Destination Lists
    

    public ObservableCollection<ChamberViewModel> Chambers { get; }

    public ObservableCollection<LoadPortViewModel> LoadPorts { get; }

  
    // Status
 

    public string CurrentLocation => _transferSystem.CurrentLocation;

    public string HoldingStatus =>
        _transferSystem.HasMaterial ? "Holding Wafer" : "Empty";

    public Brush HoldingStatusBrush =>
        _transferSystem.HasMaterial
            ? Brushes.LimeGreen
            : Brushes.Red;

  
    // Button States
    

    public bool CanMove => true;

    public bool CanPick =>
        !_transferSystem.HasMaterial &&
        _transferSystem.CurrentLocation != "Home";

    public bool CanPlace =>
        _transferSystem.HasMaterial &&
        _transferSystem.CurrentLocation != "Home";
    public bool CanMoveHome =>
        _transferSystem.CurrentLocation != "Home";

  
    // Actions
  

    public void MoveTo(string location)
    {
        _transferSystem.MoveTo(location);
    }

    public void Pick(string sourceLocation)
    {
        _transferSystem.Pick(sourceLocation);
    }

    public void Place(string destinationLocation)
    {
        _transferSystem.Place(destinationLocation);
    }

    public void MoveHome()
    {
        _transferSystem.MoveHome();
    }

  
    // Property Changed

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentLocation));
        OnPropertyChanged(nameof(HoldingStatus));
        OnPropertyChanged(nameof(HoldingStatusBrush));

        OnPropertyChanged(nameof(CanMove));
        OnPropertyChanged(nameof(CanPick));
        OnPropertyChanged(nameof(CanPlace));
        OnPropertyChanged(nameof(CanMoveHome));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}