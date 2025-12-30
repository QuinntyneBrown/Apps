// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Represents a tracked block of time spent on an activity.
/// </summary>
public class TimeBlock
{
    /// <summary>
    /// Gets or sets the unique identifier for the time block.
    /// </summary>
    public Guid TimeBlockId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this time block.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the activity category.
    /// </summary>
    public ActivityCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the activity description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start time of the activity.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the activity.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the activity.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets optional tags for categorization.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this time block is considered productive.
    /// </summary>
    public bool IsProductive { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the duration of this time block in minutes.
    /// </summary>
    /// <returns>The duration in minutes, or null if not ended.</returns>
    public double? GetDurationInMinutes()
    {
        if (EndTime == null)
        {
            return null;
        }

        return (EndTime.Value - StartTime).TotalMinutes;
    }

    /// <summary>
    /// Ends the current time block.
    /// </summary>
    /// <param name="endTime">The end time.</param>
    public void EndActivity(DateTime endTime)
    {
        if (endTime <= StartTime)
        {
            throw new InvalidOperationException("End time must be after start time.");
        }

        EndTime = endTime;
    }

    /// <summary>
    /// Checks if this time block is currently active (not ended).
    /// </summary>
    /// <returns>True if active; otherwise, false.</returns>
    public bool IsActive()
    {
        return EndTime == null;
    }
}
