// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class FollowUpCompletedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var followUpId = Guid.NewGuid();
        var completedDate = new DateTime(2024, 6, 20, 15, 30, 0);

        // Act
        var eventData = new FollowUpCompletedEvent
        {
            FollowUpId = followUpId,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.FollowUpId, Is.EqualTo(followUpId));
            Assert.That(eventData.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new FollowUpCompletedEvent
        {
            FollowUpId = Guid.NewGuid(),
            CompletedDate = DateTime.UtcNow
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
        var followUpId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new FollowUpCompletedEvent
        {
            FollowUpId = followUpId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        var event2 = new FollowUpCompletedEvent
        {
            FollowUpId = followUpId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_DifferentFollowUpIds_WorksCorrectly()
    {
        // Arrange
        var completedDate = DateTime.UtcNow;

        var event1 = new FollowUpCompletedEvent
        {
            FollowUpId = Guid.NewGuid(),
            CompletedDate = completedDate
        };

        var event2 = new FollowUpCompletedEvent
        {
            FollowUpId = Guid.NewGuid(),
            CompletedDate = completedDate
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_DifferentCompletedDates_WorksCorrectly()
    {
        // Arrange
        var followUpId = Guid.NewGuid();

        var event1 = new FollowUpCompletedEvent
        {
            FollowUpId = followUpId,
            CompletedDate = DateTime.UtcNow.AddDays(-1)
        };

        var event2 = new FollowUpCompletedEvent
        {
            FollowUpId = followUpId,
            CompletedDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
