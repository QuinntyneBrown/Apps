// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Represents a side business or hustle.
/// </summary>
public class Business
{
    /// <summary>
    /// Gets or sets the unique identifier for the business.
    /// </summary>
    public Guid BusinessId { get; set; }

    /// <summary>
    /// Gets or sets the business name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the business description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets whether the business is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the EIN or tax ID.
    /// </summary>
    public string? TaxId { get; set; }

    /// <summary>
    /// Gets or sets notes about the business.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Calculates the total income for the business.
    /// </summary>
    public decimal TotalIncome { get; set; }

    /// <summary>
    /// Calculates the total expenses for the business.
    /// </summary>
    public decimal TotalExpenses { get; set; }

    /// <summary>
    /// Calculates net profit.
    /// </summary>
    public decimal CalculateNetProfit()
    {
        return TotalIncome - TotalExpenses;
    }

    /// <summary>
    /// Closes the business.
    /// </summary>
    public void Close()
    {
        IsActive = false;
    }
}
