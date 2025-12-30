// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectTaskTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesProjectTask()
    {
        // Arrange & Act
        var task = new ProjectTask();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.ProjectTaskId, Is.EqualTo(Guid.Empty));
            Assert.That(task.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(task.MilestoneId, Is.Null);
            Assert.That(task.Title, Is.EqualTo(string.Empty));
            Assert.That(task.Description, Is.Null);
            Assert.That(task.DueDate, Is.Null);
            Assert.That(task.IsCompleted, Is.False);
            Assert.That(task.CompletionDate, Is.Null);
            Assert.That(task.EstimatedHours, Is.Null);
            Assert.That(task.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(task.Project, Is.Null);
            Assert.That(task.Milestone, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var milestoneId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(7);

        // Act
        var task = new ProjectTask
        {
            ProjectTaskId = taskId,
            ProjectId = projectId,
            MilestoneId = milestoneId,
            Title = "Test Task",
            Description = "Test Description",
            DueDate = dueDate,
            EstimatedHours = 5.5
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(task.ProjectTaskId, Is.EqualTo(taskId));
            Assert.That(task.ProjectId, Is.EqualTo(projectId));
            Assert.That(task.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(task.Title, Is.EqualTo("Test Task"));
            Assert.That(task.Description, Is.EqualTo("Test Description"));
            Assert.That(task.DueDate, Is.EqualTo(dueDate));
            Assert.That(task.EstimatedHours, Is.EqualTo(5.5));
        });
    }

    [Test]
    public void Complete_WhenCalled_SetsIsCompletedToTrue()
    {
        // Arrange
        var task = new ProjectTask
        {
            IsCompleted = false
        };

        // Act
        task.Complete();

        // Assert
        Assert.That(task.IsCompleted, Is.True);
    }

    [Test]
    public void Complete_WhenCalled_SetsCompletionDate()
    {
        // Arrange
        var task = new ProjectTask();
        var beforeComplete = DateTime.UtcNow;

        // Act
        task.Complete();

        // Assert
        var afterComplete = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(task.CompletionDate, Is.Not.Null);
            Assert.That(task.CompletionDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(task.CompletionDate.Value, Is.LessThanOrEqualTo(afterComplete));
        });
    }

    [Test]
    public void Complete_CalledMultipleTimes_UpdatesCompletionDate()
    {
        // Arrange
        var task = new ProjectTask();
        task.Complete();
        var firstCompletionDate = task.CompletionDate;

        System.Threading.Thread.Sleep(10); // Small delay to ensure different timestamp

        // Act
        task.Complete();

        // Assert
        Assert.That(task.CompletionDate, Is.Not.EqualTo(firstCompletionDate));
    }

    [Test]
    public void Project_NavigationProperty_CanBeSet()
    {
        // Arrange
        var task = new ProjectTask();
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            Name = "Test Project"
        };

        // Act
        task.Project = project;

        // Assert
        Assert.That(task.Project, Is.EqualTo(project));
    }

    [Test]
    public void Milestone_NavigationProperty_CanBeSet()
    {
        // Arrange
        var task = new ProjectTask();
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            Name = "Test Milestone"
        };

        // Act
        task.Milestone = milestone;

        // Assert
        Assert.That(task.Milestone, Is.EqualTo(milestone));
    }

    [Test]
    public void EstimatedHours_AcceptsDecimalValues()
    {
        // Arrange
        var task = new ProjectTask();

        // Act
        task.EstimatedHours = 2.75;

        // Assert
        Assert.That(task.EstimatedHours, Is.EqualTo(2.75));
    }

    [Test]
    public void MilestoneId_CanBeNull()
    {
        // Arrange & Act
        var task = new ProjectTask
        {
            MilestoneId = null
        };

        // Assert
        Assert.That(task.MilestoneId, Is.Null);
    }

    [Test]
    public void MilestoneId_CanBeSet()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();

        // Act
        var task = new ProjectTask
        {
            MilestoneId = milestoneId
        };

        // Assert
        Assert.That(task.MilestoneId, Is.EqualTo(milestoneId));
    }
}
