using System;
using System.Threading;

namespace LTS.Core.Models;

public class Chamber
{
    public Chamber(string identifier)
    {
        Identifier = identifier;
        IsInitialized = false;
        ProcessState = ProcessState.Init;
        MaterialPresence = false;
        ProcessDuration = 5;
        SelectedRecipe = "RECIPE-01";
    }

    // Properties
    public string Identifier { get; }

    // Internal property (not displayed in UI)
    public bool IsInitialized { get; private set; }

    // Displayed in UI
    public ProcessState ProcessState { get; private set; }

    // Displayed in UI
    public bool MaterialPresence { get; private set; }

    // Displayed in UI (seconds)
    public int ProcessDuration { get; private set; }

    // Displayed in UI
    public string SelectedRecipe { get; private set; }

    // Event
    public event EventHandler? StateChanged;

    // Commands
    public void Initialize()
    {
        if (ProcessState != ProcessState.Init)
            return;

        IsInitialized = true;
        ProcessState = ProcessState.Idle;

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void PrepareForTransfer(TransferAction action)
    {
        switch (action)
        {
            case TransferAction.Place:

                if (MaterialPresence)
                    return;

                MaterialPresence = true;
                break;

            case TransferAction.Pick:

                if (!MaterialPresence)
                    return;

                MaterialPresence = false;
                ProcessState = ProcessState.Idle;
                break;
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RunRecipe()
    {
        if (!MaterialPresence)
            return;

        ProcessState = ProcessState.Running;
        StateChanged?.Invoke(this, EventArgs.Empty);

        Thread.Sleep(ProcessDuration * 1000);

        ProcessState = ProcessState.Completed;
        StateChanged?.Invoke(this, EventArgs.Empty);
    }
}