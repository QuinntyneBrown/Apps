// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Tests;

/// <summary>
/// Tests for Carpool feature handlers.
/// </summary>
[TestFixture]
public class CarpoolFeatureTests
{
    private Mock<IKidsActivitySportsTrackerContext> _mockContext = null!;
    private List<Core.Carpool> _carpools = null!;

    [SetUp]
    public void Setup()
    {
        _carpools = new List<Core.Carpool>();
        _mockContext = new Mock<IKidsActivitySportsTrackerContext>();
        _mockContext.Setup(c => c.Carpools).Returns(TestHelpers.CreateMockDbSet(_carpools).Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    [Test]
    public async Task CreateCarpoolCommand_CreatesCarpool()
    {
        // Arrange
        var handler = new CreateCarpoolCommandHandler(_mockContext.Object);
        var command = new CreateCarpoolCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Tuesday Practice Carpool",
            DriverName = "Jane Smith",
            DriverContact = "555-1234",
            PickupLocation = "123 Main St"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.DriverName, Is.EqualTo(command.DriverName));
        Assert.That(_carpools.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetCarpoolsQuery_ReturnsAllCarpools()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _carpools.Add(new Core.Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = userId,
            Name = "Monday Carpool",
            DriverName = "John Doe"
        });
        _carpools.Add(new Core.Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = userId,
            Name = "Wednesday Carpool",
            DriverName = "Jane Smith"
        });

        var handler = new GetCarpoolsQueryHandler(_mockContext.Object);
        var query = new GetCarpoolsQuery { UserId = userId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetCarpoolByIdQuery_ReturnsCarpool()
    {
        // Arrange
        var carpoolId = Guid.NewGuid();
        _carpools.Add(new Core.Carpool
        {
            CarpoolId = carpoolId,
            UserId = Guid.NewGuid(),
            Name = "Tuesday Carpool",
            DriverName = "John Doe"
        });

        var handler = new GetCarpoolByIdQueryHandler(_mockContext.Object);
        var query = new GetCarpoolByIdQuery { CarpoolId = carpoolId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.CarpoolId, Is.EqualTo(carpoolId));
    }

    [Test]
    public async Task UpdateCarpoolCommand_UpdatesCarpool()
    {
        // Arrange
        var carpoolId = Guid.NewGuid();
        _carpools.Add(new Core.Carpool
        {
            CarpoolId = carpoolId,
            UserId = Guid.NewGuid(),
            Name = "Tuesday Carpool",
            DriverName = "John Doe",
            DriverContact = "555-1234"
        });

        var handler = new UpdateCarpoolCommandHandler(_mockContext.Object);
        var command = new UpdateCarpoolCommand
        {
            CarpoolId = carpoolId,
            Name = "Tuesday Updated Carpool",
            DriverName = "Jane Smith",
            DriverContact = "555-5678"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.Name, Is.EqualTo("Tuesday Updated Carpool"));
        Assert.That(result.DriverName, Is.EqualTo("Jane Smith"));
        Assert.That(result.DriverContact, Is.EqualTo("555-5678"));
    }

    [Test]
    public void UpdateCarpoolCommand_ThrowsWhenCarpoolNotFound()
    {
        // Arrange
        var handler = new UpdateCarpoolCommandHandler(_mockContext.Object);
        var command = new UpdateCarpoolCommand
        {
            CarpoolId = Guid.NewGuid(),
            Name = "Tuesday Carpool",
            DriverName = "John Doe"
        };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task DeleteCarpoolCommand_DeletesCarpool()
    {
        // Arrange
        var carpoolId = Guid.NewGuid();
        _carpools.Add(new Core.Carpool
        {
            CarpoolId = carpoolId,
            UserId = Guid.NewGuid(),
            Name = "Tuesday Carpool",
            DriverName = "John Doe"
        });

        var handler = new DeleteCarpoolCommandHandler(_mockContext.Object);
        var command = new DeleteCarpoolCommand { CarpoolId = carpoolId };

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(_carpools.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteCarpoolCommand_ThrowsWhenCarpoolNotFound()
    {
        // Arrange
        var handler = new DeleteCarpoolCommandHandler(_mockContext.Object);
        var command = new DeleteCarpoolCommand { CarpoolId = Guid.NewGuid() };

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
}
