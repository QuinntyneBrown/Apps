// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SalaryCompensationTracker.Core;

/// <summary>
/// Represents a compensation record.
/// </summary>
public class Compensation
{
    /// <summary>
    /// Gets or sets the unique identifier for the compensation record.
    /// </summary>
    public Guid CompensationId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this compensation record.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the compensation type.
    /// </summary>
    public CompensationType CompensationType { get; set; }

    /// <summary>
    /// Gets or sets the employer name.
    /// </summary>
    public string Employer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the base salary amount.
    /// </summary>
    public decimal BaseSalary { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets the bonus amount.
    /// </summary>
    public decimal? Bonus { get; set; }

    /// <summary>
    /// Gets or sets the stock/equity value.
    /// </summary>
    public decimal? StockValue { get; set; }

    /// <summary>
    /// Gets or sets other compensation (commissions, etc.).
    /// </summary>
    public decimal? OtherCompensation { get; set; }

    /// <summary>
    /// Gets or sets the total compensation.
    /// </summary>
    public decimal TotalCompensation { get; set; }

    /// <summary>
    /// Gets or sets the effective date.
    /// </summary>
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// Gets or sets the end date (if applicable).
    /// </summary>
    public DateTime? EndDate { get; set; }

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
    /// Gets or sets the collection of benefits associated with this compensation.
    /// </summary>
    public ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();

    /// <summary>
    /// Calculates the total compensation including all components.
    /// </summary>
    public void CalculateTotalCompensation()
    {
        TotalCompensation = BaseSalary +
                           (Bonus ?? 0) +
                           (StockValue ?? 0) +
                           (OtherCompensation ?? 0);
        UpdatedAt = DateTime.UtcNow;
    }
}
