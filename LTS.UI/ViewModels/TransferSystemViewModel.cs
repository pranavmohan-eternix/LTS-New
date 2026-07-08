using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class TransferSystemViewModel : EquipmentItemViewModel, INotifyPropertyChanged
{
    private readonly TransferSystem _transferSystem;

    public TransferSystemViewModel(TransferSystem transferSystem)
        : base(transferSystem.Identifier)
    {
        _transferSystem = transferSystem;
        _transferSystem.StateChanged += OnStateChanged;
    }

    // =========================
    // Status
    // =========================

    public string CurrentLocation => _transferSystem.CurrentLocation;

    public string HoldingStatus =>
        _transferSystem.HasMaterial ? "Holding Wafer" : "Empty";

    public Brush HoldingStatusBrush =>
        _transferSystem.HasMaterial
            ? Brushes.LimeGreen
            : Brushes.Red;

    // =========================
    // Button States
    // =========================

    public bool CanMoveToLoadPort =>
        _transferSystem.CurrentLocation != "Load Port";

    public bool CanMoveToChamber =>
        _transferSystem.CurrentLocation != "Chamber";

    public bool CanPickFromCarrier =>
        _transferSystem.CurrentLocation == "Load Port" &&
        !_transferSystem.HasMaterial;

    public bool CanPlaceToCarrier =>
        _transferSystem.CurrentLocation == "Load Port" &&
        _transferSystem.HasMaterial;

    public bool CanPickFromChamber =>
        _transferSystem.CurrentLocation == "Chamber" &&
        !_transferSystem.HasMaterial;

    public bool CanPlaceToChamber =>
        _transferSystem.CurrentLocation == "Chamber" &&
        _transferSystem.HasMaterial;

    public bool CanMoveHome =>
        _transferSystem.CurrentLocation != "Home";

    // =========================
    // Actions
    // =========================

    public void MoveToLoadPort()
    {
        _transferSystem.MoveToLoadPort();
    }

    public void MoveToChamber()
    {
        _transferSystem.MoveToChamber();
    }

    public void MoveHome()
    {
        _transferSystem.MoveHome();
    }

    public void PickFromCarrier()
    {
        _transferSystem.PickFromCarrier();
    }

    public void PlaceToCarrier()
    {
        _transferSystem.PlaceToCarrier();
    }

    public void PickFromChamber()
    {
        _transferSystem.PickFromChamber();
    }

    public void PlaceToChamber()
    {
        _transferSystem.PlaceToChamber();
    }

    // =========================
    // Property Changed
    // =========================

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentLocation));
        OnPropertyChanged(nameof(HoldingStatus));
        OnPropertyChanged(nameof(HoldingStatusBrush));

        OnPropertyChanged(nameof(CanMoveToLoadPort));
        OnPropertyChanged(nameof(CanMoveToChamber));
        OnPropertyChanged(nameof(CanMoveHome));

        OnPropertyChanged(nameof(CanPickFromCarrier));
        OnPropertyChanged(nameof(CanPlaceToCarrier));

        OnPropertyChanged(nameof(CanPickFromChamber));
        OnPropertyChanged(nameof(CanPlaceToChamber));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}