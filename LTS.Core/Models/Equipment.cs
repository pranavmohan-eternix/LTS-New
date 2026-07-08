using LTS.Core.Models;
using System.Collections.ObjectModel;

public class Equipment
{
    public Equipment()
    {
        Chambers.Add(new Chamber("CH-01"));
        Chambers.Add(new Chamber("CH-02"));
        Chambers.Add(new Chamber("CH-03"));

        LoadPorts.Add(new LoadPort("LP-01"));
        LoadPorts.Add(new LoadPort("LP-02"));

        TransferSystem = new TransferSystem("TS-01");
    }

    public ObservableCollection<Chamber> Chambers { get; } = new();

    public ObservableCollection<LoadPort> LoadPorts { get; } = new();

    public TransferSystem TransferSystem { get; }
}