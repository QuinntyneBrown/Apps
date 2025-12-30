// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Core;

/// <summary>
/// Represents an accomplishment during a week.
/// </summary>
public class Accomplishment
{
    /// <summary>
    /// Gets or sets the unique identifier for the accomplishment.
    /// </summary>
    public Guid AccomplishmentId { get; set; }

    /// <summary>
    /// Gets or sets the review ID this accomplishment belongs to.
    /// </summary>
    public Guid WeeklyReviewId { get; set; }

    /// <summary>
    /// Gets or sets the accomplishment title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the accomplishment description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category or area of life.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the impact level (1-10).
    /// </summary>
    public int? ImpactLevel { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the review.
    /// </summary>
    public WeeklyReview? Review { get; set; }
}
