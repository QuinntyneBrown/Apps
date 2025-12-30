// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Api.Features.Maintenance;
using HomeGymEquipmentManager.Api.Features.Maintenance.Commands;
using HomeGymEquipmentManager.Api.Features.Maintenance.Queries;
using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Tests;

public class MaintenanceTests
{
    private IHomeGymEquipmentManagerContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HomeGymEquipmentManager.Infrastructure.HomeGymEquipmentManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new HomeGymEquipmentManager.Infrastructure.HomeGymEquipmentManagerContext(options);
    }

    [Test]
    public async Task CreateMaintenanceCommand_ShouldCreateMaintenance()
    {
        // Arrange
        var handler = new CreateMaintenanceCommandHandler(_context);
        var command = new CreateMaintenanceCommand
        {
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow,
            Description = "Oil change",
            Cost = 50.00m,
            NextMaintenanceDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo("Oil change"));
        Assert.That(result.Cost, Is.EqualTo(50.00m));
    }

    [Test]
    public async Task GetMaintenanceByIdQuery_ShouldReturnMaintenance()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var maintenance = new Maintenance
        {
            MaintenanceId = maintenanceId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow,
            Description = "Belt adjustment"
        };
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync();

        var handler = new GetMaintenanceByIdQueryHandler(_context);
        var query = new GetMaintenanceByIdQuery { MaintenanceId = maintenanceId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.MaintenanceId, Is.EqualTo(maintenanceId));
        Assert.That(result.Description, Is.EqualTo("Belt adjustment"));
    }

    [Test]
    public async Task UpdateMaintenanceCommand_ShouldUpdateMaintenance()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var maintenance = new Maintenance
        {
            MaintenanceId = maintenanceId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow,
            Description = "Old Description"
        };
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync();

        var handler = new UpdateMaintenanceCommandHandler(_context);
        var command = new UpdateMaintenanceCommand
        {
            MaintenanceId = maintenanceId,
            MaintenanceDate = DateTime.UtcNow.AddDays(1),
            Description = "Updated Description",
            Cost = 100.00m
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Description, Is.EqualTo("Updated Description"));
        Assert.That(result.Cost, Is.EqualTo(100.00m));
    }

    [Test]
    public async Task DeleteMaintenanceCommand_ShouldDeleteMaintenance()
    {
        // Arrange
        var maintenanceId = Guid.NewGuid();
        var maintenance = new Maintenance
        {
            MaintenanceId = maintenanceId,
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow,
            Description = "To Delete"
        };
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync();

        var handler = new DeleteMaintenanceCommandHandler(_context);
        var command = new DeleteMaintenanceCommand { MaintenanceId = maintenanceId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedMaintenance = await _context.Maintenances.FirstOrDefaultAsync(m => m.MaintenanceId == maintenanceId);
        Assert.That(deletedMaintenance, Is.Null);
    }

    [Test]
    public async Task GetMaintenanceListQuery_ShouldReturnFilteredList()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        _context.Maintenances.Add(new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = equipmentId,
            MaintenanceDate = DateTime.UtcNow,
            Description = "Maintenance 1"
        });
        _context.Maintenances.Add(new Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            MaintenanceDate = DateTime.UtcNow,
            Description = "Maintenance 2"
        });
        await _context.SaveChangesAsync();

        var handler = new GetMaintenanceListQueryHandler(_context);
        var query = new GetMaintenanceListQuery { EquipmentId = equipmentId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Description, Is.EqualTo("Maintenance 1"));
    }
}
