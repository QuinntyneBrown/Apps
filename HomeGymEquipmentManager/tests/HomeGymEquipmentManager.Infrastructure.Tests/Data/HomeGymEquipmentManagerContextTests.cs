// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeGymEquipmentManagerContext.
/// </summary>
[TestFixture]
public class HomeGymEquipmentManagerContextTests
{
    private HomeGymEquipmentManagerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeGymEquipmentManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeGymEquipmentManagerContext(options);
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
    /// Tests that Equipment can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Equipment_CanAddAndRetrieve()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Treadmill",
            EquipmentType = EquipmentType.Cardio,
            Brand = "TestBrand",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Equipment.FindAsync(equipment.EquipmentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Treadmill"));
        Assert.That(retrieved.EquipmentType, Is.EqualTo(EquipmentType.Cardio));
    }

    /// <summary>
    /// Tests that Maintenance records can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Maintenances_CanAddAndRetrieve()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Equipment",
            EquipmentType = EquipmentType.Strength,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = equipment.UserId,
            EquipmentId = equipment.EquipmentId,
            MaintenanceDate = DateTime.UtcNow,
            Description = "Routine maintenance",
            Cost = 25.00m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Equipment.Add(equipment);
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Maintenances.FindAsync(maintenance.MaintenanceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Routine maintenance"));
        Assert.That(retrieved.Cost, Is.EqualTo(25.00m));
    }

    /// <summary>
    /// Tests that WorkoutMappings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task WorkoutMappings_CanAddAndRetrieve()
    {
        // Arrange
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Bench Press",
            MuscleGroup = "Chest",
            Instructions = "Lift weights properly",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.WorkoutMappings.Add(workoutMapping);
        await _context.SaveChangesAsync();

        var retrieved = await _context.WorkoutMappings.FindAsync(workoutMapping.WorkoutMappingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ExerciseName, Is.EqualTo("Bench Press"));
        Assert.That(retrieved.IsFavorite, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedMaintenances()
    {
        // Arrange
        var equipment = new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Equipment",
            EquipmentType = EquipmentType.Cardio,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var maintenance = new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = equipment.UserId,
            EquipmentId = equipment.EquipmentId,
            MaintenanceDate = DateTime.UtcNow,
            Description = "Test maintenance",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Equipment.Add(equipment);
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync();

        // Act
        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync();

        var retrievedMaintenance = await _context.Maintenances.FindAsync(maintenance.MaintenanceId);

        // Assert
        Assert.That(retrievedMaintenance, Is.Null);
    }
}
