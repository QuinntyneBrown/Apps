// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Infrastructure.Tests;

/// <summary>
/// Unit tests for the CarModificationPartsDatabaseContext.
/// </summary>
[TestFixture]
public class CarModificationPartsDatabaseContextTests
{
    private CarModificationPartsDatabaseContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CarModificationPartsDatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CarModificationPartsDatabaseContext(options);
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
    /// Tests that Modifications can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Modifications_CanAddAndRetrieve()
    {
        // Arrange
        var modification = new Modification
        {
            ModificationId = Guid.NewGuid(),
            Name = "Test Modification",
            Category = ModCategory.Engine,
            Description = "Test Description",
            Manufacturer = "Test Manufacturer",
            EstimatedCost = 500.00m,
            DifficultyLevel = 3,
            CompatibleVehicles = new List<string> { "Test Vehicle 1", "Test Vehicle 2" },
            RequiredTools = new List<string> { "Wrench", "Screwdriver" },
        };

        // Act
        _context.Modifications.Add(modification);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Modifications.FindAsync(modification.ModificationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Modification"));
        Assert.That(retrieved.Category, Is.EqualTo(ModCategory.Engine));
        Assert.That(retrieved.CompatibleVehicles.Count, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests that Parts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Parts_CanAddAndRetrieve()
    {
        // Arrange
        var part = new Part
        {
            PartId = Guid.NewGuid(),
            Name = "Test Part",
            PartNumber = "TEST-123",
            Manufacturer = "Test Manufacturer",
            Description = "Test Description",
            Price = 99.99m,
            Category = ModCategory.Suspension,
            CompatibleVehicles = new List<string> { "Test Vehicle" },
            InStock = true,
        };

        // Act
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Parts.FindAsync(part.PartId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Part"));
        Assert.That(retrieved.Price, Is.EqualTo(99.99m));
        Assert.That(retrieved.InStock, Is.True);
    }

    /// <summary>
    /// Tests that Installations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Installations_CanAddAndRetrieve()
    {
        // Arrange
        var modification = new Modification
        {
            ModificationId = Guid.NewGuid(),
            Name = "Test Modification",
            Category = ModCategory.Brakes,
            Description = "Test Description",
        };

        var installation = new Installation
        {
            InstallationId = Guid.NewGuid(),
            ModificationId = modification.ModificationId,
            VehicleInfo = "2020 Test Car",
            InstallationDate = DateTime.UtcNow,
            InstalledBy = "Test Installer",
            InstallationCost = 200.00m,
            PartsCost = 500.00m,
            LaborHours = 3.0m,
            PartsUsed = new List<string> { "Part 1", "Part 2" },
            Photos = new List<string> { "photo1.jpg", "photo2.jpg" },
            IsCompleted = true,
            SatisfactionRating = 5,
        };

        // Act
        _context.Modifications.Add(modification);
        _context.Installations.Add(installation);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Installations.FindAsync(installation.InstallationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VehicleInfo, Is.EqualTo("2020 Test Car"));
        Assert.That(retrieved.IsCompleted, Is.True);
        Assert.That(retrieved.PartsUsed.Count, Is.EqualTo(2));
    }
}
