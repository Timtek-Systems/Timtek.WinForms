namespace Timtek.WinForms.MVVM;

/// <summary>
///     Abstraction for dispatching work to the UI thread.
///     This interface allows commands to be testable in unit tests where UI thread dispatch may not be available.
/// </summary>
public interface IUiThreadDispatcher
{
    /// <summary>
    ///     Execute an action on the UI thread.
    /// </summary>
    /// <param name="action">The action to execute on the UI thread.</param>
    void Post(Action action);
}

/// <summary>
///     Factory and context manager for UI thread dispatchers.
///     This allows the dispatcher to be configured globally (e.g., per test) without modifying command constructors.
/// </summary>
public static class UiThreadDispatcherContext
{
    [ThreadStatic]
    private static IUiThreadDispatcher? overrideDispatcher;

    /// <summary>
    ///     Gets the current UI thread dispatcher.
    ///     If an override is set (via <see cref="SetDispatcher" />), returns that.
    ///     Otherwise, attempts to create a <see cref="UiThreadHelper" /> for the current context.
    ///     If that fails (not on a UI thread), returns a <see cref="CurrentThreadDispatcher" />.
    /// </summary>
    public static IUiThreadDispatcher Current
    {
        get
        {
            if (overrideDispatcher != null)
                return overrideDispatcher;

            try
            {
                return new UiThreadHelper();
            }
            catch (InvalidOperationException)
            {
                // Not on a WinForms UI thread, use current thread dispatcher
                return new CurrentThreadDispatcher();
            }
        }
    }

    /// <summary>
    ///     Sets a dispatcher override for the current thread.
    ///     Useful for unit testing where you want to use <see cref="CurrentThreadDispatcher" />.
    /// </summary>
    /// <param name="dispatcher">The dispatcher to use, or null to clear the override.</param>
    public static void SetDispatcher(IUiThreadDispatcher? dispatcher)
    {
        overrideDispatcher = dispatcher;
    }
}
