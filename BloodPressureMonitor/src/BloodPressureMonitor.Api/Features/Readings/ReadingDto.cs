// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;

namespace BloodPressureMonitor.Api;

/// <summary>
/// Data transfer object for Reading.
/// </summary>
public record ReadingDto
{
    /// <summary>
    /// Gets or sets the reading ID.
    /// </summary>
    public Guid ReadingId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the systolic pressure.
    /// </summary>
    public int Systolic { get; init; }

    /// <summary>
    /// Gets or sets the diastolic pressure.
    /// </summary>
    public int Diastolic { get; init; }

    /// <summary>
    /// Gets or sets the pulse rate.
    /// </summary>
    public int? Pulse { get; init; }

    /// <summary>
    /// Gets or sets the blood pressure category.
    /// </summary>
    public BloodPressureCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the timestamp when the reading was taken.
    /// </summary>
    public DateTime MeasuredAt { get; init; }

    /// <summary>
    /// Gets or sets the position when measured.
    /// </summary>
    public string? Position { get; init; }

    /// <summary>
    /// Gets or sets the arm used.
    /// </summary>
    public string? Arm { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets a value indicating whether the reading is critical.
    /// </summary>
    public bool IsCritical { get; init; }
}

/// <summary>
/// Extension methods for Reading.
/// </summary>
public static class ReadingExtensions
{
    /// <summary>
    /// Converts a Reading to a DTO.
    /// </summary>
    /// <param name="reading">The reading.</param>
    /// <returns>The DTO.</returns>
    public static ReadingDto ToDto(this Reading reading)
    {
        return new ReadingDto
        {
            ReadingId = reading.ReadingId,
            UserId = reading.UserId,
            Systolic = reading.Systolic,
            Diastolic = reading.Diastolic,
            Pulse = reading.Pulse,
            Category = reading.Category,
            MeasuredAt = reading.MeasuredAt,
            Position = reading.Position,
            Arm = reading.Arm,
            Notes = reading.Notes,
            CreatedAt = reading.CreatedAt,
            IsCritical = reading.IsCritical(),
        };
    }
}
