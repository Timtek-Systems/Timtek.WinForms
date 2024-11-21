# Timtek.WinForms

Provides a number of useful Windows Forms controls and Helpers.

## MVVM Pattern and `RelayCommand`

The *Model-View-ViewModel* pattern was poularised by WPF/XAML and is a way of separating View logic (the Form) from decision logic (The ViewModel) and data access logic (the Model).
The pros and cons of MVVM are beyond the scope of this document, but for those who wish to use it, we have provided some rudimentary support.
Use of the MVVM pattern is completely optional.

The key concept in MVVM is that the ViewModel has no knowledge of the View, so cannot directly manipulate the user interface.
Instead, the ViewModel provides public properties that the View can bind to at runtime using data binding.
Thus, changes in the ViewModel's public interface are _automatically_ reflected in the UI controls.
Conversely, some form controls such as buttons need to trigger actions in the View Model.
This is also achieved using data-binding.
The View Model can expose properties of type `ICommand` which can be bound to the `Command` property of button-like controls.
The controls have built-in logic to invoke the command at the apopropriate time.

Built-in controls inheriting `ButtonBase` (`Button`, `CheckBox`, `RadioButton`) controls have a `Command` property which can be bound to an `ICommand` implementation.
Our `ToggleSwitch` control also has a `Command` property that can be data-bound.

We provide a `IRelayCommand` interface which inherits from `ICommand`, and a few implementations:

- `RelayCommand', which ignores any parameters passed to it by the control, ;
- `RelayCommand<T>` which expects a parameter of type `T`.
- `AsyncRelayCommand` which executes asynchronously. Care is needed not to make cross-thread form updates.

A `RelayCommand` can be data-bound to a control's `Command` property and the command will be invoked in the ViewModel whenever some appropriate action has occurred on the control.
For a `Button` control, clicking the button invokes the command.
For a `ToggleSwitch` control, changing the toggle state invokes the command with the value of the `Checked` property.

### MVVM Considerations

The control does not currently work very well with data binding. For example, it does not seem to be possible to successfully bind the `Checked` property to a ViewModel property.

We have worked around this by adding a very basic support for the `ICommand` pattern.
The command is invoked whenever the state of the `Checked` property changes.
The ViewModel should expose a public property of type `RelayCommand<bool>`.
This property can then be data-bound to a `ToggleSwitch` control's `Command` property.


Since the ViewModel class should not have any knowledge of the View, there is no way for it to query the `Checked` property of the control. The `RelayCommand<bool>` implementation expects a `bool` parameter and the `ToggleSwitch` control passes in the current value of its `Checked` property.


## Cadenced Controls

Provides a generic way to have a control flash or blink with a defined `cadence pattern`. Any control implementing `ICadencedControl` can make use of this mechanism.

Cadences can be one of the predefined ones in the `CadencePattern` enum, or can be specified as a bit pattern in a 32-bit unsigned integer.

### Annunciators

The `Annunciator` control is a cadenced control useful for building up blocks of status indicators.

A companion control `AnnuniatorPanel` is a flow-layout panel with some preset look and feel and is specifically designed to contain annunciators.

### LED Indicator

An `LEDIndicator` control has a rectangular indicator that is supposed to look like an LED, plus a text label. The control implements `ICadencedControl` so the LED part can be made to flash in any of the normal patterns.

## Toggle Switch

The `ToggleSwitch` control is (logically) a type of `CheckBox` but with a visual appearance of a sliding or rocking switch.

The control is quite customisable and can take on a wide variety of appearances, including user-created images. The control can be dragged with the mouse (or a finger on touch screens) or clicked to toggle.

This control draws heavily on the work of Johnny Jorgensen thanks to his CodeProject publication, and is used under the CodeProject Open License.

## Gauge Controls

A gauge control is included based on the defunct AGauge project by Andrew J. Bauer. The control is somewhat dated but is nevertheless useful for many simple applications.

For usage hints, please see https://www.codeproject.com/articles/448721/agauge-winforms-gauge-control
