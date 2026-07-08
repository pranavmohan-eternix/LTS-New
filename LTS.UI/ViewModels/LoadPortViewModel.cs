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
        _loadPort.HasCarrier
            ? Brushes.LimeGreen
            : Brushes.Red;

    public string MappingStatus =>
        _loadPort.IsMapped ? "Mapped" : "Not Mapped";

    public Brush MappingStatusBrush =>
        _loadPort.IsMapped
            ? Brushes.LimeGreen
            : Brushes.DarkOrange;

    // =========================
    // Button States
    // =========================

    public bool CanLoad => !_loadPort.HasCarrier;

    public bool CanUnload => _loadPort.HasCarrier;

    public bool CanMap =>
        _loadPort.HasCarrier &&
        !_loadPort.IsMapped;

    // =========================
    // Actions
    // =========================

    public void LoadCarrier()
    {
        _loadPort.LoadCarrier();
    }

    public void UnloadCarrier()
    {
        _loadPort.UnloadCarrier();
    }

    public void MapCarrier()
    {
        _loadPort.MapCarrier();
    }

    // =========================
    // Property Changed
    // =========================

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CarrierStatus));
        OnPropertyChanged(nameof(CarrierStatusBrush));

        OnPropertyChanged(nameof(MappingStatus));
        OnPropertyChanged(nameof(MappingStatusBrush));

        OnPropertyChanged(nameof(CanLoad));
        OnPropertyChanged(nameof(CanUnload));
        OnPropertyChanged(nameof(CanMap));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}