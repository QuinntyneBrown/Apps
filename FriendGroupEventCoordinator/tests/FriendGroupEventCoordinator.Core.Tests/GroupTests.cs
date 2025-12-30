// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Core.Tests;

public class GroupTests
{
    [Test]
    public void Group_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var createdByUserId = Guid.NewGuid();
        var name = "Weekend Warriors";
        var description = "Friends who love weekend adventures";

        // Act
        var group = new Group
        {
            GroupId = groupId,
            CreatedByUserId = createdByUserId,
            Name = name,
            Description = description,
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(group.GroupId, Is.EqualTo(groupId));
            Assert.That(group.CreatedByUserId, Is.EqualTo(createdByUserId));
            Assert.That(group.Name, Is.EqualTo(name));
            Assert.That(group.Description, Is.EqualTo(description));
            Assert.That(group.IsActive, Is.True);
            Assert.That(group.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(group.Members, Is.Not.Null);
            Assert.That(group.Events, Is.Not.Null);
        });
    }

    [Test]
    public void Group_DefaultValues_AreSetCorrectly()
    {
        // Act
        var group = new Group();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(group.Name, Is.EqualTo(string.Empty));
            Assert.That(group.IsActive, Is.True);
            Assert.That(group.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(group.Members, Is.Not.Null);
            Assert.That(group.Members.Count, Is.EqualTo(0));
            Assert.That(group.Events, Is.Not.Null);
            Assert.That(group.Events.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Deactivate_SetsIsActiveToFalseAndUpdatesTimestamp()
    {
        // Arrange
        var group = new Group { IsActive = true };
        var beforeCall = DateTime.UtcNow;

        // Act
        group.Deactivate();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(group.IsActive, Is.False);
            Assert.That(group.UpdatedAt, Is.Not.Null);
            Assert.That(group.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Deactivate_WhenAlreadyInactive_RemainsInactive()
    {
        // Arrange
        var group = new Group { IsActive = false };

        // Act
        group.Deactivate();

        // Assert
        Assert.That(group.IsActive, Is.False);
    }

    [Test]
    public void GetActiveMemberCount_WithNoMembers_ReturnsZero()
    {
        // Arrange
        var group = new Group();

        // Act
        var count = group.GetActiveMemberCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetActiveMemberCount_WithOnlyActiveMembers_ReturnsCorrectCount()
    {
        // Arrange
        var group = new Group
        {
            Members = new List<Member>
            {
                new Member { MemberId = Guid.NewGuid(), IsActive = true },
                new Member { MemberId = Guid.NewGuid(), IsActive = true },
                new Member { MemberId = Guid.NewGuid(), IsActive = true }
            }
        };

        // Act
        var count = group.GetActiveMemberCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void GetActiveMemberCount_WithMixedActiveAndInactiveMembers_ReturnsOnlyActiveCount()
    {
        // Arrange
        var group = new Group
        {
            Members = new List<Member>
            {
                new Member { MemberId = Guid.NewGuid(), IsActive = true },
                new Member { MemberId = Guid.NewGuid(), IsActive = false },
                new Member { MemberId = Guid.NewGuid(), IsActive = true },
                new Member { MemberId = Guid.NewGuid(), IsActive = false },
                new Member { MemberId = Guid.NewGuid(), IsActive = true }
            }
        };

        // Act
        var count = group.GetActiveMemberCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void GetActiveMemberCount_WithOnlyInactiveMembers_ReturnsZero()
    {
        // Arrange
        var group = new Group
        {
            Members = new List<Member>
            {
                new Member { MemberId = Guid.NewGuid(), IsActive = false },
                new Member { MemberId = Guid.NewGuid(), IsActive = false }
            }
        };

        // Act
        var count = group.GetActiveMemberCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void Group_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var group = new Group
        {
            Description = null,
            UpdatedAt = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(group.Description, Is.Null);
            Assert.That(group.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void Group_WithEvents_MaintainsEventCollection()
    {
        // Arrange
        var group = new Group();
        var event1 = new Event { EventId = Guid.NewGuid(), Title = "BBQ Party" };
        var event2 = new Event { EventId = Guid.NewGuid(), Title = "Game Night" };

        // Act
        group.Events.Add(event1);
        group.Events.Add(event2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(group.Events.Count, Is.EqualTo(2));
            Assert.That(group.Events, Contains.Item(event1));
            Assert.That(group.Events, Contains.Item(event2));
        });
    }

    [Test]
    public void Group_Name_CanBeSetToVariousValues()
    {
        // Arrange
        var names = new[] { "Book Club", "Hiking Group", "Poker Night", "Study Group" };

        // Act & Assert
        foreach (var name in names)
        {
            var group = new Group { Name = name };
            Assert.That(group.Name, Is.EqualTo(name));
        }
    }
}
