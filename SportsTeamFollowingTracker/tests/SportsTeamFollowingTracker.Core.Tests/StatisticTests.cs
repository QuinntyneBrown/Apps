// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SportsTeamFollowingTracker.Core.Tests;

public class StatisticTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesStatistic()
    {
        // Arrange
        var statisticId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var teamId = Guid.NewGuid();
        var statName = "Batting Average";
        var value = 0.315m;
        var recordedDate = new DateTime(2024, 6, 15);

        // Act
        var statistic = new Statistic
        {
            StatisticId = statisticId,
            UserId = userId,
            TeamId = teamId,
            StatName = statName,
            Value = value,
            RecordedDate = recordedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statistic.StatisticId, Is.EqualTo(statisticId));
            Assert.That(statistic.UserId, Is.EqualTo(userId));
            Assert.That(statistic.TeamId, Is.EqualTo(teamId));
            Assert.That(statistic.StatName, Is.EqualTo(statName));
            Assert.That(statistic.Value, Is.EqualTo(value));
            Assert.That(statistic.RecordedDate, Is.EqualTo(recordedDate));
        });
    }

    [Test]
    public void DefaultValues_NewStatistic_HasExpectedDefaults()
    {
        // Act
        var statistic = new Statistic();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(statistic.StatName, Is.EqualTo(string.Empty));
            Assert.That(statistic.Value, Is.EqualTo(0));
            Assert.That(statistic.RecordedDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(statistic.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Value_PositiveDecimal_CanBeStored()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "Points Per Game",
            Value = 28.5m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(28.5m));
    }

    [Test]
    public void Value_IntegerValue_CanBeStored()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "Home Runs",
            Value = 45m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(45m));
    }

    [Test]
    public void Value_SmallDecimal_CanBeStored()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "ERA",
            Value = 2.85m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(2.85m));
    }

    [Test]
    public void Value_Zero_CanBeStored()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "Errors",
            Value = 0m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(0m));
    }

    [Test]
    public void StatName_DifferentStatTypes_CanBeStored()
    {
        // Arrange & Act
        var stat1 = new Statistic { StatName = "Goals" };
        var stat2 = new Statistic { StatName = "Assists" };
        var stat3 = new Statistic { StatName = "Rebounds" };
        var stat4 = new Statistic { StatName = "Touchdowns" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(stat1.StatName, Is.EqualTo("Goals"));
            Assert.That(stat2.StatName, Is.EqualTo("Assists"));
            Assert.That(stat3.StatName, Is.EqualTo("Rebounds"));
            Assert.That(stat4.StatName, Is.EqualTo("Touchdowns"));
        });
    }

    [Test]
    public void RecordedDate_PastDate_CanBeStored()
    {
        // Arrange
        var pastDate = new DateTime(2023, 12, 31);

        // Act
        var statistic = new Statistic
        {
            RecordedDate = pastDate
        };

        // Assert
        Assert.That(statistic.RecordedDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void RecordedDate_CurrentDate_CanBeStored()
    {
        // Arrange
        var currentDate = DateTime.UtcNow;

        // Act
        var statistic = new Statistic
        {
            RecordedDate = currentDate
        };

        // Assert
        Assert.That(statistic.RecordedDate, Is.EqualTo(currentDate).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void TeamId_CanBeAssociated()
    {
        // Arrange
        var teamId = Guid.NewGuid();

        // Act
        var statistic = new Statistic
        {
            TeamId = teamId
        };

        // Assert
        Assert.That(statistic.TeamId, Is.EqualTo(teamId));
    }

    [Test]
    public void UserId_CanBeAssociated()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var statistic = new Statistic
        {
            UserId = userId
        };

        // Assert
        Assert.That(statistic.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void Value_LargeNumber_CanBeStored()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "Total Yards",
            Value = 5432.5m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(5432.5m));
    }

    [Test]
    public void Value_Percentage_CanBeStoredAsDecimal()
    {
        // Arrange & Act
        var statistic = new Statistic
        {
            StatName = "Win Percentage",
            Value = 0.625m
        };

        // Assert
        Assert.That(statistic.Value, Is.EqualTo(0.625m));
    }

    [Test]
    public void CreatedAt_AutomaticallySet_OnCreation()
    {
        // Arrange & Act
        var statistic = new Statistic();

        // Assert
        Assert.That(statistic.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }
}
