// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents a liability in the net worth dashboard.
/// </summary>
public class Liability
{
    /// <summary>
    /// Gets or sets the unique identifier for the liability.
    /// </summary>
    public Guid LiabilityId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the liability.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the liability.
    /// </summary>
    public LiabilityType LiabilityType { get; set; }

    /// <summary>
    /// Gets or sets the current balance of the liability.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets the original amount of the liability.
    /// </summary>
    public decimal? OriginalAmount { get; set; }

    /// <summary>
    /// Gets or sets the interest rate (as a percentage).
    /// </summary>
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Gets or sets the monthly payment amount.
    /// </summary>
    public decimal? MonthlyPayment { get; set; }

    /// <summary>
    /// Gets or sets the creditor or lender.
    /// </summary>
    public string? Creditor { get; set; }

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets the due date for payments.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets notes about the liability.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when the balance was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets whether the liability is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Updates the current balance of the liability.
    /// </summary>
    /// <param name="newBalance">The new balance.</param>
    public void UpdateBalance(decimal newBalance)
    {
        if (newBalance < 0)
        {
            throw new ArgumentException("Liability balance cannot be negative.", nameof(newBalance));
        }

        CurrentBalance = newBalance;
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Records a payment towards the liability.
    /// </summary>
    /// <param name="paymentAmount">The payment amount.</param>
    public void RecordPayment(decimal paymentAmount)
    {
        if (paymentAmount <= 0)
        {
            throw new ArgumentException("Payment amount must be positive.", nameof(paymentAmount));
        }

        CurrentBalance = Math.Max(0, CurrentBalance - paymentAmount);
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the liability as paid off.
    /// </summary>
    public void MarkAsPaidOff()
    {
        CurrentBalance = 0;
        IsActive = false;
        LastUpdated = DateTime.UtcNow;
    }
}
