// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the type of an important date.
/// </summary>
public enum DateType
{
    /// <summary>
    /// A birthday celebration.
    /// </summary>
    Birthday = 0,

    /// <summary>
    /// An anniversary celebration.
    /// </summary>
    Anniversary = 1,

    /// <summary>
    /// A custom or other type of celebration.
    /// </summary>
    Custom = 2,
}
