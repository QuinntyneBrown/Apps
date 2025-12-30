// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FinancialGoalTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinancialGoalTracker.Infrastructure;

/// <summary>
/// Provides seed data for the FinancialGoalTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FinancialGoalTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Goals.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGoalsAsync(context);
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

    private static async Task SeedGoalsAsync(FinancialGoalTrackerContext context)
    {
        var goals = new List<Goal>
        {
            new Goal
            {
                GoalId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Emergency Fund",
                Description = "Build a 6-month emergency fund for unexpected expenses",
                GoalType = GoalType.Emergency,
                TargetAmount = 15000.00m,
                CurrentAmount = 5000.00m,
                TargetDate = new DateTime(2025, 6, 30),
                Status = GoalStatus.InProgress,
                Notes = "Saving $500 per month to reach this goal",
            },
            new Goal
            {
                GoalId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "New Car",
                Description = "Save for a down payment on a new car",
                GoalType = GoalType.Purchase,
                TargetAmount = 8000.00m,
                CurrentAmount = 3500.00m,
                TargetDate = new DateTime(2025, 3, 15),
                Status = GoalStatus.InProgress,
                Notes = "Looking at SUVs in the $30,000 range",
            },
            new Goal
            {
                GoalId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Credit Card Payoff",
                Description = "Pay off credit card debt completely",
                GoalType = GoalType.DebtPayoff,
                TargetAmount = 5000.00m,
                CurrentAmount = 2000.00m,
                TargetDate = new DateTime(2025, 2, 28),
                Status = GoalStatus.InProgress,
                Notes = "Focus on high-interest card first",
            },
            new Goal
            {
                GoalId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                Name = "Retirement Savings",
                Description = "Build retirement fund through 401k",
                GoalType = GoalType.Retirement,
                TargetAmount = 50000.00m,
                CurrentAmount = 12000.00m,
                TargetDate = new DateTime(2030, 12, 31),
                Status = GoalStatus.InProgress,
                Notes = "Contributing 10% of salary plus employer match",
            },
        };

        context.Goals.AddRange(goals);

        var milestones = new List<Milestone>
        {
            new Milestone
            {
                MilestoneId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                Name = "First $5,000",
                TargetAmount = 5000.00m,
                TargetDate = new DateTime(2024, 12, 31),
                IsCompleted = true,
                CompletedDate = new DateTime(2024, 12, 15),
                Notes = "Completed ahead of schedule!",
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                Name = "Halfway Point",
                TargetAmount = 7500.00m,
                TargetDate = new DateTime(2025, 3, 31),
                IsCompleted = false,
                Notes = "Next major milestone",
            },
            new Milestone
            {
                MilestoneId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[1].GoalId,
                Name = "50% Saved",
                TargetAmount = 4000.00m,
                TargetDate = new DateTime(2025, 1, 31),
                IsCompleted = false,
                Notes = "Halfway to down payment goal",
            },
        };

        context.Milestones.AddRange(milestones);

        var contributions = new List<Contribution>
        {
            new Contribution
            {
                ContributionId = Guid.Parse("aaa11111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                Amount = 500.00m,
                ContributionDate = new DateTime(2024, 11, 1),
                Notes = "Monthly contribution - November",
            },
            new Contribution
            {
                ContributionId = Guid.Parse("bbb22222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[0].GoalId,
                Amount = 500.00m,
                ContributionDate = new DateTime(2024, 12, 1),
                Notes = "Monthly contribution - December",
            },
            new Contribution
            {
                ContributionId = Guid.Parse("ccc33333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[1].GoalId,
                Amount = 300.00m,
                ContributionDate = new DateTime(2024, 11, 15),
                Notes = "Bonus contribution",
            },
            new Contribution
            {
                ContributionId = Guid.Parse("ddd44444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GoalId = goals[2].GoalId,
                Amount = 250.00m,
                ContributionDate = new DateTime(2024, 12, 1),
                Notes = "Extra payment from holiday bonus",
            },
        };

        context.Contributions.AddRange(contributions);

        await context.SaveChangesAsync();
    }
}
