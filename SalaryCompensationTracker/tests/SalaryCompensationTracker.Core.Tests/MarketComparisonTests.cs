namespace SalaryCompensationTracker.Core.Tests;

public class MarketComparisonTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMarketComparison()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MarketComparisonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            JobTitle = "Senior Software Engineer",
            Location = "San Francisco, CA",
            ExperienceLevel = "5-7 years",
            MinSalary = 140000m,
            MaxSalary = 200000m,
            MedianSalary = 170000m,
            DataSource = "Glassdoor",
            ComparisonDate = DateTime.UtcNow,
            Notes = "Tech industry average"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(comparison.MarketComparisonId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(comparison.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(comparison.JobTitle, Is.EqualTo("Senior Software Engineer"));
            Assert.That(comparison.Location, Is.EqualTo("San Francisco, CA"));
            Assert.That(comparison.ExperienceLevel, Is.EqualTo("5-7 years"));
            Assert.That(comparison.MinSalary, Is.EqualTo(140000m));
            Assert.That(comparison.MaxSalary, Is.EqualTo(200000m));
            Assert.That(comparison.MedianSalary, Is.EqualTo(170000m));
            Assert.That(comparison.DataSource, Is.EqualTo("Glassdoor"));
        });
    }

    [Test]
    public void GetMidpoint_WithMinAndMax_ReturnsCorrectMidpoint()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = 100000m,
            MaxSalary = 150000m
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.EqualTo(125000m));
    }

    [Test]
    public void GetMidpoint_OnlyMinSalary_ReturnsNull()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = 100000m,
            MaxSalary = null
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.Null);
    }

    [Test]
    public void GetMidpoint_OnlyMaxSalary_ReturnsNull()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = null,
            MaxSalary = 150000m
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.Null);
    }

    [Test]
    public void GetMidpoint_NoMinOrMaxButHasMedian_ReturnsMedian()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = null,
            MaxSalary = null,
            MedianSalary = 120000m
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.EqualTo(120000m));
    }

    [Test]
    public void GetMidpoint_HasAllValues_PrioritizesMidpointOverMedian()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = 100000m,
            MaxSalary = 150000m,
            MedianSalary = 120000m
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.EqualTo(125000m)); // Uses calculated midpoint, not median
    }

    [Test]
    public void JobTitle_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var comparison = new MarketComparison();

        // Assert
        Assert.That(comparison.JobTitle, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Location_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var comparison = new MarketComparison();

        // Assert
        Assert.That(comparison.Location, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ComparisonDate_DefaultValue_IsSet()
    {
        // Arrange & Act
        var comparison = new MarketComparison();

        // Assert
        Assert.That(comparison.ComparisonDate, Is.Not.EqualTo(default(DateTime)));
        Assert.That(comparison.ComparisonDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var comparison = new MarketComparison();

        // Assert
        Assert.That(comparison.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(comparison.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void ExperienceLevel_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            ExperienceLevel = null
        };

        // Assert
        Assert.That(comparison.ExperienceLevel, Is.Null);
    }

    [Test]
    public void MinSalary_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MinSalary = null
        };

        // Assert
        Assert.That(comparison.MinSalary, Is.Null);
    }

    [Test]
    public void MaxSalary_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MaxSalary = null
        };

        // Assert
        Assert.That(comparison.MaxSalary, Is.Null);
    }

    [Test]
    public void MedianSalary_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            MedianSalary = null
        };

        // Assert
        Assert.That(comparison.MedianSalary, Is.Null);
    }

    [Test]
    public void DataSource_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            DataSource = null
        };

        // Assert
        Assert.That(comparison.DataSource, Is.Null);
    }

    [Test]
    public void UpdatedAt_CanBeNull()
    {
        // Arrange & Act
        var comparison = new MarketComparison
        {
            UpdatedAt = null
        };

        // Assert
        Assert.That(comparison.UpdatedAt, Is.Null);
    }

    [Test]
    public void GetMidpoint_EqualMinAndMax_ReturnsSameValue()
    {
        // Arrange
        var comparison = new MarketComparison
        {
            MinSalary = 100000m,
            MaxSalary = 100000m
        };

        // Act
        var midpoint = comparison.GetMidpoint();

        // Assert
        Assert.That(midpoint, Is.EqualTo(100000m));
    }
}
