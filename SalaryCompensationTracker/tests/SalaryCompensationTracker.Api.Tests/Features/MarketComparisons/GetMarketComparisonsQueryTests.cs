using SalaryCompensationTracker.Api.Features.MarketComparisons;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.MarketComparisons;

[TestFixture]
public class GetMarketComparisonsQueryTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetMarketComparisonsQueryHandler>> _loggerMock = null!;
    private GetMarketComparisonsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<GetMarketComparisonsQueryHandler>>();
        _handler = new GetMarketComparisonsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_NoFilters_ReturnsAllMarketComparisons()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var marketComparisons = new List<MarketComparison>
        {
            new MarketComparison
            {
                MarketComparisonId = Guid.NewGuid(),
                UserId = userId,
                JobTitle = "Software Engineer",
                Location = "San Francisco, CA",
                MedianSalary = 150000m,
                ComparisonDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new MarketComparison
            {
                MarketComparisonId = Guid.NewGuid(),
                UserId = userId,
                JobTitle = "Data Scientist",
                Location = "New York, NY",
                MedianSalary = 140000m,
                ComparisonDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(marketComparisons);
        _contextMock.Setup(c => c.MarketComparisons).Returns(mockDbSet.Object);

        var query = new GetMarketComparisonsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FilterByJobTitle_ReturnsMatchingComparisons()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var marketComparisons = new List<MarketComparison>
        {
            new MarketComparison
            {
                MarketComparisonId = Guid.NewGuid(),
                UserId = userId,
                JobTitle = "Software Engineer",
                Location = "San Francisco, CA",
                MedianSalary = 150000m,
                ComparisonDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new MarketComparison
            {
                MarketComparisonId = Guid.NewGuid(),
                UserId = userId,
                JobTitle = "Data Scientist",
                Location = "New York, NY",
                MedianSalary = 140000m,
                ComparisonDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(marketComparisons);
        _contextMock.Setup(c => c.MarketComparisons).Returns(mockDbSet.Object);

        var query = new GetMarketComparisonsQuery { JobTitle = "Software" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().JobTitle, Does.Contain("Software"));
    }
}
