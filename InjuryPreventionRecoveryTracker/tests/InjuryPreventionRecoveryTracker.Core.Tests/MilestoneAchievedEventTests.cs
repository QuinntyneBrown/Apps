// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class MilestoneAchievedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMilestoneAchievedEvent()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var name = "First Steps";
        var achievedDate = new DateTime(2025, 1, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MilestoneAchievedEvent
        {
            MilestoneId = milestoneId,
            UserId = userId,
            InjuryId = injuryId,
            Name = name,
            AchievedDate = achievedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.InjuryId, Is.EqualTo(injuryId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.AchievedDate, Is.EqualTo(achievedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new MilestoneAchievedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Name, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var achievedDate = new DateTime(2025, 1, 1);
        var timestamp = new DateTime(2025, 1, 1, 10, 0, 0);

        var evt1 = new MilestoneAchievedEvent
        {
            MilestoneId = milestoneId,
            UserId = userId,
            InjuryId = injuryId,
            Name = "Test Milestone",
            AchievedDate = achievedDate,
            Timestamp = timestamp
        };

        var evt2 = new MilestoneAchievedEvent
        {
            MilestoneId = milestoneId,
            UserId = userId,
            InjuryId = injuryId,
            Name = "Test Milestone",
            AchievedDate = achievedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new MilestoneAchievedEvent
        {
            MilestoneId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryId = Guid.NewGuid(),
            Name = "Original",
            AchievedDate = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Name = "Modified" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.MilestoneId, Is.EqualTo(original.MilestoneId));
            Assert.That(modified.Name, Is.EqualTo("Modified"));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }
}
