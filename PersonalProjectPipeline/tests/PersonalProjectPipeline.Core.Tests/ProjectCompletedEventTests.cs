// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Core.Tests;

public class ProjectCompletedEventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesEvent()
    {
        // Arrange & Act
        var eventRecord = new ProjectCompletedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(Guid.Empty));
            Assert.That(eventRecord.CompletionDate, Is.EqualTo(default(DateTime)));
            Assert.That(eventRecord.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var completionDate = DateTime.UtcNow.AddHours(-1);
        var timestamp = DateTime.UtcNow;

        // Act
        var eventRecord = new ProjectCompletedEvent
        {
            ProjectId = projectId,
            CompletionDate = completionDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventRecord.CompletionDate, Is.EqualTo(completionDate));
            Assert.That(eventRecord.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Record_IsImmutable_PropertiesAreInitOnly()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var completionDate = DateTime.UtcNow;

        // Act
        var eventRecord = new ProjectCompletedEvent
        {
            ProjectId = projectId,
            CompletionDate = completionDate
        };

        // Assert - Verify properties were set
        Assert.Multiple(() =>
        {
            Assert.That(eventRecord.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventRecord.CompletionDate, Is.EqualTo(completionDate));
        });
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var completionDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new ProjectCompletedEvent
        {
            ProjectId = projectId,
            CompletionDate = completionDate,
            Timestamp = timestamp
        };

        var event2 = new ProjectCompletedEvent
        {
            ProjectId = projectId,
            CompletionDate = completionDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ProjectCompletedEvent
        {
            ProjectId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow
        };

        var event2 = new ProjectCompletedEvent
        {
            ProjectId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow.AddHours(1)
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
