// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Infrastructure.Tests;

/// <summary>
/// Unit tests for the HomeMaintenanceScheduleContext.
/// </summary>
[TestFixture]
public class HomeMaintenanceScheduleContextTests
{
    private HomeMaintenanceScheduleContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeMaintenanceScheduleContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeMaintenanceScheduleContext(options);
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
    /// Tests that MaintenanceTasks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MaintenanceTasks_CanAddAndRetrieve()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Task",
            Description = "Test Description",
            MaintenanceType = MaintenanceType.Preventive,
            Status = TaskStatus.Scheduled,
            DueDate = DateTime.UtcNow.AddDays(30),
            Priority = 2,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MaintenanceTasks.Add(task);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MaintenanceTasks.FindAsync(task.MaintenanceTaskId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Task"));
        Assert.That(retrieved.MaintenanceType, Is.EqualTo(MaintenanceType.Preventive));
    }

    /// <summary>
    /// Tests that Contractors can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Contractors_CanAddAndRetrieve()
    {
        // Arrange
        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Contractor",
            Specialty = "HVAC",
            PhoneNumber = "555-1234",
            IsInsured = true,
            Rating = 5,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Contractors.Add(contractor);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contractors.FindAsync(contractor.ContractorId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Contractor"));
        Assert.That(retrieved.Specialty, Is.EqualTo("HVAC"));
    }

    /// <summary>
    /// Tests that ServiceLogs can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ServiceLogs_CanAddAndRetrieve()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Task",
            MaintenanceType = MaintenanceType.Corrective,
            Status = TaskStatus.Completed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = task.MaintenanceTaskId,
            ServiceDate = DateTime.UtcNow,
            Description = "Test service",
            Cost = 150m,
            LaborHours = 2.5m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MaintenanceTasks.Add(task);
        _context.ServiceLogs.Add(serviceLog);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ServiceLogs.FindAsync(serviceLog.ServiceLogId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test service"));
        Assert.That(retrieved.Cost, Is.EqualTo(150m));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedServiceLogs()
    {
        // Arrange
        var task = new MaintenanceTask
        {
            MaintenanceTaskId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Task",
            MaintenanceType = MaintenanceType.Inspection,
            Status = TaskStatus.Completed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        var serviceLog = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = task.MaintenanceTaskId,
            ServiceDate = DateTime.UtcNow,
            Description = "Test service",
            CreatedAt = DateTime.UtcNow,
        };

        _context.MaintenanceTasks.Add(task);
        _context.ServiceLogs.Add(serviceLog);
        await _context.SaveChangesAsync();

        // Act
        _context.MaintenanceTasks.Remove(task);
        await _context.SaveChangesAsync();

        var retrievedServiceLog = await _context.ServiceLogs.FindAsync(serviceLog.ServiceLogId);

        // Assert
        Assert.That(retrievedServiceLog, Is.Null);
    }
}
