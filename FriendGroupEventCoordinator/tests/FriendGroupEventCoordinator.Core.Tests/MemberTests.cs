// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Core.Tests;

public class MemberTests
{
    [Test]
    public void Member_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "John Doe";
        var email = "john@example.com";
        var joinedAt = DateTime.UtcNow;

        // Act
        var member = new Member
        {
            MemberId = memberId,
            GroupId = groupId,
            UserId = userId,
            Name = name,
            Email = email,
            IsAdmin = true,
            IsActive = true,
            JoinedAt = joinedAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.MemberId, Is.EqualTo(memberId));
            Assert.That(member.GroupId, Is.EqualTo(groupId));
            Assert.That(member.UserId, Is.EqualTo(userId));
            Assert.That(member.Name, Is.EqualTo(name));
            Assert.That(member.Email, Is.EqualTo(email));
            Assert.That(member.IsAdmin, Is.True);
            Assert.That(member.IsActive, Is.True);
            Assert.That(member.JoinedAt, Is.EqualTo(joinedAt));
            Assert.That(member.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(member.RSVPs, Is.Not.Null);
        });
    }

    [Test]
    public void Member_DefaultValues_AreSetCorrectly()
    {
        // Act
        var member = new Member();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.Name, Is.EqualTo(string.Empty));
            Assert.That(member.IsAdmin, Is.False);
            Assert.That(member.IsActive, Is.True);
            Assert.That(member.JoinedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(member.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(member.RSVPs, Is.Not.Null);
            Assert.That(member.RSVPs.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Remove_SetsIsActiveToFalseAndUpdatesTimestamp()
    {
        // Arrange
        var member = new Member { IsActive = true };
        var beforeCall = DateTime.UtcNow;

        // Act
        member.Remove();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.IsActive, Is.False);
            Assert.That(member.UpdatedAt, Is.Not.Null);
            Assert.That(member.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Remove_WhenAlreadyInactive_RemainsInactive()
    {
        // Arrange
        var member = new Member { IsActive = false };

        // Act
        member.Remove();

        // Assert
        Assert.That(member.IsActive, Is.False);
    }

    [Test]
    public void PromoteToAdmin_SetsIsAdminToTrueAndUpdatesTimestamp()
    {
        // Arrange
        var member = new Member { IsAdmin = false };
        var beforeCall = DateTime.UtcNow;

        // Act
        member.PromoteToAdmin();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.IsAdmin, Is.True);
            Assert.That(member.UpdatedAt, Is.Not.Null);
            Assert.That(member.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void PromoteToAdmin_WhenAlreadyAdmin_RemainsAdmin()
    {
        // Arrange
        var member = new Member { IsAdmin = true };

        // Act
        member.PromoteToAdmin();

        // Assert
        Assert.That(member.IsAdmin, Is.True);
    }

    [Test]
    public void Member_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var member = new Member
        {
            Email = null,
            UpdatedAt = null,
            Group = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.Email, Is.Null);
            Assert.That(member.UpdatedAt, Is.Null);
            Assert.That(member.Group, Is.Null);
        });
    }

    [Test]
    public void Member_WithRSVPs_MaintainsRSVPCollection()
    {
        // Arrange
        var member = new Member();
        var rsvp1 = new RSVP { RSVPId = Guid.NewGuid(), Response = RSVPResponse.Yes };
        var rsvp2 = new RSVP { RSVPId = Guid.NewGuid(), Response = RSVPResponse.No };

        // Act
        member.RSVPs.Add(rsvp1);
        member.RSVPs.Add(rsvp2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.RSVPs.Count, Is.EqualTo(2));
            Assert.That(member.RSVPs, Contains.Item(rsvp1));
            Assert.That(member.RSVPs, Contains.Item(rsvp2));
        });
    }

    [Test]
    public void Member_Email_CanBeValidEmailAddress()
    {
        // Arrange & Act
        var member = new Member { Email = "test@example.com" };

        // Assert
        Assert.That(member.Email, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void Member_CanBeActiveAndNotAdmin()
    {
        // Arrange & Act
        var member = new Member
        {
            IsActive = true,
            IsAdmin = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.IsActive, Is.True);
            Assert.That(member.IsAdmin, Is.False);
        });
    }

    [Test]
    public void Member_CanBeInactiveAndAdmin()
    {
        // Arrange & Act
        var member = new Member
        {
            IsActive = false,
            IsAdmin = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(member.IsActive, Is.False);
            Assert.That(member.IsAdmin, Is.True);
        });
    }

    [Test]
    public void Member_JoinedAt_CanBeSetToSpecificDateTime()
    {
        // Arrange
        var specificDate = new DateTime(2024, 1, 1, 12, 0, 0);
        var member = new Member();

        // Act
        member.JoinedAt = specificDate;

        // Assert
        Assert.That(member.JoinedAt, Is.EqualTo(specificDate));
    }
}
