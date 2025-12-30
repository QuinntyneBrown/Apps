using LifeAdminDashboard.Api.Features.Renewals;
using Microsoft.Extensions.Logging;
using Moq;

namespace LifeAdminDashboard.Api.Tests.Features.Renewals;

[TestFixture]
public class CreateRenewalCommandTests
{
    private Mock<ILifeAdminDashboardContext> _contextMock = null!;
    private Mock<ILogger<CreateRenewalCommandHandler>> _loggerMock = null!;
    private CreateRenewalCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILifeAdminDashboardContext>();
        _loggerMock = new Mock<ILogger<CreateRenewalCommandHandler>>();
        _handler = new CreateRenewalCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesRenewal()
    {
        // Arrange
        var command = new CreateRenewalCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Netflix Subscription",
            RenewalType = "Subscription",
            Provider = "Netflix",
            RenewalDate = DateTime.UtcNow.AddMonths(1),
            Cost = 15.99m,
            Frequency = "Monthly",
            IsAutoRenewal = true,
            Notes = "Premium plan",
        };

        var renewals = new List<Renewal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(renewals);
        _contextMock.Setup(c => c.Renewals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.RenewalType, Is.EqualTo(command.RenewalType));
        Assert.That(result.Provider, Is.EqualTo(command.Provider));
        Assert.That(result.RenewalDate, Is.EqualTo(command.RenewalDate));
        Assert.That(result.Cost, Is.EqualTo(command.Cost));
        Assert.That(result.Frequency, Is.EqualTo(command.Frequency));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.IsActive, Is.True);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithAutoRenewal_IncludesAutoRenewalSetting()
    {
        // Arrange
        var command = new CreateRenewalCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Car Insurance",
            RenewalType = "Insurance",
            Provider = "State Farm",
            RenewalDate = DateTime.UtcNow.AddYears(1),
            Cost = 1200m,
            Frequency = "Yearly",
            IsAutoRenewal = false,
        };

        var renewals = new List<Renewal>();
        var mockDbSet = TestHelpers.CreateMockDbSet(renewals);
        _contextMock.Setup(c => c.Renewals).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.IsAutoRenewal, Is.False);
    }
}
