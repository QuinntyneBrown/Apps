namespace RunningLogRaceTracker.Core.Tests;

public class RaceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRace()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Boston Marathon",
            RaceType = RaceType.Marathon,
            RaceDate = new DateTime(2024, 4, 15),
            Location = "Boston, MA",
            Distance = 42.2m,
            FinishTimeMinutes = 240,
            GoalTimeMinutes = 250,
            Placement = 150,
            IsCompleted = true,
            Notes = "Great race!"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(race.RaceId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(race.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(race.Name, Is.EqualTo("Boston Marathon"));
            Assert.That(race.RaceType, Is.EqualTo(RaceType.Marathon));
            Assert.That(race.Location, Is.EqualTo("Boston, MA"));
            Assert.That(race.Distance, Is.EqualTo(42.2m));
            Assert.That(race.FinishTimeMinutes, Is.EqualTo(240));
            Assert.That(race.GoalTimeMinutes, Is.EqualTo(250));
            Assert.That(race.Placement, Is.EqualTo(150));
            Assert.That(race.IsCompleted, Is.True);
        });
    }

    [Test]
    public void AchievedGoal_FinishTimeLessThanGoal_ReturnsTrue()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = true,
            FinishTimeMinutes = 180,
            GoalTimeMinutes = 200
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.True);
    }

    [Test]
    public void AchievedGoal_FinishTimeEqualsGoal_ReturnsTrue()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = true,
            FinishTimeMinutes = 200,
            GoalTimeMinutes = 200
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.True);
    }

    [Test]
    public void AchievedGoal_FinishTimeGreaterThanGoal_ReturnsFalse()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = true,
            FinishTimeMinutes = 220,
            GoalTimeMinutes = 200
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.False);
    }

    [Test]
    public void AchievedGoal_RaceNotCompleted_ReturnsFalse()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = false,
            FinishTimeMinutes = 180,
            GoalTimeMinutes = 200
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.False);
    }

    [Test]
    public void AchievedGoal_NoFinishTime_ReturnsFalse()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = true,
            FinishTimeMinutes = null,
            GoalTimeMinutes = 200
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.False);
    }

    [Test]
    public void AchievedGoal_NoGoalTime_ReturnsFalse()
    {
        // Arrange
        var race = new Race
        {
            IsCompleted = true,
            FinishTimeMinutes = 180,
            GoalTimeMinutes = null
        };

        // Act
        var achieved = race.AchievedGoal();

        // Assert
        Assert.That(achieved, Is.False);
    }

    [Test]
    public void IsUpcoming_FutureDate_ReturnsTrue()
    {
        // Arrange
        var race = new Race
        {
            RaceDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var isUpcoming = race.IsUpcoming();

        // Assert
        Assert.That(isUpcoming, Is.True);
    }

    [Test]
    public void IsUpcoming_PastDate_ReturnsFalse()
    {
        // Arrange
        var race = new Race
        {
            RaceDate = DateTime.UtcNow.AddDays(-30)
        };

        // Act
        var isUpcoming = race.IsUpcoming();

        // Assert
        Assert.That(isUpcoming, Is.False);
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var race = new Race();

        // Assert
        Assert.That(race.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Location_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var race = new Race();

        // Assert
        Assert.That(race.Location, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsCompleted_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var race = new Race();

        // Assert
        Assert.That(race.IsCompleted, Is.False);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var race = new Race();

        // Assert
        Assert.That(race.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(race.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void FinishTimeMinutes_CanBeNull()
    {
        // Arrange & Act
        var race = new Race
        {
            FinishTimeMinutes = null
        };

        // Assert
        Assert.That(race.FinishTimeMinutes, Is.Null);
    }

    [Test]
    public void GoalTimeMinutes_CanBeNull()
    {
        // Arrange & Act
        var race = new Race
        {
            GoalTimeMinutes = null
        };

        // Assert
        Assert.That(race.GoalTimeMinutes, Is.Null);
    }

    [Test]
    public void Placement_CanBeNull()
    {
        // Arrange & Act
        var race = new Race
        {
            Placement = null
        };

        // Assert
        Assert.That(race.Placement, Is.Null);
    }

    [Test]
    public void RaceType_CanBeSetToAnyValue()
    {
        // Arrange & Act
        var race = new Race
        {
            RaceType = RaceType.HalfMarathon
        };

        // Assert
        Assert.That(race.RaceType, Is.EqualTo(RaceType.HalfMarathon));
    }
}
