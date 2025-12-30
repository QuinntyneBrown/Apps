using SalaryCompensationTracker.Api.Features.MarketComparisons;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.MarketComparisons;

[TestFixture]
public class CreateMarketComparisonCommandTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<CreateMarketComparisonCommandHandler>> _loggerMock = null!;
    private CreateMarketComparisonCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<CreateMarketComparisonCommandHandler>>();
        _handler = new CreateMarketComparisonCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesMarketComparison()
    {
        // Arrange
        var command = new CreateMarketComparisonCommand
        {
            UserId = Guid.NewGuid(),
            JobTitle = "Software Engineer",
            Location = "San Francisco, CA",
            ExperienceLevel = "Senior",
            MinSalary = 120000m,
            MaxSalary = 180000m,
            MedianSalary = 150000m,
            DataSource = "Glassdoor",
            ComparisonDate = DateTime.UtcNow,
            Notes = "Market data for 2024",
        };

        var marketComparisons = new List<MarketComparison>();
        var mockDbSet = TestHelpers.CreateMockDbSet(marketComparisons);
        _contextMock.Setup(c => c.MarketComparisons).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.JobTitle, Is.EqualTo(command.JobTitle));
        Assert.That(result.Location, Is.EqualTo(command.Location));
        Assert.That(result.ExperienceLevel, Is.EqualTo(command.ExperienceLevel));
        Assert.That(result.MinSalary, Is.EqualTo(command.MinSalary));
        Assert.That(result.MaxSalary, Is.EqualTo(command.MaxSalary));
        Assert.That(result.MedianSalary, Is.EqualTo(command.MedianSalary));
        Assert.That(result.DataSource, Is.EqualTo(command.DataSource));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_MinimalCommand_CreatesMarketComparisonWithDefaults()
    {
        // Arrange
        var command = new CreateMarketComparisonCommand
        {
            UserId = Guid.NewGuid(),
            JobTitle = "Data Analyst",
            Location = "Remote",
            ComparisonDate = DateTime.UtcNow,
        };

        var marketComparisons = new List<MarketComparison>();
        var mockDbSet = TestHelpers.CreateMockDbSet(marketComparisons);
        _contextMock.Setup(c => c.MarketComparisons).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.JobTitle, Is.EqualTo(command.JobTitle));
        Assert.That(result.Location, Is.EqualTo(command.Location));
        Assert.That(result.MinSalary, Is.Null);
        Assert.That(result.MaxSalary, Is.Null);
        Assert.That(result.MedianSalary, Is.Null);
        Assert.That(result.DataSource, Is.Null);
    }
}
