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

    public string ProcessDuration =>
     _chamber.ProcessDuration <= 0 ? "Not Set" : $"{_chamber.ProcessDuration} Seconds";

    public string MaterialPresence =>
        _chamber.MaterialPresence ? "Present" : "Empty";

    public string DoorStatus =>
        _chamber.IsDoorOpen ? "Open" : "Closed";

    public Brush DoorStatusBrush =>
        _chamber.IsDoorOpen ? Brushes.LimeGreen : Brushes.Red;

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

    public bool CanOpenDoor =>
        _chamber.IsInitialized &&
        !_chamber.IsDoorOpen &&
        _chamber.ProcessState != ProcessState.Running;

    public bool CanCloseDoor =>
        _chamber.IsInitialized &&
        _chamber.IsDoorOpen;

    public bool CanRunRecipe =>
        _chamber.MaterialPresence &&
        !_chamber.IsDoorOpen &&
        _chamber.ProcessState == ProcessState.Idle;

    public bool CanCancelRecipe =>
        _chamber.ProcessState == ProcessState.Running;

    // =========================
    // Actions
    // =========================

    public void Initialize()
    {
        _chamber.Initialize();
    }

    public void OpenDoor()
    {
        _chamber.OpenDoor();
    }

    public void CloseDoor()
    {
        _chamber.CloseDoor();
    }




    // NEW: called after the user picks a recipe file from disk.
    public async Task<bool> RunRecipe(string recipeFilePath)
    {
        return await Task.Run(() => _chamber.RunRecipeFromFile(recipeFilePath));
    }

    public void CancelRecipe()
    {
        _chamber.CancelRecipe();
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
        OnPropertyChanged(nameof(DoorStatus));
        OnPropertyChanged(nameof(DoorStatusBrush));
        OnPropertyChanged(nameof(SelectedRecipe));
        OnPropertyChanged(nameof(ProcessStateBrush));

        OnPropertyChanged(nameof(CanInitialize));
        OnPropertyChanged(nameof(CanOpenDoor));
        OnPropertyChanged(nameof(CanCloseDoor));
        OnPropertyChanged(nameof(CanRunRecipe));
        OnPropertyChanged(nameof(CanCancelRecipe));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}