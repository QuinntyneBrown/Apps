// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Api.Features.SessionAnalytics;

/// <summary>
/// Data transfer object for session analytics.
/// </summary>
public class SessionAnalyticsDto
{
    /// <summary>
    /// Gets or sets the unique identifier.
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
    /// Gets or sets the completion rate.
    /// </summary>
    public double CompletionRate { get; set; }

    /// <summary>
    /// Gets or sets the most productive session type.
    /// </summary>
    public SessionType? MostProductiveSessionType { get; set; }

    /// <summary>
    /// Gets or sets the average session duration.
    /// </summary>
    public double AverageSessionDuration { get; set; }

    /// <summary>
    /// Gets or sets the average distractions per session.
    /// </summary>
    public double AverageDistractions { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
