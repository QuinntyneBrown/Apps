// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class EventTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var evt = new Event();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.Name, Is.EqualTo(string.Empty));
            Assert.That(evt.EventType, Is.EqualTo(EventType.Conference));
            Assert.That(evt.Location, Is.Null);
            Assert.That(evt.IsVirtual, Is.False);
            Assert.That(evt.Website, Is.Null);
            Assert.That(evt.RegistrationFee, Is.Null);
            Assert.That(evt.IsRegistered, Is.False);
            Assert.That(evt.DidAttend, Is.False);
            Assert.That(evt.Notes, Is.Null);
            Assert.That(evt.UpdatedAt, Is.Null);
            Assert.That(evt.Sessions, Is.Not.Null);
            Assert.That(evt.Contacts, Is.Not.Null);
            Assert.That(evt.EventNotes, Is.Not.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = DateTime.UtcNow.AddDays(30);
        var endDate = DateTime.UtcNow.AddDays(32);

        // Act
        var evt = new Event
        {
            EventId = eventId,
            UserId = userId,
            Name = "Tech Conference 2024",
            EventType = EventType.Conference,
            StartDate = startDate,
            EndDate = endDate,
            Location = "San Francisco, CA",
            IsVirtual = false,
            Website = "https://techconf.com",
            RegistrationFee = 500m,
            IsRegistered = true,
            DidAttend = false,
            Notes = "Great networking opportunity"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Tech Conference 2024"));
            Assert.That(evt.EventType, Is.EqualTo(EventType.Conference));
            Assert.That(evt.StartDate, Is.EqualTo(startDate));
            Assert.That(evt.EndDate, Is.EqualTo(endDate));
            Assert.That(evt.Location, Is.EqualTo("San Francisco, CA"));
            Assert.That(evt.IsVirtual, Is.False);
            Assert.That(evt.Website, Is.EqualTo("https://techconf.com"));
            Assert.That(evt.RegistrationFee, Is.EqualTo(500m));
            Assert.That(evt.IsRegistered, Is.True);
            Assert.That(evt.DidAttend, Is.False);
            Assert.That(evt.Notes, Is.EqualTo("Great networking opportunity"));
        });
    }

    [Test]
    public void MarkAsAttended_SetsDidAttendTrueAndUpdatesTimestamp()
    {
        // Arrange
        var evt = new Event
        {
            DidAttend = false,
            UpdatedAt = null
        };

        // Act
        evt.MarkAsAttended();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DidAttend, Is.True);
            Assert.That(evt.UpdatedAt, Is.Not.Null);
            Assert.That(evt.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsAttended_AlreadyAttended_UpdatesTimestamp()
    {
        // Arrange
        var originalTime = DateTime.UtcNow.AddDays(-1);
        var evt = new Event
        {
            DidAttend = true,
            UpdatedAt = originalTime
        };

        // Act
        evt.MarkAsAttended();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DidAttend, Is.True);
            Assert.That(evt.UpdatedAt, Is.Not.EqualTo(originalTime));
            Assert.That(evt.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Register_SetsIsRegisteredTrueAndUpdatesTimestamp()
    {
        // Arrange
        var evt = new Event
        {
            IsRegistered = false,
            UpdatedAt = null
        };

        // Act
        evt.Register();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IsRegistered, Is.True);
            Assert.That(evt.UpdatedAt, Is.Not.Null);
            Assert.That(evt.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Register_AlreadyRegistered_UpdatesTimestamp()
    {
        // Arrange
        var originalTime = DateTime.UtcNow.AddDays(-1);
        var evt = new Event
        {
            IsRegistered = true,
            UpdatedAt = originalTime
        };

        // Act
        evt.Register();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IsRegistered, Is.True);
            Assert.That(evt.UpdatedAt, Is.Not.EqualTo(originalTime));
            Assert.That(evt.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Collections_CanAddItems()
    {
        // Arrange
        var evt = new Event();
        var session = new Session { SessionId = Guid.NewGuid() };
        var contact = new Contact { ContactId = Guid.NewGuid() };
        var note = new Note { NoteId = Guid.NewGuid() };

        // Act
        evt.Sessions.Add(session);
        evt.Contacts.Add(contact);
        evt.EventNotes.Add(note);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.Sessions, Has.Count.EqualTo(1));
            Assert.That(evt.Contacts, Has.Count.EqualTo(1));
            Assert.That(evt.EventNotes, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public void IsVirtual_CanBeSetToTrue()
    {
        // Arrange & Act
        var evt = new Event { IsVirtual = true };

        // Assert
        Assert.That(evt.IsVirtual, Is.True);
    }
}
