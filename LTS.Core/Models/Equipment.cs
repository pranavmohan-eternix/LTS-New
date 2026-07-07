using System.Collections.ObjectModel;

namespace LTS.Core.Models;

public class Equipment
{
    public Equipment()
    {
        Chambers.Add(new Chamber("CH-01"));
        Chambers.Add(new Chamber("CH-02"));
        Chambers.Add(new Chamber("CH-03"));
    }

    public ObservableCollection<Chamber> Chambers { get; } = new();
}