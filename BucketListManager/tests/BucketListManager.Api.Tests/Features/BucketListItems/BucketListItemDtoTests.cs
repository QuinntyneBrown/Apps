using BucketListManager.Api.Features.BucketListItems;
using BucketListManager.Core;
using NUnit.Framework;

namespace BucketListManager.Api.Tests.Features.BucketListItems;

[TestFixture]
public class BucketListItemDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenBucketListItemIsValid()
    {
        // Arrange
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Visit Paris",
            Description = "See the Eiffel Tower and Louvre",
            Category = Category.Travel,
            Priority = Priority.High,
            Status = ItemStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            CompletedDate = null,
            Notes = "Save $5000 for trip",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = item.ToDto();

        // Assert
        Assert.That(dto.BucketListItemId, Is.EqualTo(item.BucketListItemId));
        Assert.That(dto.UserId, Is.EqualTo(item.UserId));
        Assert.That(dto.Title, Is.EqualTo(item.Title));
        Assert.That(dto.Description, Is.EqualTo(item.Description));
        Assert.That(dto.Category, Is.EqualTo(item.Category));
        Assert.That(dto.Priority, Is.EqualTo(item.Priority));
        Assert.That(dto.Status, Is.EqualTo(item.Status));
        Assert.That(dto.TargetDate, Is.EqualTo(item.TargetDate));
        Assert.That(dto.CompletedDate, Is.EqualTo(item.CompletedDate));
        Assert.That(dto.Notes, Is.EqualTo(item.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(item.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullableProperties_WhenTheyAreNull()
    {
        // Arrange
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Learn Piano",
            Description = "Complete beginner course",
            Category = Category.Learning,
            Priority = Priority.Medium,
            Status = ItemStatus.NotStarted,
            TargetDate = null,
            CompletedDate = null,
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = item.ToDto();

        // Assert
        Assert.That(dto.TargetDate, Is.Null);
        Assert.That(dto.CompletedDate, Is.Null);
        Assert.That(dto.Notes, Is.Null);
    }

    [Test]
    public void ToDto_ShouldMapCompletedItem_WhenStatusIsCompleted()
    {
        // Arrange
        var completedDate = DateTime.UtcNow;
        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Run a Marathon",
            Description = "Complete a full 42km marathon",
            Category = Category.Health,
            Priority = Priority.High,
            Status = ItemStatus.Completed,
            TargetDate = completedDate.AddMonths(-1),
            CompletedDate = completedDate,
            Notes = "Finished in 4 hours",
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
        };

        // Act
        var dto = item.ToDto();

        // Assert
        Assert.That(dto.Status, Is.EqualTo(ItemStatus.Completed));
        Assert.That(dto.CompletedDate, Is.EqualTo(completedDate));
    }
}
