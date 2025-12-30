// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void FamilyMemberAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new FamilyMemberAddedEvent
        {
            FamilyMemberId = familyMemberId,
            UserId = userId,
            Name = "John Doe",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("John Doe"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PointsAwardedEvent_Properties_CanBeSet()
    {
        // Arrange
        var familyMemberId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PointsAwardedEvent
        {
            FamilyMemberId = familyMemberId,
            Points = 50,
            Reason = "Completed chores",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(evt.Points, Is.EqualTo(50));
            Assert.That(evt.Reason, Is.EqualTo("Completed chores"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ChoreCreatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var choreId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ChoreCreatedEvent
        {
            ChoreId = choreId,
            UserId = userId,
            Name = "Wash Dishes",
            Frequency = ChoreFrequency.Daily,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ChoreId, Is.EqualTo(choreId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Wash Dishes"));
            Assert.That(evt.Frequency, Is.EqualTo(ChoreFrequency.Daily));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AssignmentCreatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var assignmentId = Guid.NewGuid();
        var choreId = Guid.NewGuid();
        var familyMemberId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AssignmentCreatedEvent
        {
            AssignmentId = assignmentId,
            ChoreId = choreId,
            FamilyMemberId = familyMemberId,
            DueDate = dueDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AssignmentId, Is.EqualTo(assignmentId));
            Assert.That(evt.ChoreId, Is.EqualTo(choreId));
            Assert.That(evt.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(evt.DueDate, Is.EqualTo(dueDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AssignmentCompletedEvent_Properties_CanBeSet()
    {
        // Arrange
        var assignmentId = Guid.NewGuid();
        var familyMemberId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AssignmentCompletedEvent
        {
            AssignmentId = assignmentId,
            FamilyMemberId = familyMemberId,
            CompletedDate = completedDate,
            PointsEarned = 20,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AssignmentId, Is.EqualTo(assignmentId));
            Assert.That(evt.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.PointsEarned, Is.EqualTo(20));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RewardRedeemedEvent_Properties_CanBeSet()
    {
        // Arrange
        var rewardId = Guid.NewGuid();
        var familyMemberId = Guid.NewGuid();
        var redeemedDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RewardRedeemedEvent
        {
            RewardId = rewardId,
            FamilyMemberId = familyMemberId,
            PointCost = 100,
            RedeemedDate = redeemedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RewardId, Is.EqualTo(rewardId));
            Assert.That(evt.FamilyMemberId, Is.EqualTo(familyMemberId));
            Assert.That(evt.PointCost, Is.EqualTo(100));
            Assert.That(evt.RedeemedDate, Is.EqualTo(redeemedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
