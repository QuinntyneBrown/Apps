// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class DomainEventTests
{
    [Test]
    public void EventRegisteredEvent_Properties_CanBeSet()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(30);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new EventRegisteredEvent
        {
            EventId = eventId,
            UserId = userId,
            Name = "Tech Conference 2024",
            StartDate = startDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Tech Conference 2024"));
            Assert.That(evt.StartDate, Is.EqualTo(startDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void SessionAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var startTime = DateTime.UtcNow.AddDays(1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new SessionAddedEvent
        {
            SessionId = sessionId,
            EventId = eventId,
            Title = "Building Scalable Systems",
            StartTime = startTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SessionId, Is.EqualTo(sessionId));
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.Title, Is.EqualTo("Building Scalable Systems"));
            Assert.That(evt.StartTime, Is.EqualTo(startTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ContactAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ContactAddedEvent
        {
            ContactId = contactId,
            EventId = eventId,
            Name = "Jane Doe",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ContactId, Is.EqualTo(contactId));
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.Name, Is.EqualTo("Jane Doe"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NoteAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var noteId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NoteAddedEvent
        {
            NoteId = noteId,
            EventId = eventId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NoteId, Is.EqualTo(noteId));
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
