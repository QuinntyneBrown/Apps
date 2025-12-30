// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SideHustleIncomeTracker.Core;

/// <summary>
/// Represents a quarterly tax estimate for a side business.
/// </summary>
public class TaxEstimate
{
    /// <summary>
    /// Gets or sets the unique identifier for the tax estimate.
    /// </summary>
    public Guid TaxEstimateId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the business.
    /// </summary>
    public Guid BusinessId { get; set; }

    /// <summary>
    /// Gets or sets the tax year.
    /// </summary>
    public int TaxYear { get; set; }

    /// <summary>
    /// Gets or sets the quarter (1-4).
    /// </summary>
    public int Quarter { get; set; }

    /// <summary>
    /// Gets or sets the net profit for the quarter.
    /// </summary>
    public decimal NetProfit { get; set; }

    /// <summary>
    /// Gets or sets the estimated self-employment tax.
    /// </summary>
    public decimal SelfEmploymentTax { get; set; }

    /// <summary>
    /// Gets or sets the estimated income tax.
    /// </summary>
    public decimal IncomeTax { get; set; }

    /// <summary>
    /// Gets or sets the total estimated tax.
    /// </summary>
    public decimal TotalEstimatedTax { get; set; }

    /// <summary>
    /// Gets or sets whether the payment has been made.
    /// </summary>
    public bool IsPaid { get; set; }

    /// <summary>
    /// Gets or sets the payment date.
    /// </summary>
    public DateTime? PaymentDate { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the business.
    /// </summary>
    public Business? Business { get; set; }

    /// <summary>
    /// Calculates the total estimated tax.
    /// </summary>
    public void CalculateTotalTax()
    {
        TotalEstimatedTax = SelfEmploymentTax + IncomeTax;
    }

    /// <summary>
    /// Records a tax payment.
    /// </summary>
    public void RecordPayment()
    {
        IsPaid = true;
        PaymentDate = DateTime.UtcNow;
    }
}
