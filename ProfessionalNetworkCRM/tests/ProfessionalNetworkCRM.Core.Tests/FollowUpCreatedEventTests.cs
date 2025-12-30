// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class FollowUpCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var followUpId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var description = "Follow up on proposal";
        var dueDate = new DateTime(2024, 7, 1, 10, 0, 0);

        // Act
        var eventData = new FollowUpCreatedEvent
        {
            FollowUpId = followUpId,
            ContactId = contactId,
            Description = description,
            DueDate = dueDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.FollowUpId, Is.EqualTo(followUpId));
            Assert.That(eventData.ContactId, Is.EqualTo(contactId));
            Assert.That(eventData.Description, Is.EqualTo(description));
            Assert.That(eventData.DueDate, Is.EqualTo(dueDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_EmptyDescription_StoresCorrectly()
    {
        // Arrange & Act
        var eventData = new FollowUpCreatedEvent
        {
            FollowUpId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = string.Empty,
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        // Assert
        Assert.That(eventData.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new FollowUpCreatedEvent
        {
            FollowUpId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Test follow-up",
            DueDate = DateTime.UtcNow.AddDays(7)
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
        var contactId = Guid.NewGuid();
        var dueDate = new DateTime(2024, 7, 1);
        var timestamp = DateTime.UtcNow;

        var event1 = new FollowUpCreatedEvent
        {
            FollowUpId = followUpId,
            ContactId = contactId,
            Description = "Test",
            DueDate = dueDate,
            Timestamp = timestamp
        };

        var event2 = new FollowUpCreatedEvent
        {
            FollowUpId = followUpId,
            ContactId = contactId,
            Description = "Test",
            DueDate = dueDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new FollowUpCreatedEvent
        {
            FollowUpId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "First task",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        var event2 = new FollowUpCreatedEvent
        {
            FollowUpId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            Description = "Second task",
            DueDate = DateTime.UtcNow.AddDays(2)
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
