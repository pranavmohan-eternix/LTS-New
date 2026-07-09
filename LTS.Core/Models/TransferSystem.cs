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

   
    public void MoveTo(string location)
    {
        CurrentLocation = location;
        OnStateChanged();
    }

    public void MoveHome()
    {
        CurrentLocation = "Home";
        OnStateChanged();
    }

    
    public void Pick(string sourceLocation)
    {
        HasMaterial = true;
        OnStateChanged();
    }

    
    public void Place(string destinationLocation)
    {
        HasMaterial = false;
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}