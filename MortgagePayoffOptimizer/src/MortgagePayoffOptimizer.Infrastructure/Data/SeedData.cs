// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MortgagePayoffOptimizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MortgagePayoffOptimizer.Core.Model.UserAggregate;
using MortgagePayoffOptimizer.Core.Model.UserAggregate.Entities;
using MortgagePayoffOptimizer.Core.Services;
namespace MortgagePayoffOptimizer.Infrastructure;

/// <summary>
/// Provides seed data for the MortgagePayoffOptimizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MortgagePayoffOptimizerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Mortgages.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMortgagesAsync(context);
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

    private static async Task SeedMortgagesAsync(MortgagePayoffOptimizerContext context)
    {
        var mortgage1 = new Mortgage
        {
            MortgageId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            PropertyAddress = "123 Main Street, Anytown, USA",
            Lender = "First National Bank",
            OriginalLoanAmount = 300000m,
            CurrentBalance = 275000m,
            InterestRate = 3.75m,
            LoanTermYears = 30,
            MonthlyPayment = 1389.35m,
            StartDate = new DateTime(2020, 1, 15),
            MortgageType = MortgageType.Conventional,
            IsActive = true,
            Notes = "Primary residence mortgage",
        };

        var mortgage2 = new Mortgage
        {
            MortgageId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            PropertyAddress = "456 Oak Avenue, Springfield, USA",
            Lender = "Community Mortgage Company",
            OriginalLoanAmount = 450000m,
            CurrentBalance = 425000m,
            InterestRate = 4.25m,
            LoanTermYears = 30,
            MonthlyPayment = 2213.61m,
            StartDate = new DateTime(2021, 6, 1),
            MortgageType = MortgageType.Conventional,
            IsActive = true,
            Notes = "Investment property",
        };

        context.Mortgages.AddRange(mortgage1, mortgage2);

        // Add sample payments for the first mortgage
        var payment1 = new Payment
        {
            PaymentId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            MortgageId = mortgage1.MortgageId,
            PaymentDate = new DateTime(2024, 11, 1),
            Amount = 1389.35m,
            PrincipalAmount = 527.10m,
            InterestAmount = 862.25m,
            ExtraPrincipal = 100m,
            Notes = "Regular monthly payment with extra principal",
        };

        var payment2 = new Payment
        {
            PaymentId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            MortgageId = mortgage1.MortgageId,
            PaymentDate = new DateTime(2024, 12, 1),
            Amount = 1389.35m,
            PrincipalAmount = 528.75m,
            InterestAmount = 860.60m,
            Notes = "Regular monthly payment",
        };

        context.Payments.AddRange(payment1, payment2);

        // Add sample refinance scenarios for the first mortgage
        var scenario1 = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            MortgageId = mortgage1.MortgageId,
            Name = "Lower Rate 30-Year",
            NewInterestRate = 3.25m,
            NewLoanTermYears = 30,
            RefinancingCosts = 5000m,
            NewMonthlyPayment = 1196.50m,
            MonthlySavings = 192.85m,
            BreakEvenMonths = 26,
            TotalSavings = 69426m,
            CreatedAt = DateTime.UtcNow,
        };

        var scenario2 = new RefinanceScenario
        {
            RefinanceScenarioId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            MortgageId = mortgage1.MortgageId,
            Name = "15-Year Payoff",
            NewInterestRate = 2.75m,
            NewLoanTermYears = 15,
            RefinancingCosts = 4500m,
            NewMonthlyPayment = 1859.36m,
            MonthlySavings = -470.01m,
            BreakEvenMonths = 0,
            TotalSavings = 95432m,
            CreatedAt = DateTime.UtcNow,
        };

        context.RefinanceScenarios.AddRange(scenario1, scenario2);

        await context.SaveChangesAsync();
    }
}
