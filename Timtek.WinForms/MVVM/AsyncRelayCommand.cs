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

    public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null, string? name = null,
        ILog? log = null)
    {
        Name = name ?? "unnamed";
        this.execute = execute;
        canExecuteQuery = canExecute ?? (() => true);
        this.log = log ?? new DegenerateLoggerService();
    }

    public string Name { get; init; }

    public event EventHandler CanExecuteChanged;

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

    public bool CanExecute(object parameter)
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

    public async void Execute(object parameter)
    {
        Interlocked.Exchange(ref isExecuting, 1);
        RaiseCanExecuteChanged();

        try
        {
            log.Trace()
                .Message("AsyncRelayCommand {name} Executing", Name)
                .Property("relayCommand", this)
                .Write();
            await execute();
        }
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