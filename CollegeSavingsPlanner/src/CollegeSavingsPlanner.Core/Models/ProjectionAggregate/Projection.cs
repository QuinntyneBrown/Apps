// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CollegeSavingsPlanner.Core;

/// <summary>
/// Represents a college savings projection.
/// </summary>
public class Projection
{
    /// <summary>
    /// Gets or sets the unique identifier for the projection.
    /// </summary>
    public Guid ProjectionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the plan.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Gets or sets the projection name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current savings amount.
    /// </summary>
    public decimal CurrentSavings { get; set; }

    /// <summary>
    /// Gets or sets the monthly contribution amount.
    /// </summary>
    public decimal MonthlyContribution { get; set; }

    /// <summary>
    /// Gets or sets the expected annual return rate (as a percentage).
    /// </summary>
    public decimal ExpectedReturnRate { get; set; }

    /// <summary>
    /// Gets or sets the years until college.
    /// </summary>
    public int YearsUntilCollege { get; set; }

    /// <summary>
    /// Gets or sets the target savings goal.
    /// </summary>
    public decimal TargetGoal { get; set; }

    /// <summary>
    /// Gets or sets the projected final balance.
    /// </summary>
    public decimal ProjectedBalance { get; set; }

    /// <summary>
    /// Gets or sets the date when the projection was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the plan.
    /// </summary>
    public Plan? Plan { get; set; }

    /// <summary>
    /// Calculates the projected balance at college start.
    /// </summary>
    /// <returns>The projected balance.</returns>
    public decimal CalculateProjectedBalance()
    {
        decimal balance = CurrentSavings;
        decimal monthlyReturn = ExpectedReturnRate / 100 / 12;
        int months = YearsUntilCollege * 12;

        for (int i = 0; i < months; i++)
        {
            balance = (balance + MonthlyContribution) * (1 + monthlyReturn);
        }

        ProjectedBalance = Math.Round(balance, 2);
        return ProjectedBalance;
    }

    /// <summary>
    /// Calculates the shortfall or surplus compared to the target goal.
    /// </summary>
    /// <returns>The shortfall (negative) or surplus (positive).</returns>
    public decimal CalculateGoalDifference()
    {
        return ProjectedBalance - TargetGoal;
    }

    /// <summary>
    /// Calculates the monthly contribution needed to reach the target goal.
    /// </summary>
    /// <returns>The required monthly contribution.</returns>
    public decimal CalculateRequiredMonthlyContribution()
    {
        if (YearsUntilCollege <= 0)
        {
            return 0;
        }

        decimal monthlyReturn = ExpectedReturnRate / 100 / 12;
        int months = YearsUntilCollege * 12;

        // Using future value of annuity formula
        decimal futureValueFactor = (decimal)((Math.Pow((double)(1 + monthlyReturn), months) - 1) / (double)monthlyReturn);
        decimal futureValueOfCurrent = CurrentSavings * (decimal)Math.Pow((double)(1 + monthlyReturn), months);

        return Math.Round((TargetGoal - futureValueOfCurrent) / futureValueFactor, 2);
    }
}
