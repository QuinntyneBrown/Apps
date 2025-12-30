// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Represents a health trend analysis over time.
/// </summary>
public class HealthTrend
{
    /// <summary>
    /// Gets or sets the unique identifier for the health trend.
    /// </summary>
    public Guid HealthTrendId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this trend.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the metric being tracked (e.g., Weight, Blood Pressure, Steps).
    /// </summary>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the trend period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the trend period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the average value during the period.
    /// </summary>
    public double AverageValue { get; set; }

    /// <summary>
    /// Gets or sets the minimum value during the period.
    /// </summary>
    public double MinValue { get; set; }

    /// <summary>
    /// Gets or sets the maximum value during the period.
    /// </summary>
    public double MaxValue { get; set; }

    /// <summary>
    /// Gets or sets the trend direction (e.g., Increasing, Decreasing, Stable).
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the percentage change over the period.
    /// </summary>
    public double PercentageChange { get; set; }

    /// <summary>
    /// Gets or sets optional insights or recommendations.
    /// </summary>
    public string? Insights { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the trend shows improvement.
    /// </summary>
    /// <param name="isHigherBetter">Indicates whether higher values are better.</param>
    /// <returns>True if improving; otherwise, false.</returns>
    public bool IsImproving(bool isHigherBetter)
    {
        if (isHigherBetter)
        {
            return TrendDirection.Equals("Increasing", StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            return TrendDirection.Equals("Decreasing", StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// Gets the duration of the trend period in days.
    /// </summary>
    /// <returns>The duration in days.</returns>
    public int GetPeriodDuration()
    {
        return (EndDate - StartDate).Days;
    }
}
