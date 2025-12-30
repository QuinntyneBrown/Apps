// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Represents a rating for a date idea or experience.
/// </summary>
public class Rating
{
    /// <summary>
    /// Gets or sets the unique identifier for the rating.
    /// </summary>
    public Guid RatingId { get; set; }

    /// <summary>
    /// Gets or sets the date idea ID this rating is associated with.
    /// </summary>
    public Guid? DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the experience ID this rating is associated with.
    /// </summary>
    public Guid? ExperienceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this rating.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the rating score (1-5).
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Gets or sets the review text.
    /// </summary>
    public string? Review { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date idea this rating belongs to.
    /// </summary>
    public DateIdea? DateIdea { get; set; }

    /// <summary>
    /// Gets or sets the experience this rating belongs to.
    /// </summary>
    public Experience? Experience { get; set; }

    /// <summary>
    /// Validates that the score is within the valid range.
    /// </summary>
    /// <returns>True if the score is valid; otherwise, false.</returns>
    public bool IsValidScore()
    {
        return Score >= 1 && Score <= 5;
    }

    /// <summary>
    /// Updates the rating score.
    /// </summary>
    /// <param name="newScore">The new score.</param>
    public void UpdateScore(int newScore)
    {
        if (newScore >= 1 && newScore <= 5)
        {
            Score = newScore;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
