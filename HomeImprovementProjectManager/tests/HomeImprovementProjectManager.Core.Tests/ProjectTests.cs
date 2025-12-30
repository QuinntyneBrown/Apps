// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class ProjectTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesProject()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Kitchen Remodel";
        var description = "Complete kitchen renovation";
        var status = ProjectStatus.Planning;
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddMonths(3);
        var estimatedCost = 25000m;
        var actualCost = 27500m;

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Description = description,
            Status = status,
            StartDate = startDate,
            EndDate = endDate,
            EstimatedCost = estimatedCost,
            ActualCost = actualCost
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Status, Is.EqualTo(status));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.EndDate, Is.EqualTo(endDate));
            Assert.That(project.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(project.ActualCost, Is.EqualTo(actualCost));
        });
    }

    [Test]
    public void Project_DefaultValues_AreSetCorrectly()
    {
        // Act
        var project = new Project();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Name, Is.EqualTo(string.Empty));
            Assert.That(project.Description, Is.Null);
            Assert.That(project.Status, Is.EqualTo(ProjectStatus.Planning));
            Assert.That(project.StartDate, Is.Null);
            Assert.That(project.EndDate, Is.Null);
            Assert.That(project.EstimatedCost, Is.Null);
            Assert.That(project.ActualCost, Is.Null);
            Assert.That(project.Budgets, Is.Not.Null);
            Assert.That(project.Contractors, Is.Not.Null);
            Assert.That(project.Materials, Is.Not.Null);
            Assert.That(project.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Project_Collections_InitializeAsEmpty()
    {
        // Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "New Project"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Budgets, Is.Empty);
            Assert.That(project.Contractors, Is.Empty);
            Assert.That(project.Materials, Is.Empty);
        });
    }

    [Test]
    public void Project_AllProjectStatuses_CanBeAssigned()
    {
        // Arrange
        var statuses = new[]
        {
            ProjectStatus.Planning,
            ProjectStatus.InProgress,
            ProjectStatus.Completed,
            ProjectStatus.OnHold,
            ProjectStatus.Cancelled
        };

        // Act & Assert
        foreach (var status in statuses)
        {
            var project = new Project
            {
                ProjectId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Project",
                Status = status
            };

            Assert.That(project.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void Project_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Simple Project"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.Description, Is.Null);
            Assert.That(project.StartDate, Is.Null);
            Assert.That(project.EndDate, Is.Null);
            Assert.That(project.EstimatedCost, Is.Null);
            Assert.That(project.ActualCost, Is.Null);
        });
    }

    [Test]
    public void Project_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(project.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Project_ActualCostExceedsEstimated_CanBeSet()
    {
        // Arrange & Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Over Budget Project",
            EstimatedCost = 10000m,
            ActualCost = 12500m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.EstimatedCost, Is.EqualTo(10000m));
            Assert.That(project.ActualCost, Is.EqualTo(12500m));
        });
    }

    [Test]
    public void Project_WithDescription_IsValid()
    {
        // Arrange
        var description = "This is a detailed project description with multiple requirements";

        // Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project",
            Description = description
        };

        // Assert
        Assert.That(project.Description, Is.EqualTo(description));
    }

    [Test]
    public void Project_WithStartAndEndDates_IsValid()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 6, 30);

        // Act
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project",
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void Project_CanUpdateStatus()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project",
            Status = ProjectStatus.Planning
        };

        // Act
        project.Status = ProjectStatus.InProgress;

        // Assert
        Assert.That(project.Status, Is.EqualTo(ProjectStatus.InProgress));
    }

    [Test]
    public void Project_AllProperties_CanBeSet()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Complete Project";
        var description = "Full project description";
        var status = ProjectStatus.InProgress;
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);
        var estimatedCost = 50000m;
        var actualCost = 48000m;
        var createdAt = DateTime.UtcNow.AddDays(-30);

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Description = description,
            Status = status,
            StartDate = startDate,
            EndDate = endDate,
            EstimatedCost = estimatedCost,
            ActualCost = actualCost,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Status, Is.EqualTo(status));
            Assert.That(project.StartDate, Is.EqualTo(startDate));
            Assert.That(project.EndDate, Is.EqualTo(endDate));
            Assert.That(project.EstimatedCost, Is.EqualTo(estimatedCost));
            Assert.That(project.ActualCost, Is.EqualTo(actualCost));
            Assert.That(project.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
