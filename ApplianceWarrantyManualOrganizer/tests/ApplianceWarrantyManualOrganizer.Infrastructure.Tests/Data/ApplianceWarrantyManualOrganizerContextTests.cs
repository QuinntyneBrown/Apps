// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ApplianceWarrantyManualOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the ApplianceWarrantyManualOrganizerContext.
/// </summary>
[TestFixture]
public class ApplianceWarrantyManualOrganizerContextTests
{
    private ApplianceWarrantyManualOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplianceWarrantyManualOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplianceWarrantyManualOrganizerContext(options);
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
    /// Tests that Appliances can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Appliances_CanAddAndRetrieve()
    {
        // Arrange
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Refrigerator",
            ApplianceType = ApplianceType.Refrigerator,
            Brand = "Samsung",
            ModelNumber = "RF123",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Appliances.Add(appliance);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Appliances.FindAsync(appliance.ApplianceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Refrigerator"));
        Assert.That(retrieved.ApplianceType, Is.EqualTo(ApplianceType.Refrigerator));
    }

    /// <summary>
    /// Tests that Warranties can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Warranties_CanAddAndRetrieve()
    {
        // Arrange
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Oven",
            ApplianceType = ApplianceType.Oven,
            CreatedAt = DateTime.UtcNow,
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            ApplianceId = appliance.ApplianceId,
            Provider = "Test Provider",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            CoverageDetails = "Full coverage",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Appliances.Add(appliance);
        _context.Warranties.Add(warranty);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Warranties.FindAsync(warranty.WarrantyId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Provider, Is.EqualTo("Test Provider"));
        Assert.That(retrieved.CoverageDetails, Is.EqualTo("Full coverage"));
    }

    /// <summary>
    /// Tests that Manuals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Manuals_CanAddAndRetrieve()
    {
        // Arrange
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Dishwasher",
            ApplianceType = ApplianceType.Dishwasher,
            CreatedAt = DateTime.UtcNow,
        };

        var manual = new Manual
        {
            ManualId = Guid.NewGuid(),
            ApplianceId = appliance.ApplianceId,
            Title = "User Manual",
            FileUrl = "https://example.com/manual.pdf",
            FileType = "PDF",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Appliances.Add(appliance);
        _context.Manuals.Add(manual);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Manuals.FindAsync(manual.ManualId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("User Manual"));
        Assert.That(retrieved.FileType, Is.EqualTo("PDF"));
    }

    /// <summary>
    /// Tests that ServiceRecords can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ServiceRecords_CanAddAndRetrieve()
    {
        // Arrange
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Washer",
            ApplianceType = ApplianceType.WasherDryer,
            CreatedAt = DateTime.UtcNow,
        };

        var serviceRecord = new ServiceRecord
        {
            ServiceRecordId = Guid.NewGuid(),
            ApplianceId = appliance.ApplianceId,
            ServiceDate = DateTime.UtcNow,
            ServiceProvider = "Test Repair Shop",
            Description = "Fixed leak",
            Cost = 150.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Appliances.Add(appliance);
        _context.ServiceRecords.Add(serviceRecord);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ServiceRecords.FindAsync(serviceRecord.ServiceRecordId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Fixed leak"));
        Assert.That(retrieved.Cost, Is.EqualTo(150.00m));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedWarranties()
    {
        // Arrange
        var appliance = new Appliance
        {
            ApplianceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Appliance",
            ApplianceType = ApplianceType.Refrigerator,
            CreatedAt = DateTime.UtcNow,
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            ApplianceId = appliance.ApplianceId,
            Provider = "Test Provider",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Appliances.Add(appliance);
        _context.Warranties.Add(warranty);
        await _context.SaveChangesAsync();

        // Act
        _context.Appliances.Remove(appliance);
        await _context.SaveChangesAsync();

        var retrievedWarranty = await _context.Warranties.FindAsync(warranty.WarrantyId);

        // Assert
        Assert.That(retrievedWarranty, Is.Null);
    }
}
