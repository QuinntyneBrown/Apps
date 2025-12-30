// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RetirementSavingsCalculator.Core;

/// <summary>
/// Represents a retirement planning scenario with income and expense projections.
/// </summary>
public class RetirementScenario
{
    /// <summary>
    /// Gets or sets the unique identifier for the scenario.
    /// </summary>
    public Guid RetirementScenarioId { get; set; }

    /// <summary>
    /// Gets or sets the name of the scenario.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current age.
    /// </summary>
    public int CurrentAge { get; set; }

    /// <summary>
    /// Gets or sets the planned retirement age.
    /// </summary>
    public int RetirementAge { get; set; }

    /// <summary>
    /// Gets or sets the life expectancy age.
    /// </summary>
    public int LifeExpectancyAge { get; set; }

    /// <summary>
    /// Gets or sets the current retirement savings balance.
    /// </summary>
    public decimal CurrentSavings { get; set; }

    /// <summary>
    /// Gets or sets the annual contribution amount.
    /// </summary>
    public decimal AnnualContribution { get; set; }

    /// <summary>
    /// Gets or sets the expected annual return rate (as a percentage).
    /// </summary>
    public decimal ExpectedReturnRate { get; set; }

    /// <summary>
    /// Gets or sets the expected inflation rate (as a percentage).
    /// </summary>
    public decimal InflationRate { get; set; }

    /// <summary>
    /// Gets or sets the projected annual income in retirement.
    /// </summary>
    public decimal ProjectedAnnualIncome { get; set; }

    /// <summary>
    /// Gets or sets the projected annual expenses in retirement.
    /// </summary>
    public decimal ProjectedAnnualExpenses { get; set; }

    /// <summary>
    /// Gets or sets notes about the scenario.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when the scenario was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date when the scenario was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the projected retirement savings at retirement age.
    /// </summary>
    /// <returns>The projected savings amount.</returns>
    public decimal CalculateProjectedSavings()
    {
        int yearsToRetirement = RetirementAge - CurrentAge;
        if (yearsToRetirement <= 0)
        {
            return CurrentSavings;
        }

        decimal futureValue = CurrentSavings;
        decimal monthlyReturn = ExpectedReturnRate / 100 / 12;
        decimal monthlyContribution = AnnualContribution / 12;

        for (int i = 0; i < yearsToRetirement * 12; i++)
        {
            futureValue = (futureValue + monthlyContribution) * (1 + monthlyReturn);
        }

        return Math.Round(futureValue, 2);
    }

    /// <summary>
    /// Calculates the annual withdrawal amount needed in retirement.
    /// </summary>
    /// <returns>The annual withdrawal amount.</returns>
    public decimal CalculateAnnualWithdrawal()
    {
        return ProjectedAnnualExpenses - ProjectedAnnualIncome;
    }

    /// <summary>
    /// Updates the scenario parameters.
    /// </summary>
    public void UpdateParameters(int retirementAge, decimal annualContribution, decimal expectedReturnRate)
    {
        RetirementAge = retirementAge;
        AnnualContribution = annualContribution;
        ExpectedReturnRate = expectedReturnRate;
        LastUpdated = DateTime.UtcNow;
    }
}
