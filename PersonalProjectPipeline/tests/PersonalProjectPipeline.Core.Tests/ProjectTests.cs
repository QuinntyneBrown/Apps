// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesProject()
    {
        // Arrange & Act
        var project = new Project();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(project.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(project.Name, Is.EqualTo(string.Empty));
            Assert.That(project.Description, Is.Null);
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Idea));
            Assert.That(project.Priority, Is.EqualTo(ProjectPriority.Low));
            Assert.That(project.StartDate, Is.Null);
            Assert.That(project.TargetDate, Is.Null);
            Assert.That(project.CompletionDate, Is.Null);
            Assert.That(project.Tags, Is.Null);
            Assert.That(project.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(project.Tasks, Is.Not.Null);
            Assert.That(project.Tasks, Is.Empty);
            Assert.That(project.Milestones, Is.Not.Null);
            Assert.That(project.Milestones, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow;
        var targetDate = DateTime.UtcNow.AddDays(30);

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Test Project",
            Description = "Test Description",
            Status = ProjectStatus.Planned,
            Priority = ProjectPriority.High,
            StartDate = startDate,
            TargetDate = targetDate,
            Tags = "tag1,tag2"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.Name, Is.EqualTo("Test Project"));
            Assert.That(project.Description, Is.EqualTo("Test Description"));
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Planned));
            Assert.That(project.Priority, Is.EqualTo(ProjectPriority.High));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.TargetDate, Is.EqualTo(targetDate));
            Assert.That(project.Tags, Is.EqualTo("tag1,tag2"));
        });
    }

    [Test]
    public void Start_WhenCalled_SetsStatusToInProgress()
    {
        // Arrange
        var project = new Project
        {
            Status = ProjectStatus.Planned
        };

        // Act
        project.Start();

        // Assert
        Assert.That(project.Status, Is.EqualTo(ProjectStatus.InProgress));
    }

    [Test]
    public void Start_WhenStartDateIsNull_SetsStartDate()
    {
        // Arrange
        var project = new Project
        {
            StartDate = null
        };
        var beforeStart = DateTime.UtcNow;

        // Act
        project.Start();

        // Assert
        var afterStart = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(project.StartDate, Is.Not.Null);
            Assert.That(project.StartDate.Value, Is.GreaterThanOrEqualTo(beforeStart));
            Assert.That(project.StartDate.Value, Is.LessThanOrEqualTo(afterStart));
        });
    }

    [Test]
    public void Start_WhenStartDateIsSet_DoesNotChangeStartDate()
    {
        // Arrange
        var originalStartDate = DateTime.UtcNow.AddDays(-5);
        var project = new Project
        {
            StartDate = originalStartDate
        };

        // Act
        project.Start();

        // Assert
        Assert.That(project.StartDate, Is.EqualTo(originalStartDate));
    }

    [Test]
    public void Complete_WhenCalled_SetsStatusToCompletedAndSetsCompletionDate()
    {
        // Arrange
        var project = new Project
        {
            Status = ProjectStatus.InProgress
        };
        var beforeComplete = DateTime.UtcNow;

        // Act
        project.Complete();

        // Assert
        var afterComplete = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Completed));
            Assert.That(project.CompletionDate, Is.Not.Null);
            Assert.That(project.CompletionDate.Value, Is.GreaterThanOrEqualTo(beforeComplete));
            Assert.That(project.CompletionDate.Value, Is.LessThanOrEqualTo(afterComplete));
        });
    }

    [Test]
    public void Cancel_WhenCalled_SetsStatusToCancelled()
    {
        // Arrange
        var project = new Project
        {
            Status = ProjectStatus.InProgress
        };

        // Act
        project.Cancel();

        // Assert
        Assert.That(project.Status, Is.EqualTo(ProjectStatus.Cancelled));
    }

    [Test]
    public void GetProgressPercentage_WhenNoTasks_ReturnsZero()
    {
        // Arrange
        var project = new Project();

        // Act
        var progress = project.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(0));
    }

    [Test]
    public void GetProgressPercentage_WhenNoTasksCompleted_ReturnsZero()
    {
        // Arrange
        var project = new Project
        {
            Tasks = new List<ProjectTask>
            {
                new ProjectTask { IsCompleted = false },
                new ProjectTask { IsCompleted = false }
            }
        };

        // Act
        var progress = project.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(0));
    }

    [Test]
    public void GetProgressPercentage_WhenAllTasksCompleted_Returns100()
    {
        // Arrange
        var project = new Project
        {
            Tasks = new List<ProjectTask>
            {
                new ProjectTask { IsCompleted = true },
                new ProjectTask { IsCompleted = true }
            }
        };

        // Act
        var progress = project.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(100));
    }

    [Test]
    public void GetProgressPercentage_WhenHalfTasksCompleted_Returns50()
    {
        // Arrange
        var project = new Project
        {
            Tasks = new List<ProjectTask>
            {
                new ProjectTask { IsCompleted = true },
                new ProjectTask { IsCompleted = false }
            }
        };

        // Act
        var progress = project.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(50));
    }

    [Test]
    public void GetProgressPercentage_WithVariousCompletionRates_CalculatesCorrectly()
    {
        // Arrange
        var project = new Project
        {
            Tasks = new List<ProjectTask>
            {
                new ProjectTask { IsCompleted = true },
                new ProjectTask { IsCompleted = true },
                new ProjectTask { IsCompleted = false },
                new ProjectTask { IsCompleted = false }
            }
        };

        // Act
        var progress = project.GetProgressPercentage();

        // Assert
        Assert.That(progress, Is.EqualTo(50));
    }

    [Test]
    public void Milestones_Collection_CanBeModified()
    {
        // Arrange
        var project = new Project();
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            Name = "Milestone 1"
        };

        // Act
        project.Milestones.Add(milestone);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Milestones.Count, Is.EqualTo(1));
            Assert.That(project.Milestones.First(), Is.EqualTo(milestone));
        });
    }

    [Test]
    public void Tasks_Collection_CanBeModified()
    {
        // Arrange
        var project = new Project();
        var task = new ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            Title = "Task 1"
        };

        // Act
        project.Tasks.Add(task);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Tasks.Count, Is.EqualTo(1));
            Assert.That(project.Tasks.First(), Is.EqualTo(task));
        });
    }
}
