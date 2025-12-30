// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class ProjectCreatedEventTests
{
    [Test]
    public void ProjectCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Kitchen Remodel";
        var timestamp = DateTime.UtcNow;

        // Act
        var projectEvent = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(projectEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(projectEvent.UserId, Is.EqualTo(userId));
            Assert.That(projectEvent.Name, Is.EqualTo(name));
            Assert.That(projectEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ProjectCreatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var projectEvent = new ProjectCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(projectEvent.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(projectEvent.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(projectEvent.Name, Is.EqualTo(string.Empty));
            Assert.That(projectEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ProjectCreatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var projectEvent = new ProjectCreatedEvent
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(projectEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ProjectCreatedEvent_WithEmptyName_IsValid()
    {
        // Arrange & Act
        var projectEvent = new ProjectCreatedEvent
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = ""
        };

        // Assert
        Assert.That(projectEvent.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ProjectCreatedEvent_IsImmutable()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Bathroom Renovation";

        // Act
        var projectEvent = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(projectEvent.ProjectId, Is.EqualTo(projectId));
            Assert.That(projectEvent.UserId, Is.EqualTo(userId));
            Assert.That(projectEvent.Name, Is.EqualTo(name));
        });
    }

    [Test]
    public void ProjectCreatedEvent_EqualityByValue()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Deck Construction";
        var timestamp = DateTime.UtcNow;

        var event1 = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        var event2 = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ProjectCreatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ProjectCreatedEvent
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project 1"
        };

        var event2 = new ProjectCreatedEvent
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Project 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void ProjectCreatedEvent_WithLongName_IsValid()
    {
        // Arrange
        var longName = new string('A', 500);

        // Act
        var projectEvent = new ProjectCreatedEvent
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = longName
        };

        // Assert
        Assert.That(projectEvent.Name, Is.EqualTo(longName));
    }
}
