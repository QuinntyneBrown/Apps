// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Core;

/// <summary>
/// Represents a weekly review.
/// </summary>
public class WeeklyReview
{
    /// <summary>
    /// Gets or sets the unique identifier for the weekly review.
    /// </summary>
    public Guid WeeklyReviewId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this review.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the week start date.
    /// </summary>
    public DateTime WeekStartDate { get; set; }

    /// <summary>
    /// Gets or sets the week end date.
    /// </summary>
    public DateTime WeekEndDate { get; set; }

    /// <summary>
    /// Gets or sets the overall week rating (1-10).
    /// </summary>
    public int? OverallRating { get; set; }

    /// <summary>
    /// Gets or sets reflections or notes for the week.
    /// </summary>
    public string? Reflections { get; set; }

    /// <summary>
    /// Gets or sets lessons learned during the week.
    /// </summary>
    public string? LessonsLearned { get; set; }

    /// <summary>
    /// Gets or sets gratitude items.
    /// </summary>
    public string? Gratitude { get; set; }

    /// <summary>
    /// Gets or sets improvement areas.
    /// </summary>
    public string? ImprovementAreas { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the review is completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of accomplishments for this review.
    /// </summary>
    public ICollection<Accomplishment> Accomplishments { get; set; } = new List<Accomplishment>();

    /// <summary>
    /// Gets or sets the collection of challenges for this review.
    /// </summary>
    public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();

    /// <summary>
    /// Gets or sets the collection of priorities for the next week.
    /// </summary>
    public ICollection<WeeklyPriority> Priorities { get; set; } = new List<WeeklyPriority>();

    /// <summary>
    /// Marks the review as completed.
    /// </summary>
    public void CompleteReview()
    {
        IsCompleted = true;
    }

    /// <summary>
    /// Gets the week number of the year.
    /// </summary>
    /// <returns>The week number.</returns>
    public int GetWeekNumber()
    {
        return System.Globalization.ISOWeek.GetWeekOfYear(WeekStartDate);
    }
}
