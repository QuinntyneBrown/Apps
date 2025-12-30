namespace RunningLogRaceTracker.Core.Tests;

public class RunTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRun()
    {
        // Arrange & Act
        var run = new Run
        {
            RunId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Distance = 10.5m,
            DurationMinutes = 60,
            CompletedAt = DateTime.UtcNow,
            AveragePace = 5.71m,
            AverageHeartRate = 150,
            ElevationGain = 200,
            CaloriesBurned = 700,
            Route = "Park Loop",
            Weather = "Sunny, 72F",
            Notes = "Great run!",
            EffortRating = 7
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(run.RunId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(run.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(run.Distance, Is.EqualTo(10.5m));
            Assert.That(run.DurationMinutes, Is.EqualTo(60));
            Assert.That(run.AveragePace, Is.EqualTo(5.71m));
            Assert.That(run.AverageHeartRate, Is.EqualTo(150));
            Assert.That(run.ElevationGain, Is.EqualTo(200));
            Assert.That(run.CaloriesBurned, Is.EqualTo(700));
            Assert.That(run.EffortRating, Is.EqualTo(7));
        });
    }

    [Test]
    public void CalculatePace_ValidDistance_ReturnsCorrectPace()
    {
        // Arrange
        var run = new Run
        {
            Distance = 10m,
            DurationMinutes = 50
        };

        // Act
        var pace = run.CalculatePace();

        // Assert
        Assert.That(pace, Is.EqualTo(5m)); // 50 minutes / 10 km = 5 min/km
    }

    [Test]
    public void CalculatePace_ZeroDistance_ReturnsZero()
    {
        // Arrange
        var run = new Run
        {
            Distance = 0m,
            DurationMinutes = 50
        };

        // Act
        var pace = run.CalculatePace();

        // Assert
        Assert.That(pace, Is.EqualTo(0m));
    }

    [Test]
    public void CalculatePace_SmallDistance_ReturnsCorrectPace()
    {
        // Arrange
        var run = new Run
        {
            Distance = 5m,
            DurationMinutes = 30
        };

        // Act
        var pace = run.CalculatePace();

        // Assert
        Assert.That(pace, Is.EqualTo(6m)); // 30 / 5 = 6 min/km
    }

    [Test]
    public void IsToday_CompletedToday_ReturnsTrue()
    {
        // Arrange
        var run = new Run
        {
            CompletedAt = DateTime.UtcNow
        };

        // Act
        var isToday = run.IsToday();

        // Assert
        Assert.That(isToday, Is.True);
    }

    [Test]
    public void IsToday_CompletedYesterday_ReturnsFalse()
    {
        // Arrange
        var run = new Run
        {
            CompletedAt = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var isToday = run.IsToday();

        // Assert
        Assert.That(isToday, Is.False);
    }

    [Test]
    public void IsToday_CompletedTomorrow_ReturnsFalse()
    {
        // Arrange
        var run = new Run
        {
            CompletedAt = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var isToday = run.IsToday();

        // Assert
        Assert.That(isToday, Is.False);
    }

    [Test]
    public void CompletedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var run = new Run();

        // Assert
        Assert.That(run.CompletedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(run.CompletedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSet()
    {
        // Arrange & Act
        var run = new Run();

        // Assert
        Assert.That(run.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        Assert.That(run.CreatedAt, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }

    [Test]
    public void AveragePace_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            AveragePace = null
        };

        // Assert
        Assert.That(run.AveragePace, Is.Null);
    }

    [Test]
    public void AverageHeartRate_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            AverageHeartRate = null
        };

        // Assert
        Assert.That(run.AverageHeartRate, Is.Null);
    }

    [Test]
    public void ElevationGain_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            ElevationGain = null
        };

        // Assert
        Assert.That(run.ElevationGain, Is.Null);
    }

    [Test]
    public void CaloriesBurned_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            CaloriesBurned = null
        };

        // Assert
        Assert.That(run.CaloriesBurned, Is.Null);
    }

    [Test]
    public void Route_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            Route = null
        };

        // Assert
        Assert.That(run.Route, Is.Null);
    }

    [Test]
    public void Weather_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            Weather = null
        };

        // Assert
        Assert.That(run.Weather, Is.Null);
    }

    [Test]
    public void EffortRating_CanBeNull()
    {
        // Arrange & Act
        var run = new Run
        {
            EffortRating = null
        };

        // Assert
        Assert.That(run.EffortRating, Is.Null);
    }

    [Test]
    public void EffortRating_CanBeSetToMinValue()
    {
        // Arrange & Act
        var run = new Run
        {
            EffortRating = 1
        };

        // Assert
        Assert.That(run.EffortRating, Is.EqualTo(1));
    }

    [Test]
    public void EffortRating_CanBeSetToMaxValue()
    {
        // Arrange & Act
        var run = new Run
        {
            EffortRating = 10
        };

        // Assert
        Assert.That(run.EffortRating, Is.EqualTo(10));
    }
}
