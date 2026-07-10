using System;
using System.IO;
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
        IsDoorOpen = false;
        ProcessDuration = 0;
        SelectedRecipe = "No Recipe Selected";
    }

    // Properties
    public string Identifier { get; }

    // Internal properTY
    public bool IsInitialized { get; private set; }


    public ProcessState ProcessState { get; private set; }


    public bool MaterialPresence { get; private set; }


    public bool IsDoorOpen { get; private set; }


    public int ProcessDuration { get; private set; }


    public string SelectedRecipe { get; private set; }


    public event EventHandler? StateChanged;

    // Used to signal a running recipe to stop early.
    private CancellationTokenSource? _cts;


    public void Initialize()
    {
        if (ProcessState != ProcessState.Init)
            return;

        IsInitialized = true;
        ProcessState = ProcessState.Idle;

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    // Door opens to load or unload material.
    // Not allowed while a recipe is running (safety interlock),
    // and not allowed if the door is already open.
    public void OpenDoor()
    {
        if (!IsInitialized)
            return;

        if (ProcessState == ProcessState.Running)
            return;

        if (IsDoorOpen)
            return;

        IsDoorOpen = true;

        // Opening the door after a completed run unloads the material
        if (MaterialPresence)
        {
            MaterialPresence = false;
            ProcessState = ProcessState.Idle;

            // Recipe/duration belonged to the wafer that just left.
            // Clear it so the next lot must select a fresh recipe file.
            SelectedRecipe = "No Recipe Selected";
            ProcessDuration = 0;
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    // Door closes to seal the chamber, loading the material for processing.
    // Not allowed if the door is already closed.
    public void CloseDoor()
    {
        if (!IsDoorOpen)
            return;

        IsDoorOpen = false;
        MaterialPresence = true;

        StateChanged?.Invoke(this, EventArgs.Empty);
    }


    // NEW: Loads recipe name + duration from a file selected by the user, then runs it.
    // Expected file format (plain text):
    //     RecipeName=RECIPE-02
    //     Duration=20
    // Returns true if a valid recipe was found and the run started.
    // Returns false if the file was invalid/empty — caller should show an error.
    public bool RunRecipeFromFile(string filePath)
    {
        if (!MaterialPresence)
            return false;

        if (IsDoorOpen)
            return false;

        string? recipeName = null;
        int? duration = null;

        try
        {
            foreach (var rawLine in File.ReadAllLines(filePath))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                var parts = line.Split('=', 2);
                if (parts.Length != 2)
                    continue;

                var key = parts[0].Trim().ToLowerInvariant();
                var value = parts[1].Trim();

                if (key == "recipename" || key == "recipe")
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        recipeName = value;
                }
                else if (key == "duration")
                {
                    if (int.TryParse(value, out int parsedDuration) && parsedDuration > 0)
                        duration = parsedDuration;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }

        // File must have a real recipe name AND a duration greater than 0.
        if (string.IsNullOrWhiteSpace(recipeName) || duration is null)
            return false;

        SelectedRecipe = recipeName;
        ProcessDuration = duration.Value;

        StateChanged?.Invoke(this, EventArgs.Empty);

        RunRecipeInternal();
        return true;
    }
    private void RunRecipeInternal()
    {
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        ProcessState = ProcessState.Running;
        StateChanged?.Invoke(this, EventArgs.Empty);

        // Waits for the recipe duration, but wakes up early if CancelRecipe() is called.
        // WaitOne returns true if the token was cancelled before the timeout elapsed.
        bool wasCancelled = token.WaitHandle.WaitOne(ProcessDuration * 1000);

        ProcessState = wasCancelled ? ProcessState.Idle : ProcessState.Completed;
        StateChanged?.Invoke(this, EventArgs.Empty);

        _cts.Dispose();
        _cts = null;
    }

    // Cancels a currently running recipe. No effect if no recipe is running.
    public void CancelRecipe()
    {
        if (ProcessState != ProcessState.Running)
            return;

        _cts?.Cancel();
    }
}