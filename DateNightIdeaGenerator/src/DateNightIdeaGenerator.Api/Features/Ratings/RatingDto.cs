// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Api.Features.Ratings;

/// <summary>
/// Data transfer object for Rating.
/// </summary>
public class RatingDto
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
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
