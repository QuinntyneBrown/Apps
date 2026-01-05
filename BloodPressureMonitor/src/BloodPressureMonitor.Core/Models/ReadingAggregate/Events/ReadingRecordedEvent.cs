// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core;

/// <summary>
/// Event raised when a blood pressure reading is recorded.
/// </summary>
public record ReadingRecordedEvent
{
    /// <summary>
    /// Gets the reading ID.
    /// </summary>
    public Guid ReadingId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the systolic pressure.
    /// </summary>
    public int Systolic { get; init; }

    /// <summary>
    /// Gets the diastolic pressure.
    /// </summary>
    public int Diastolic { get; init; }

    /// <summary>
    /// Gets the blood pressure category.
    /// </summary>
    public BloodPressureCategory Category { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
