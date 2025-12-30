using MensGroupDiscussionTracker.Api.Features.Groups;
using MensGroupDiscussionTracker.Api.Features.Meetings;
using MensGroupDiscussionTracker.Api.Features.Topics;
using MensGroupDiscussionTracker.Api.Features.Resources;

namespace MensGroupDiscussionTracker.Api.Tests;

[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void GroupDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var group = new Core.Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            Description = "Test Description",
            MeetingSchedule = "Weekly on Thursdays",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = group.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GroupId, Is.EqualTo(group.GroupId));
            Assert.That(dto.CreatedByUserId, Is.EqualTo(group.CreatedByUserId));
            Assert.That(dto.Name, Is.EqualTo(group.Name));
            Assert.That(dto.Description, Is.EqualTo(group.Description));
            Assert.That(dto.MeetingSchedule, Is.EqualTo(group.MeetingSchedule));
            Assert.That(dto.IsActive, Is.EqualTo(group.IsActive));
            Assert.That(dto.CreatedAt, Is.EqualTo(group.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(group.UpdatedAt));
        });
    }

    [Test]
    public void MeetingDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var meeting = new Core.Meeting
        {
            MeetingId = Guid.NewGuid(),
            GroupId = Guid.NewGuid(),
            Title = "Test Meeting",
            MeetingDateTime = DateTime.UtcNow,
            Location = "Community Center",
            Notes = "Test Notes",
            AttendeeCount = 12,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = meeting.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MeetingId, Is.EqualTo(meeting.MeetingId));
            Assert.That(dto.GroupId, Is.EqualTo(meeting.GroupId));
            Assert.That(dto.Title, Is.EqualTo(meeting.Title));
            Assert.That(dto.MeetingDateTime, Is.EqualTo(meeting.MeetingDateTime));
            Assert.That(dto.Location, Is.EqualTo(meeting.Location));
            Assert.That(dto.Notes, Is.EqualTo(meeting.Notes));
            Assert.That(dto.AttendeeCount, Is.EqualTo(meeting.AttendeeCount));
            Assert.That(dto.CreatedAt, Is.EqualTo(meeting.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(meeting.UpdatedAt));
        });
    }

    [Test]
    public void TopicDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var topic = new Core.Topic
        {
            TopicId = Guid.NewGuid(),
            MeetingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Topic",
            Description = "Test Description",
            Category = Core.TopicCategory.FaithAndSpirituality,
            DiscussionNotes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = topic.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.TopicId, Is.EqualTo(topic.TopicId));
            Assert.That(dto.MeetingId, Is.EqualTo(topic.MeetingId));
            Assert.That(dto.UserId, Is.EqualTo(topic.UserId));
            Assert.That(dto.Title, Is.EqualTo(topic.Title));
            Assert.That(dto.Description, Is.EqualTo(topic.Description));
            Assert.That(dto.Category, Is.EqualTo(topic.Category));
            Assert.That(dto.DiscussionNotes, Is.EqualTo(topic.DiscussionNotes));
            Assert.That(dto.CreatedAt, Is.EqualTo(topic.CreatedAt));
            Assert.That(dto.UpdatedAt, Is.EqualTo(topic.UpdatedAt));
        });
    }

    [Test]
    public void ResourceDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var resource = new Core.Resource
        {
            ResourceId = Guid.NewGuid(),
            GroupId = Guid.NewGuid(),
            SharedByUserId = Guid.NewGuid(),
            Title = "Test Resource",
            Description = "Test Description",
            Url = "https://example.com",
            ResourceType = "Book",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = resource.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.ResourceId, Is.EqualTo(resource.ResourceId));
            Assert.That(dto.GroupId, Is.EqualTo(resource.GroupId));
            Assert.That(dto.SharedByUserId, Is.EqualTo(resource.SharedByUserId));
            Assert.That(dto.Title, Is.EqualTo(resource.Title));
            Assert.That(dto.Description, Is.EqualTo(resource.Description));
            Assert.That(dto.Url, Is.EqualTo(resource.Url));
            Assert.That(dto.ResourceType, Is.EqualTo(resource.ResourceType));
            Assert.That(dto.CreatedAt, Is.EqualTo(resource.CreatedAt));
        });
    }
}
