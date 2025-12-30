// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Core;

/// <summary>
/// Represents a challenge encountered during a week.
/// </summary>
public class Challenge
{
    /// <summary>
    /// Gets or sets the unique identifier for the challenge.
    /// </summary>
    public Guid ChallengeId { get; set; }

    /// <summary>
    /// Gets or sets the review ID this challenge belongs to.
    /// </summary>
    public Guid WeeklyReviewId { get; set; }

    /// <summary>
    /// Gets or sets the challenge title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the challenge description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets how the challenge was addressed.
    /// </summary>
    public string? Resolution { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the challenge is resolved.
    /// </summary>
    public bool IsResolved { get; set; }

    /// <summary>
    /// Gets or sets the lessons learned from the challenge.
    /// </summary>
    public string? LessonsLearned { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the review.
    /// </summary>
    public WeeklyReview? Review { get; set; }

    /// <summary>
    /// Marks the challenge as resolved.
    /// </summary>
    /// <param name="resolution">The resolution description.</param>
    public void Resolve(string resolution)
    {
        IsResolved = true;
        Resolution = resolution;
    }
}
