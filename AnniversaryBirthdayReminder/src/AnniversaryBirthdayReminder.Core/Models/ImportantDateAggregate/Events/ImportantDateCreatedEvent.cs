// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Core;

/// <summary>
/// Event raised when a new important date is created.
/// </summary>
public record ImportantDateCreatedEvent
{
    /// <summary>
    /// Gets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the person name.
    /// </summary>
    public string PersonName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the date type.
    /// </summary>
    public DateType DateType { get; init; }

    /// <summary>
    /// Gets the date value.
    /// </summary>
    public DateTime DateValue { get; init; }

    /// <summary>
    /// Gets the recurrence pattern.
    /// </summary>
    public RecurrencePattern RecurrencePattern { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
