// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: ICadencedControl.cs  Last modified: 2019-09-21@02:42 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

namespace Timtek.WinForms;

/// <summary>
///     Defines the members necessary for a control to register and be managed by the
///     <see cref="CadencedControlUpdater" /> singleton.
/// </summary>
public interface ICadencedControl
{
    /// <summary>
    ///     Gets or sets the cadence (blink pattern) of the control.
    ///     Different cadence patterns imply different levels of urgency or severity.
    /// </summary>
    /// <value>The cadence pattern.</value>
    CadencePattern Cadence { get; set; }

    /// <summary>
    ///     Updates the control's display.
    ///     <see cref="CadencedControlUpdater" /> always calls this method on the GUI thread so that control updates are
    ///     thread-safe.
    /// </summary>
    void CadenceUpdate(bool cadenceState);
}