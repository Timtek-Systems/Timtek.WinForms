// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: AnnunciatorPanel.cs  Last modified: 2019-09-21@02:42 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

namespace Timtek.WinForms;

/// <summary>
///     A panel control for grouping and arranging <see cref="FlowLayoutPanel" /> controls. This control inherits most
///     of its behaviour from the <see cref="FlowLayoutPanel" />base class, but provides some defaults that are
///     appropriate for use with ASCOM.
/// </summary>
public sealed class AnnunciatorPanel : FlowLayoutPanel
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AnnunciatorPanel" /> class.
    /// </summary>
    public AnnunciatorPanel() => BackColor = Color.FromArgb(64, 0, 0);

    /// <summary>
    ///     Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.
    /// </summary>
    public new void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}