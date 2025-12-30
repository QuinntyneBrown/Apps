using HabitFormationApp.Api.Features.Streaks;
using HabitFormationApp.Core;
using NUnit.Framework;

namespace HabitFormationApp.Api.Tests.Features.Streaks;

[TestFixture]
public class StreakDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenStreakIsValid()
    {
        // Arrange
        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            CurrentStreak = 15,
            LongestStreak = 22,
            LastCompletedDate = DateTime.UtcNow.AddDays(-1),
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        // Act
        var dto = streak.ToDto();

        // Assert
        Assert.That(dto.StreakId, Is.EqualTo(streak.StreakId));
        Assert.That(dto.HabitId, Is.EqualTo(streak.HabitId));
        Assert.That(dto.CurrentStreak, Is.EqualTo(streak.CurrentStreak));
        Assert.That(dto.LongestStreak, Is.EqualTo(streak.LongestStreak));
        Assert.That(dto.LastCompletedDate, Is.EqualTo(streak.LastCompletedDate));
        Assert.That(dto.CreatedAt, Is.EqualTo(streak.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullLastCompletedDate()
    {
        // Arrange
        var streak = new Streak
        {
            StreakId = Guid.NewGuid(),
            HabitId = Guid.NewGuid(),
            CurrentStreak = 0,
            LongestStreak = 0,
            LastCompletedDate = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = streak.ToDto();

        // Assert
        Assert.That(dto.LastCompletedDate, Is.Null);
    }
}
