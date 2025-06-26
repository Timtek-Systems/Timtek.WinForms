using System.ComponentModel;

namespace Timtek.WinForms;

/// <summary>
///     Represents a specialized button control that visually updates its appearance
///     based on its enabled or disabled state. This control allows customization of
///     background colors for both states.
/// </summary>
public sealed class VisualEnableDisableButton : Button
{
    private Color backColourWhenEnabled  = Color.DarkSeaGreen;
    private Color backColourWhenDisabled = Color.IndianRed;

    public VisualEnableDisableButton()
    {
        EnabledChanged += VisualEnableDisableButton_EnabledChanged;
        UpdateButtonAppearance();
    }

    private void VisualEnableDisableButton_EnabledChanged(object? sender, EventArgs e) =>
        UpdateButtonAppearance();

    private void UpdateButtonAppearance()
    {
        BackColor = Enabled ? BackColourWhenEnabled : BackColourWhenDisabled;
        Invalidate();
    }

    /// <summary>
    ///     This property is overridden primarily to remove visibility in the designer.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Color BackColor
    {
        get => base.BackColor;
        set => base.BackColor = value;
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Background colour when enabled")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof(Color), "DarkSeaGreen")]
    public Color BackColourWhenEnabled
    {
        get => backColourWhenEnabled;
        set
        {
            backColourWhenEnabled = value;
            UpdateButtonAppearance();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Background colour when disabled")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof(Color), "IndianRed")]
    public Color BackColourWhenDisabled
    {
        get => backColourWhenDisabled;
        set
        {
            backColourWhenDisabled = value;
            UpdateButtonAppearance();
        }
    }
}