// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Represents a mortgage loan.
/// </summary>
public class Mortgage
{
    /// <summary>
    /// Gets or sets the unique identifier for the mortgage.
    /// </summary>
    public Guid MortgageId { get; set; }

    /// <summary>
    /// Gets or sets the property address.
    /// </summary>
    public string PropertyAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the lender name.
    /// </summary>
    public string Lender { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the original loan amount.
    /// </summary>
    public decimal OriginalLoanAmount { get; set; }

    /// <summary>
    /// Gets or sets the current balance.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets the interest rate (as a percentage).
    /// </summary>
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Gets or sets the loan term in years.
    /// </summary>
    public int LoanTermYears { get; set; }

    /// <summary>
    /// Gets or sets the monthly payment amount.
    /// </summary>
    public decimal MonthlyPayment { get; set; }

    /// <summary>
    /// Gets or sets the loan start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the mortgage type.
    /// </summary>
    public MortgageType MortgageType { get; set; }

    /// <summary>
    /// Gets or sets whether the mortgage is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets notes about the mortgage.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Calculates the remaining balance after a payment.
    /// </summary>
    public void ApplyPayment(decimal paymentAmount)
    {
        decimal monthlyInterestRate = InterestRate / 100 / 12;
        decimal interestPortion = CurrentBalance * monthlyInterestRate;
        decimal principalPortion = paymentAmount - interestPortion;
        CurrentBalance = Math.Max(0, CurrentBalance - principalPortion);
    }

    /// <summary>
    /// Calculates the payoff date based on current payment schedule.
    /// </summary>
    public DateTime CalculatePayoffDate()
    {
        int monthsRemaining = CalculateMonthsRemaining();
        return StartDate.AddMonths(monthsRemaining);
    }

    /// <summary>
    /// Calculates the number of months remaining.
    /// </summary>
    public int CalculateMonthsRemaining()
    {
        if (CurrentBalance <= 0 || MonthlyPayment <= 0)
        {
            return 0;
        }

        decimal monthlyRate = InterestRate / 100 / 12;
        double months = -Math.Log(1 - (double)(CurrentBalance * monthlyRate / MonthlyPayment)) / Math.Log(1 + (double)monthlyRate);
        return (int)Math.Ceiling(months);
    }
}
