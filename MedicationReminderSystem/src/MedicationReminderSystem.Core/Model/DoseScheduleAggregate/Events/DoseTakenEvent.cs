// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Event raised when a dose is taken.
/// </summary>
public record DoseTakenEvent
{
    /// <summary>
    /// Gets the dose schedule ID.
    /// </summary>
    public Guid DoseScheduleId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the medication ID.
    /// </summary>
    public Guid MedicationId { get; init; }

    /// <summary>
    /// Gets the timestamp when the dose was taken.
    /// </summary>
    public DateTime TakenAt { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
