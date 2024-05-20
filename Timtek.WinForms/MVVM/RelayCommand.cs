using System.Windows.Input;
using TA.Utils.Core.Diagnostics;

namespace Timtek.WinForms.MVVM;

/// <summary>
///     A Relay Command in which the <see cref="ICommand.Execute" /> and <see cref="ICommand.CanExecute" /> methods accept
///     a parameter of type <typeparamref name="TParam" />.
/// </summary>
/// <typeparam name="TParam"></typeparam>
public interface IRelayCommand<TParam> : IRelayCommand
{
}

public class RelayCommand : IRelayCommand
{
    public string Name { get; }
    private readonly Action executeAction;
    private readonly Func<bool> canExecuteQuery;
    private readonly ILog log;

    public RelayCommand(Action execute, Func<bool>? canExecute, string? name = null, ILog? log = null)
    {
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
        var typedParam = (TParam?)parameter; // Will throw if parameter is not assignable to TParam.
        try
        {
            var canExecute = canExecuteQuery(typedParam);
            log.Trace()
                .Message("RelayCommand {{name}} CanExecute({parameter}) = {{canExecute}}", Name, typedParam, canExecute)
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
        var typedParam = (TParam?)parameter; // Will throw if parameter is not assignable to TParam.
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