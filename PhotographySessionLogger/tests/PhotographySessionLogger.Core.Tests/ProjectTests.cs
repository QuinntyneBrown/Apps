// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

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
            Assert.That(project.DueDate, Is.Null);
            Assert.That(project.IsCompleted, Is.False);
            Assert.That(project.Notes, Is.Null);
            Assert.That(project.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var dueDate = DateTime.UtcNow.AddDays(30);

        // Act
        var project = new Project
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Portfolio Website",
            Description = "Create new portfolio website",
            DueDate = dueDate,
            IsCompleted = false,
            Notes = "Use recent photos"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(project.ProjectId, Is.EqualTo(projectId));
            Assert.That(project.UserId, Is.EqualTo(userId));
            Assert.That(project.Name, Is.EqualTo("Portfolio Website"));
            Assert.That(project.Description, Is.EqualTo("Create new portfolio website"));
            Assert.That(project.DueDate, Is.EqualTo(dueDate));
            Assert.That(project.IsCompleted, Is.False);
            Assert.That(project.Notes, Is.EqualTo("Use recent photos"));
        });
    }

    [Test]
    public void IsCompleted_CanBeSetToTrue()
    {
        // Arrange & Act
        var project = new Project { IsCompleted = true };

        // Assert
        Assert.That(project.IsCompleted, Is.True);
    }

    [Test]
    public void IsCompleted_DefaultsToFalse()
    {
        // Arrange & Act
        var project = new Project();

        // Assert
        Assert.That(project.IsCompleted, Is.False);
    }

    [Test]
    public void Description_CanBeNull()
    {
        // Arrange & Act
        var project = new Project { Description = null };

        // Assert
        Assert.That(project.Description, Is.Null);
    }

    [Test]
    public void DueDate_CanBeNull()
    {
        // Arrange & Act
        var project = new Project { DueDate = null };

        // Assert
        Assert.That(project.DueDate, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var project = new Project { Notes = null };

        // Assert
        Assert.That(project.Notes, Is.Null);
    }
}
