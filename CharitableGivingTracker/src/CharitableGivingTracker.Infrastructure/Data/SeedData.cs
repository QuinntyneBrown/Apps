// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using CharitableGivingTracker.Core.Models.UserAggregate;
using CharitableGivingTracker.Core.Models.UserAggregate.Entities;
using CharitableGivingTracker.Core.Services;
namespace CharitableGivingTracker.Infrastructure;

/// <summary>
/// Provides seed data for the CharitableGivingTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(CharitableGivingTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Organizations.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedOrganizationsAsync(context);
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

    private static async Task SeedOrganizationsAsync(CharitableGivingTrackerContext context)
    {
        var organizations = new List<Organization>
        {
            new Organization
            {
                OrganizationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "American Red Cross",
                EIN = "53-0196605",
                Address = "National Headquarters, 431 18th Street NW, Washington, DC 20006",
                Website = "https://www.redcross.org",
                Is501c3 = true,
                Notes = "Disaster relief and emergency assistance",
            },
            new Organization
            {
                OrganizationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Doctors Without Borders",
                EIN = "13-3433452",
                Address = "40 Rector Street, 16th Floor, New York, NY 10006",
                Website = "https://www.doctorswithoutborders.org",
                Is501c3 = true,
                Notes = "International medical humanitarian organization",
            },
            new Organization
            {
                OrganizationId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Local Food Bank",
                EIN = "12-3456789",
                Address = "123 Main Street, Anytown, USA",
                Website = "https://www.localfoodbank.org",
                Is501c3 = true,
                Notes = "Community food assistance program",
            },
        };

        context.Organizations.AddRange(organizations);

        // Add sample donations
        var donations = new List<Donation>
        {
            new Donation
            {
                DonationId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                OrganizationId = organizations[0].OrganizationId,
                Amount = 500.00m,
                DonationDate = new DateTime(2024, 1, 15),
                DonationType = DonationType.Cash,
                ReceiptNumber = "ARC-2024-001",
                IsTaxDeductible = true,
                Notes = "Annual contribution",
            },
            new Donation
            {
                DonationId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                OrganizationId = organizations[1].OrganizationId,
                Amount = 1000.00m,
                DonationDate = new DateTime(2024, 3, 20),
                DonationType = DonationType.Cash,
                ReceiptNumber = "MSF-2024-042",
                IsTaxDeductible = true,
                Notes = "Emergency relief fund",
            },
            new Donation
            {
                DonationId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                OrganizationId = organizations[2].OrganizationId,
                Amount = 250.00m,
                DonationDate = new DateTime(2024, 6, 10),
                DonationType = DonationType.InKind,
                ReceiptNumber = "LFB-2024-156",
                IsTaxDeductible = true,
                Notes = "Food and clothing donation",
            },
            new Donation
            {
                DonationId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                OrganizationId = organizations[0].OrganizationId,
                Amount = 300.00m,
                DonationDate = new DateTime(2024, 9, 5),
                DonationType = DonationType.Cash,
                ReceiptNumber = "ARC-2024-187",
                IsTaxDeductible = true,
                Notes = "Hurricane relief fund",
            },
        };

        context.Donations.AddRange(donations);

        // Add sample tax reports
        var taxReports = new List<TaxReport>
        {
            new TaxReport
            {
                TaxReportId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                TaxYear = 2024,
                TotalCashDonations = 1800.00m,
                TotalNonCashDonations = 250.00m,
                TotalDeductibleAmount = 2050.00m,
                GeneratedDate = DateTime.UtcNow,
                Notes = "Annual tax summary for 2024",
            },
        };

        context.TaxReports.AddRange(taxReports);

        await context.SaveChangesAsync();
    }
}
