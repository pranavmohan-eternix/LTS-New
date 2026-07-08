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
        _transferSystem.CurrentLocation == "Home" &&
        !_transferSystem.HasMaterial;

    public bool CanPick =>
        _transferSystem.CurrentLocation == "Load Port" &&
        !_transferSystem.HasMaterial;

    public bool CanMoveToChamber =>
        _transferSystem.CurrentLocation == "Load Port" &&
        _transferSystem.HasMaterial;

    public bool CanPlace =>
        _transferSystem.CurrentLocation == "Chamber" &&
        _transferSystem.HasMaterial;

    public bool CanMoveHome =>
        _transferSystem.CurrentLocation == "Chamber" &&
        !_transferSystem.HasMaterial;

    // =========================
    // Actions
    // =========================

    public void MoveToLoadPort()
    {
        _transferSystem.MoveToLoadPort();
    }

    public void Pick()
    {
        _transferSystem.Pick();
    }

    public void MoveToChamber()
    {
        _transferSystem.MoveToChamber();
    }

    public void Place()
    {
        _transferSystem.Place();
    }

    public void MoveHome()
    {
        _transferSystem.MoveHome();
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
        OnPropertyChanged(nameof(CanPick));
        OnPropertyChanged(nameof(CanMoveToChamber));
        OnPropertyChanged(nameof(CanPlace));
        OnPropertyChanged(nameof(CanMoveHome));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}