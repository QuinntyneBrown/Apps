// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CouplesGoalTracker.Core;

/// <summary>
/// Represents a progress entry for a goal.
/// </summary>
public class Progress
{
    /// <summary>
    /// Gets or sets the unique identifier for the progress entry.
    /// </summary>
    public Guid ProgressId { get; set; }

    /// <summary>
    /// Gets or sets the goal ID this progress entry belongs to.
    /// </summary>
    public Guid GoalId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this progress entry.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the date of the progress update.
    /// </summary>
    public DateTime ProgressDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the notes or description of the progress.
    /// </summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the completion percentage at the time of this update (0-100).
    /// </summary>
    public double CompletionPercentage { get; set; }

    /// <summary>
    /// Gets or sets the effort hours spent on this progress update.
    /// </summary>
    public decimal? EffortHours { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this was a significant milestone.
    /// </summary>
    public bool IsSignificant { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the goal this progress entry belongs to.
    /// </summary>
    public Goal? Goal { get; set; }

    /// <summary>
    /// Marks this progress entry as significant.
    /// </summary>
    public void MarkAsSignificant()
    {
        IsSignificant = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates the completion percentage.
    /// </summary>
    /// <param name="percentage">The new percentage (0-100).</param>
    public void UpdatePercentage(double percentage)
    {
        if (percentage >= 0 && percentage <= 100)
        {
            CompletionPercentage = percentage;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
