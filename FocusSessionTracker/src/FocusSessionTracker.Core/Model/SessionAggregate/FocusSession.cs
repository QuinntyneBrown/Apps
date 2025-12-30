// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Represents a focus session.
/// </summary>
public class FocusSession
{
    /// <summary>
    /// Gets or sets the unique identifier for the focus session.
    /// </summary>
    public Guid FocusSessionId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this session.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the session type.
    /// </summary>
    public SessionType SessionType { get; set; }

    /// <summary>
    /// Gets or sets the session name or task description.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the planned duration in minutes.
    /// </summary>
    public int PlannedDurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the start time of the session.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the session.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the focus score (1-10).
    /// </summary>
    public int? FocusScore { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the session was completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of distractions during this session.
    /// </summary>
    public ICollection<Distraction> Distractions { get; set; } = new List<Distraction>();

    /// <summary>
    /// Calculates the actual duration in minutes.
    /// </summary>
    /// <returns>The actual duration, or null if not ended.</returns>
    public double? GetActualDurationMinutes()
    {
        if (EndTime == null)
        {
            return null;
        }

        return (EndTime.Value - StartTime).TotalMinutes;
    }

    /// <summary>
    /// Ends the focus session.
    /// </summary>
    /// <param name="endTime">The end time.</param>
    public void EndSession(DateTime endTime)
    {
        if (endTime <= StartTime)
        {
            throw new InvalidOperationException("End time must be after start time.");
        }

        EndTime = endTime;
        IsCompleted = true;
    }

    /// <summary>
    /// Gets the count of distractions.
    /// </summary>
    /// <returns>The number of distractions.</returns>
    public int GetDistractionCount()
    {
        return Distractions.Count;
    }
}
