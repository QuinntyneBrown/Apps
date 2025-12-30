// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CharitableGivingTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the CharitableGivingTrackerContext.
/// </summary>
[TestFixture]
public class CharitableGivingTrackerContextTests
{
    private CharitableGivingTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CharitableGivingTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CharitableGivingTrackerContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Organizations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Organizations_CanAddAndRetrieve()
    {
        // Arrange
        var organization = new Organization
        {
            OrganizationId = Guid.NewGuid(),
            Name = "Test Charity",
            EIN = "12-3456789",
            Address = "123 Test Street",
            Website = "https://www.testcharity.org",
            Is501c3 = true,
            Notes = "Test notes",
        };

        // Act
        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Organizations.FindAsync(organization.OrganizationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Charity"));
        Assert.That(retrieved.EIN, Is.EqualTo("12-3456789"));
        Assert.That(retrieved.Is501c3, Is.True);
    }

    /// <summary>
    /// Tests that Donations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Donations_CanAddAndRetrieve()
    {
        // Arrange
        var organization = new Organization
        {
            OrganizationId = Guid.NewGuid(),
            Name = "Test Charity",
            Is501c3 = true,
        };

        var donation = new Donation
        {
            DonationId = Guid.NewGuid(),
            OrganizationId = organization.OrganizationId,
            Amount = 500.00m,
            DonationDate = DateTime.UtcNow,
            DonationType = DonationType.Cash,
            ReceiptNumber = "TEST-001",
            IsTaxDeductible = true,
            Notes = "Test donation",
        };

        // Act
        _context.Organizations.Add(organization);
        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Donations.FindAsync(donation.DonationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(500.00m));
        Assert.That(retrieved.DonationType, Is.EqualTo(DonationType.Cash));
        Assert.That(retrieved.IsTaxDeductible, Is.True);
    }

    /// <summary>
    /// Tests that TaxReports can be added and retrieved.
    /// </summary>
    [Test]
    public async Task TaxReports_CanAddAndRetrieve()
    {
        // Arrange
        var taxReport = new TaxReport
        {
            TaxReportId = Guid.NewGuid(),
            TaxYear = 2024,
            TotalCashDonations = 1000.00m,
            TotalNonCashDonations = 500.00m,
            TotalDeductibleAmount = 1500.00m,
            GeneratedDate = DateTime.UtcNow,
            Notes = "Test tax report",
        };

        // Act
        _context.TaxReports.Add(taxReport);
        await _context.SaveChangesAsync();

        var retrieved = await _context.TaxReports.FindAsync(taxReport.TaxReportId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TaxYear, Is.EqualTo(2024));
        Assert.That(retrieved.TotalCashDonations, Is.EqualTo(1000.00m));
        Assert.That(retrieved.TotalDeductibleAmount, Is.EqualTo(1500.00m));
    }
}
