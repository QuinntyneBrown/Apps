// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Represents a market salary comparison.
/// </summary>
public class MarketComparison
{
    /// <summary>
    /// Gets or sets the unique identifier for the market comparison.
    /// </summary>
    public Guid MarketComparisonId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this comparison.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the job title being compared.
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location/market.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the experience level.
    /// </summary>
    public string? ExperienceLevel { get; set; }

    /// <summary>
    /// Gets or sets the minimum salary in the market.
    /// </summary>
    public decimal? MinSalary { get; set; }

    /// <summary>
    /// Gets or sets the maximum salary in the market.
    /// </summary>
    public decimal? MaxSalary { get; set; }

    /// <summary>
    /// Gets or sets the median/average salary.
    /// </summary>
    public decimal? MedianSalary { get; set; }

    /// <summary>
    /// Gets or sets the data source (e.g., Glassdoor, Payscale, LinkedIn).
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// Gets or sets the date of the comparison.
    /// </summary>
    public DateTime ComparisonDate { get; set; } = DateTime.UtcNow;

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
    /// Calculates the midpoint salary.
    /// </summary>
    /// <returns>The midpoint salary.</returns>
    public decimal? GetMidpoint()
    {
        if (MinSalary.HasValue && MaxSalary.HasValue)
        {
            return (MinSalary.Value + MaxSalary.Value) / 2;
        }
        return MedianSalary;
    }
}
