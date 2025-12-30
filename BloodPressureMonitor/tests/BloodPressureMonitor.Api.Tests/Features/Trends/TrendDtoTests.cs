// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;

namespace BloodPressureMonitor.Api.Tests;

/// <summary>
/// Unit tests for TrendDto mapping.
/// </summary>
[TestFixture]
public class TrendDtoTests
{
    /// <summary>
    /// Tests that ToDto maps all properties correctly.
    /// </summary>
    [Test]
    public void ToDto_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var trend = new Trend
        {
            TrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            StartDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            EndDate = new DateTime(2024, 1, 31, 23, 59, 59, DateTimeKind.Utc),
            AverageSystolic = 125.5m,
            AverageDiastolic = 82.3m,
            HighestSystolic = 145,
            HighestDiastolic = 95,
            LowestSystolic = 110,
            LowestDiastolic = 70,
            ReadingCount = 30,
            TrendDirection = "Improving",
            Insights = "Blood pressure is improving",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = trend.ToDto();

        // Assert
        Assert.That(dto.TrendId, Is.EqualTo(trend.TrendId));
        Assert.That(dto.UserId, Is.EqualTo(trend.UserId));
        Assert.That(dto.AverageSystolic, Is.EqualTo(125.5m));
        Assert.That(dto.AverageDiastolic, Is.EqualTo(82.3m));
        Assert.That(dto.HighestSystolic, Is.EqualTo(145));
        Assert.That(dto.LowestSystolic, Is.EqualTo(110));
        Assert.That(dto.ReadingCount, Is.EqualTo(30));
        Assert.That(dto.TrendDirection, Is.EqualTo("Improving"));
        Assert.That(dto.IsImproving, Is.True);
        Assert.That(dto.PeriodDuration, Is.GreaterThan(0));
    }

    /// <summary>
    /// Tests that ToDto correctly identifies improving trends.
    /// </summary>
    [Test]
    public void ToDto_IdentifiesImprovingTrends()
    {
        // Arrange
        var trend = new Trend
        {
            TrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            AverageSystolic = 120,
            AverageDiastolic = 80,
            HighestSystolic = 130,
            HighestDiastolic = 85,
            LowestSystolic = 110,
            LowestDiastolic = 75,
            ReadingCount = 20,
            TrendDirection = "Improving",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = trend.ToDto();

        // Assert
        Assert.That(dto.IsImproving, Is.True);
    }

    /// <summary>
    /// Tests that ToDto handles stable trends.
    /// </summary>
    [Test]
    public void ToDto_HandlesStableTrends()
    {
        // Arrange
        var trend = new Trend
        {
            TrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            AverageSystolic = 120,
            AverageDiastolic = 80,
            HighestSystolic = 125,
            HighestDiastolic = 82,
            LowestSystolic = 115,
            LowestDiastolic = 78,
            ReadingCount = 15,
            TrendDirection = "Stable",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = trend.ToDto();

        // Assert
        Assert.That(dto.IsImproving, Is.False);
        Assert.That(dto.TrendDirection, Is.EqualTo("Stable"));
    }
}
