// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class ContactTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var contact = new Contact();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.Name, Is.EqualTo(string.Empty));
            Assert.That(contact.Company, Is.Null);
            Assert.That(contact.JobTitle, Is.Null);
            Assert.That(contact.Email, Is.Null);
            Assert.That(contact.LinkedInUrl, Is.Null);
            Assert.That(contact.Notes, Is.Null);
            Assert.That(contact.UpdatedAt, Is.Null);
            Assert.That(contact.Event, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var eventId = Guid.NewGuid();

        // Act
        var contact = new Contact
        {
            ContactId = contactId,
            UserId = userId,
            EventId = eventId,
            Name = "Jane Doe",
            Company = "Tech Corp",
            JobTitle = "Senior Developer",
            Email = "jane.doe@techcorp.com",
            LinkedInUrl = "https://linkedin.com/in/janedoe",
            Notes = "Met at networking session"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.ContactId, Is.EqualTo(contactId));
            Assert.That(contact.UserId, Is.EqualTo(userId));
            Assert.That(contact.EventId, Is.EqualTo(eventId));
            Assert.That(contact.Name, Is.EqualTo("Jane Doe"));
            Assert.That(contact.Company, Is.EqualTo("Tech Corp"));
            Assert.That(contact.JobTitle, Is.EqualTo("Senior Developer"));
            Assert.That(contact.Email, Is.EqualTo("jane.doe@techcorp.com"));
            Assert.That(contact.LinkedInUrl, Is.EqualTo("https://linkedin.com/in/janedoe"));
            Assert.That(contact.Notes, Is.EqualTo("Met at networking session"));
        });
    }

    [Test]
    public void UpdatedAt_CanBeSet()
    {
        // Arrange
        var updatedAt = DateTime.UtcNow;
        var contact = new Contact();

        // Act
        contact.UpdatedAt = updatedAt;

        // Assert
        Assert.That(contact.UpdatedAt, Is.EqualTo(updatedAt));
    }

    [Test]
    public void Event_CanBeAssigned()
    {
        // Arrange
        var evt = new Event { EventId = Guid.NewGuid(), Name = "Tech Conference" };
        var contact = new Contact();

        // Act
        contact.Event = evt;

        // Assert
        Assert.That(contact.Event, Is.EqualTo(evt));
    }
}
