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

    public string BusyStatus =>
        _transferSystem.IsBusy ? "Busy" : "Idle";

    public Brush BusyStatusBrush =>
        _transferSystem.IsBusy
            ? Brushes.Orange
            : Brushes.LimeGreen;

    // =========================
    // Button States
    // =========================

    public bool CanMove => !_transferSystem.IsBusy;

    public bool CanStop => _transferSystem.IsBusy;

    // =========================
    // Actions
    // =========================

    public void MoveHome()
    {
        _transferSystem.MoveTo("Home");
    }

    public void MoveLoadPort()
    {
        _transferSystem.MoveTo("Load Port");
    }

    public void MoveChamber()
    {
        _transferSystem.MoveTo("Chamber");
    }

    public void Stop()
    {
        _transferSystem.Stop();
    }

    // =========================
    // Property Changed
    // =========================

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentLocation));
        OnPropertyChanged(nameof(BusyStatus));
        OnPropertyChanged(nameof(BusyStatusBrush));

        OnPropertyChanged(nameof(CanMove));
        OnPropertyChanged(nameof(CanStop));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}