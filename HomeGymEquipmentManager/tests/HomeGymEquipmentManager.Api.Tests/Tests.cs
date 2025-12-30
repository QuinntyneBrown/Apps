using HomeGymEquipmentManager.Api.Features.Equipment;
using HomeGymEquipmentManager.Api.Features.Equipment.Commands;
using HomeGymEquipmentManager.Api.Features.Equipment.Queries;
using HomeGymEquipmentManager.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HomeGymEquipmentManager.Api.Tests;

public class EquipmentTests
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
    public async Task CreateEquipmentCommand_ShouldCreateEquipment()
    {
        // Arrange
        var handler = new CreateEquipmentCommandHandler(_context);
        var command = new CreateEquipmentCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Treadmill",
            EquipmentType = EquipmentType.Cardio,
            Brand = "NordicTrack",
            Model = "C990",
            PurchasePrice = 999.99m,
            Location = "Home Gym"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Treadmill"));
        Assert.That(result.EquipmentType, Is.EqualTo(EquipmentType.Cardio));
        Assert.That(result.Brand, Is.EqualTo("NordicTrack"));
        Assert.That(result.IsActive, Is.True);
    }

    [Test]
    public async Task GetEquipmentByIdQuery_ShouldReturnEquipment()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var equipment = new Equipment
        {
            EquipmentId = equipmentId,
            UserId = Guid.NewGuid(),
            Name = "Dumbbells",
            EquipmentType = EquipmentType.Strength,
            IsActive = true
        };
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        var handler = new GetEquipmentByIdQueryHandler(_context);
        var query = new GetEquipmentByIdQuery { EquipmentId = equipmentId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.EquipmentId, Is.EqualTo(equipmentId));
        Assert.That(result.Name, Is.EqualTo("Dumbbells"));
    }

    [Test]
    public async Task UpdateEquipmentCommand_ShouldUpdateEquipment()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var equipment = new Equipment
        {
            EquipmentId = equipmentId,
            UserId = Guid.NewGuid(),
            Name = "Old Name",
            EquipmentType = EquipmentType.Cardio,
            IsActive = true
        };
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        var handler = new UpdateEquipmentCommandHandler(_context);
        var command = new UpdateEquipmentCommand
        {
            EquipmentId = equipmentId,
            Name = "Updated Name",
            EquipmentType = EquipmentType.Strength,
            IsActive = false
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
        Assert.That(result.EquipmentType, Is.EqualTo(EquipmentType.Strength));
        Assert.That(result.IsActive, Is.False);
    }

    [Test]
    public async Task DeleteEquipmentCommand_ShouldDeleteEquipment()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        var equipment = new Equipment
        {
            EquipmentId = equipmentId,
            UserId = Guid.NewGuid(),
            Name = "To Delete",
            EquipmentType = EquipmentType.Accessory,
            IsActive = true
        };
        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync();

        var handler = new DeleteEquipmentCommandHandler(_context);
        var command = new DeleteEquipmentCommand { EquipmentId = equipmentId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedEquipment = await _context.Equipment.FirstOrDefaultAsync(e => e.EquipmentId == equipmentId);
        Assert.That(deletedEquipment, Is.Null);
    }

    [Test]
    public async Task GetEquipmentListQuery_ShouldReturnFilteredList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _context.Equipment.Add(new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = userId,
            Name = "Equipment 1",
            EquipmentType = EquipmentType.Cardio,
            IsActive = true
        });
        _context.Equipment.Add(new Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Equipment 2",
            EquipmentType = EquipmentType.Strength,
            IsActive = false
        });
        await _context.SaveChangesAsync();

        var handler = new GetEquipmentListQueryHandler(_context);
        var query = new GetEquipmentListQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Equipment 1"));
    }
}
