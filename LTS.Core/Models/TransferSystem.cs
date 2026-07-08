using System;

namespace LTS.Core.Models;

public class TransferSystem
{
    public string Identifier { get; }

    public bool IsBusy { get; private set; }

    public string CurrentLocation { get; private set; } = "Home";

    public event EventHandler? StateChanged;

    public TransferSystem(string identifier)
    {
        Identifier = identifier;
    }

    public void MoveTo(string location)
    {
        CurrentLocation = location;
        IsBusy = true;
        OnStateChanged();
    }

    public void Stop()
    {
        IsBusy = false;
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}