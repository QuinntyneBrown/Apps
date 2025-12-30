// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class InteractionLoggedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var interactionId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var interactionType = "Email";
        var interactionDate = new DateTime(2024, 6, 15, 14, 30, 0);

        // Act
        var eventData = new InteractionLoggedEvent
        {
            InteractionId = interactionId,
            ContactId = contactId,
            InteractionType = interactionType,
            InteractionDate = interactionDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.InteractionId, Is.EqualTo(interactionId));
            Assert.That(eventData.ContactId, Is.EqualTo(contactId));
            Assert.That(eventData.InteractionType, Is.EqualTo(interactionType));
            Assert.That(eventData.InteractionDate, Is.EqualTo(interactionDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DifferentInteractionTypes_StoresCorrectly()
    {
        // Arrange & Act
        var emailEvent = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Email",
            InteractionDate = DateTime.UtcNow
        };

        var meetingEvent = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Meeting",
            InteractionDate = DateTime.UtcNow
        };

        var callEvent = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Call",
            InteractionDate = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(emailEvent.InteractionType, Is.EqualTo("Email"));
            Assert.That(meetingEvent.InteractionType, Is.EqualTo("Meeting"));
            Assert.That(callEvent.InteractionType, Is.EqualTo("Call"));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Email",
            InteractionDate = DateTime.UtcNow
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
        var interactionId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var interactionDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        var event1 = new InteractionLoggedEvent
        {
            InteractionId = interactionId,
            ContactId = contactId,
            InteractionType = "Email",
            InteractionDate = interactionDate,
            Timestamp = timestamp
        };

        var event2 = new InteractionLoggedEvent
        {
            InteractionId = interactionId,
            ContactId = contactId,
            InteractionType = "Email",
            InteractionDate = interactionDate,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Email",
            InteractionDate = DateTime.UtcNow
        };

        var event2 = new InteractionLoggedEvent
        {
            InteractionId = Guid.NewGuid(),
            ContactId = Guid.NewGuid(),
            InteractionType = "Meeting",
            InteractionDate = DateTime.UtcNow
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
