// This file is part of the TA.WinForms.Controls project
// Copyright © 2016-2019 Tigra Astronomy, all rights reserved.
// File: CadencePattern.cs  Last modified: 2019-09-21@02:42 by Tim Long
// Licensed under the Tigra MIT License, see https://tigra.mit-license.org/

namespace Timtek.WinForms;

/// <summary>
///     Cadence patterns for blinking LEDs. Cadences are based on 32-bit unsigned integers, such that the ordinal
///     value of each item represents a bit mask that can be used directly in an update routine.
/// </summary>
public enum CadencePattern : uint
{
    /// <summary>
    ///     Permanently off,
    ///     appropriate for indication of a non-critical inactive state.
    /// </summary>
    SteadyOff = 0x00000000,

    /// <summary>
    ///     Permanently on,
    ///     appropriate for indication of a non-critical active state.
    /// </summary>
    SteadyOn = 0xFFFFFFFF,

    /// <summary>
    ///     Fast blink,
    ///     appropriate for indicating a state of hightened but non-critical alert.
    ///     Usage example: during movement of robotic equipment.
    /// </summary>
    BlinkFast = 0xF0F0F0F0,

    /// <summary>
    ///     Slow blink,
    ///     appropriate for non-critical persistent conditions.
    ///     Usage example: image exposure in progress.
    /// </summary>
    BlinkSlow = 0xFF00FF00,

    /// <summary>
    ///     Very fast blink,
    ///     appropriate for drawing attention to urgent conditions that require operator intervention.
    ///     Usage example: Rain detected
    /// </summary>
    BlinkAlarm = 0xAAAAAAAA,

    /// <summary>
    ///     Strobe is mostly off but with an occasional short blip on,
    ///     appropriate for indicating non-critical ongoing steady idle state.
    /// </summary>
    Strobe = 0x00000001,

    /// <summary>
    ///     Wink (mostly on with occasional short wink-off),
    ///     appropriate for indicating non-critical ongoing steady active state.
    /// </summary>
    Wink = 0xFFFFFFFE,
    /// <summary>
    /// One short pulse per cadence cycle
    /// </summary>
    Pulse1 = 0x00000003,
    /// <summary>
    /// Two short pulses per cadence cycle
    /// </summary>
    Pulse2 = 0x00000033,
    /// <summary>
    /// Three short pulses per cadence cycle
    /// </summary>
    Pulse3 = 0x00000333,
    /// <summary>
    /// Four short pulses per cadence cycle
    /// </summary>
    Pulse4 = 0x00003333,
    /// <summary>
    /// A double pulse reminiscent of a heart beat
    /// </summary>
    Heartbeat = 0x0000000A,
    /// <summary>
    /// Like a heart beat, but syncopated
    /// </summary>
    Offbeat = 0x000A0000,
}