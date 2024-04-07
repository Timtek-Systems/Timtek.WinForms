using TA.Utils.Core;
using TA.Utils.Core.Diagnostics;

namespace Timtek.WinForms.MVVM;

/// <summary>
///     A <see cref="RelayCommand" /> that executes asynchronously. While the command is executing,
///     <see cref="CanExecute" /> is <c>false</c> so that command executions cannot overlap.
/// </summary>
public class AsyncRelayCommand : IRelayCommand
{
    private readonly Func<Task> execute;
    private readonly Func<bool> canExecuteQuery;
    private long isExecuting;
    private readonly ILog log;

    /// <summary>
    ///     Initialize a new instance.
    /// </summary>
    /// <param name="execute">A function that returns a task that completes after the command fully executes.</param>
    /// <param name="canExecute">A function that returns a boolean indicating whether the command can currently execute.</param>
    /// <param name="name">The name of the command (for diagnostic/display purposes). Optional; defaults to "unnamed".</param>
    /// <param name="log">
    ///     An optional logging service. If provided, log entries will be generated for <see cref="CanExecute" />
    ///     state changes, <see cref="CanExecuteChanged" /> events and command invocations.
    /// </param>
    public AsyncRelayCommand(
        Func<Task>  execute,
        Func<bool>? canExecute = null,
        string?     name       = null,
        ILog?       log        = null)
    {
        Name = name ?? "unnamed";
        this.execute = execute;
        canExecuteQuery = canExecute ?? (() => true);
        this.log = log ?? new DegenerateLoggerService();
    }

    /// <summary>
    ///     The name of the command for diagnostic/display purposes.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Raised as a notification to the control that it should re-check the value of <see cref="CanExecute" />.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    ///     Called from a ViewModel to notify the control that it should check the state of <see cref="CanExecute" />.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        try
        {
            log.Trace()
                .Message("AsyncRelayCommand {name} RaiseCanExecuteChanged", Name)
                .Property("relayCommand", this)
                .Write();

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("AsyncRelayCommand {name} RaiseCanExecuteChanged exception: {message}", Name, e.Message)
                .Write();
        }
    }

    public bool CanExecute(object? parameter)
    {
        if (Interlocked.Read(ref isExecuting) != 0)
            return false;

        try
        {
            var canExecute = canExecuteQuery();
            log.Trace()
                .Message("RelayCommand {name} CanExecute = {canExecute}", Name, canExecute)
                .Property("relayCommand", this)
                .Write();
            return canExecute;
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("RelayCommand {name} CanExecute exception: {message}", Name, e.Message)
                .Write();
        }

        return false;
    }

    /// <summary>
    ///     Execute the command asynchronously.
    ///     Note: the command may run on a different thread; be careful about cross-thread control updates.
    /// </summary>
    /// <param name="parameter">Not used.</param>
    public async void Execute(object? parameter)
    {
        try
        {
            Interlocked.Exchange(ref isExecuting, 1);
            RaiseCanExecuteChanged();
            log.Trace()
                .Message("AsyncRelayCommand {name} Executing", Name)
                .Property("relayCommand", this)
                .Write();
            await execute().ContinueOnAnyThread(); // ToDo - do we need to continue on the UI thread?
        }
        // We are starting a fire-and-forget task, so we MUST handle any and all exceptions to avoid application crashes.
        catch (Exception ex)
        {
            log.Error()
                .Exception(ex)
                .Message("AsyncRelayCommand {name} Execute exception: {message}", Name, ex.Message)
                .Write();
        }
        finally
        {
            Interlocked.Exchange(ref isExecuting, 0);
            RaiseCanExecuteChanged();
        }
    }
}