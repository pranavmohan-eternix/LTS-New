using System;

namespace LTS.Core.Models;

public class LoadPort
{
    public string Identifier { get; }

    public bool HasCarrier { get; private set; }

    public bool IsDocked { get; private set; }

    public bool IsClamped { get; private set; }

    public bool IsDoorOpen { get; private set; }

    public bool IsMapped { get; private set; }

    public event EventHandler? StateChanged;

    public LoadPort(string identifier)
    {
        Identifier = identifier;

        // Initial State
        HasCarrier = true;
        IsDocked = false;
        IsClamped = false;
        IsDoorOpen = false;
        IsMapped = false;
    }

    public void Dock()
    {
        if (!HasCarrier || IsDocked)
            return;

        IsDocked = true;
        OnStateChanged();
    }

    public void Clamp()
    {
        if (!IsDocked || IsClamped)
            return;

        IsClamped = true;
        OnStateChanged();
    }

    public void OpenDoor()
    {
        if (!IsClamped || IsDoorOpen)
            return;

        IsDoorOpen = true;
        OnStateChanged();
    }

    public void MapCarrier()
    {
        if (!IsDoorOpen || IsMapped)
            return;

        IsMapped = true;
        OnStateChanged();
    }

    public void CloseDoor()
    {
        if (!IsDoorOpen)
            return;

        IsDoorOpen = false;
        OnStateChanged();
    }

    public void Unclamp()
    {
        if (IsDoorOpen || !IsClamped)
            return;

        IsClamped = false;
        OnStateChanged();
    }

    public void Undock()
    {
        if (IsClamped || IsDoorOpen || !IsDocked)
            return;

        IsDocked = false;
        IsMapped = false;
        OnStateChanged();
    }

    private void OnStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}