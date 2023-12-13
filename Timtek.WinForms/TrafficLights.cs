// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: TrafficLights.cs  Last modified: 2019-09-21@02:42 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

namespace Timtek.WinForms;

/// <summary>
///     The TrafficLight enumeration may be used in any situation where a Normal/Warning/Error status indication is
///     needed.
/// </summary>
public enum TrafficLight
{
    /// <summary>
    ///     Green traffic light represents a good or normal status.
    /// </summary>
    Green,

    /// <summary>
    ///     Yellow traffic light represents a warning condition, which does not necessarily prevent continued
    ///     operation but which merits further investigation.
    /// </summary>
    Yellow,

    /// <summary>
    ///     Red traffic light represents an error condition or a situation that prevents further progress.
    /// </summary>
    Red
}