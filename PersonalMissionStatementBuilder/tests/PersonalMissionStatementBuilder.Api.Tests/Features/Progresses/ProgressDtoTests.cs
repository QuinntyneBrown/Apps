using PersonalMissionStatementBuilder.Api.Features.Progresses;
using PersonalMissionStatementBuilder.Core;

namespace PersonalMissionStatementBuilder.Api.Tests.Features.Progresses;

[TestFixture]
public class ProgressDtoTests
{
    [Test]
    public void ToDto_ValidProgress_MapsAllProperties()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var progressDate = DateTime.UtcNow.AddDays(-2);
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddDays(1);

        var progress = new Progress
        {
            ProgressId = progressId,
            GoalId = goalId,
            UserId = userId,
            ProgressDate = progressDate,
            Notes = "Made significant progress today",
            CompletionPercentage = 75.5,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
        };

        // Act
        var dto = progress.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.ProgressId, Is.EqualTo(progressId));
        Assert.That(dto.GoalId, Is.EqualTo(goalId));
        Assert.That(dto.UserId, Is.EqualTo(userId));
        Assert.That(dto.ProgressDate, Is.EqualTo(progressDate));
        Assert.That(dto.Notes, Is.EqualTo("Made significant progress today"));
        Assert.That(dto.CompletionPercentage, Is.EqualTo(75.5));
        Assert.That(dto.CreatedAt, Is.EqualTo(createdAt));
        Assert.That(dto.UpdatedAt, Is.EqualTo(updatedAt));
    }

    [Test]
    public void ToDto_ProgressWithNullUpdatedAt_MapsCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProgressDate = DateTime.UtcNow,
            Notes = "First update",
            CompletionPercentage = 10.0,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = progress.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.UpdatedAt, Is.Null);
    }
}
