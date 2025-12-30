// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Represents data synchronized from wearable devices.
/// </summary>
public class WearableData
{
    /// <summary>
    /// Gets or sets the unique identifier for the wearable data.
    /// </summary>
    public Guid WearableDataId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this data.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the wearable device (e.g., Fitbit, Apple Watch).
    /// </summary>
    public string DeviceName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of data (e.g., Steps, Calories, Distance).
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data value.
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets or sets the unit of measurement.
    /// </summary>
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the data was recorded.
    /// </summary>
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the timestamp when the data was synced.
    /// </summary>
    public DateTime SyncedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets additional metadata in JSON format.
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the data is from today.
    /// </summary>
    /// <returns>True if from today; otherwise, false.</returns>
    public bool IsFromToday()
    {
        return RecordedAt.Date == DateTime.UtcNow.Date;
    }

    /// <summary>
    /// Gets the age of the data in hours.
    /// </summary>
    /// <returns>The age in hours.</returns>
    public double GetDataAgeInHours()
    {
        return (DateTime.UtcNow - RecordedAt).TotalHours;
    }
}
