using BucketListManager.Api.Features.Milestones;
using BucketListManager.Core;
using NUnit.Framework;

namespace BucketListManager.Api.Tests.Features.Milestones;

[TestFixture]
public class MilestoneDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenMilestoneIsValid()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Book flights",
            Description = "Research and book the best flight deals",
            IsCompleted = false,
            CompletedDate = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = milestone.ToDto();

        // Assert
        Assert.That(dto.MilestoneId, Is.EqualTo(milestone.MilestoneId));
        Assert.That(dto.UserId, Is.EqualTo(milestone.UserId));
        Assert.That(dto.BucketListItemId, Is.EqualTo(milestone.BucketListItemId));
        Assert.That(dto.Title, Is.EqualTo(milestone.Title));
        Assert.That(dto.Description, Is.EqualTo(milestone.Description));
        Assert.That(dto.IsCompleted, Is.EqualTo(milestone.IsCompleted));
        Assert.That(dto.CompletedDate, Is.EqualTo(milestone.CompletedDate));
        Assert.That(dto.CreatedAt, Is.EqualTo(milestone.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullableProperties_WhenTheyAreNull()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Research destinations",
            Description = null,
            IsCompleted = false,
            CompletedDate = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = milestone.ToDto();

        // Assert
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.CompletedDate, Is.Null);
    }

    [Test]
    public void ToDto_ShouldMapCompletedMilestone_WhenIsCompletedIsTrue()
    {
        // Arrange
        var completedDate = DateTime.UtcNow;
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Get passport",
            Description = "Apply for and receive passport",
            IsCompleted = true,
            CompletedDate = completedDate,
            CreatedAt = DateTime.UtcNow.AddMonths(-2),
        };

        // Act
        var dto = milestone.ToDto();

        // Assert
        Assert.That(dto.IsCompleted, Is.True);
        Assert.That(dto.CompletedDate, Is.EqualTo(completedDate));
    }
}
