// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the status of a reminder.
/// </summary>
public enum ReminderStatus
{
    /// <summary>
    /// The reminder is scheduled for delivery.
    /// </summary>
    Scheduled = 0,

    /// <summary>
    /// The reminder has been sent.
    /// </summary>
    Sent = 1,

    /// <summary>
    /// The reminder has been dismissed by the user.
    /// </summary>
    Dismissed = 2,

    /// <summary>
    /// The reminder has been snoozed by the user.
    /// </summary>
    Snoozed = 3,
}
