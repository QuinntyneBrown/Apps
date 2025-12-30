// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core;

/// <summary>
/// Represents a blood pressure trend analysis over time.
/// </summary>
public class Trend
{
    /// <summary>
    /// Gets or sets the unique identifier for the trend.
    /// </summary>
    public Guid TrendId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this trend.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the start date of the trend period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the trend period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the average systolic pressure.
    /// </summary>
    public decimal AverageSystolic { get; set; }

    /// <summary>
    /// Gets or sets the average diastolic pressure.
    /// </summary>
    public decimal AverageDiastolic { get; set; }

    /// <summary>
    /// Gets or sets the highest systolic reading.
    /// </summary>
    public int HighestSystolic { get; set; }

    /// <summary>
    /// Gets or sets the highest diastolic reading.
    /// </summary>
    public int HighestDiastolic { get; set; }

    /// <summary>
    /// Gets or sets the lowest systolic reading.
    /// </summary>
    public int LowestSystolic { get; set; }

    /// <summary>
    /// Gets or sets the lowest diastolic reading.
    /// </summary>
    public int LowestDiastolic { get; set; }

    /// <summary>
    /// Gets or sets the number of readings analyzed.
    /// </summary>
    public int ReadingCount { get; set; }

    /// <summary>
    /// Gets or sets the trend direction (e.g., Improving, Worsening, Stable).
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets insights or recommendations.
    /// </summary>
    public string? Insights { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if the trend shows improvement.
    /// </summary>
    /// <returns>True if improving; otherwise, false.</returns>
    public bool IsImproving()
    {
        return TrendDirection.Equals("Improving", StringComparison.OrdinalIgnoreCase);
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
