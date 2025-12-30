// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Data transfer object for TimeEntry.
/// </summary>
public class TimeEntryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the time entry.
    /// </summary>
    public Guid TimeEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this time entry.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the date of the work.
    /// </summary>
    public DateTime WorkDate { get; set; }

    /// <summary>
    /// Gets or sets the number of hours worked.
    /// </summary>
    public decimal Hours { get; set; }

    /// <summary>
    /// Gets or sets the description of the work performed.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this entry is billable.
    /// </summary>
    public bool IsBillable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this entry has been invoiced.
    /// </summary>
    public bool IsInvoiced { get; set; }

    /// <summary>
    /// Gets or sets the invoice ID (if invoiced).
    /// </summary>
    public Guid? InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
