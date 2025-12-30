// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FreelanceProjectManager.Core;

/// <summary>
/// Represents an invoice.
/// </summary>
public class Invoice
{
    /// <summary>
    /// Gets or sets the unique identifier for the invoice.
    /// </summary>
    public Guid InvoiceId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this invoice.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the project ID (if applicable).
    /// </summary>
    public Guid? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the invoice date.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// Gets or sets the total amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets the status (Draft, Sent, Paid, Overdue, Cancelled).
    /// </summary>
    public string Status { get; set; } = "Draft";

    /// <summary>
    /// Gets or sets the paid date.
    /// </summary>
    public DateTime? PaidDate { get; set; }

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
    /// Gets or sets the navigation property to the client.
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the project.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// Marks the invoice as paid.
    /// </summary>
    public void MarkAsPaid()
    {
        Status = "Paid";
        PaidDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Sends the invoice to the client.
    /// </summary>
    public void Send()
    {
        Status = "Sent";
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Checks if the invoice is overdue.
    /// </summary>
    /// <returns>True if overdue; otherwise, false.</returns>
    public bool IsOverdue()
    {
        return Status == "Sent" && DueDate < DateTime.UtcNow && PaidDate == null;
    }
}
