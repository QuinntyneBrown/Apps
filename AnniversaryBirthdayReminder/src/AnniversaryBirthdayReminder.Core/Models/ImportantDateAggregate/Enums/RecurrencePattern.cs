// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the recurrence pattern for important dates.
/// </summary>
public enum RecurrencePattern
{
    /// <summary>
    /// The date occurs once per year.
    /// </summary>
    Annual = 0,

    /// <summary>
    /// The date has a custom recurrence pattern.
    /// </summary>
    Custom = 1,
}
