// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Core;

/// <summary>
/// Represents a priority for the upcoming week.
/// </summary>
public class WeeklyPriority
{
    /// <summary>
    /// Gets or sets the unique identifier for the priority.
    /// </summary>
    public Guid WeeklyPriorityId { get; set; }

    /// <summary>
    /// Gets or sets the review ID this priority belongs to.
    /// </summary>
    public Guid WeeklyReviewId { get; set; }

    /// <summary>
    /// Gets or sets the priority title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the priority description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the priority level.
    /// </summary>
    public PriorityLevel Level { get; set; }

    /// <summary>
    /// Gets or sets the category or area of focus.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the target completion date.
    /// </summary>
    public DateTime? TargetDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the priority is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the review.
    /// </summary>
    public WeeklyReview? Review { get; set; }

    /// <summary>
    /// Marks the priority as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
    }
}
