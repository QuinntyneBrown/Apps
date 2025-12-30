// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Tests;

[TestFixture]
public class RefillTests
{
    [Test]
    public void RefillToDto_MapsAllProperties()
    {
        // Arrange
        var refill = new Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 30,
            PharmacyName = "CVS Pharmacy",
            Cost = 25.50m,
            NextRefillDate = DateTime.UtcNow.AddDays(30),
            RefillsRemaining = 3,
            Notes = "Refill at local pharmacy",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = refill.ToDto();

        // Assert
        Assert.That(dto.RefillId, Is.EqualTo(refill.RefillId));
        Assert.That(dto.UserId, Is.EqualTo(refill.UserId));
        Assert.That(dto.MedicationId, Is.EqualTo(refill.MedicationId));
        Assert.That(dto.RefillDate, Is.EqualTo(refill.RefillDate));
        Assert.That(dto.Quantity, Is.EqualTo(refill.Quantity));
        Assert.That(dto.PharmacyName, Is.EqualTo(refill.PharmacyName));
        Assert.That(dto.Cost, Is.EqualTo(refill.Cost));
        Assert.That(dto.NextRefillDate, Is.EqualTo(refill.NextRefillDate));
        Assert.That(dto.RefillsRemaining, Is.EqualTo(refill.RefillsRemaining));
        Assert.That(dto.Notes, Is.EqualTo(refill.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(refill.CreatedAt));
    }

    [Test]
    public async Task CreateRefillCommand_CreatesNewRefill()
    {
        // Arrange
        var refills = new List<Refill>();
        var mockSet = TestHelpers.CreateMockDbSet(refills);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<CreateRefillCommandHandler>();

        mockContext.Setup(c => c.Refills).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateRefillCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new CreateRefillCommand
        {
            UserId = Guid.NewGuid(),
            MedicationId = Guid.NewGuid(),
            RefillDate = DateTime.UtcNow,
            Quantity = 60,
            PharmacyName = "Walgreens",
            Cost = 15.75m,
            NextRefillDate = DateTime.UtcNow.AddDays(60),
            RefillsRemaining = 2,
            Notes = "Generic brand"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Quantity, Is.EqualTo(command.Quantity));
        Assert.That(result.PharmacyName, Is.EqualTo(command.PharmacyName));
        Assert.That(result.Cost, Is.EqualTo(command.Cost));
        Assert.That(refills.Count, Is.EqualTo(1));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetRefillsQuery_ReturnsAllRefills()
    {
        // Arrange
        var refills = new List<Refill>
        {
            new Refill
            {
                RefillId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                RefillDate = DateTime.UtcNow,
                Quantity = 30,
                PharmacyName = "CVS",
                CreatedAt = DateTime.UtcNow
            },
            new Refill
            {
                RefillId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                RefillDate = DateTime.UtcNow.AddDays(-30),
                Quantity = 60,
                PharmacyName = "Walgreens",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(refills);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<GetRefillsQueryHandler>();

        mockContext.Setup(c => c.Refills).Returns(mockSet.Object);

        var handler = new GetRefillsQueryHandler(mockContext.Object, mockLogger.Object);
        var query = new GetRefillsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task UpdateRefillCommand_UpdatesRefill()
    {
        // Arrange
        var refillId = Guid.NewGuid();
        var refills = new List<Refill>
        {
            new Refill
            {
                RefillId = refillId,
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                RefillDate = DateTime.UtcNow,
                Quantity = 30,
                PharmacyName = "CVS",
                Cost = 20.00m,
                RefillsRemaining = 3
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(refills);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<UpdateRefillCommandHandler>();

        mockContext.Setup(c => c.Refills).Returns(mockSet.Object);
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new UpdateRefillCommandHandler(mockContext.Object, mockLogger.Object);
        var command = new UpdateRefillCommand
        {
            RefillId = refillId,
            RefillDate = DateTime.UtcNow.AddDays(1),
            Quantity = 60,
            PharmacyName = "Walgreens",
            Cost = 18.50m,
            NextRefillDate = DateTime.UtcNow.AddDays(60),
            RefillsRemaining = 2,
            Notes = "Updated refill information"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Quantity, Is.EqualTo(command.Quantity));
        Assert.That(result.PharmacyName, Is.EqualTo(command.PharmacyName));
        Assert.That(result.Cost, Is.EqualTo(command.Cost));
        Assert.That(result.RefillsRemaining, Is.EqualTo(command.RefillsRemaining));
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetRefillByIdQuery_ReturnsRefill_WhenExists()
    {
        // Arrange
        var refillId = Guid.NewGuid();
        var refills = new List<Refill>
        {
            new Refill
            {
                RefillId = refillId,
                UserId = Guid.NewGuid(),
                MedicationId = Guid.NewGuid(),
                RefillDate = DateTime.UtcNow,
                Quantity = 30,
                PharmacyName = "CVS"
            }
        };

        var mockSet = TestHelpers.CreateMockDbSet(refills);
        var mockContext = new Mock<IMedicationReminderSystemContext>();
        var mockLogger = TestHelpers.CreateMockLogger<GetRefillByIdQueryHandler>();

        mockContext.Setup(c => c.Refills).Returns(mockSet.Object);

        var handler = new GetRefillByIdQueryHandler(mockContext.Object, mockLogger.Object);
        var query = new GetRefillByIdQuery(refillId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.RefillId, Is.EqualTo(refillId));
    }
}
