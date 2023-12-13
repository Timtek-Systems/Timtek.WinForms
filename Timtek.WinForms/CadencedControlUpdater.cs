// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: CadencedControlUpdater.cs  Last modified: 2019-09-21@02:42 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

using System.Diagnostics;
using TA.Utils.Core;

namespace Timtek.WinForms;

/// <summary>
///     Manages <see cref="ICadencedControl" /> objects that must be toggled on and off in a regular pattern over
///     time. This is known as a cadence. Intended primarily for Windows Forms controls, but potentially useful in
///     other situations.
/// </summary>
/// <remarks>
///     Maintains weak references to the cadenced items so that the items may still be garbage collected.
/// </remarks>
internal sealed class CadencedControlUpdater
{
    /// <summary>
    ///     Indicates the current bit position within the cadence register.
    /// </summary>
    internal static int CadenceBitPosition;

    /// <summary>
    ///     A list of all the cadenced controls that have been created.
    /// </summary>
    private static readonly IDictionary<int, WeakReference<ICadencedControl>> ControlList =
        new Dictionary<int, WeakReference<ICadencedControl>>();

    /// <summary>
    ///     <para>The one and only <see cref="TA.WinFormsControls.CadencedControlUpdater.instance" /></para>
    ///     <para>of this class.</para>
    /// </summary>
    private static readonly Lazy<CadencedControlUpdater> lazyInstance = new(() => new CadencedControlUpdater());

    /// <summary>
    ///     An object used for thread synchronization during object initialization. This ensures that the singleton
    ///     is thread-safe.
    /// </summary>
    private static readonly object SyncRoot = new();

    private CancellationTokenSource cancellationTokenSource;
    private Task updateTask;

    private CadencedControlUpdater()
    {
    }

    /// <summary>
    ///     Gets a reference to the Singleton. If the Singleton has not yet be instantiated, this causes the object
    ///     to be created and the constructor to execute. This operation is thread-safe.
    /// </summary>
    public static CadencedControlUpdater Instance => lazyInstance.Value;

    /// <summary>
    ///     Adds the specified <see cref="ICadencedControl" /> to the list of managed controls. If this is the first
    ///     <paramref name="control" /> being added, then the update timer is configured and started.
    /// </summary>
    /// <remarks>
    ///     Each <paramref name="control" /> can only appear in the list once (duplicate adds will be silently
    ///     ignored).
    /// </remarks>
    /// <param name="control">The control to be managed.</param>
    public void Add(ICadencedControl control)
    {
        var hashCode = control.GetHashCode();
        Trace.WriteLine($"Adding hash {hashCode}");
        if (ControlList.ContainsKey(hashCode))
            Trace.TraceWarning($"Ignoring duplicate hash code {hashCode}.");
        else
            ControlList.Add(hashCode, new WeakReference<ICadencedControl>(control));
        if (updateTask == null) StartUpdates();
    }

    /// <summary>
    ///     Removes a <paramref name="control" /> from the
    ///     <see cref="ControlList" /> . If no managed controls remain in
    ///     the list, then the update timer is stopped.
    /// </summary>
    public void Remove(ICadencedControl control)
    {
        try
        {
            var hash = control.GetHashCode();
            Trace.TraceInformation($"Removing hash {hash}");
            if (ControlList.ContainsKey(hash))
                ControlList.Remove(hash);
        }
        catch (Exception e)
        {
            Trace.TraceError($"{e.GetType().Name} removing control: {e.Message}");
        }
        finally
        {
            if (!ControlList?.Any() ?? false)
                StopUpdates();
        }
    }

    /// <summary>
    ///     Starts asynchronous cadence updates.
    /// </summary>
    private void StartUpdates()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;
        updateTask = UpdateTask(TimeSpan.FromMilliseconds(125), token)
            .ContinueWith(task => Trace.TraceInformation("Cadence updates stopped."));
    }

    /// <summary>
    ///     Stops cadence updates and waits until the update task has stopped or completed.
    /// </summary>
    private void StopUpdates()
    {
        cancellationTokenSource?.Cancel();
        updateTask?.ContinueWith(task => Trace.TraceInformation("Cadence updates cancelled."))
            .Wait(TimeSpan.FromMilliseconds(200));
        updateTask = null;
        cancellationTokenSource?.Dispose();
        cancellationTokenSource = null;
    }

    /// <summary>
    ///     <para>
    ///         Performs a cadence update on a single control. Any exception will cause the <paramref name="control" />
    ///     </para>
    ///     <para>to be removed from the update list.</para>
    /// </summary>
    /// <param name="control">The control.</param>
    private void UpdateOneControl(KeyValuePair<int, WeakReference<ICadencedControl>> item)
    {
        try
        {
            var maybeControl = item.Value.Maybe(); // Weak reference is potentially invalid.
            if (!maybeControl.Any())
            {
                // Weak reference has become invalid, so this control should no longer be updated.
                ControlList.Remove(item.Key);
                return;
            }

            // We now have a strong reference so we can go ahead and update the control.
            var control = maybeControl.Single();
            var state = ((uint)control.Cadence).Bit(CadenceBitPosition);
            control.CadenceUpdate(state);
        }
        catch
        {
            // Any error whatsoever, and the control is toast.
            ControlList.Remove(item.Key);
        }
    }

    /// <summary>
    ///     Updates the state of each of the managed <see cref="ICadencedControl" /> objects. The task runs
    ///     asynchronously and repeatedly updates the managed items at the specified interval, until cancelled.
    /// </summary>
    /// <param name="updateInterval">The update interval.</param>
    /// <param name="cancel">A cancellation token that can be used to terminate the task.</param>
    private async Task UpdateTask(TimeSpan updateInterval, CancellationToken cancel)
    {
        while (!cancel.IsCancellationRequested)
        {
            await Task.Delay(updateInterval, cancel).ContinueInCurrentContext();
            var updateList = ControlList.ToArray(); // Iterate over a copy of the collection.
            foreach (var control in updateList)
            {
                if (cancel.IsCancellationRequested)
                    return;
                UpdateOneControl(control);
            }

            // Increment and (if necessary) wrap the cadence bit position index.
            if (++CadenceBitPosition > 31)
                CadenceBitPosition = 0;
        }
    }
}