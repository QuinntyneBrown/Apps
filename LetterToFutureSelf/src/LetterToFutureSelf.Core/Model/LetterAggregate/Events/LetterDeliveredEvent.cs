// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core;

/// <summary>
/// Event raised when a letter is delivered.
/// </summary>
public record LetterDeliveredEvent
{
    /// <summary>
    /// Gets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the delivery date.
    /// </summary>
    public DateTime DeliveryDate { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
