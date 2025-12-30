// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SalaryCompensationTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Infrastructure;

/// <summary>
/// Provides seed data for the SalaryCompensationTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SalaryCompensationTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Compensations.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedCompensationsAsync(context);
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

    private static async Task SeedCompensationsAsync(SalaryCompensationTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var compensations = new List<Compensation>
        {
            new Compensation
            {
                CompensationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                CompensationType = CompensationType.FullTime,
                Employer = "Tech Corp Inc.",
                JobTitle = "Software Engineer",
                BaseSalary = 120000m,
                Currency = "USD",
                Bonus = 15000m,
                StockValue = 25000m,
                TotalCompensation = 160000m,
                EffectiveDate = new DateTime(2023, 1, 15),
                Notes = "Includes annual performance bonus and RSUs",
                CreatedAt = DateTime.UtcNow,
            },
            new Compensation
            {
                CompensationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                CompensationType = CompensationType.FullTime,
                Employer = "Tech Corp Inc.",
                JobTitle = "Senior Software Engineer",
                BaseSalary = 145000m,
                Currency = "USD",
                Bonus = 20000m,
                StockValue = 35000m,
                TotalCompensation = 200000m,
                EffectiveDate = new DateTime(2024, 1, 15),
                Notes = "Promotion with increased RSU grant",
                CreatedAt = DateTime.UtcNow,
            },
            new Compensation
            {
                CompensationId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                CompensationType = CompensationType.Contract,
                Employer = "Consulting Firm LLC",
                JobTitle = "Technical Consultant",
                BaseSalary = 95000m,
                Currency = "USD",
                TotalCompensation = 95000m,
                EffectiveDate = new DateTime(2022, 6, 1),
                EndDate = new DateTime(2022, 12, 31),
                Notes = "6-month contract position",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Compensations.AddRange(compensations);

        // Add sample benefits for the current compensation
        var benefits = new List<Benefit>
        {
            new Benefit
            {
                BenefitId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CompensationId = compensations[1].CompensationId,
                UserId = sampleUserId,
                Name = "Health Insurance",
                Category = "Health",
                Description = "PPO plan with dental and vision",
                EstimatedValue = 12000m,
                EmployerContribution = 10000m,
                EmployeeContribution = 2000m,
                CreatedAt = DateTime.UtcNow,
            },
            new Benefit
            {
                BenefitId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CompensationId = compensations[1].CompensationId,
                UserId = sampleUserId,
                Name = "401(k) Match",
                Category = "Retirement",
                Description = "6% employer match",
                EstimatedValue = 8700m,
                EmployerContribution = 8700m,
                EmployeeContribution = 8700m,
                CreatedAt = DateTime.UtcNow,
            },
            new Benefit
            {
                BenefitId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CompensationId = compensations[1].CompensationId,
                UserId = sampleUserId,
                Name = "Paid Time Off",
                Category = "PTO",
                Description = "25 days per year",
                EstimatedValue = 14000m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Benefits.AddRange(benefits);

        // Add sample market comparisons
        var marketComparisons = new List<MarketComparison>
        {
            new MarketComparison
            {
                MarketComparisonId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                JobTitle = "Senior Software Engineer",
                Location = "San Francisco Bay Area",
                ExperienceLevel = "5-7 years",
                MinSalary = 135000m,
                MaxSalary = 180000m,
                MedianSalary = 157500m,
                DataSource = "Glassdoor",
                ComparisonDate = DateTime.UtcNow.AddDays(-30),
                Notes = "Market data for similar roles in the region",
                CreatedAt = DateTime.UtcNow,
            },
            new MarketComparison
            {
                MarketComparisonId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                JobTitle = "Senior Software Engineer",
                Location = "New York City",
                ExperienceLevel = "5-7 years",
                MinSalary = 140000m,
                MaxSalary = 190000m,
                MedianSalary = 165000m,
                DataSource = "LinkedIn Salary",
                ComparisonDate = DateTime.UtcNow.AddDays(-15),
                Notes = "Higher cost of living area",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.MarketComparisons.AddRange(marketComparisons);

        await context.SaveChangesAsync();
    }
}
