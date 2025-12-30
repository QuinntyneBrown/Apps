using SalaryCompensationTracker.Api.Features.Compensations;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.Compensations;

[TestFixture]
public class CreateCompensationCommandTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateCompensationCommandHandler>> _loggerMock = null!;
    private CreateCompensationCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateCompensationCommandHandler>>();
        _handler = new CreateCompensationCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesCompensation()
    {
        // Arrange
        var command = new CreateCompensationCommand
        {
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.FullTime,
            Employer = "Tech Corp",
            JobTitle = "Software Engineer",
            BaseSalary = 100000m,
            Currency = "USD",
            Bonus = 10000m,
            StockValue = 5000m,
            OtherCompensation = 2000m,
            EffectiveDate = DateTime.UtcNow,
            Notes = "Annual compensation",
        };

        var compensations = new List<Compensation>();
        var mockDbSet = TestHelpers.CreateMockDbSet(compensations);
        _contextMock.Setup(c => c.Compensations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Employer, Is.EqualTo(command.Employer));
        Assert.That(result.JobTitle, Is.EqualTo(command.JobTitle));
        Assert.That(result.BaseSalary, Is.EqualTo(command.BaseSalary));
        Assert.That(result.Bonus, Is.EqualTo(command.Bonus));
        Assert.That(result.StockValue, Is.EqualTo(command.StockValue));
        Assert.That(result.OtherCompensation, Is.EqualTo(command.OtherCompensation));
        Assert.That(result.TotalCompensation, Is.EqualTo(117000m));
        Assert.That(result.CompensationType, Is.EqualTo(command.CompensationType));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_MinimalCommand_CreatesCompensationWithDefaults()
    {
        // Arrange
        var command = new CreateCompensationCommand
        {
            UserId = Guid.NewGuid(),
            CompensationType = CompensationType.Contract,
            Employer = "Startup Inc",
            JobTitle = "Consultant",
            BaseSalary = 80000m,
            Currency = "USD",
            EffectiveDate = DateTime.UtcNow,
        };

        var compensations = new List<Compensation>();
        var mockDbSet = TestHelpers.CreateMockDbSet(compensations);
        _contextMock.Setup(c => c.Compensations).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Employer, Is.EqualTo(command.Employer));
        Assert.That(result.JobTitle, Is.EqualTo(command.JobTitle));
        Assert.That(result.BaseSalary, Is.EqualTo(command.BaseSalary));
        Assert.That(result.TotalCompensation, Is.EqualTo(80000m));
        Assert.That(result.Bonus, Is.Null);
        Assert.That(result.StockValue, Is.Null);
        Assert.That(result.OtherCompensation, Is.Null);
    }
}
