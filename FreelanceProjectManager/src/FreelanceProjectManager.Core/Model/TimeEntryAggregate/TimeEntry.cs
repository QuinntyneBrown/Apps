// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Represents a time entry for a project.
/// </summary>
public class TimeEntry
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
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

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
    public bool IsBillable { get; set; } = true;

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
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the project.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Marks the time entry as invoiced.
    /// </summary>
    /// <param name="invoiceId">The invoice ID.</param>
    public void MarkAsInvoiced(Guid invoiceId)
    {
        IsInvoiced = true;
        InvoiceId = invoiceId;
        UpdatedAt = DateTime.UtcNow;
    }
}
