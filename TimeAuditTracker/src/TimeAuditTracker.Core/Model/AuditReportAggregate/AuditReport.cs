// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Represents a time audit report for a specific period.
/// </summary>
public class AuditReport
{
    /// <summary>
    /// Gets or sets the unique identifier for the audit report.
    /// </summary>
    public Guid AuditReportId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this report.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the report title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the audit period.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the audit period.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the total tracked hours.
    /// </summary>
    public double TotalTrackedHours { get; set; }

    /// <summary>
    /// Gets or sets the productive hours.
    /// </summary>
    public double ProductiveHours { get; set; }

    /// <summary>
    /// Gets or sets the summary of findings.
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Gets or sets the insights gained from the audit.
    /// </summary>
    public string? Insights { get; set; }

    /// <summary>
    /// Gets or sets the recommendations based on the audit.
    /// </summary>
    public string? Recommendations { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the productivity percentage.
    /// </summary>
    /// <returns>The productivity percentage.</returns>
    public double GetProductivityPercentage()
    {
        if (TotalTrackedHours == 0)
        {
            return 0;
        }

        return (ProductiveHours / TotalTrackedHours) * 100;
    }

    /// <summary>
    /// Checks if the report covers the current week.
    /// </summary>
    /// <returns>True if it covers the current week; otherwise, false.</returns>
    public bool IsCurrentWeek()
    {
        var now = DateTime.UtcNow;
        return StartDate.Date <= now.Date && EndDate.Date >= now.Date;
    }

    /// <summary>
    /// Gets the duration of the audit period in days.
    /// </summary>
    /// <returns>The duration in days.</returns>
    public int GetPeriodDays()
    {
        return (EndDate.Date - StartDate.Date).Days + 1;
    }
}
