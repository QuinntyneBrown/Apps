// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core.Tests;

public class MeetingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMeeting()
    {
        var meetingId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var title = "Monthly Discussion";
        var meetingDateTime = DateTime.UtcNow;
        var location = "Community Center";
        var notes = "Great discussion";
        var attendeeCount = 12;

        var meeting = new Meeting
        {
            MeetingId = meetingId,
            GroupId = groupId,
            Title = title,
            MeetingDateTime = meetingDateTime,
            Location = location,
            Notes = notes,
            AttendeeCount = attendeeCount
        };

        Assert.Multiple(() =>
        {
            Assert.That(meeting.MeetingId, Is.EqualTo(meetingId));
            Assert.That(meeting.GroupId, Is.EqualTo(groupId));
            Assert.That(meeting.Title, Is.EqualTo(title));
            Assert.That(meeting.MeetingDateTime, Is.EqualTo(meetingDateTime));
            Assert.That(meeting.Location, Is.EqualTo(location));
            Assert.That(meeting.Notes, Is.EqualTo(notes));
            Assert.That(meeting.AttendeeCount, Is.EqualTo(attendeeCount));
        });
    }

    [Test]
    public void Topics_CanAddTopics_ToCollection()
    {
        var meeting = new Meeting { MeetingId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Title = "Test" };
        var topic = new Topic { TopicId = Guid.NewGuid(), MeetingId = meeting.MeetingId, UserId = Guid.NewGuid(), Title = "Topic 1" };
        meeting.Topics.Add(topic);
        Assert.That(meeting.Topics.Count, Is.EqualTo(1));
    }
}

public class ResourceTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesResource()
    {
        var resourceId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var sharedByUserId = Guid.NewGuid();
        var title = "Recommended Book";
        var description = "Great read for spiritual growth";
        var url = "https://example.com/book";
        var resourceType = "Book";

        var resource = new Resource
        {
            ResourceId = resourceId,
            GroupId = groupId,
            SharedByUserId = sharedByUserId,
            Title = title,
            Description = description,
            Url = url,
            ResourceType = resourceType
        };

        Assert.Multiple(() =>
        {
            Assert.That(resource.ResourceId, Is.EqualTo(resourceId));
            Assert.That(resource.GroupId, Is.EqualTo(groupId));
            Assert.That(resource.SharedByUserId, Is.EqualTo(sharedByUserId));
            Assert.That(resource.Title, Is.EqualTo(title));
            Assert.That(resource.Description, Is.EqualTo(description));
            Assert.That(resource.Url, Is.EqualTo(url));
            Assert.That(resource.ResourceType, Is.EqualTo(resourceType));
        });
    }
}

public class TopicTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTopic()
    {
        var topicId = Guid.NewGuid();
        var meetingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Faith and Work";
        var description = "Integrating faith in workplace";
        var category = TopicCategory.FaithAndSpirituality;
        var discussionNotes = "Key insights shared";

        var topic = new Topic
        {
            TopicId = topicId,
            MeetingId = meetingId,
            UserId = userId,
            Title = title,
            Description = description,
            Category = category,
            DiscussionNotes = discussionNotes
        };

        Assert.Multiple(() =>
        {
            Assert.That(topic.TopicId, Is.EqualTo(topicId));
            Assert.That(topic.MeetingId, Is.EqualTo(meetingId));
            Assert.That(topic.UserId, Is.EqualTo(userId));
            Assert.That(topic.Title, Is.EqualTo(title));
            Assert.That(topic.Description, Is.EqualTo(description));
            Assert.That(topic.Category, Is.EqualTo(category));
            Assert.That(topic.DiscussionNotes, Is.EqualTo(discussionNotes));
        });
    }

    [Test]
    public void Category_CanBeSetToAllValues()
    {
        var topic = new Topic { TopicId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test" };
        topic.Category = TopicCategory.FaithAndSpirituality;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.FaithAndSpirituality));
        topic.Category = TopicCategory.RelationshipsAndFamily;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.RelationshipsAndFamily));
        topic.Category = TopicCategory.CareerAndWork;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.CareerAndWork));
        topic.Category = TopicCategory.PersonalGrowth;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.PersonalGrowth));
        topic.Category = TopicCategory.MentalHealth;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.MentalHealth));
        topic.Category = TopicCategory.PhysicalHealth;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.PhysicalHealth));
        topic.Category = TopicCategory.Financial;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.Financial));
        topic.Category = TopicCategory.CommunityAndService;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.CommunityAndService));
        topic.Category = TopicCategory.Leadership;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.Leadership));
        topic.Category = TopicCategory.Other;
        Assert.That(topic.Category, Is.EqualTo(TopicCategory.Other));
    }
}

public class TopicCategoryTests
{
    [Test]
    public void TopicCategory_AllValues_CanBeAssigned()
    {
        Assert.That((int)TopicCategory.FaithAndSpirituality, Is.EqualTo(0));
        Assert.That((int)TopicCategory.RelationshipsAndFamily, Is.EqualTo(1));
        Assert.That((int)TopicCategory.CareerAndWork, Is.EqualTo(2));
        Assert.That((int)TopicCategory.PersonalGrowth, Is.EqualTo(3));
        Assert.That((int)TopicCategory.MentalHealth, Is.EqualTo(4));
        Assert.That((int)TopicCategory.PhysicalHealth, Is.EqualTo(5));
        Assert.That((int)TopicCategory.Financial, Is.EqualTo(6));
        Assert.That((int)TopicCategory.CommunityAndService, Is.EqualTo(7));
        Assert.That((int)TopicCategory.Leadership, Is.EqualTo(8));
        Assert.That((int)TopicCategory.Other, Is.EqualTo(9));
    }
}

public class DomainEventsTests
{
    [Test]
    public void GroupCreatedEvent_ValidParameters_CreatesEvent()
    {
        var groupId = Guid.NewGuid();
        var createdByUserId = Guid.NewGuid();
        var name = "Test Group";
        var timestamp = DateTime.UtcNow;

        var evt = new GroupCreatedEvent
        {
            GroupId = groupId,
            CreatedByUserId = createdByUserId,
            Name = name,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.GroupId, Is.EqualTo(groupId));
            Assert.That(evt.CreatedByUserId, Is.EqualTo(createdByUserId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MeetingCreatedEvent_ValidParameters_CreatesEvent()
    {
        var meetingId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var title = "Meeting Title";
        var meetingDateTime = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var evt = new MeetingCreatedEvent
        {
            MeetingId = meetingId,
            GroupId = groupId,
            Title = title,
            MeetingDateTime = meetingDateTime,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.MeetingId, Is.EqualTo(meetingId));
            Assert.That(evt.GroupId, Is.EqualTo(groupId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.MeetingDateTime, Is.EqualTo(meetingDateTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ResourceSharedEvent_ValidParameters_CreatesEvent()
    {
        var resourceId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var sharedByUserId = Guid.NewGuid();
        var title = "Resource Title";
        var timestamp = DateTime.UtcNow;

        var evt = new ResourceSharedEvent
        {
            ResourceId = resourceId,
            GroupId = groupId,
            SharedByUserId = sharedByUserId,
            Title = title,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.ResourceId, Is.EqualTo(resourceId));
            Assert.That(evt.GroupId, Is.EqualTo(groupId));
            Assert.That(evt.SharedByUserId, Is.EqualTo(sharedByUserId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TopicCreatedEvent_ValidParameters_CreatesEvent()
    {
        var topicId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Topic Title";
        var category = TopicCategory.FaithAndSpirituality;
        var timestamp = DateTime.UtcNow;

        var evt = new TopicCreatedEvent
        {
            TopicId = topicId,
            UserId = userId,
            Title = title,
            Category = category,
            Timestamp = timestamp
        };

        Assert.Multiple(() =>
        {
            Assert.That(evt.TopicId, Is.EqualTo(topicId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
