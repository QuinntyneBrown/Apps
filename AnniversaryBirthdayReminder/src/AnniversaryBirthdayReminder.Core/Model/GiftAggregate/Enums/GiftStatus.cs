// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Represents the status of a gift.
/// </summary>
public enum GiftStatus
{
    /// <summary>
    /// The gift is just an idea.
    /// </summary>
    Idea = 0,

    /// <summary>
    /// The gift has been purchased.
    /// </summary>
    Purchased = 1,

    /// <summary>
    /// The gift has been delivered to the recipient.
    /// </summary>
    Delivered = 2,
}
