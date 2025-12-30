// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core.Tests;

public class TrendTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTrend()
    {
        // Arrange
        var trendId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(-30);
        var endDate = DateTime.UtcNow;
        var avgSystolic = 125.5m;
        var avgDiastolic = 82.3m;
        var highestSystolic = 145;
        var highestDiastolic = 95;
        var lowestSystolic = 110;
        var lowestDiastolic = 70;
        var readingCount = 30;
        var trendDirection = "Improving";
        var insights = "Blood pressure trending downward";

        // Act
        var trend = new Trend
        {
            TrendId = trendId,
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            AverageSystolic = avgSystolic,
            AverageDiastolic = avgDiastolic,
            HighestSystolic = highestSystolic,
            HighestDiastolic = highestDiastolic,
            LowestSystolic = lowestSystolic,
            LowestDiastolic = lowestDiastolic,
            ReadingCount = readingCount,
            TrendDirection = trendDirection,
            Insights = insights
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trend.TrendId, Is.EqualTo(trendId));
            Assert.That(trend.UserId, Is.EqualTo(userId));
            Assert.That(trend.StartDate, Is.EqualTo(startDate));
            Assert.That(trend.EndDate, Is.EqualTo(endDate));
            Assert.That(trend.AverageSystolic, Is.EqualTo(avgSystolic));
            Assert.That(trend.AverageDiastolic, Is.EqualTo(avgDiastolic));
            Assert.That(trend.HighestSystolic, Is.EqualTo(highestSystolic));
            Assert.That(trend.HighestDiastolic, Is.EqualTo(highestDiastolic));
            Assert.That(trend.LowestSystolic, Is.EqualTo(lowestSystolic));
            Assert.That(trend.LowestDiastolic, Is.EqualTo(lowestDiastolic));
            Assert.That(trend.ReadingCount, Is.EqualTo(readingCount));
            Assert.That(trend.TrendDirection, Is.EqualTo(trendDirection));
            Assert.That(trend.Insights, Is.EqualTo(insights));
            Assert.That(trend.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var trend = new Trend();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trend.TrendDirection, Is.EqualTo(string.Empty));
            Assert.That(trend.Insights, Is.Null);
            Assert.That(trend.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsImproving_WhenImprovingCaseSensitive_ReturnsTrue()
    {
        // Arrange
        var trend = new Trend { TrendDirection = "Improving" };

        // Act
        var result = trend.IsImproving();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsImproving_WhenImprovingLowercase_ReturnsTrue()
    {
        // Arrange
        var trend = new Trend { TrendDirection = "improving" };

        // Act
        var result = trend.IsImproving();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsImproving_WhenImprovingMixedCase_ReturnsTrue()
    {
        // Arrange
        var trend = new Trend { TrendDirection = "ImPrOvInG" };

        // Act
        var result = trend.IsImproving();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsImproving_WhenStable_ReturnsFalse()
    {
        // Arrange
        var trend = new Trend { TrendDirection = "Stable" };

        // Act
        var result = trend.IsImproving();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsImproving_WhenWorsening_ReturnsFalse()
    {
        // Arrange
        var trend = new Trend { TrendDirection = "Worsening" };

        // Act
        var result = trend.IsImproving();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetPeriodDuration_With30Days_Returns30()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var trend = new Trend { StartDate = startDate, EndDate = endDate };

        // Act
        var duration = trend.GetPeriodDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(30));
    }

    [Test]
    public void GetPeriodDuration_With7Days_Returns7()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 8);
        var trend = new Trend { StartDate = startDate, EndDate = endDate };

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
        var trend = new Trend { StartDate = date, EndDate = date };

        // Act
        var duration = trend.GetPeriodDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(0));
    }

    [Test]
    public void TrendDirection_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var trend = new Trend();
        var direction = "Stable";

        // Act
        trend.TrendDirection = direction;

        // Assert
        Assert.That(trend.TrendDirection, Is.EqualTo(direction));
    }

    [Test]
    public void AverageSystolic_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var trend = new Trend();
        var avg = 120.5m;

        // Act
        trend.AverageSystolic = avg;

        // Assert
        Assert.That(trend.AverageSystolic, Is.EqualTo(avg));
    }

    [Test]
    public void AverageDiastolic_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var trend = new Trend();
        var avg = 80.3m;

        // Act
        trend.AverageDiastolic = avg;

        // Assert
        Assert.That(trend.AverageDiastolic, Is.EqualTo(avg));
    }

    [Test]
    public void ReadingCount_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var trend = new Trend();
        var count = 45;

        // Act
        trend.ReadingCount = count;

        // Assert
        Assert.That(trend.ReadingCount, Is.EqualTo(count));
    }

    [Test]
    public void Insights_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var trend = new Trend { Insights = "Some insights" };

        // Act
        trend.Insights = null;

        // Assert
        Assert.That(trend.Insights, Is.Null);
    }
}
