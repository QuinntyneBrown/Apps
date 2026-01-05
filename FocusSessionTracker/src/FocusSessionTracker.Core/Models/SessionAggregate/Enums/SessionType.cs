// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Represents the type of focus session.
/// </summary>
public enum SessionType
{
    /// <summary>
    /// Deep work session.
    /// </summary>
    DeepWork = 0,

    /// <summary>
    /// Pomodoro session (25 minutes).
    /// </summary>
    Pomodoro = 1,

    /// <summary>
    /// Study session.
    /// </summary>
    Study = 2,

    /// <summary>
    /// Creative work session.
    /// </summary>
    Creative = 3,

    /// <summary>
    /// Meeting or collaboration session.
    /// </summary>
    Meeting = 4,

    /// <summary>
    /// Learning or training session.
    /// </summary>
    Learning = 5,

    /// <summary>
    /// Planning session.
    /// </summary>
    Planning = 6,

    /// <summary>
    /// Other custom session type.
    /// </summary>
    Other = 7,
}
