// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core;

/// <summary>
/// Event raised when an audit report is generated.
/// </summary>
public record AuditReportGeneratedEvent
{
    /// <summary>
    /// Gets the audit report ID.
    /// </summary>
    public Guid AuditReportId { get; init; }

    /// <summary>
    /// Gets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the report title.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets the end date.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets the total tracked hours.
    /// </summary>
    public double TotalTrackedHours { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
