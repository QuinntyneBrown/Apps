// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the status of a celebration.
/// </summary>
public enum CelebrationStatus
{
    /// <summary>
    /// The celebration was completed.
    /// </summary>
    Completed = 0,

    /// <summary>
    /// The celebration was skipped.
    /// </summary>
    Skipped = 1,
}
