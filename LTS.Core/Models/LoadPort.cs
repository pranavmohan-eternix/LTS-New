using System;

namespace LTS.Core.Models;

public class LoadPort
{
    public string Identifier { get; }

    public bool HasCarrier { get; private set; }

    public bool IsMapped { get; private set; }

    public event EventHandler? StateChanged;

    public LoadPort(string identifier)
    {
        Identifier = identifier;
    }

    public void LoadCarrier()
    {
        HasCarrier = true;
        OnStateChanged();
    }

    public void UnloadCarrier()
    {
        HasCarrier = false;
        IsMapped = false;
        OnStateChanged();
    }

    public void MapCarrier()
    {
        if (HasCarrier)
        {
            IsMapped = true;
            OnStateChanged();
        }
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}