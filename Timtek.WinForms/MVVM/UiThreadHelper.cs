namespace Timtek.WinForms.MVVM;

/// <summary>
///     A utility class to help with marshalling operations to the UI thread. The purpose of this helper is to ensure that
///     operations that should run on the UI thread, do run on that thread. Thus, it tries hard to protect against doing
///     the wrong thing and will throw exceptions if something looks wrong.
/// </summary>
public class UiThreadHelper
{
    private readonly SynchronizationContext syncContext;

    /// <summary>
    ///     Create a new instance that captures the Windows Forms UI synchronization context. This may be passed in via the
    ///     optional <paramref name="context" /> parameter, or discovered on-the-fly. In either case, the context must not be
    ///     null and must be an instance of exactly <see cref="WindowsFormsSynchronizationContext" />.
    /// </summary>
    /// <exception cref="InvalidOperationException">Unable to capture the Windows Forms UI thread synchronization context</exception>
    public UiThreadHelper(WindowsFormsSynchronizationContext? context = null)
    {
        syncContext = context ?? SynchronizationContext.Current;
        if (!RunningOnUiThread())
            throw new InvalidOperationException("Unable to capture the Windows Forms UI thread synchronization context");
    }

    /// <summary>
    ///     Indicates whether the caller is running on the Windows Forms UI thread.
    /// </summary>
    /// <returns><c>true</c> if the current execution context is the Windows Forms UI thread.</returns>
    public bool RunningOnUiThread() => syncContext is not null && ReferenceEquals(syncContext, SynchronizationContext.Current);

    /// <summary>
    ///     Runs an action on the Windows Forms UI thread. The action is run directly if already on the UI thread, or posted to
    ///     the UI's synchronization context.
    /// </summary>
    /// <param name="action">The action to run.</param>
    public void RunOnUiThread(Action action)
    {
        //if (RunningOnUiThread())
        //    action();
        //else
        //    syncContext.Post(_ => action(), null);
            syncContext.Post(_ => action(), null);
    }
}