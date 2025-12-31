// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SideHustleIncomeTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SideHustleIncomeTracker.Core.Model.UserAggregate;
using SideHustleIncomeTracker.Core.Model.UserAggregate.Entities;
using SideHustleIncomeTracker.Core.Services;
namespace SideHustleIncomeTracker.Infrastructure;

/// <summary>
/// Provides seed data for the SideHustleIncomeTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(SideHustleIncomeTrackerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Businesses.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedBusinessesAsync(context);
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

    private static async Task SeedBusinessesAsync(SideHustleIncomeTrackerContext context)
    {
        var businesses = new List<Business>
        {
            new Business
            {
                BusinessId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Freelance Web Development",
                Description = "Building websites and web applications for clients",
                StartDate = new DateTime(2023, 1, 1),
                IsActive = true,
                TotalIncome = 45000m,
                TotalExpenses = 8500m,
            },
            new Business
            {
                BusinessId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Online Tutoring",
                Description = "Teaching mathematics and programming online",
                StartDate = new DateTime(2023, 6, 15),
                IsActive = true,
                TotalIncome = 12000m,
                TotalExpenses = 1200m,
            },
            new Business
            {
                BusinessId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Etsy Shop",
                Description = "Selling handmade crafts",
                StartDate = new DateTime(2022, 3, 1),
                IsActive = false,
                TotalIncome = 5000m,
                TotalExpenses = 3000m,
                Notes = "Closed due to time constraints",
            },
        };

        context.Businesses.AddRange(businesses);

        // Add sample incomes
        var incomes = new List<Income>
        {
            new Income
            {
                IncomeId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                Description = "E-commerce website development",
                Amount = 5500m,
                IncomeDate = new DateTime(2024, 1, 15),
                Client = "ABC Retail Co.",
                InvoiceNumber = "INV-2024-001",
                IsPaid = true,
            },
            new Income
            {
                IncomeId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                Description = "Website maintenance and updates",
                Amount = 1200m,
                IncomeDate = new DateTime(2024, 2, 1),
                Client = "ABC Retail Co.",
                InvoiceNumber = "INV-2024-002",
                IsPaid = true,
            },
            new Income
            {
                IncomeId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[1].BusinessId,
                Description = "Math tutoring - February sessions",
                Amount = 800m,
                IncomeDate = new DateTime(2024, 2, 28),
                Client = "Various Students",
                IsPaid = true,
            },
            new Income
            {
                IncomeId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                Description = "React application development",
                Amount = 8000m,
                IncomeDate = new DateTime(2024, 3, 10),
                Client = "XYZ Tech Inc.",
                InvoiceNumber = "INV-2024-003",
                IsPaid = false,
                Notes = "Payment expected by March 30",
            },
        };

        context.Incomes.AddRange(incomes);

        // Add sample expenses
        var expenses = new List<Expense>
        {
            new Expense
            {
                ExpenseId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                Description = "Adobe Creative Cloud subscription",
                Amount = 54.99m,
                ExpenseDate = new DateTime(2024, 1, 1),
                Category = "Software",
                Vendor = "Adobe",
                IsTaxDeductible = true,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                Description = "Hosting services",
                Amount = 120m,
                ExpenseDate = new DateTime(2024, 1, 5),
                Category = "Services",
                Vendor = "AWS",
                IsTaxDeductible = true,
            },
            new Expense
            {
                ExpenseId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[1].BusinessId,
                Description = "Online whiteboard tool subscription",
                Amount = 15m,
                ExpenseDate = new DateTime(2024, 2, 1),
                Category = "Software",
                Vendor = "Miro",
                IsTaxDeductible = true,
            },
        };

        context.Expenses.AddRange(expenses);

        // Add sample tax estimates
        var taxEstimates = new List<TaxEstimate>
        {
            new TaxEstimate
            {
                TaxEstimateId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                TaxYear = 2024,
                Quarter = 1,
                NetProfit = 9125m,
                SelfEmploymentTax = 1290m,
                IncomeTax = 2010m,
                TotalEstimatedTax = 3300m,
                IsPaid = true,
                PaymentDate = new DateTime(2024, 4, 15),
            },
            new TaxEstimate
            {
                TaxEstimateId = Guid.Parse("99999999-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                BusinessId = businesses[0].BusinessId,
                TaxYear = 2024,
                Quarter = 2,
                NetProfit = 8800m,
                SelfEmploymentTax = 1244m,
                IncomeTax = 1936m,
                TotalEstimatedTax = 3180m,
                IsPaid = false,
            },
        };

        context.TaxEstimates.AddRange(taxEstimates);

        await context.SaveChangesAsync();
    }
}
