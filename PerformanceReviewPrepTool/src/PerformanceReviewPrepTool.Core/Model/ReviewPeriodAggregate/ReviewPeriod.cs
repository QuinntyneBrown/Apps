// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents a performance review period.
/// </summary>
public class ReviewPeriod
{
    /// <summary>
    /// Gets or sets the unique identifier for the review period.
    /// </summary>
    public Guid ReviewPeriodId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this review period.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the review period.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the review due date.
    /// </summary>
    public DateTime? ReviewDueDate { get; set; }

    /// <summary>
    /// Gets or sets the reviewer name (manager).
    /// </summary>
    public string? ReviewerName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the review is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the completion date.
    /// </summary>
    public DateTime? CompletedDate { get; set; }

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of achievements for this period.
    /// </summary>
    public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    /// <summary>
    /// Gets or sets the collection of goals for this period.
    /// </summary>
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();

    /// <summary>
    /// Gets or sets the collection of feedback for this period.
    /// </summary>
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    /// <summary>
    /// Marks the review period as completed.
    /// </summary>
    public void Complete()
    {
        IsCompleted = true;
        CompletedDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
