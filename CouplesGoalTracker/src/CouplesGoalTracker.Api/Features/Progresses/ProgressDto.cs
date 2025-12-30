// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Data transfer object for Progress.
/// </summary>
public class ProgressDto
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
    public DateTime ProgressDate { get; set; }

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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Creates a ProgressDto from a Progress entity.
    /// </summary>
    /// <param name="progress">The progress entity.</param>
    /// <returns>A ProgressDto.</returns>
    public static ProgressDto FromProgress(Progress progress)
    {
        return new ProgressDto
        {
            ProgressId = progress.ProgressId,
            GoalId = progress.GoalId,
            UserId = progress.UserId,
            ProgressDate = progress.ProgressDate,
            Notes = progress.Notes,
            CompletionPercentage = progress.CompletionPercentage,
            EffortHours = progress.EffortHours,
            IsSignificant = progress.IsSignificant,
            CreatedAt = progress.CreatedAt,
            UpdatedAt = progress.UpdatedAt,
        };
    }
}
