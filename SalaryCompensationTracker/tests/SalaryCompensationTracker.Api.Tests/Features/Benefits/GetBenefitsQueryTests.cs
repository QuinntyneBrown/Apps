using SalaryCompensationTracker.Api.Features.Benefits;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.Benefits;

[TestFixture]
public class GetBenefitsQueryTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetBenefitsQueryHandler>> _loggerMock = null!;
    private GetBenefitsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<GetBenefitsQueryHandler>>();
        _handler = new GetBenefitsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_NoFilters_ReturnsAllBenefits()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var benefits = new List<Benefit>
        {
            new Benefit
            {
                BenefitId = Guid.NewGuid(),
                UserId = userId,
                Name = "Health Insurance",
                Category = "Health",
                EstimatedValue = 12000m,
                CreatedAt = DateTime.UtcNow,
            },
            new Benefit
            {
                BenefitId = Guid.NewGuid(),
                UserId = userId,
                Name = "401k Match",
                Category = "Retirement",
                EstimatedValue = 6000m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(benefits);
        _contextMock.Setup(c => c.Benefits).Returns(mockDbSet.Object);

        var query = new GetBenefitsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FilterByCategory_ReturnsCategoryBenefits()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var benefits = new List<Benefit>
        {
            new Benefit
            {
                BenefitId = Guid.NewGuid(),
                UserId = userId,
                Name = "Health Insurance",
                Category = "Health",
                EstimatedValue = 12000m,
                CreatedAt = DateTime.UtcNow,
            },
            new Benefit
            {
                BenefitId = Guid.NewGuid(),
                UserId = userId,
                Name = "401k Match",
                Category = "Retirement",
                EstimatedValue = 6000m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(benefits);
        _contextMock.Setup(c => c.Benefits).Returns(mockDbSet.Object);

        var query = new GetBenefitsQuery { Category = "Health" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Category, Is.EqualTo("Health"));
    }
}
