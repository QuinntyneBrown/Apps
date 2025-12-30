namespace PersonalHealthDashboard.Core.Tests;

public class HealthTrendTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesHealthTrend()
    {
        // Arrange
        var healthTrendId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(-30);
        var endDate = DateTime.UtcNow;

        // Act
        var trend = new HealthTrend
        {
            HealthTrendId = healthTrendId,
            UserId = userId,
            MetricName = "Weight",
            StartDate = startDate,
            EndDate = endDate,
            AverageValue = 180.5,
            MinValue = 178.0,
            MaxValue = 183.0,
            TrendDirection = "Decreasing",
            PercentageChange = -2.5,
            Insights = "Good progress on weight loss"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trend.HealthTrendId, Is.EqualTo(healthTrendId));
            Assert.That(trend.UserId, Is.EqualTo(userId));
            Assert.That(trend.MetricName, Is.EqualTo("Weight"));
            Assert.That(trend.StartDate, Is.EqualTo(startDate));
            Assert.That(trend.EndDate, Is.EqualTo(endDate));
            Assert.That(trend.AverageValue, Is.EqualTo(180.5));
            Assert.That(trend.MinValue, Is.EqualTo(178.0));
            Assert.That(trend.MaxValue, Is.EqualTo(183.0));
            Assert.That(trend.TrendDirection, Is.EqualTo("Decreasing"));
            Assert.That(trend.PercentageChange, Is.EqualTo(-2.5));
            Assert.That(trend.Insights, Is.EqualTo("Good progress on weight loss"));
            Assert.That(trend.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsImproving_HigherIsBetterAndIncreasing_ReturnsTrue()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Steps",
            TrendDirection = "Increasing"
        };

        // Act
        var result = trend.IsImproving(isHigherBetter: true);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsImproving_HigherIsBetterAndDecreasing_ReturnsFalse()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Steps",
            TrendDirection = "Decreasing"
        };

        // Act
        var result = trend.IsImproving(isHigherBetter: true);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsImproving_LowerIsBetterAndDecreasing_ReturnsTrue()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            TrendDirection = "Decreasing"
        };

        // Act
        var result = trend.IsImproving(isHigherBetter: false);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsImproving_LowerIsBetterAndIncreasing_ReturnsFalse()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            TrendDirection = "Increasing"
        };

        // Act
        var result = trend.IsImproving(isHigherBetter: false);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsImproving_StableTrend_ReturnsFalse()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            TrendDirection = "Stable"
        };

        // Act
        var resultHigherBetter = trend.IsImproving(isHigherBetter: true);
        var resultLowerBetter = trend.IsImproving(isHigherBetter: false);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(resultHigherBetter, Is.False);
            Assert.That(resultLowerBetter, Is.False);
        });
    }

    [Test]
    public void IsImproving_CaseInsensitiveComparison_ReturnsTrue()
    {
        // Arrange
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Steps",
            TrendDirection = "increasing"
        };

        // Act
        var result = trend.IsImproving(isHigherBetter: true);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GetPeriodDuration_30DayPeriod_Returns30()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            StartDate = startDate,
            EndDate = endDate
        };

        // Act
        var duration = trend.GetPeriodDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(30));
    }

    [Test]
    public void GetPeriodDuration_7DayPeriod_Returns7()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 8);
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            StartDate = startDate,
            EndDate = endDate
        };

        // Act
        var duration = trend.GetPeriodDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(7));
    }

    [Test]
    public void GetPeriodDuration_SameDay_Returns0()
    {
        // Arrange
        var date = new DateTime(2024, 1, 1);
        var trend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            StartDate = date,
            EndDate = date
        };

        // Act
        var duration = trend.GetPeriodDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(0));
    }
}
