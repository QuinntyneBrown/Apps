// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Core;

/// <summary>
/// Represents a date night idea.
/// </summary>
public class DateIdea
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

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
    /// Gets or sets the season suitability (e.g., "Summer", "Winter", "Year-round").
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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of experiences for this date idea.
    /// </summary>
    public ICollection<Experience> Experiences { get; set; } = new List<Experience>();

    /// <summary>
    /// Gets or sets the collection of ratings for this date idea.
    /// </summary>
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    /// <summary>
    /// Marks the date idea as favorite.
    /// </summary>
    public void MarkAsFavorite()
    {
        IsFavorite = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the date idea as tried.
    /// </summary>
    public void MarkAsTried()
    {
        HasBeenTried = true;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the average rating for this date idea.
    /// </summary>
    /// <returns>The average rating, or null if no ratings exist.</returns>
    public double? GetAverageRating()
    {
        if (Ratings == null || !Ratings.Any())
        {
            return null;
        }

        return Ratings.Average(r => r.Score);
    }
}
