using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class ChamberStatusControlVM : INotifyPropertyChanged
{
    private readonly Chamber _chamber;

    public ChamberStatusControlVM(Chamber chamber)
    {
        _chamber = chamber;
        _chamber.StateChanged += OnStateChanged;
    }

    public string Identifier => _chamber.Identifier;

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(ProcessState));
        OnPropertyChanged(nameof(ProcessDuration));
        OnPropertyChanged(nameof(MaterialPresence));
        OnPropertyChanged(nameof(SelectedRecipe));
        OnPropertyChanged(nameof(ProcessStateBrush));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}