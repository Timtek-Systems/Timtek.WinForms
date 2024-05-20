using System.Windows.Input;

namespace Timtek.WinForms.MVVM;

/// <summary>
///     An MVVM-style Relay Command which can report when it may be executed via <see cref="ICommand.CanExecute" /> and
///     which can notify a bound control when the <see cref="ICommand.CanExecute" /> might have changed via the
///     <see cref="ICommand.CanExecuteChanged" /> event. ViewModels should call <see cref="RaiseCanExecuteChanged" />
///     to inform the bound control that it should re-query the <see cref="ICommand.CanExecute" /> state.
///     Bound controls should use the <see cref="ICommand.CanExecute" /> value to enable/disable themselves.
/// </summary>
public interface IRelayCommand : ICommand
{
    /// <summary>
    ///     Raise the <see cref="ICommand.CanExecuteChanged" /> event on the bound control.
    /// </summary>
    void RaiseCanExecuteChanged();
}