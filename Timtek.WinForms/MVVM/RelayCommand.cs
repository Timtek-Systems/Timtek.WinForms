using TA.Utils.Core.Diagnostics;

namespace Timtek.WinForms.MVVM;

/// <summary>
///     Represents a command that can be executed and queried for its ability to execute.
///     This class implements the <see cref="IRelayCommand" /> interface and provides support for
///     executing actions, determining executability, and raising notifications when the executability changes.
/// </summary>
/// <remarks>
///     The <see cref="RelayCommand" /> is typically used in MVVM patterns to bind user interface actions
///     to logic in the view model. It ensures that commands are executed on the UI thread and provides
///     optional logging capabilities.
/// </remarks>
public class RelayCommand : IRelayCommand
{
    public           string         Name { get; }
    private readonly Action         executeAction;
    private readonly Func<bool>     canExecuteQuery;
    private readonly ILog           log;
    private readonly UiThreadHelper uiThreadHelper;


    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
    /// </summary>
    /// <param name="execute">
    ///     The action to execute when the command is invoked. This parameter is required.
    /// </param>
    /// <param name="canExecute">
    ///     A function that determines whether the command can execute. If null, the command is always executable.
    /// </param>
    /// <param name="name">
    ///     An optional name for the command. If null, the default value "unnamed" will be used.
    /// </param>
    /// <param name="log">
    ///     An optional logger instance for logging purposes. If null, a degenerate logger will be used.
    /// </param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the instance is created on a thread other than the UI thread.
    /// </exception>
    public RelayCommand(Action execute, Func<bool>? canExecute, string? name = null, ILog? log = null)
    {
        uiThreadHelper = new UiThreadHelper(); // will throw if not created on the UI thread.
        Name = name ?? "unnamed";
        executeAction = execute;
        canExecuteQuery = canExecute ?? (() => true);
        this.log = log ?? new DegenerateLoggerService();
    }

    public bool CanExecute(object? parameter)
    {
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

    public void Execute(object? parameter)
    {
        try
        {
            log.Trace()
                .Message("RelayCommand {name} Executing", Name)
                .Property("relayCommand", this)
                .Write();
            executeAction();
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("RelayCommand {name} Execute exception: {message}", Name, e.Message)
                .Write();
        }
    }

    public event EventHandler? CanExecuteChanged;

    protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public void RaiseCanExecuteChanged()
    {
        try
        {
            log.Trace()
                .Message("RelayCommand {name} RaiseCanExecuteChanged", Name)
                .Property("relayCommand", this)
                .Write();
            uiThreadHelper.RunOnUiThread(OnCanExecuteChanged);
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("RelayCommand {name} RaiseCanExecuteChanged exception: {message}", Name, e.Message)
                .Write();
        }
    }
}

/// <inheritdoc />
public class RelayCommand<TParam> : IRelayCommand<TParam>
{
    public string Name { get; }
    private readonly Action<TParam?> executeAction;
    private readonly Func<TParam?, bool> canExecuteQuery;
    private readonly ILog log;

    public RelayCommand(Action<TParam> execute, Func<TParam,bool>? canExecute, string? name = null, ILog? log = null)
    {
        Name = name ?? "unnamed";
        executeAction = execute;
        canExecuteQuery = canExecute ?? ((TParam? _) => true);
        this.log = log ?? new DegenerateLoggerService();
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter is not TParam typedParam)
        {
            log.Warn()
                .Message("RelayCommand {name} CanExecute received parameter of an incorrect type: {parameter}", Name, parameter)
                .Property("relayCommand", this)
                .Write();
            return false;
        }

        try
        {
            var canExecute = canExecuteQuery(typedParam);
            log.Trace()
                .Message("RelayCommand {name} CanExecute({parameter}) = {canExecute}", Name, typedParam, canExecute)
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
    ///     Execute the command, passing in a nullable parameter of type <typeparamref name="TParam" />.
    /// </summary>
    /// <param name="parameter">The command parameter which must be of runtime type <typeparamref name="TParam" /> or null.</param>
    public void Execute(object? parameter)
    {
        if (parameter is not TParam typedParam)
        {
            log.Warn()
                .Message("RelayCommand {name} Execute received parameter of an incorrect type: {parameter}", Name, parameter)
                .Property("relayCommand", this)
                .Write();
            return;
        }

        try
        {
            log.Trace()
                .Message("RelayCommand {name} Execute({typedParam})", Name, typedParam)
                .Property("relayCommand", this)
                .Write();
            executeAction(typedParam);
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("RelayCommand {name} Execute exception: {message}", Name, e.Message)
                .Write();
        }
    }

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        try
        {
            log.Trace()
                .Message("RelayCommand {name} RaiseCanExecuteChanged", Name)
                .Property("relayCommand", this)
                .Write();

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            log.Error()
                .Exception(e)
                .Message("RelayCommand {name} RaiseCanExecuteChanged exception: {message}", Name, e.Message)
                .Write();
        }
    }
}