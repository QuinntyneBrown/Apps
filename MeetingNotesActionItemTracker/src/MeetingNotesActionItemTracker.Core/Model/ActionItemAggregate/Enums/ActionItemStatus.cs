// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MeetingNotesActionItemTracker.Core;

/// <summary>
/// Represents the status of an action item.
/// </summary>
public enum ActionItemStatus
{
    /// <summary>
    /// Not started.
    /// </summary>
    NotStarted = 0,

    /// <summary>
    /// In progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Completed.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Cancelled.
    /// </summary>
    Cancelled = 3,

    /// <summary>
    /// On hold.
    /// </summary>
    OnHold = 4,
}
