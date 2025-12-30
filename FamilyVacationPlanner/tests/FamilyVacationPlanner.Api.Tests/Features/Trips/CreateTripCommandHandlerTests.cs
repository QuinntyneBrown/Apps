using FamilyVacationPlanner.Api.Features.Trips;
using FamilyVacationPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FamilyVacationPlanner.Api.Tests.Features.Trips;

[TestFixture]
public class CreateTripCommandHandlerTests
{
    private Mock<IFamilyVacationPlannerContext> _mockContext;
    private Mock<ILogger<CreateTripCommandHandler>> _mockLogger;
    private Mock<DbSet<Trip>> _mockDbSet;
    private CreateTripCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockContext = new Mock<IFamilyVacationPlannerContext>();
        _mockLogger = new Mock<ILogger<CreateTripCommandHandler>>();
        _mockDbSet = new Mock<DbSet<Trip>>();

        _mockContext.Setup(c => c.Trips).Returns(_mockDbSet.Object);
        _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _handler = new CreateTripCommandHandler(_mockContext.Object, _mockLogger.Object);
    }

    [Test]
    public async Task Handle_ShouldCreateTrip_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateTripCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Summer Vacation",
            Destination = "Hawaii",
            StartDate = DateTime.UtcNow.AddMonths(1),
            EndDate = DateTime.UtcNow.AddMonths(1).AddDays(7)
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Destination, Is.EqualTo(command.Destination));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _mockDbSet.Verify(m => m.Add(It.IsAny<Trip>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
