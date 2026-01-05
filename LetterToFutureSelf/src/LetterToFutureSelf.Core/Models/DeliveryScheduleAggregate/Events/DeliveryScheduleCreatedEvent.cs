// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Core;

/// <summary>
/// Event raised when a new delivery schedule is created.
/// </summary>
public record DeliveryScheduleCreatedEvent
{
    /// <summary>
    /// Gets the delivery schedule ID.
    /// </summary>
    public Guid DeliveryScheduleId { get; init; }

    /// <summary>
    /// Gets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

    /// <summary>
    /// Gets the scheduled date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; init; }

    /// <summary>
    /// Gets the delivery method.
    /// </summary>
    public string DeliveryMethod { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
