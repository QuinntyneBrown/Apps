// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core;

/// <summary>
/// Represents the delivery status of a letter.
/// </summary>
public enum DeliveryStatus
{
    /// <summary>
    /// Letter is pending delivery.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Letter has been delivered.
    /// </summary>
    Delivered = 1,

    /// <summary>
    /// Letter delivery was cancelled.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// Letter delivery failed.
    /// </summary>
    Failed = 3,
}
