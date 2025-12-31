// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Represents a business expense.
/// </summary>
public class Expense
{
    /// <summary>
    /// Gets or sets the unique identifier for the expense.
    /// </summary>
    public Guid ExpenseId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the business.
    /// </summary>
    public Guid BusinessId { get; set; }

    /// <summary>
    /// Gets or sets the expense description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expense amount.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the expense date.
    /// </summary>
    public DateTime ExpenseDate { get; set; }

    /// <summary>
    /// Gets or sets the expense category.
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the vendor.
    /// </summary>
    public string? Vendor { get; set; }

    /// <summary>
    /// Gets or sets whether this is tax deductible.
    /// </summary>
    public bool IsTaxDeductible { get; set; } = true;

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the business.
    /// </summary>
    public Business? Business { get; set; }
}
