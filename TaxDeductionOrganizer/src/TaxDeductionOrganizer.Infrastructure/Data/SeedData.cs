// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TaxDeductionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TaxDeductionOrganizer.Core.Models.UserAggregate;
using TaxDeductionOrganizer.Core.Models.UserAggregate.Entities;
using TaxDeductionOrganizer.Core.Services;
namespace TaxDeductionOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the TaxDeductionOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(TaxDeductionOrganizerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.TaxYears.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedTaxYearsAndDeductionsAsync(context);
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

    private static async Task SeedTaxYearsAndDeductionsAsync(TaxDeductionOrganizerContext context)
    {
        var taxYear2024 = new TaxYear
        {
            TaxYearId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Year = 2024,
            IsFiled = false,
            TotalDeductions = 0,
            Notes = "Current tax year",
        };

        var taxYear2023 = new TaxYear
        {
            TaxYearId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Year = 2023,
            IsFiled = true,
            FilingDate = new DateTime(2024, 4, 15),
            TotalDeductions = 0,
            Notes = "Filed on time",
        };

        context.TaxYears.AddRange(taxYear2024, taxYear2023);

        var deductions2024 = new List<Deduction>
        {
            new Deduction
            {
                DeductionId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2024.TaxYearId,
                Description = "Home Office Equipment",
                Amount = 1250.00m,
                Date = new DateTime(2024, 3, 15),
                Category = DeductionCategory.HomeOffice,
                Notes = "New desk and ergonomic chair",
                HasReceipt = true,
            },
            new Deduction
            {
                DeductionId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2024.TaxYearId,
                Description = "Professional Development Course",
                Amount = 599.00m,
                Date = new DateTime(2024, 5, 20),
                Category = DeductionCategory.Education,
                Notes = "Online certification program",
                HasReceipt = true,
            },
            new Deduction
            {
                DeductionId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2024.TaxYearId,
                Description = "Business Travel Expenses",
                Amount = 450.00m,
                Date = new DateTime(2024, 7, 10),
                Category = DeductionCategory.Travel,
                Notes = "Conference in Chicago",
                HasReceipt = true,
            },
            new Deduction
            {
                DeductionId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2024.TaxYearId,
                Description = "Medical Expenses",
                Amount = 850.00m,
                Date = new DateTime(2024, 8, 25),
                Category = DeductionCategory.Medical,
                Notes = "Dental work",
                HasReceipt = true,
            },
            new Deduction
            {
                DeductionId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2024.TaxYearId,
                Description = "Charitable Donation",
                Amount = 500.00m,
                Date = new DateTime(2024, 12, 1),
                Category = DeductionCategory.Charity,
                Notes = "Annual donation to local food bank",
                HasReceipt = true,
            },
        };

        context.Deductions.AddRange(deductions2024);

        var deductions2023 = new List<Deduction>
        {
            new Deduction
            {
                DeductionId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2023.TaxYearId,
                Description = "Business Software Subscription",
                Amount = 1200.00m,
                Date = new DateTime(2023, 1, 15),
                Category = DeductionCategory.Business,
                Notes = "Annual software licenses",
                HasReceipt = true,
            },
            new Deduction
            {
                DeductionId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYearId = taxYear2023.TaxYearId,
                Description = "Vehicle Expenses",
                Amount = 3500.00m,
                Date = new DateTime(2023, 6, 30),
                Category = DeductionCategory.Business,
                Notes = "Mileage and maintenance for business use",
                HasReceipt = true,
            },
        };

        context.Deductions.AddRange(deductions2023);

        var receipts = new List<Receipt>
        {
            new Receipt
            {
                ReceiptId = Guid.Parse("aaaaaaaa-1111-1111-1111-111111111111"),
                DeductionId = deductions2024[0].DeductionId,
                FileName = "home_office_receipt.pdf",
                FileUrl = "/receipts/2024/home_office_receipt.pdf",
                UploadDate = new DateTime(2024, 3, 16),
                Notes = "Receipt from office supply store",
            },
            new Receipt
            {
                ReceiptId = Guid.Parse("bbbbbbbb-1111-1111-1111-111111111111"),
                DeductionId = deductions2024[1].DeductionId,
                FileName = "course_receipt.pdf",
                FileUrl = "/receipts/2024/course_receipt.pdf",
                UploadDate = new DateTime(2024, 5, 21),
                Notes = "Email confirmation and receipt",
            },
            new Receipt
            {
                ReceiptId = Guid.Parse("cccccccc-1111-1111-1111-111111111111"),
                DeductionId = deductions2024[2].DeductionId,
                FileName = "travel_expenses.pdf",
                FileUrl = "/receipts/2024/travel_expenses.pdf",
                UploadDate = new DateTime(2024, 7, 11),
                Notes = "Hotel and flight receipts",
            },
        };

        context.Receipts.AddRange(receipts);

        await context.SaveChangesAsync();
    }
}
