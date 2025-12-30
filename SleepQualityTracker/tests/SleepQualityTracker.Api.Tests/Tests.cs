using SleepQualityTracker.Api.Features.SleepSessions;
using SleepQualityTracker.Api.Features.Habits;
using SleepQualityTracker.Api.Features.Patterns;

namespace SleepQualityTracker.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void SleepSessionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var sleepSession = new Core.SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Bedtime = DateTime.UtcNow.AddHours(-8),
            WakeTime = DateTime.UtcNow,
            TotalSleepMinutes = 450,
            SleepQuality = Core.SleepQuality.Good,
            TimesAwakened = 2,
            DeepSleepMinutes = 120,
            RemSleepMinutes = 90,
            SleepEfficiency = 92.5m,
            Notes = "Slept well",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = sleepSession.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.SleepSessionId, Is.EqualTo(sleepSession.SleepSessionId));
            Assert.That(dto.UserId, Is.EqualTo(sleepSession.UserId));
            Assert.That(dto.Bedtime, Is.EqualTo(sleepSession.Bedtime));
            Assert.That(dto.WakeTime, Is.EqualTo(sleepSession.WakeTime));
            Assert.That(dto.TotalSleepMinutes, Is.EqualTo(sleepSession.TotalSleepMinutes));
            Assert.That(dto.SleepQuality, Is.EqualTo(sleepSession.SleepQuality));
            Assert.That(dto.TimesAwakened, Is.EqualTo(sleepSession.TimesAwakened));
            Assert.That(dto.DeepSleepMinutes, Is.EqualTo(sleepSession.DeepSleepMinutes));
            Assert.That(dto.RemSleepMinutes, Is.EqualTo(sleepSession.RemSleepMinutes));
            Assert.That(dto.SleepEfficiency, Is.EqualTo(sleepSession.SleepEfficiency));
            Assert.That(dto.Notes, Is.EqualTo(sleepSession.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(sleepSession.CreatedAt));
            Assert.That(dto.TimeInBedMinutes, Is.EqualTo(sleepSession.GetTimeInBed()));
            Assert.That(dto.MeetsRecommendedDuration, Is.EqualTo(sleepSession.MeetsRecommendedDuration()));
            Assert.That(dto.IsGoodQuality, Is.EqualTo(sleepSession.IsGoodQuality()));
        });
    }

    [Test]
    public void SleepSessionDto_ToDto_HandlesNullableProperties()
    {
        // Arrange
        var sleepSession = new Core.SleepSession
        {
            SleepSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Bedtime = DateTime.UtcNow.AddHours(-7),
            WakeTime = DateTime.UtcNow,
            TotalSleepMinutes = 390,
            SleepQuality = Core.SleepQuality.Fair,
            TimesAwakened = null,
            DeepSleepMinutes = null,
            RemSleepMinutes = null,
            SleepEfficiency = null,
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = sleepSession.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TimesAwakened, Is.Null);
            Assert.That(dto.DeepSleepMinutes, Is.Null);
            Assert.That(dto.RemSleepMinutes, Is.Null);
            Assert.That(dto.SleepEfficiency, Is.Null);
            Assert.That(dto.Notes, Is.Null);
        });
    }

    [Test]
    public void HabitDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var habit = new Core.Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Morning Coffee",
            Description = "Coffee intake in the morning",
            HabitType = "Caffeine",
            IsPositive = false,
            TypicalTime = new TimeSpan(8, 0, 0),
            ImpactLevel = 3,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = habit.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.HabitId, Is.EqualTo(habit.HabitId));
            Assert.That(dto.UserId, Is.EqualTo(habit.UserId));
            Assert.That(dto.Name, Is.EqualTo(habit.Name));
            Assert.That(dto.Description, Is.EqualTo(habit.Description));
            Assert.That(dto.HabitType, Is.EqualTo(habit.HabitType));
            Assert.That(dto.IsPositive, Is.EqualTo(habit.IsPositive));
            Assert.That(dto.TypicalTime, Is.EqualTo(habit.TypicalTime));
            Assert.That(dto.ImpactLevel, Is.EqualTo(habit.ImpactLevel));
            Assert.That(dto.IsActive, Is.EqualTo(habit.IsActive));
            Assert.That(dto.CreatedAt, Is.EqualTo(habit.CreatedAt));
            Assert.That(dto.IsHighImpact, Is.EqualTo(habit.IsHighImpact()));
        });
    }

    [Test]
    public void HabitDto_ToDto_HighImpactHabit_ReturnsTrue()
    {
        // Arrange
        var habit = new Core.Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Late Night Screen Time",
            Description = "Using phone before bed",
            HabitType = "Screen Time",
            IsPositive = false,
            TypicalTime = new TimeSpan(22, 30, 0),
            ImpactLevel = 5,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = habit.ToDto();

        // Assert
        Assert.That(dto.IsHighImpact, Is.True);
        Assert.That(dto.ImpactLevel, Is.GreaterThanOrEqualTo(4));
    }

    [Test]
    public void HabitDto_ToDto_LowImpactHabit_ReturnsFalse()
    {
        // Arrange
        var habit = new Core.Habit
        {
            HabitId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Afternoon Walk",
            Description = "Light exercise",
            HabitType = "Exercise",
            IsPositive = true,
            TypicalTime = new TimeSpan(15, 0, 0),
            ImpactLevel = 2,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = habit.ToDto();

        // Assert
        Assert.That(dto.IsHighImpact, Is.False);
        Assert.That(dto.ImpactLevel, Is.LessThan(4));
    }

    [Test]
    public void PatternDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var pattern = new Core.Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Weekend Sleep Pattern",
            Description = "Better sleep quality on weekends",
            PatternType = "Weekly",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            ConfidenceLevel = 85,
            Insights = "User consistently sleeps better on weekends",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = pattern.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.PatternId, Is.EqualTo(pattern.PatternId));
            Assert.That(dto.UserId, Is.EqualTo(pattern.UserId));
            Assert.That(dto.Name, Is.EqualTo(pattern.Name));
            Assert.That(dto.Description, Is.EqualTo(pattern.Description));
            Assert.That(dto.PatternType, Is.EqualTo(pattern.PatternType));
            Assert.That(dto.StartDate, Is.EqualTo(pattern.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(pattern.EndDate));
            Assert.That(dto.ConfidenceLevel, Is.EqualTo(pattern.ConfidenceLevel));
            Assert.That(dto.Insights, Is.EqualTo(pattern.Insights));
            Assert.That(dto.CreatedAt, Is.EqualTo(pattern.CreatedAt));
            Assert.That(dto.IsHighConfidence, Is.EqualTo(pattern.IsHighConfidence()));
            Assert.That(dto.DurationDays, Is.EqualTo(pattern.GetDuration()));
        });
    }

    [Test]
    public void PatternDto_ToDto_HighConfidencePattern_ReturnsTrue()
    {
        // Arrange
        var pattern = new Core.Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Exercise Impact Pattern",
            Description = "Exercise improves sleep quality",
            PatternType = "Behavioral",
            StartDate = DateTime.UtcNow.AddDays(-60),
            EndDate = DateTime.UtcNow,
            ConfidenceLevel = 90,
            Insights = "Strong correlation between exercise and sleep quality",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = pattern.ToDto();

        // Assert
        Assert.That(dto.IsHighConfidence, Is.True);
        Assert.That(dto.ConfidenceLevel, Is.GreaterThan(70));
    }

    [Test]
    public void PatternDto_ToDto_LowConfidencePattern_ReturnsFalse()
    {
        // Arrange
        var pattern = new Core.Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Temperature Pattern",
            Description = "Room temperature might affect sleep",
            PatternType = "Environmental",
            StartDate = DateTime.UtcNow.AddDays(-14),
            EndDate = DateTime.UtcNow,
            ConfidenceLevel = 55,
            Insights = "Insufficient data to confirm correlation",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = pattern.ToDto();

        // Assert
        Assert.That(dto.IsHighConfidence, Is.False);
        Assert.That(dto.ConfidenceLevel, Is.LessThanOrEqualTo(70));
    }

    [Test]
    public void PatternDto_ToDto_CalculatesDurationCorrectly()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 1, 31);
        var pattern = new Core.Pattern
        {
            PatternId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Monthly Pattern",
            Description = "Pattern over one month",
            PatternType = "Monthly",
            StartDate = startDate,
            EndDate = endDate,
            ConfidenceLevel = 75,
            Insights = "Monthly analysis",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = pattern.ToDto();

        // Assert
        Assert.That(dto.DurationDays, Is.EqualTo(30));
        Assert.That(dto.DurationDays, Is.EqualTo((endDate - startDate).Days));
    }
}
