// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PerformanceReviewPrepTool.Core;

/// <summary>
/// Represents feedback received during a review period.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Gets or sets the unique identifier for the feedback.
    /// </summary>
    public Guid FeedbackId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this feedback.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the review period ID.
    /// </summary>
    public Guid ReviewPeriodId { get; set; }

    /// <summary>
    /// Gets or sets the source of the feedback (manager, peer, client, etc.).
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the feedback content.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date received.
    /// </summary>
    public DateTime ReceivedDate { get; set; }

    /// <summary>
    /// Gets or sets the type (Positive, Constructive, Neutral).
    /// </summary>
    public string? FeedbackType { get; set; }

    /// <summary>
    /// Gets or sets the category or topic.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is key feedback.
    /// </summary>
    public bool IsKeyFeedback { get; set; }

    /// <summary>
    /// Gets or sets optional notes or action items.
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
    /// Gets or sets the navigation property to the review period.
    /// </summary>
    public ReviewPeriod? ReviewPeriod { get; set; }

    /// <summary>
    /// Marks the feedback as key feedback.
    /// </summary>
    public void MarkAsKey()
    {
        IsKeyFeedback = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
