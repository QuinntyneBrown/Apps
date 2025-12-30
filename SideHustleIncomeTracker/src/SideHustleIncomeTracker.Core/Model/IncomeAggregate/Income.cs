// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Represents income from a side business.
/// </summary>
public class Income
{
    /// <summary>
    /// Gets or sets the unique identifier for the income.
    /// </summary>
    public Guid IncomeId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the business.
    /// </summary>
    public Guid BusinessId { get; set; }

    /// <summary>
    /// Gets or sets the income description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the income amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the income date.
    /// </summary>
    public DateTime IncomeDate { get; set; }

    /// <summary>
    /// Gets or sets the client or customer name.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    /// Gets or sets the invoice number.
    /// </summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Gets or sets whether payment has been received.
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the business.
    /// </summary>
    public Business? Business { get; set; }

    /// <summary>
    /// Marks the income as paid.
    /// </summary>
    public void MarkAsPaid()
    {
        IsPaid = true;
    }
}
