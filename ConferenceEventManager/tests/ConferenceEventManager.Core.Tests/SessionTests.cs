// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class SessionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesInstance()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.Title, Is.EqualTo(string.Empty));
            Assert.That(session.Speaker, Is.Null);
            Assert.That(session.Description, Is.Null);
            Assert.That(session.EndTime, Is.Null);
            Assert.That(session.Room, Is.Null);
            Assert.That(session.PlansToAttend, Is.False);
            Assert.That(session.DidAttend, Is.False);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.UpdatedAt, Is.Null);
            Assert.That(session.Event, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var startTime = DateTime.UtcNow.AddDays(1);
        var endTime = DateTime.UtcNow.AddDays(1).AddHours(2);

        // Act
        var session = new Session
        {
            SessionId = sessionId,
            UserId = userId,
            EventId = eventId,
            Title = "Building Scalable Systems",
            Speaker = "John Smith",
            Description = "Learn about scalable architecture",
            StartTime = startTime,
            EndTime = endTime,
            Room = "Hall A",
            PlansToAttend = true,
            DidAttend = false,
            Notes = "Bring questions"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.EventId, Is.EqualTo(eventId));
            Assert.That(session.Title, Is.EqualTo("Building Scalable Systems"));
            Assert.That(session.Speaker, Is.EqualTo("John Smith"));
            Assert.That(session.Description, Is.EqualTo("Learn about scalable architecture"));
            Assert.That(session.StartTime, Is.EqualTo(startTime));
            Assert.That(session.EndTime, Is.EqualTo(endTime));
            Assert.That(session.Room, Is.EqualTo("Hall A"));
            Assert.That(session.PlansToAttend, Is.True);
            Assert.That(session.DidAttend, Is.False);
            Assert.That(session.Notes, Is.EqualTo("Bring questions"));
        });
    }

    [Test]
    public void MarkAsAttended_SetsDidAttendTrueAndUpdatesTimestamp()
    {
        // Arrange
        var session = new Session
        {
            DidAttend = false,
            UpdatedAt = null
        };

        // Act
        session.MarkAsAttended();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.DidAttend, Is.True);
            Assert.That(session.UpdatedAt, Is.Not.Null);
            Assert.That(session.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void MarkAsAttended_AlreadyAttended_UpdatesTimestamp()
    {
        // Arrange
        var originalTime = DateTime.UtcNow.AddDays(-1);
        var session = new Session
        {
            DidAttend = true,
            UpdatedAt = originalTime
        };

        // Act
        session.MarkAsAttended();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.DidAttend, Is.True);
            Assert.That(session.UpdatedAt, Is.Not.EqualTo(originalTime));
            Assert.That(session.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void PlansToAttend_DefaultsToFalse()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        Assert.That(session.PlansToAttend, Is.False);
    }

    [Test]
    public void PlansToAttend_CanBeSetToTrue()
    {
        // Arrange
        var session = new Session();

        // Act
        session.PlansToAttend = true;

        // Assert
        Assert.That(session.PlansToAttend, Is.True);
    }

    [Test]
    public void Event_CanBeAssigned()
    {
        // Arrange
        var evt = new Event { EventId = Guid.NewGuid(), Name = "Tech Conference" };
        var session = new Session();

        // Act
        session.Event = evt;

        // Assert
        Assert.That(session.Event, Is.EqualTo(evt));
    }
}
