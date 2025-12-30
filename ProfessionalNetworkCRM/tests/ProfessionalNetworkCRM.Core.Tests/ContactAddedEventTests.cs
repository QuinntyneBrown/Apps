// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class ContactAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fullName = "John Doe";
        var contactType = ContactType.Client;
        var company = "Acme Corp";

        // Act
        var eventData = new ContactAddedEvent
        {
            ContactId = contactId,
            UserId = userId,
            FullName = fullName,
            ContactType = contactType,
            Company = company
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ContactId, Is.EqualTo(contactId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.FullName, Is.EqualTo(fullName));
            Assert.That(eventData.ContactType, Is.EqualTo(contactType));
            Assert.That(eventData.Company, Is.EqualTo(company));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_WithoutCompany_CreatesEventWithNullCompany()
    {
        // Arrange & Act
        var eventData = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Jane Smith",
            ContactType = ContactType.Mentor,
            Company = null
        };

        // Assert
        Assert.That(eventData.Company, Is.Null);
    }

    [Test]
    public void Constructor_DifferentContactTypes_StoresCorrectly()
    {
        // Arrange & Act
        var event1 = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Contact 1",
            ContactType = ContactType.Colleague
        };

        var event2 = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Contact 2",
            ContactType = ContactType.Recruiter
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(event1.ContactType, Is.EqualTo(ContactType.Colleague));
            Assert.That(event2.ContactType, Is.EqualTo(ContactType.Recruiter));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Test Contact",
            ContactType = ContactType.Client
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
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ContactAddedEvent
        {
            ContactId = contactId,
            UserId = userId,
            FullName = "John Doe",
            ContactType = ContactType.Client,
            Company = "Acme Corp",
            Timestamp = timestamp
        };

        var event2 = new ContactAddedEvent
        {
            ContactId = contactId,
            UserId = userId,
            FullName = "John Doe",
            ContactType = ContactType.Client,
            Company = "Acme Corp",
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "John Doe",
            ContactType = ContactType.Client
        };

        var event2 = new ContactAddedEvent
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FullName = "Jane Smith",
            ContactType = ContactType.Mentor
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
