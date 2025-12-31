// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateInvestmentAnalyzer.Core;

using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate;
using RealEstateInvestmentAnalyzer.Core.Model.UserAggregate.Entities;
using RealEstateInvestmentAnalyzer.Core.Services;
namespace RealEstateInvestmentAnalyzer.Infrastructure;

/// <summary>
/// Provides seed data for the RealEstateInvestmentAnalyzer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(RealEstateInvestmentAnalyzerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Properties.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedRealEstateDataAsync(context);
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

    private static async Task SeedRealEstateDataAsync(RealEstateInvestmentAnalyzerContext context)
    {
        // Seed Properties
        var properties = new List<Property>
        {
            new Property
            {
                PropertyId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Address = "123 Main Street, Springfield, IL 62701",
                PropertyType = PropertyType.SingleFamily,
                PurchasePrice = 250000.00m,
                PurchaseDate = new DateTime(2020, 3, 15),
                CurrentValue = 320000.00m,
                SquareFeet = 2000,
                Bedrooms = 3,
                Bathrooms = 2,
                Notes = "Great neighborhood, near schools",
            },
            new Property
            {
                PropertyId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Address = "456 Oak Avenue, Apt 101, Chicago, IL 60601",
                PropertyType = PropertyType.Condo,
                PurchasePrice = 180000.00m,
                PurchaseDate = new DateTime(2021, 6, 1),
                CurrentValue = 195000.00m,
                SquareFeet = 1200,
                Bedrooms = 2,
                Bathrooms = 2,
                Notes = "Downtown location, HOA $300/month",
            },
            new Property
            {
                PropertyId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Address = "789 Elm Street, Units 1-4, Peoria, IL 61602",
                PropertyType = PropertyType.MultiFamily,
                PurchasePrice = 450000.00m,
                PurchaseDate = new DateTime(2019, 9, 20),
                CurrentValue = 520000.00m,
                SquareFeet = 4800,
                Bedrooms = 12,
                Bathrooms = 8,
                Notes = "4-unit apartment building, all units occupied",
            },
        };

        context.Properties.AddRange(properties);

        // Seed Leases
        var leases = new List<Lease>
        {
            new Lease
            {
                LeaseId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PropertyId = properties[0].PropertyId,
                TenantName = "John and Sarah Smith",
                MonthlyRent = 1800.00m,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                SecurityDeposit = 3600.00m,
                IsActive = true,
                Notes = "Great tenants, always pay on time",
            },
            new Lease
            {
                LeaseId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                PropertyId = properties[1].PropertyId,
                TenantName = "Emily Rodriguez",
                MonthlyRent = 1400.00m,
                StartDate = new DateTime(2023, 7, 1),
                EndDate = new DateTime(2024, 6, 30),
                SecurityDeposit = 2800.00m,
                IsActive = true,
                Notes = "Young professional, clean tenant",
            },
            new Lease
            {
                LeaseId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                PropertyId = properties[2].PropertyId,
                TenantName = "Unit 1 - Michael Chen",
                MonthlyRent = 1100.00m,
                StartDate = new DateTime(2022, 5, 1),
                EndDate = new DateTime(2024, 4, 30),
                SecurityDeposit = 2200.00m,
                IsActive = true,
                Notes = "Long-term tenant",
            },
            new Lease
            {
                LeaseId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                PropertyId = properties[2].PropertyId,
                TenantName = "Unit 2 - David Park",
                MonthlyRent = 1100.00m,
                StartDate = new DateTime(2023, 3, 1),
                EndDate = new DateTime(2024, 2, 28),
                SecurityDeposit = 2200.00m,
                IsActive = true,
                Notes = "Quiet tenant",
            },
        };

        context.Leases.AddRange(leases);

        // Seed Expenses
        var expenses = new List<Expense>
        {
            new Expense
            {
                ExpenseId = Guid.Parse("55555555-eeee-eeee-eeee-eeeeeeeeeeee"),
                PropertyId = properties[0].PropertyId,
                Description = "Property Tax",
                Amount = 3500.00m,
                Date = new DateTime(2024, 1, 15),
                Category = "Tax",
                IsRecurring = true,
                Notes = "Annual property tax payment",
            },
            new Expense
            {
                ExpenseId = Guid.Parse("66666666-ffff-ffff-ffff-ffffffffffff"),
                PropertyId = properties[0].PropertyId,
                Description = "Roof Repair",
                Amount = 2500.00m,
                Date = new DateTime(2024, 4, 10),
                Category = "Maintenance",
                IsRecurring = false,
                Notes = "Fixed leak in master bedroom",
            },
            new Expense
            {
                ExpenseId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PropertyId = properties[1].PropertyId,
                Description = "HOA Fees",
                Amount = 300.00m,
                Date = new DateTime(2024, 6, 1),
                Category = "HOA",
                IsRecurring = true,
                Notes = "Monthly HOA payment",
            },
            new Expense
            {
                ExpenseId = Guid.Parse("88888888-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                PropertyId = properties[2].PropertyId,
                Description = "Landscaping",
                Amount = 800.00m,
                Date = new DateTime(2024, 5, 15),
                Category = "Maintenance",
                IsRecurring = true,
                Notes = "Monthly landscaping service",
            },
        };

        context.Expenses.AddRange(expenses);

        // Seed Cash Flows
        var cashFlows = new List<CashFlow>
        {
            new CashFlow
            {
                CashFlowId = Guid.Parse("99999999-cccc-cccc-cccc-cccccccccccc"),
                PropertyId = properties[0].PropertyId,
                Date = new DateTime(2024, 6, 1),
                Income = 1800.00m,
                Expenses = 500.00m,
                NetCashFlow = 1300.00m,
                Notes = "Rent collected, insurance and maintenance",
            },
            new CashFlow
            {
                CashFlowId = Guid.Parse("aaaaaaaa-dddd-dddd-dddd-dddddddddddd"),
                PropertyId = properties[1].PropertyId,
                Date = new DateTime(2024, 6, 1),
                Income = 1400.00m,
                Expenses = 450.00m,
                NetCashFlow = 950.00m,
                Notes = "Rent collected, HOA and utilities",
            },
            new CashFlow
            {
                CashFlowId = Guid.Parse("bbbbbbbb-eeee-eeee-eeee-eeeeeeeeeeee"),
                PropertyId = properties[2].PropertyId,
                Date = new DateTime(2024, 6, 1),
                Income = 4400.00m,
                Expenses = 1500.00m,
                NetCashFlow = 2900.00m,
                Notes = "All units rented, maintenance and landscaping",
            },
        };

        context.CashFlows.AddRange(cashFlows);

        await context.SaveChangesAsync();
    }
}
