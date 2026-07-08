using System;

namespace LTS.Core.Models;

public class TransferSystem
{
    public string Identifier { get; }

    public bool IsBusy { get; private set; }

    public bool HasMaterial { get; private set; }

    public string CurrentLocation { get; private set; }

    public event EventHandler? StateChanged;

    public TransferSystem(string identifier)
    {
        Identifier = identifier;

        CurrentLocation = "Home";
        IsBusy = false;
        HasMaterial = false;
    }

    public void MoveToLoadPort()
    {
        CurrentLocation = "Load Port";
        OnStateChanged();
    }

    public void Pick()
    {
        HasMaterial = true;
        OnStateChanged();
    }

    public void MoveToChamber()
    {
        CurrentLocation = "Chamber";
        OnStateChanged();
    }

    public void Place()
    {
        HasMaterial = false;
        OnStateChanged();
    }

    public void MoveHome()
    {
        CurrentLocation = "Home";
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}