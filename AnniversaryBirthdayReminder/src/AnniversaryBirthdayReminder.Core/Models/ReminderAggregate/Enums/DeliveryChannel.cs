// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the delivery channel for reminders.
/// </summary>
public enum DeliveryChannel
{
    /// <summary>
    /// Deliver reminder via email.
    /// </summary>
    Email = 0,

    /// <summary>
    /// Deliver reminder via SMS.
    /// </summary>
    Sms = 1,

    /// <summary>
    /// Deliver reminder via push notification.
    /// </summary>
    Push = 2,
}
