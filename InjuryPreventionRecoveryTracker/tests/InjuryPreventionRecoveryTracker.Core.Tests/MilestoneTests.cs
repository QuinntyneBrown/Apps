// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class MilestoneTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMilestone()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var name = "Walk without crutches";
        var description = "First walk without assistance";
        var targetDate = new DateTime(2025, 2, 1);
        var achievedDate = new DateTime(2025, 1, 30);
        var isAchieved = true;
        var notes = "Went well";

        // Act
        var milestone = new Milestone
        {
            MilestoneId = milestoneId,
            UserId = userId,
            InjuryId = injuryId,
            Name = name,
            Description = description,
            TargetDate = targetDate,
            AchievedDate = achievedDate,
            IsAchieved = isAchieved,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(milestone.UserId, Is.EqualTo(userId));
            Assert.That(milestone.InjuryId, Is.EqualTo(injuryId));
            Assert.That(milestone.Name, Is.EqualTo(name));
            Assert.That(milestone.Description, Is.EqualTo(description));
            Assert.That(milestone.TargetDate, Is.EqualTo(targetDate));
            Assert.That(milestone.AchievedDate, Is.EqualTo(achievedDate));
            Assert.That(milestone.IsAchieved, Is.EqualTo(isAchieved));
            Assert.That(milestone.Notes, Is.EqualTo(notes));
            Assert.That(milestone.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var milestone = new Milestone();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.Name, Is.EqualTo(string.Empty));
            Assert.That(milestone.IsAchieved, Is.False);
            Assert.That(milestone.TargetDate, Is.Null);
            Assert.That(milestone.AchievedDate, Is.Null);
        });
    }

    [Test]
    public void MarkAsAchieved_SetsIsAchievedAndDate()
    {
        // Arrange
        var milestone = new Milestone { IsAchieved = false, AchievedDate = null };

        // Act
        milestone.MarkAsAchieved();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsAchieved, Is.True);
            Assert.That(milestone.AchievedDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsAchieved_AlreadyAchieved_UpdatesDate()
    {
        // Arrange
        var oldDate = new DateTime(2025, 1, 1);
        var milestone = new Milestone { IsAchieved = true, AchievedDate = oldDate };

        // Act
        milestone.MarkAsAchieved();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsAchieved, Is.True);
            Assert.That(milestone.AchievedDate, Is.GreaterThan(oldDate));
        });
    }

    [Test]
    public void IsOverdue_NotAchievedAndPastTargetDate_ReturnsTrue()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = milestone.IsOverdue();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOverdue_NotAchievedAndFutureTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = milestone.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_AchievedAndPastTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = true,
            TargetDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = milestone.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOverdue_NoTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = null
        };

        // Act
        var result = milestone.IsOverdue();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void TargetDate_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var milestone = new Milestone { TargetDate = null };

        // Assert
        Assert.That(milestone.TargetDate, Is.Null);
    }

    [Test]
    public void AchievedDate_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var milestone = new Milestone { AchievedDate = null };

        // Assert
        Assert.That(milestone.AchievedDate, Is.Null);
    }

    [Test]
    public void Description_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var milestone = new Milestone { Description = null };

        // Assert
        Assert.That(milestone.Description, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var milestone = new Milestone { Notes = null };

        // Assert
        Assert.That(milestone.Notes, Is.Null);
    }

    [Test]
    public void Injury_NavigationProperty_CanBeSet()
    {
        // Arrange
        var injury = new Injury { InjuryId = Guid.NewGuid() };
        var milestone = new Milestone();

        // Act
        milestone.Injury = injury;

        // Assert
        Assert.That(milestone.Injury, Is.EqualTo(injury));
    }
}
