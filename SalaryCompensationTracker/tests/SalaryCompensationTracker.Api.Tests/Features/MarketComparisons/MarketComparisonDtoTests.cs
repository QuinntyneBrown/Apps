using SalaryCompensationTracker.Api.Features.MarketComparisons;
using SalaryCompensationTracker.Core;

namespace SalaryCompensationTracker.Api.Tests.Features.MarketComparisons;

[TestFixture]
public class MarketComparisonDtoTests
{
    [Test]
    public void ToDto_ValidMarketComparison_MapsAllProperties()
    {
        // Arrange
        var marketComparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
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
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = marketComparison.ToDto();

        // Assert
        Assert.That(dto.MarketComparisonId, Is.EqualTo(marketComparison.MarketComparisonId));
        Assert.That(dto.UserId, Is.EqualTo(marketComparison.UserId));
        Assert.That(dto.JobTitle, Is.EqualTo(marketComparison.JobTitle));
        Assert.That(dto.Location, Is.EqualTo(marketComparison.Location));
        Assert.That(dto.ExperienceLevel, Is.EqualTo(marketComparison.ExperienceLevel));
        Assert.That(dto.MinSalary, Is.EqualTo(marketComparison.MinSalary));
        Assert.That(dto.MaxSalary, Is.EqualTo(marketComparison.MaxSalary));
        Assert.That(dto.MedianSalary, Is.EqualTo(marketComparison.MedianSalary));
        Assert.That(dto.DataSource, Is.EqualTo(marketComparison.DataSource));
        Assert.That(dto.Notes, Is.EqualTo(marketComparison.Notes));
    }
}
