// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FocusSessionTracker.Core;

/// <summary>
/// Represents analytics for focus sessions over a period.
/// </summary>
public class SessionAnalytics
{
    /// <summary>
    /// Gets or sets the unique identifier for the analytics record.
    /// </summary>
    public Guid SessionAnalyticsId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the period start date.
    /// </summary>
    public DateTime PeriodStartDate { get; set; }

    /// <summary>
    /// Gets or sets the period end date.
    /// </summary>
    public DateTime PeriodEndDate { get; set; }

    /// <summary>
    /// Gets or sets the total sessions count.
    /// </summary>
    public int TotalSessions { get; set; }

    /// <summary>
    /// Gets or sets the total focus time in minutes.
    /// </summary>
    public double TotalFocusMinutes { get; set; }

    /// <summary>
    /// Gets or sets the average focus score.
    /// </summary>
    public double? AverageFocusScore { get; set; }

    /// <summary>
    /// Gets or sets the total distractions count.
    /// </summary>
    public int TotalDistractions { get; set; }

    /// <summary>
    /// Gets or sets the completion rate (percentage).
    /// </summary>
    public double CompletionRate { get; set; }

    /// <summary>
    /// Gets or sets the most productive session type.
    /// </summary>
    public SessionType? MostProductiveSessionType { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the average session duration in minutes.
    /// </summary>
    /// <returns>The average duration.</returns>
    public double GetAverageSessionDuration()
    {
        if (TotalSessions == 0)
        {
            return 0;
        }

        return TotalFocusMinutes / TotalSessions;
    }

    /// <summary>
    /// Calculates the distractions per session average.
    /// </summary>
    /// <returns>The average distractions per session.</returns>
    public double GetAverageDistractions()
    {
        if (TotalSessions == 0)
        {
            return 0;
        }

        return (double)TotalDistractions / TotalSessions;
    }
}
