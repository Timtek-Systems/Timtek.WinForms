// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: Annunciator.cs  Last modified: 2019-09-21@23:00 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

using System.ComponentModel;

namespace Timtek.WinForms;

/// <summary>
///     <para>
///         Wikipedia: An annunciator panel is a group of lights used as a central indicator of
///         status of equipment or systems in an aircraft, industrial process, building or other
///         installation. Usually the annunciator panel includes a main warning lamp or audible
///         signal to draw the attention of operating personnel to the annunciator panel for
///         abnormal events or conditions.
///     </para>
///     <para>
///         <para>
///             <para>
///                 The <see cref="Annunciator" /> control provides a simple, standard method of
///                 displaying a status notification to the user within a Windows Forms
///                 application. Anunciators are best used with the companion
///                 <see cref="AnnunciatorPanel" />
///             </para>
///             <para>
///                 control, although they can be placed anywhere on a Windows Form. The control
///                 can be used to provide simple On/Off status displays or can be configured to
///                 blink with various levels of urgency so that it can represent alarm
///                 conditions.
///             </para>
///         </para>
///         <example>
///             An annunciator may represent the slewing state of a telescope. It would be
///             represented by the word "SLEW". When the telescope is stationary, the
///             annunciator remains inactive. When the telescope begins to slew, the annunciator
///             is set to <see cref="TA.WinFormsControls.CadencePattern.BlinkFast" /> to alert
///             the user that the equipment is in motion.
///         </example>
///     </para>
///     <para>
///         Each annunciator has active and inactive states. When inactive, the control displays
///         in a subdued colour that is readable but does not draw attention. When active, the
///         control will display in a stronger, more visible colour and will either have a
///         steady state or will blink in one of a number of predefined cadence patterns. The
///         cadence patterns are fixed and not user-definable, so that a standard 'look and
///         feel' is promoted across different applications.
///     </para>
///     <para>
///         Whilst the user is at liberty to choose different colours for both
///         <see cref="TA.WinFormsControls.Annunciator.ActiveColor" /> and
///         <see cref="TA.WinFormsControls.Annunciator.InactiveColor" /> , The default colours
///         have been chosen to look similar to earlier applications that use similar displays
///         and the defaults are highly recommended for most circumstances. The control's
///         background colour is inherited from the parent control (which should normally be an
///         <see cref="AnnunciatorPanel" /> ) and is not directly settable by the user.
///     </para>
/// </summary>
[DefaultProperty(nameof(Text))]
public sealed class Annunciator : Label, ICadencedControl
{
    private bool disposed;
    private Color activeColor;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Annunciator" /> class.
    /// </summary>
    public Annunciator()
    {
        InactiveColor = Color.FromArgb(96, 4, 4);
        ActiveColor = Color.FromArgb(200, 4, 4);
        Font = new Font("Consolas", 10.0F);
        Cadence = CadencePattern.SteadyOn;
        BackColor = Parent?.BackColor ?? Color.FromArgb(64, 0, 0);
        if (Parent != null)
            BackColor = Parent.BackColor; // Inherit background colour from parent.
        ParentChanged += AnunciatorParentChanged;
        var currentState = ((uint) Cadence).Bit(CadencedControlUpdater.CadenceBitPosition);
        ForeColor = currentState ? ActiveColor : InactiveColor;
        CadencedControlUpdater.Instance.Add(this);
    }

    /// <summary>
    ///     Gets or sets the color of the anunciator text when active.
    /// </summary>
    /// <value>
    ///     The color of the anunciator text when active.
    /// </value>
    [Category("Annunciator")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(0xffdc143c)] // Crimson
    [Description(
        "The active color is displayed when the annunciator is on (illuminated). This should be bright and have a high contrast with the control's background. The default value is recommended for most situations."
    )]
    public Color ActiveColor
    {
        get => activeColor;
        set
        {
            activeColor = value;
            if (DesignMode)
                ForeColor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the cadence (blink pattern) of the anunciator. Different cadence
    ///     patterns imply different levels of urgency or severity.
    /// </summary>
    /// <value>
    ///     The cadence pattern.
    /// </value>
    [Category("Annunciator")]
    [DefaultValue(CadencePattern.SteadyOn)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description(
        "Determines the cadence (blink pattern) for the annunciator. Different cadences imply different levels of severity or urgency."
    )]
    public CadencePattern Cadence { get; set; }

    /// <summary>
    ///     Hides the <see cref="ForeColor" /> property of the
    ///     <see cref="Label" /> base class because the foreground colour is controlled by the
    ///     cadence and should not be user editable.
    /// </summary>
    /// <value>
    ///     The color of the fore.
    /// </value>
    [Category("Appearance")]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("Do not set this value directly. Use ActiveColor instead.")]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set => base.ForeColor = value;
    }

    /// <summary>
    ///     Gets or sets the color of the annunciatior text when inactive (off, dim). The color
    ///     chosen should provide low contrast with the control's background such that it is
    ///     barely readable.
    /// </summary>
    [Category("Annunciator")]
    [DefaultValue(0xff480404)]
    [Description(
        "The anunciator's inactive colour. This is usually set to a value close to (but not equal) to the background colour such that the text is barely discernable. The default value is recommended for most situations."
    )]
    public Color InactiveColor { get; set; }

    /// <summary>
    ///     When muted, an anunciator will always display in its
    ///     <see cref="InactiveColor" /> . This provides a handy
    ///     On/Off <see langword="switch" /> without disturbing the cadence pattern.
    /// </summary>
    [Category("Annunciator")]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description(
        "When muted, the annunciator always displays in its InactiveColor regardless of Cadence.")]
    public bool Mute { get; set; }

    /// <summary>
    ///     Updates the annunciator's display, if it has changed since the last update. This
    ///     method is typically called periodically by <see cref="CadencedControlUpdater" /> .
    /// </summary>
    /// <param name="cadenceState">
    ///     The new state of the control's appearance ('on' or 'off').
    /// </param>
    public void CadenceUpdate(bool cadenceState)
    {
        if (IsDisposed)
            throw new ObjectDisposedException(
                "Attempt to update an annunciator control after it has been disposed.");

        bool targetState = cadenceState && !Mute;
        Color targetColor = targetState ? ActiveColor : InactiveColor;

        if (ForeColor != targetColor)
        {
            ForeColor = targetColor;
            Invalidate();
            Update();
        }
    }

    /// <summary>
    ///     Releases all resources used by the <see cref="Component" /> .
    /// </summary>
    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Releases the unmanaged resources used by the <see cref="Label" /> and optionally
    ///     releases the managed resources.
    /// </summary>
    /// <param name="fromUserCode">
    ///     <see langword="true" /> to release both managed and unmanaged resources;
    ///     <see langword="false" /> to release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool fromUserCode)
    {
        if (!disposed)
        {
            if (fromUserCode)
                StopCadenceUpdates();
            disposed = true;
        }
        base.Dispose(fromUserCode); // Let the underlying control class clean itself up.
    }

    /// <summary>
    ///     Handles the ParentChanged event of the Annunciator control. Changes the control's
    ///     background colour to blend in with the parent control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void AnunciatorParentChanged(object sender, EventArgs e)
    {
        if (Parent != null)
            BackColor = Parent.BackColor;
        else
            BackColor = Color.FromArgb(64, 0, 0);
    }

    /// <summary>
    ///     Registers this control with the <see cref="CadencedControlUpdater" /> so that it
    ///     will receive cadence updates.
    /// </summary>
    private void StartCadenceUpdates()
    {
        CadencedControlUpdater.Instance.Add(this);
    }

    /// <summary>
    ///     Unregisters this control from the <see cref="CadencedControlUpdater" /> so that it
    ///     will no longer receive cadence updates.
    /// </summary>
    private void StopCadenceUpdates()
    {
        CadencedControlUpdater.Instance.Remove(this);
    }
}