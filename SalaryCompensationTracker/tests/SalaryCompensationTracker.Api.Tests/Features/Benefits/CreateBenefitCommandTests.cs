using SalaryCompensationTracker.Api.Features.Benefits;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.Benefits;

[TestFixture]
public class CreateBenefitCommandTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateBenefitCommandHandler>> _loggerMock = null!;
    private CreateBenefitCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateBenefitCommandHandler>>();
        _handler = new CreateBenefitCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesBenefit()
    {
        // Arrange
        var command = new CreateBenefitCommand
        {
            UserId = Guid.NewGuid(),
            CompensationId = Guid.NewGuid(),
            Name = "Health Insurance",
            Category = "Health",
            Description = "Premium health coverage",
            EstimatedValue = 12000m,
            EmployerContribution = 10000m,
            EmployeeContribution = 2000m,
        };

        var benefits = new List<Benefit>();
        var mockDbSet = TestHelpers.CreateMockDbSet(benefits);
        _contextMock.Setup(c => c.Benefits).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.EstimatedValue, Is.EqualTo(command.EstimatedValue));
        Assert.That(result.EmployerContribution, Is.EqualTo(command.EmployerContribution));
        Assert.That(result.EmployeeContribution, Is.EqualTo(command.EmployeeContribution));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.CompensationId, Is.EqualTo(command.CompensationId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_MinimalCommand_CreatesBenefitWithDefaults()
    {
        // Arrange
        var command = new CreateBenefitCommand
        {
            UserId = Guid.NewGuid(),
            Name = "PTO",
            Category = "Time Off",
        };

        var benefits = new List<Benefit>();
        var mockDbSet = TestHelpers.CreateMockDbSet(benefits);
        _contextMock.Setup(c => c.Benefits).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Category, Is.EqualTo(command.Category));
        Assert.That(result.Description, Is.Null);
        Assert.That(result.EstimatedValue, Is.Null);
        Assert.That(result.EmployerContribution, Is.Null);
        Assert.That(result.EmployeeContribution, Is.Null);
    }
}
