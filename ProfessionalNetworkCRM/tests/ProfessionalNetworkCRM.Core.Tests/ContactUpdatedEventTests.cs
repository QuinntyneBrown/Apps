// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class ContactUpdatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var contactId = Guid.NewGuid();

        // Act
        var eventData = new ContactUpdatedEvent
        {
            ContactId = contactId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ContactId, Is.EqualTo(contactId));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ContactUpdatedEvent
        {
            ContactId = Guid.NewGuid()
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
        var contactId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ContactUpdatedEvent
        {
            ContactId = contactId,
            Timestamp = timestamp
        };

        var event2 = new ContactUpdatedEvent
        {
            ContactId = contactId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_DifferentContactIds_WorksCorrectly()
    {
        // Arrange
        var event1 = new ContactUpdatedEvent
        {
            ContactId = Guid.NewGuid()
        };

        var event2 = new ContactUpdatedEvent
        {
            ContactId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
