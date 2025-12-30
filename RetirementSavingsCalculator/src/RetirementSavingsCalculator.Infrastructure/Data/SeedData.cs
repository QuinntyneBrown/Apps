// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using RetirementSavingsCalculator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RetirementSavingsCalculator.Infrastructure;

/// <summary>
/// Provides seed data for the RetirementSavingsCalculator database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RetirementSavingsCalculatorContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.RetirementScenarios.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDataAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedDataAsync(RetirementSavingsCalculatorContext context)
    {
        var scenarios = new List<RetirementScenario>
        {
            new RetirementScenario
            {
                RetirementScenarioId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Conservative Plan",
                CurrentAge = 35,
                RetirementAge = 65,
                LifeExpectancyAge = 90,
                CurrentSavings = 100000m,
                AnnualContribution = 18000m,
                ExpectedReturnRate = 5m,
                InflationRate = 2.5m,
                ProjectedAnnualIncome = 25000m,
                ProjectedAnnualExpenses = 60000m,
                Notes = "Conservative plan with moderate risk tolerance",
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            },
            new RetirementScenario
            {
                RetirementScenarioId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Aggressive Growth Plan",
                CurrentAge = 30,
                RetirementAge = 60,
                LifeExpectancyAge = 95,
                CurrentSavings = 50000m,
                AnnualContribution = 25000m,
                ExpectedReturnRate = 8m,
                InflationRate = 3m,
                ProjectedAnnualIncome = 30000m,
                ProjectedAnnualExpenses = 80000m,
                Notes = "Aggressive plan with higher risk tolerance for maximum growth",
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            },
            new RetirementScenario
            {
                RetirementScenarioId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Early Retirement Plan",
                CurrentAge = 40,
                RetirementAge = 55,
                LifeExpectancyAge = 85,
                CurrentSavings = 300000m,
                AnnualContribution = 30000m,
                ExpectedReturnRate = 6.5m,
                InflationRate = 2.8m,
                ProjectedAnnualIncome = 20000m,
                ProjectedAnnualExpenses = 50000m,
                Notes = "Plan for early retirement with careful expense management",
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
            },
        };

        context.RetirementScenarios.AddRange(scenarios);

        // Add contributions for the first scenario
        var contributions = new List<Contribution>
        {
            new Contribution
            {
                ContributionId = Guid.NewGuid(),
                RetirementScenarioId = scenarios[0].RetirementScenarioId,
                Amount = 1500m,
                ContributionDate = DateTime.UtcNow.AddMonths(-2),
                AccountName = "401(k)",
                IsEmployerMatch = false,
                Notes = "Monthly contribution",
            },
            new Contribution
            {
                ContributionId = Guid.NewGuid(),
                RetirementScenarioId = scenarios[0].RetirementScenarioId,
                Amount = 750m,
                ContributionDate = DateTime.UtcNow.AddMonths(-2),
                AccountName = "401(k)",
                IsEmployerMatch = true,
                Notes = "Employer match contribution",
            },
            new Contribution
            {
                ContributionId = Guid.NewGuid(),
                RetirementScenarioId = scenarios[0].RetirementScenarioId,
                Amount = 1500m,
                ContributionDate = DateTime.UtcNow.AddMonths(-1),
                AccountName = "401(k)",
                IsEmployerMatch = false,
                Notes = "Monthly contribution",
            },
        };

        context.Contributions.AddRange(contributions);

        // Add withdrawal strategies
        var strategies = new List<WithdrawalStrategy>
        {
            new WithdrawalStrategy
            {
                WithdrawalStrategyId = Guid.NewGuid(),
                RetirementScenarioId = scenarios[0].RetirementScenarioId,
                Name = "4% Rule",
                WithdrawalRate = 4m,
                AnnualWithdrawalAmount = 0m,
                AdjustForInflation = true,
                MinimumBalance = 100000m,
                StrategyType = WithdrawalStrategyType.PercentageBased,
                Notes = "Traditional 4% safe withdrawal rate",
            },
            new WithdrawalStrategy
            {
                WithdrawalStrategyId = Guid.NewGuid(),
                RetirementScenarioId = scenarios[1].RetirementScenarioId,
                Name = "Fixed Annual Amount",
                WithdrawalRate = 0m,
                AnnualWithdrawalAmount = 50000m,
                AdjustForInflation = true,
                MinimumBalance = 200000m,
                StrategyType = WithdrawalStrategyType.FixedAmount,
                Notes = "Fixed withdrawal adjusted for inflation",
            },
        };

        context.WithdrawalStrategies.AddRange(strategies);

        await context.SaveChangesAsync();
    }
}
