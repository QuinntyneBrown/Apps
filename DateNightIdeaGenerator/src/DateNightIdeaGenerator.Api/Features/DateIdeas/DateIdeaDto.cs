// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;

namespace DateNightIdeaGenerator.Api.Features.DateIdeas;

/// <summary>
/// Data transfer object for DateIdea.
/// </summary>
public class DateIdeaDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the date idea.
    /// </summary>
    public Guid DateIdeaId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this idea.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the title of the date idea.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the date idea.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the date idea.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the budget range for this date idea.
    /// </summary>
    public BudgetRange BudgetRange { get; set; }

    /// <summary>
    /// Gets or sets the location for the date.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets the estimated duration in minutes.
    /// </summary>
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the season suitability.
    /// </summary>
    public string? Season { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this idea is a favorite.
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this idea has been tried.
    /// </summary>
    public bool HasBeenTried { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the average rating.
    /// </summary>
    public double? AverageRating { get; set; }
}
