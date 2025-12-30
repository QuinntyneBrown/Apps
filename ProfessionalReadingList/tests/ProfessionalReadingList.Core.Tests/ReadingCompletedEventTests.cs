// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalReadingList.Core.Tests;

public class ReadingCompletedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var readingProgressId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var completionDate = new DateTime(2024, 7, 1, 15, 30, 0);

        // Act
        var eventData = new ReadingCompletedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            CompletionDate = completionDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ReadingProgressId, Is.EqualTo(readingProgressId));
            Assert.That(eventData.ResourceId, Is.EqualTo(resourceId));
            Assert.That(eventData.CompletionDate, Is.EqualTo(completionDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ReadingCompletedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var readingProgressId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();
        var completionDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new ReadingCompletedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            CompletionDate = completionDate,
            Timestamp = timestamp
        };

        var event2 = new ReadingCompletedEvent
        {
            ReadingProgressId = readingProgressId,
            ResourceId = resourceId,
            CompletionDate = completionDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new ReadingCompletedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow
        };

        var event2 = new ReadingCompletedEvent
        {
            ReadingProgressId = Guid.NewGuid(),
            ResourceId = Guid.NewGuid(),
            CompletionDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
