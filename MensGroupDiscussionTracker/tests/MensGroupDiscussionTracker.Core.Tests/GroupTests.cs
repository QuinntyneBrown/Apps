// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MensGroupDiscussionTracker.Core.Tests;

public class GroupTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGroup()
    {
        var groupId = Guid.NewGuid();
        var createdByUserId = Guid.NewGuid();
        var name = "Men's Bible Study";
        var description = "Weekly Bible study and fellowship";
        var meetingSchedule = "Tuesdays 7:00 PM";

        var group = new Group
        {
            GroupId = groupId,
            CreatedByUserId = createdByUserId,
            Name = name,
            Description = description,
            MeetingSchedule = meetingSchedule
        };

        Assert.Multiple(() =>
        {
            Assert.That(group.GroupId, Is.EqualTo(groupId));
            Assert.That(group.CreatedByUserId, Is.EqualTo(createdByUserId));
            Assert.That(group.Name, Is.EqualTo(name));
            Assert.That(group.Description, Is.EqualTo(description));
            Assert.That(group.MeetingSchedule, Is.EqualTo(meetingSchedule));
            Assert.That(group.IsActive, Is.True);
            Assert.That(group.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_InitializesCorrectly()
    {
        var group = new Group();

        Assert.Multiple(() =>
        {
            Assert.That(group.Name, Is.EqualTo(string.Empty));
            Assert.That(group.Description, Is.Null);
            Assert.That(group.MeetingSchedule, Is.Null);
            Assert.That(group.IsActive, Is.True);
            Assert.That(group.UpdatedAt, Is.Null);
            Assert.That(group.Meetings, Is.Not.Null);
            Assert.That(group.Meetings.Count, Is.EqualTo(0));
            Assert.That(group.Resources, Is.Not.Null);
            Assert.That(group.Resources.Count, Is.EqualTo(0));
            Assert.That(group.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Deactivate_WhenCalled_SetsIsActiveToFalseAndUpdatesTimestamp()
    {
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group",
            IsActive = true
        };

        var beforeCall = DateTime.UtcNow;
        group.Deactivate();
        var afterCall = DateTime.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(group.IsActive, Is.False);
            Assert.That(group.UpdatedAt, Is.Not.Null);
            Assert.That(group.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall));
            Assert.That(group.UpdatedAt!.Value, Is.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Meetings_CanAddMeetings_ToCollection()
    {
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group"
        };

        var meeting = new Meeting
        {
            MeetingId = Guid.NewGuid(),
            GroupId = group.GroupId,
            Title = "Weekly Meeting",
            MeetingDateTime = DateTime.UtcNow
        };

        group.Meetings.Add(meeting);

        Assert.Multiple(() =>
        {
            Assert.That(group.Meetings.Count, Is.EqualTo(1));
            Assert.That(group.Meetings.First().MeetingId, Is.EqualTo(meeting.MeetingId));
        });
    }

    [Test]
    public void Resources_CanAddResources_ToCollection()
    {
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = Guid.NewGuid(),
            Name = "Test Group"
        };

        var resource = new Resource
        {
            ResourceId = Guid.NewGuid(),
            GroupId = group.GroupId,
            SharedByUserId = Guid.NewGuid(),
            Title = "Recommended Book"
        };

        group.Resources.Add(resource);

        Assert.Multiple(() =>
        {
            Assert.That(group.Resources.Count, Is.EqualTo(1));
            Assert.That(group.Resources.First().ResourceId, Is.EqualTo(resource.ResourceId));
        });
    }
}
