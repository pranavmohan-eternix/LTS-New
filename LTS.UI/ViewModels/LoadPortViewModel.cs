using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class LoadPortViewModel : EquipmentItemViewModel, INotifyPropertyChanged
{
    private readonly LoadPort _loadPort;

    public LoadPortViewModel(LoadPort loadPort)
        : base(loadPort.Identifier)
    {
        _loadPort = loadPort;
        _loadPort.StateChanged += OnStateChanged;
    }

    // =========================
    // Status
    // =========================

    public string CarrierStatus =>
        _loadPort.HasCarrier ? "Present" : "Empty";

    public Brush CarrierStatusBrush =>
        _loadPort.HasCarrier ? Brushes.LimeGreen : Brushes.Red;

    public string DockStatus =>
        _loadPort.IsDocked ? "Docked" : "Undocked";

    public Brush DockStatusBrush =>
        _loadPort.IsDocked ? Brushes.LimeGreen : Brushes.Red;

    public string ClampStatus =>
        _loadPort.IsClamped ? "Clamped" : "Unclamped";

    public Brush ClampStatusBrush =>
        _loadPort.IsClamped ? Brushes.LimeGreen : Brushes.Red;

    public string DoorStatus =>
        _loadPort.IsDoorOpen ? "Open" : "Closed";

    public Brush DoorStatusBrush =>
        _loadPort.IsDoorOpen ? Brushes.LimeGreen : Brushes.Red;

    public string MappingStatus =>
        _loadPort.IsMapped ? "Mapped" : "Not Mapped";

    public Brush MappingStatusBrush =>
        _loadPort.IsMapped ? Brushes.LimeGreen : Brushes.DarkOrange;

    // =========================
    // Button States
    // =========================

    public bool CanDock =>
        _loadPort.HasCarrier &&
        !_loadPort.IsDocked;

    public bool CanClamp =>
        _loadPort.IsDocked &&
        !_loadPort.IsClamped;

    public bool CanOpenDoor =>
        _loadPort.IsClamped &&
        !_loadPort.IsDoorOpen;

    public bool CanMap =>
        _loadPort.IsDoorOpen &&
        !_loadPort.IsMapped;

    public bool CanCloseDoor =>
        _loadPort.IsDoorOpen;

    public bool CanUnclamp =>
        !_loadPort.IsDoorOpen &&
        _loadPort.IsClamped;

    public bool CanUndock =>
        !_loadPort.IsDoorOpen &&
        !_loadPort.IsClamped &&
        _loadPort.IsDocked;

    // =========================
    // Actions
    // =========================

    public void Dock() => _loadPort.Dock();

    public void Clamp() => _loadPort.Clamp();

    public void OpenDoor() => _loadPort.OpenDoor();

    public void MapCarrier() => _loadPort.MapCarrier();

    public void CloseDoor() => _loadPort.CloseDoor();

    public void Unclamp() => _loadPort.Unclamp();

    public void Undock() => _loadPort.Undock();

    // =========================
    // Property Changed
    // =========================

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CarrierStatus));
        OnPropertyChanged(nameof(CarrierStatusBrush));

        OnPropertyChanged(nameof(DockStatus));
        OnPropertyChanged(nameof(DockStatusBrush));

        OnPropertyChanged(nameof(ClampStatus));
        OnPropertyChanged(nameof(ClampStatusBrush));

        OnPropertyChanged(nameof(DoorStatus));
        OnPropertyChanged(nameof(DoorStatusBrush));

        OnPropertyChanged(nameof(MappingStatus));
        OnPropertyChanged(nameof(MappingStatusBrush));

        OnPropertyChanged(nameof(CanDock));
        OnPropertyChanged(nameof(CanClamp));
        OnPropertyChanged(nameof(CanOpenDoor));
        OnPropertyChanged(nameof(CanMap));
        OnPropertyChanged(nameof(CanCloseDoor));
        OnPropertyChanged(nameof(CanUnclamp));
        OnPropertyChanged(nameof(CanUndock));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}