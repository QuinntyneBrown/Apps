// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class MilestoneTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesMilestone()
    {
        // Arrange & Act
        var milestone = new Milestone();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(milestone.Name, Is.EqualTo(string.Empty));
            Assert.That(milestone.Description, Is.Null);
            Assert.That(milestone.TargetDate, Is.Null);
            Assert.That(milestone.IsAchieved, Is.False);
            Assert.That(milestone.AchievementDate, Is.Null);
            Assert.That(milestone.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(milestone.Project, Is.Null);
            Assert.That(milestone.Tasks, Is.Not.Null);
            Assert.That(milestone.Tasks, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var targetDate = DateTime.UtcNow.AddDays(14);

        // Act
        var milestone = new Milestone
        {
            MilestoneId = milestoneId,
            ProjectId = projectId,
            Name = "Test Milestone",
            Description = "Test Description",
            TargetDate = targetDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(milestone.ProjectId, Is.EqualTo(projectId));
            Assert.That(milestone.Name, Is.EqualTo("Test Milestone"));
            Assert.That(milestone.Description, Is.EqualTo("Test Description"));
            Assert.That(milestone.TargetDate, Is.EqualTo(targetDate));
        });
    }

    [Test]
    public void Achieve_WhenCalled_SetsIsAchievedToTrue()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false
        };

        // Act
        milestone.Achieve();

        // Assert
        Assert.That(milestone.IsAchieved, Is.True);
    }

    [Test]
    public void Achieve_WhenCalled_SetsAchievementDate()
    {
        // Arrange
        var milestone = new Milestone();
        var beforeAchieve = DateTime.UtcNow;

        // Act
        milestone.Achieve();

        // Assert
        var afterAchieve = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(milestone.AchievementDate, Is.Not.Null);
            Assert.That(milestone.AchievementDate.Value, Is.GreaterThanOrEqualTo(beforeAchieve));
            Assert.That(milestone.AchievementDate.Value, Is.LessThanOrEqualTo(afterAchieve));
        });
    }

    [Test]
    public void IsOverdue_WhenNotAchievedAndTargetDatePassed_ReturnsTrue()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void IsOverdue_WhenNotAchievedAndTargetDateInFuture_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WhenAchievedAndTargetDatePassed_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = true,
            TargetDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WhenTargetDateIsNull_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = false,
            TargetDate = null
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_WhenAchievedAndNoTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            IsAchieved = true,
            TargetDate = null
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void Tasks_Collection_CanBeModified()
    {
        // Arrange
        var milestone = new Milestone();
        var task = new ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            Title = "Task 1"
        };

        // Act
        milestone.Tasks.Add(task);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.Tasks.Count, Is.EqualTo(1));
            Assert.That(milestone.Tasks.First(), Is.EqualTo(task));
        });
    }

    [Test]
    public void Project_NavigationProperty_CanBeSet()
    {
        // Arrange
        var milestone = new Milestone();
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            Name = "Test Project"
        };

        // Act
        milestone.Project = project;

        // Assert
        Assert.That(milestone.Project, Is.EqualTo(project));
    }
}
