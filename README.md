# Timtek.WinForms

Provides a number of useful Windows Forms controls.

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