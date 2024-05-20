// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: LEDIndicator.cs  Last modified: 2019-09-21@02:51 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

using System.ComponentModel;

namespace Timtek.WinForms;

/// <summary>
///     Provides a status indicator modeled on a bi-colour red/green LED lamp. The lamp can be red or green and
///     (traffic light colours) and can be steady or can flash with a choice of different cadences.
/// </summary>
[DefaultProperty("LabelText")]
public sealed class LedIndicator : UserControl, ICadencedControl
{
    private readonly Container components; // required designer variable
    private TableLayoutPanel contentLayoutPanel;
    private bool disposed;
    private Label ledLabel;
    private Panel ledPanel;
    private bool powerOn = true;

    public LedIndicator()
    {
        //components = null;
        InitializeComponent(); // This call is required by the Windows.Forms Form Designer.
        Cadence = CadencePattern.SteadyOn;
        StartCadenceUpdates();
    }

    /// <summary>
    ///     Sets or reads the 'power status' of the LED
    ///     When the LED is Enabled, it reflects the current colour settings and cadence.
    ///     When disabled, the LED appears off and cadencing is disabled.
    /// </summary>
    [Category("LED Indicator")]
    [DefaultValue(true)]
    [Description(
        "Works like a power switch. When disabled (false), the LED always has the 'off' appearance regardless of colour or cadence settings."
    )]
    public new bool Enabled
    {
        get => powerOn;
        set
        {
            if (disposed)
                throw new ObjectDisposedException("LedIndicator");
            powerOn = value;
        }
    }

    /// <summary>
    ///     Sets the text displayed alongside the indicator
    /// </summary>
    [Category("LED Indicator")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue("LED Indicator")]
    [Description("Sets the text that appears next to the LED indicator.")]
    public string LabelText
    {
        get => ledLabel.Text;
        set => ledLabel.Text = value;
    }

    /// <summary>
    ///     Gets or sets the LED's status (which controls its display colour).
    /// </summary>
    [Category("LED Indicator")]
    [DefaultValue(TrafficLight.Green)]
    [Description(
        "Sets the LEDs status which controls the display colour. LEDs support 'traffic light' colours where Green represents normal status, Yellow represents a warning condition and Red represents an error."
    )]
    public TrafficLight Status { get; set; }

    /// <summary>
    ///     Gets or sets the LED cadence bitmap.
    ///     If the cadence has changed and is non-steady and the LED is enabled, then the cadence timer is started.
    /// </summary>
    /// <remarks>
    ///     Implements the <see cref="ICadencedControl.Cadence" /> property.
    /// </remarks>
    [Category("LED Indicator")]
    [DefaultValue(CadencePattern.SteadyOn)]
    [Description(
        "Sets the cadence (blinking pattern) of the LED indicator. Available cadences range from SteadyOff, through a number of alternating patterns of various urgency, to SteadyOn."
    )]
    public CadencePattern Cadence { get; set; }

    /// <summary>
    ///     Refreshes the LED display, taking account of the power,
    ///     colour and cadence settings.
    /// </summary>
    /// <param name="cadenceState">The new state of the control's appearance ('on' or 'off').</param>
    /// <remarks>
    ///     Implements the <see cref="ICadencedControl.CadenceUpdate" /> method.
    ///     The <see cref="CadencedControlUpdater" /> always calls this method on the GUI thread.
    /// </remarks>
    public void CadenceUpdate(bool cadenceState)
    {
        if (disposed)
            throw new ObjectDisposedException("Attempt to update a control after it has been disposed.");
        var targetState = cadenceState && powerOn;
        if (targetState)
            RenderOnAppearance();
        else
            RenderOffAppearance();
    }

    /// <summary>
    ///     Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.
    /// </summary>
    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Releases the unmanaged resources and optionally releases the managed resources.
    /// </summary>
    /// <param name="fromUserCode">
    ///     true to release both managed and unmanaged resources; false to release only unmanaged
    ///     resources.
    /// </param>
    protected override void Dispose(bool fromUserCode)
    {
        if (!disposed)
        {
            if (fromUserCode)
            {
                StopCadenceUpdates();
                if (components != null)
                    components.Dispose();
                if (ledPanel != null)
                {
                    ledPanel.Dispose();
                    ledPanel = null;
                }

                if (ledLabel != null)
                {
                    ledLabel.Dispose();
                    ledLabel = null;
                }
            }

            disposed = true;
        }

        base.Dispose(fromUserCode);
    }

    /// <summary>
    ///     Required method for Designer support - do not modify
    ///     the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ledPanel = new Panel();
        ledLabel = new Label();
        contentLayoutPanel = new TableLayoutPanel();
        contentLayoutPanel.SuspendLayout();
        SuspendLayout();
        //
        // ledPanel
        //
        ledPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                           | AnchorStyles.Left;
        ledPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        ledPanel.BackColor = Color.WhiteSmoke;
        ledPanel.BorderStyle = BorderStyle.FixedSingle;
        ledPanel.CausesValidation = false;
        ledPanel.Location = new Point(3, 3);
        ledPanel.MaximumSize = new Size(20, 10);
        ledPanel.MinimumSize = new Size(20, 10);
        ledPanel.Name = "ledPanel";
        ledPanel.Size = new Size(20, 10);
        ledPanel.TabIndex = 0;
        //
        // ledLabel
        //
        ledLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                           | AnchorStyles.Left
                                           | AnchorStyles.Right;
        ledLabel.AutoSize = true;
        ledLabel.CausesValidation = false;
        ledLabel.Location = new Point(26, 0);
        ledLabel.Margin = new Padding(0);
        ledLabel.Name = "ledLabel";
        ledLabel.Size = new Size(72, 16);
        ledLabel.TabIndex = 1;
        ledLabel.Text = "LED Indicator";
        ledLabel.TextAlign = ContentAlignment.MiddleLeft;
        //
        // contentLayoutPanel
        //
        contentLayoutPanel.AutoSize = true;
        contentLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        contentLayoutPanel.ColumnCount = 2;
        contentLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        contentLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        contentLayoutPanel.Controls.Add(ledPanel, 0, 0);
        contentLayoutPanel.Controls.Add(ledLabel, 1, 0);
        contentLayoutPanel.Location = new Point(0, 0);
        contentLayoutPanel.Margin = new Padding(0, 0, 0, 0);
        contentLayoutPanel.Name = "contentLayoutPanel";
        contentLayoutPanel.RowCount = 1;
        contentLayoutPanel.RowStyles.Add(new RowStyle());
        contentLayoutPanel.Size = new Size(98, 16);
        contentLayoutPanel.TabIndex = 2;
        //
        // LedIndicator
        //
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        Controls.Add(contentLayoutPanel);
        Margin = new Padding(0, 0, 0, 0);
        Name = "LedIndicator";
        Size = new Size(98, 16);
        AutoSizeChanged += LedIndicator_AutoSizeChanged;
        contentLayoutPanel.ResumeLayout(false);
        contentLayoutPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    /// <summary>
    ///     Renders the 'power off' appearance of the LED indicator.
    /// </summary>
    private void RenderOffAppearance()
    {
        SetColour(Color.WhiteSmoke);
    }

    /// <summary>
    ///     Renders the 'power on' appearance of the LED indicator. The exact appearance depends on the <see cref="Status" />
    ///     property.
    /// </summary>
    private void RenderOnAppearance()
    {
        switch (Status) // Render the 'power on' appearance according to status.
        {
            case TrafficLight.Green:
                SetColour(Color.LightGreen);
                break;
            case TrafficLight.Yellow:
                SetColour(Color.Orange);
                break;
            case TrafficLight.Red:
                SetColour(Color.Red);
                break;
        }
    }

    /// <summary>
    ///     Sets the colour of the LED.
    ///     If the colour is changed, then the LED's panel control is invalidated to force a re-draw.
    /// </summary>
    /// <param name="newColour">The new led colour.</param>
    private void SetColour(Color newColour)
    {
        if (newColour != ledPanel.BackColor)
        {
            ledPanel.BackColor = newColour;
            ledPanel.Invalidate();
            ledPanel.Update();
        }
    }

    /// <summary>
    ///     Register with the <see cref="CadencedControlUpdater" />.
    /// </summary>
    private void StartCadenceUpdates()
    {
        CadencedControlUpdater.Instance.Add(this);
    }

    /// <summary>
    ///     Unregister from the <see cref="CadencedControlUpdater" />.
    /// </summary>
    private void StopCadenceUpdates()
    {
        CadencedControlUpdater.Instance.Remove(this);
        RenderOffAppearance();
    }

    private void LedIndicator_AutoSizeChanged(object sender, EventArgs e)
    {
        ledLabel.AutoSize = AutoSize;
    }
}