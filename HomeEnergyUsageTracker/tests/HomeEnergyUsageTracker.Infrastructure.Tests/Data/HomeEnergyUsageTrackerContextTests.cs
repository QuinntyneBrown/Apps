// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeEnergyUsageTrackerContext.
/// </summary>
[TestFixture]
public class HomeEnergyUsageTrackerContextTests
{
    private HomeEnergyUsageTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeEnergyUsageTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeEnergyUsageTrackerContext(options);
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
    /// Tests that UtilityBills can be added and retrieved.
    /// </summary>
    [Test]
    public async Task UtilityBills_CanAddAndRetrieve()
    {
        // Arrange
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Electricity,
            BillingDate = DateTime.UtcNow,
            Amount = 125.50m,
            UsageAmount = 850.0m,
            Unit = "kWh",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.UtilityBills.Add(utilityBill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.UtilityBills.FindAsync(utilityBill.UtilityBillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.UtilityType, Is.EqualTo(UtilityType.Electricity));
        Assert.That(retrieved.Amount, Is.EqualTo(125.50m));
    }

    /// <summary>
    /// Tests that Usages can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Usages_CanAddAndRetrieve()
    {
        // Arrange
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Gas,
            BillingDate = DateTime.UtcNow,
            Amount = 75.00m,
            CreatedAt = DateTime.UtcNow,
        };

        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = utilityBill.UtilityBillId,
            Date = DateTime.UtcNow,
            Amount = 25.5m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.UtilityBills.Add(utilityBill);
        _context.Usages.Add(usage);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Usages.FindAsync(usage.UsageId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.UtilityBillId, Is.EqualTo(utilityBill.UtilityBillId));
        Assert.That(retrieved.Amount, Is.EqualTo(25.5m));
    }

    /// <summary>
    /// Tests that SavingsTips can be added and retrieved.
    /// </summary>
    [Test]
    public async Task SavingsTips_CanAddAndRetrieve()
    {
        // Arrange
        var savingsTip = new SavingsTip
        {
            SavingsTipId = Guid.NewGuid(),
            Title = "Test Tip",
            Description = "This is a test tip",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.SavingsTips.Add(savingsTip);
        await _context.SaveChangesAsync();

        var retrieved = await _context.SavingsTips.FindAsync(savingsTip.SavingsTipId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Tip"));
        Assert.That(retrieved.Description, Is.EqualTo("This is a test tip"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedUsages()
    {
        // Arrange
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Water,
            BillingDate = DateTime.UtcNow,
            Amount = 50.00m,
            CreatedAt = DateTime.UtcNow,
        };

        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = utilityBill.UtilityBillId,
            Date = DateTime.UtcNow,
            Amount = 15.0m,
            CreatedAt = DateTime.UtcNow,
        };

        _context.UtilityBills.Add(utilityBill);
        _context.Usages.Add(usage);
        await _context.SaveChangesAsync();

        // Act
        _context.UtilityBills.Remove(utilityBill);
        await _context.SaveChangesAsync();

        var retrievedUsage = await _context.Usages.FindAsync(usage.UsageId);

        // Assert
        Assert.That(retrievedUsage, Is.Null);
    }
}
