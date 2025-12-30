// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MortgagePayoffOptimizer.Core;

/// <summary>
/// Represents a mortgage refinance scenario analysis.
/// </summary>
public class RefinanceScenario
{
    /// <summary>
    /// Gets or sets the unique identifier for the scenario.
    /// </summary>
    public Guid RefinanceScenarioId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the mortgage.
    /// </summary>
    public Guid MortgageId { get; set; }

    /// <summary>
    /// Gets or sets the scenario name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new interest rate (as a percentage).
    /// </summary>
    public decimal NewInterestRate { get; set; }

    /// <summary>
    /// Gets or sets the new loan term in years.
    /// </summary>
    public int NewLoanTermYears { get; set; }

    /// <summary>
    /// Gets or sets the refinancing costs.
    /// </summary>
    public decimal RefinancingCosts { get; set; }

    /// <summary>
    /// Gets or sets the new monthly payment.
    /// </summary>
    public decimal NewMonthlyPayment { get; set; }

    /// <summary>
    /// Gets or sets the monthly savings.
    /// </summary>
    public decimal MonthlySavings { get; set; }

    /// <summary>
    /// Gets or sets the break-even months.
    /// </summary>
    public int BreakEvenMonths { get; set; }

    /// <summary>
    /// Gets or sets the total savings over the loan term.
    /// </summary>
    public decimal TotalSavings { get; set; }

    /// <summary>
    /// Gets or sets the date when the scenario was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the mortgage.
    /// </summary>
    public Mortgage? Mortgage { get; set; }

    /// <summary>
    /// Calculates the break-even point in months.
    /// </summary>
    public void CalculateBreakEven()
    {
        if (MonthlySavings > 0)
        {
            BreakEvenMonths = (int)Math.Ceiling(RefinancingCosts / MonthlySavings);
        }
        else
        {
            BreakEvenMonths = 0;
        }
    }

    /// <summary>
    /// Determines if refinancing is recommended.
    /// </summary>
    public bool IsRefinancingRecommended()
    {
        return MonthlySavings > 0 && BreakEvenMonths > 0 && BreakEvenMonths < (NewLoanTermYears * 12);
    }
}
