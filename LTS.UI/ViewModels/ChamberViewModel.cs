using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class ChamberViewModel : EquipmentItemViewModel, INotifyPropertyChanged
{
    private readonly Chamber _chamber;

    public ChamberViewModel(Chamber chamber)
        : base(chamber.Identifier)
    {
        _chamber = chamber;
        _chamber.StateChanged += OnStateChanged;
    }

    // =========================
    // Status Properties
    // =========================

    public ProcessState ProcessState => _chamber.ProcessState;

    public string ProcessDuration => $"{_chamber.ProcessDuration} Seconds";

    public string MaterialPresence =>
        _chamber.MaterialPresence ? "Present" : "Empty";

    public string SelectedRecipe => _chamber.SelectedRecipe;

    public Brush ProcessStateBrush
    {
        get
        {
            return _chamber.ProcessState switch
            {
                ProcessState.Init => Brushes.Red,
                ProcessState.Idle => Brushes.Gold,
                ProcessState.Running => Brushes.Orange,
                ProcessState.Completed => Brushes.LimeGreen,
                _ => Brushes.Gray
            };
        }
    }

    // =========================
    // Command State
    // =========================

    public bool CanInitialize => !_chamber.IsInitialized;

    public bool CanPlaceMaterial =>
        _chamber.IsInitialized &&
        !_chamber.MaterialPresence &&
        _chamber.ProcessState == ProcessState.Idle;

    public bool CanPickMaterial =>
        _chamber.MaterialPresence &&
        _chamber.ProcessState != ProcessState.Running;

    public bool CanRunRecipe =>
        _chamber.MaterialPresence &&
        _chamber.ProcessState == ProcessState.Idle;

    // =========================
    // Actions
    // =========================

    public void Initialize()
    {
        _chamber.Initialize();
    }

    public void PlaceMaterial()
    {
        _chamber.PrepareForTransfer(TransferAction.Place);
    }

    public void PickMaterial()
    {
        _chamber.PrepareForTransfer(TransferAction.Pick);
    }

    public void RunRecipe()
    {
        Task.Run(() => _chamber.RunRecipe());
    }

    // =========================
    // Property Changed
    // =========================

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ProcessState));
        OnPropertyChanged(nameof(ProcessDuration));
        OnPropertyChanged(nameof(MaterialPresence));
        OnPropertyChanged(nameof(SelectedRecipe));
        OnPropertyChanged(nameof(ProcessStateBrush));

        OnPropertyChanged(nameof(CanInitialize));
        OnPropertyChanged(nameof(CanPlaceMaterial));
        OnPropertyChanged(nameof(CanPickMaterial));
        OnPropertyChanged(nameof(CanRunRecipe));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}