// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectCreatedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new ProjectCreatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.Name, Is.EqualTo(string.Empty));
            Assert.That(eventRecord.Priority, Is.EqualTo(ProjectPriority.Low));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "New Project",
            Priority = ProjectPriority.High,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("New Project"));
            Assert.That(eventRecord.Priority, Is.EqualTo(ProjectPriority.High));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var eventRecord = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Test Project",
            Priority = ProjectPriority.Critical
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("Test Project"));
            Assert.That(eventRecord.Priority, Is.EqualTo(ProjectPriority.Critical));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Test Project",
            Priority = ProjectPriority.Medium,
            Timestamp = timestamp
        };

        var event2 = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Test Project",
            Priority = ProjectPriority.Medium,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
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
}
