// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

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
            Name = "Portfolio Website",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventRecord.UserId, Is.EqualTo(userId));
            Assert.That(eventRecord.Name, Is.EqualTo("Portfolio Website"));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
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
            Name = "Portfolio",
            Timestamp = timestamp
        };

        var event2 = new ProjectCreatedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = "Portfolio",
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
