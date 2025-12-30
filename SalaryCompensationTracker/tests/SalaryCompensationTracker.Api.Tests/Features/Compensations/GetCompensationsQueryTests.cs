using SalaryCompensationTracker.Api.Features.Compensations;
using SalaryCompensationTracker.Core;
using Microsoft.Extensions.Logging;

namespace SalaryCompensationTracker.Api.Tests.Features.Compensations;

[TestFixture]
public class GetCompensationsQueryTests
{
    private Mock<ISalaryCompensationTrackerContext> _contextMock = null!;
    private Mock<ILogger<GetCompensationsQueryHandler>> _loggerMock = null!;
    private GetCompensationsQueryHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ISalaryCompensationTrackerContext>();
        _loggerMock = new Mock<ILogger<GetCompensationsQueryHandler>>();
        _handler = new GetCompensationsQueryHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_NoFilters_ReturnsAllCompensations()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var compensations = new List<Compensation>
        {
            new Compensation
            {
                CompensationId = Guid.NewGuid(),
                UserId = userId,
                CompensationType = CompensationType.FullTime,
                Employer = "Tech Corp",
                JobTitle = "Software Engineer",
                BaseSalary = 100000m,
                TotalCompensation = 100000m,
                EffectiveDate = DateTime.UtcNow.AddMonths(-6),
                CreatedAt = DateTime.UtcNow,
            },
            new Compensation
            {
                CompensationId = Guid.NewGuid(),
                UserId = userId,
                CompensationType = CompensationType.Contract,
                Employer = "Startup Inc",
                JobTitle = "Consultant",
                BaseSalary = 80000m,
                TotalCompensation = 80000m,
                EffectiveDate = DateTime.UtcNow.AddMonths(-3),
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(compensations);
        _contextMock.Setup(c => c.Compensations).Returns(mockDbSet.Object);

        var query = new GetCompensationsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Handle_FilterByUserId_ReturnsUserCompensations()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();
        var compensations = new List<Compensation>
        {
            new Compensation
            {
                CompensationId = Guid.NewGuid(),
                UserId = userId,
                CompensationType = CompensationType.FullTime,
                Employer = "Tech Corp",
                JobTitle = "Software Engineer",
                BaseSalary = 100000m,
                TotalCompensation = 100000m,
                EffectiveDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Compensation
            {
                CompensationId = Guid.NewGuid(),
                UserId = otherUserId,
                CompensationType = CompensationType.Contract,
                Employer = "Other Corp",
                JobTitle = "Consultant",
                BaseSalary = 80000m,
                TotalCompensation = 80000m,
                EffectiveDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        var mockDbSet = TestHelpers.CreateMockDbSet(compensations);
        _contextMock.Setup(c => c.Compensations).Returns(mockDbSet.Object);

        var query = new GetCompensationsQuery { UserId = userId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().UserId, Is.EqualTo(userId));
    }
}
