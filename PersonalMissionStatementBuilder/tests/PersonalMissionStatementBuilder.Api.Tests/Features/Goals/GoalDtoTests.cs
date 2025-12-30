using PersonalMissionStatementBuilder.Api.Features.Goals;
using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Goals;

[TestFixture]
public class GoalDtoTests
{
    [Test]
    public void ToDto_ValidGoal_MapsAllProperties()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var missionStatementId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddDays(1);
        var targetDate = DateTime.UtcNow.AddDays(30);
        var completedDate = DateTime.UtcNow.AddDays(20);

        var goal = new Goal
        {
            GoalId = goalId,
            MissionStatementId = missionStatementId,
            UserId = userId,
            Title = "Learn a new skill",
            Description = "Complete an online course",
            Status = GoalStatus.Completed,
            TargetDate = targetDate,
            CompletedDate = completedDate,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
        };

        // Act
        var dto = goal.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.GoalId, Is.EqualTo(goalId));
        Assert.That(dto.MissionStatementId, Is.EqualTo(missionStatementId));
        Assert.That(dto.UserId, Is.EqualTo(userId));
        Assert.That(dto.Title, Is.EqualTo("Learn a new skill"));
        Assert.That(dto.Description, Is.EqualTo("Complete an online course"));
        Assert.That(dto.Status, Is.EqualTo(GoalStatus.Completed));
        Assert.That(dto.TargetDate, Is.EqualTo(targetDate));
        Assert.That(dto.CompletedDate, Is.EqualTo(completedDate));
        Assert.That(dto.CreatedAt, Is.EqualTo(createdAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(updatedAt));
    }

    [Test]
    public void ToDto_GoalWithNullableFields_MapsCorrectly()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Simple Goal",
            Status = GoalStatus.NotStarted,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = goal.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.MissionStatementId, Is.Null);
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.TargetDate, Is.Null);
        Assert.That(dto.CompletedDate, Is.Null);
        Assert.That(dto.UpdatedAt, Is.Null);
    }
}
