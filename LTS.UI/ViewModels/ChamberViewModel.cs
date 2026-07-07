using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LTS.Core.Models;

namespace LTS.UI.ViewModels;

public class ChamberViewModel : INotifyPropertyChanged
{
    private readonly Chamber _chamber;
    public string Identifier => _chamber.Identifier;

    public ChamberStatusControlVM ChamberVM { get; }

    public ChamberViewModel(Chamber chamber)
    {
        _chamber = chamber;

        ChamberVM = new ChamberStatusControlVM(chamber);

        _chamber.StateChanged += OnStateChanged;
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnStateChanged(object? sender, EventArgs e)
    {
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