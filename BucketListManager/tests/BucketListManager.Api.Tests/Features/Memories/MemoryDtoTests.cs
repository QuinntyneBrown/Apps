using BucketListManager.Api.Features.Memories;
using BucketListManager.Core;
using NUnit.Framework;

namespace BucketListManager.Api.Tests.Features.Memories;

[TestFixture]
public class MemoryDtoTests
{
    [Test]
    public void ToDto_ShouldMapAllProperties_WhenMemoryIsValid()
    {
        // Arrange
        var memoryDate = DateTime.UtcNow.AddDays(-30);
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "First Marathon",
            Description = "Amazing experience running through the city",
            MemoryDate = memoryDate,
            PhotoUrl = "https://example.com/photos/marathon.jpg",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = memory.ToDto();

        // Assert
        Assert.That(dto.MemoryId, Is.EqualTo(memory.MemoryId));
        Assert.That(dto.UserId, Is.EqualTo(memory.UserId));
        Assert.That(dto.BucketListItemId, Is.EqualTo(memory.BucketListItemId));
        Assert.That(dto.Title, Is.EqualTo(memory.Title));
        Assert.That(dto.Description, Is.EqualTo(memory.Description));
        Assert.That(dto.MemoryDate, Is.EqualTo(memory.MemoryDate));
        Assert.That(dto.PhotoUrl, Is.EqualTo(memory.PhotoUrl));
        Assert.That(dto.CreatedAt, Is.EqualTo(memory.CreatedAt));
    }

    [Test]
    public void ToDto_ShouldHandleNullableProperties_WhenTheyAreNull()
    {
        // Arrange
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Paris Trip",
            Description = null,
            MemoryDate = DateTime.UtcNow,
            PhotoUrl = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = memory.ToDto();

        // Assert
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.PhotoUrl, Is.Null);
    }

    [Test]
    public void ToDto_ShouldMapMemoryWithPhoto_WhenPhotoUrlIsProvided()
    {
        // Arrange
        var photoUrl = "https://example.com/photos/bucket-list-item.jpg";
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BucketListItemId = Guid.NewGuid(),
            Title = "Skydiving Adventure",
            Description = "Jumped from 15,000 feet",
            MemoryDate = DateTime.UtcNow.AddDays(-10),
            PhotoUrl = photoUrl,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = memory.ToDto();

        // Assert
        Assert.That(dto.PhotoUrl, Is.EqualTo(photoUrl));
    }
}
