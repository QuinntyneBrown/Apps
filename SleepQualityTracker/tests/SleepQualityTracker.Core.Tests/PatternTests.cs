// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core.Tests;

public class PatternTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPattern()
    {
        // Arrange
        var patternId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Weekend Sleep Improvement";
        var description = "Better sleep quality on weekends";
        var patternType = "Weekly";
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var confidenceLevel = 85;
        var insights = "Sleep quality improves by 20% on weekends";

        // Act
        var pattern = new Pattern
        {
            PatternId = patternId,
            UserId = userId,
            Name = name,
            Description = description,
            PatternType = patternType,
            StartDate = startDate,
            EndDate = endDate,
            ConfidenceLevel = confidenceLevel,
            Insights = insights
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pattern.PatternId, Is.EqualTo(patternId));
            Assert.That(pattern.UserId, Is.EqualTo(userId));
            Assert.That(pattern.Name, Is.EqualTo(name));
            Assert.That(pattern.Description, Is.EqualTo(description));
            Assert.That(pattern.PatternType, Is.EqualTo(patternType));
            Assert.That(pattern.StartDate, Is.EqualTo(startDate));
            Assert.That(pattern.EndDate, Is.EqualTo(endDate));
            Assert.That(pattern.ConfidenceLevel, Is.EqualTo(85));
            Assert.That(pattern.Insights, Is.EqualTo(insights));
        });
    }

    [Test]
    public void DefaultValues_NewPattern_HasExpectedDefaults()
    {
        // Act
        var pattern = new Pattern();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pattern.Name, Is.EqualTo(string.Empty));
            Assert.That(pattern.Description, Is.EqualTo(string.Empty));
            Assert.That(pattern.PatternType, Is.EqualTo(string.Empty));
            Assert.That(pattern.ConfidenceLevel, Is.EqualTo(0));
            Assert.That(pattern.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsHighConfidence_ConfidenceLevel71_ReturnsTrue()
    {
        // Arrange
        var pattern = new Pattern
        {
            ConfidenceLevel = 71
        };

        // Act
        var result = pattern.IsHighConfidence();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighConfidence_ConfidenceLevel100_ReturnsTrue()
    {
        // Arrange
        var pattern = new Pattern
        {
            ConfidenceLevel = 100
        };

        // Act
        var result = pattern.IsHighConfidence();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighConfidence_ConfidenceLevel70_ReturnsFalse()
    {
        // Arrange
        var pattern = new Pattern
        {
            ConfidenceLevel = 70
        };

        // Act
        var result = pattern.IsHighConfidence();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighConfidence_ConfidenceLevel50_ReturnsFalse()
    {
        // Arrange
        var pattern = new Pattern
        {
            ConfidenceLevel = 50
        };

        // Act
        var result = pattern.IsHighConfidence();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighConfidence_ConfidenceLevel0_ReturnsFalse()
    {
        // Arrange
        var pattern = new Pattern
        {
            ConfidenceLevel = 0
        };

        // Act
        var result = pattern.IsHighConfidence();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetDuration_OneMonthRange_Returns30Days()
    {
        // Arrange
        var pattern = new Pattern
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 1, 31)
        };

        // Act
        var duration = pattern.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(30));
    }

    [Test]
    public void GetDuration_OneWeekRange_Returns7Days()
    {
        // Arrange
        var pattern = new Pattern
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2024, 1, 8)
        };

        // Act
        var duration = pattern.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(7));
    }

    [Test]
    public void GetDuration_SameDay_Returns0Days()
    {
        // Arrange
        var pattern = new Pattern
        {
            StartDate = new DateTime(2024, 1, 1, 9, 0, 0),
            EndDate = new DateTime(2024, 1, 1, 17, 0, 0)
        };

        // Act
        var duration = pattern.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(0));
    }

    [Test]
    public void GetDuration_OneYearRange_Returns365Days()
    {
        // Arrange
        var pattern = new Pattern
        {
            StartDate = new DateTime(2024, 1, 1),
            EndDate = new DateTime(2025, 1, 1)
        };

        // Act
        var duration = pattern.GetDuration();

        // Assert
        Assert.That(duration, Is.EqualTo(366)); // 2024 is a leap year
    }

    [Test]
    public void Insights_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var pattern = new Pattern
        {
            Insights = null
        };

        // Assert
        Assert.That(pattern.Insights, Is.Null);
    }

    [Test]
    public void ConfidenceLevel_ValidRange_CanBeStored()
    {
        // Arrange & Act
        var pattern1 = new Pattern { ConfidenceLevel = 0 };
        var pattern2 = new Pattern { ConfidenceLevel = 50 };
        var pattern3 = new Pattern { ConfidenceLevel = 100 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(pattern1.ConfidenceLevel, Is.EqualTo(0));
            Assert.That(pattern2.ConfidenceLevel, Is.EqualTo(50));
            Assert.That(pattern3.ConfidenceLevel, Is.EqualTo(100));
        });
    }

    [Test]
    public void PatternType_DifferentTypes_CanBeStored()
    {
        // Arrange & Act
        var weeklyPattern = new Pattern { PatternType = "Weekly" };
        var monthlyPattern = new Pattern { PatternType = "Monthly" };
        var seasonalPattern = new Pattern { PatternType = "Seasonal" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(weeklyPattern.PatternType, Is.EqualTo("Weekly"));
            Assert.That(monthlyPattern.PatternType, Is.EqualTo("Monthly"));
            Assert.That(seasonalPattern.PatternType, Is.EqualTo("Seasonal"));
        });
    }

    [Test]
    public void Name_RequiredField_CanStoreValue()
    {
        // Arrange
        var name = "Caffeine Impact Pattern";

        // Act
        var pattern = new Pattern
        {
            Name = name
        };

        // Assert
        Assert.That(pattern.Name, Is.EqualTo(name));
    }

    [Test]
    public void Description_RequiredField_CanStoreValue()
    {
        // Arrange
        var description = "Caffeine after 3 PM reduces sleep quality";

        // Act
        var pattern = new Pattern
        {
            Description = description
        };

        // Assert
        Assert.That(pattern.Description, Is.EqualTo(description));
    }
}
