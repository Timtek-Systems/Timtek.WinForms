using System.Windows.Input;

namespace Timtek.WinForms.SlidingToggleButton;

/// <summary>
///     A type that can accept an ICommand and execute it at the appropriate time.
///     MVVM-style View-ViewModel integration.
/// </summary>
/// <remarks>
///     Many of the built-in Windows Forms controls implement <c>ICommandBindingTargetProvider</c>, but that interface is
///     declared <c>internal</c> so cannot be used directly. This is our simplified implementation of that interface.
///     The intent is to provide MVVM-style RelayCommand functionality as simply as possible.
/// </remarks>
public interface ICommander
{
    public ICommand Command { get; set; }
}