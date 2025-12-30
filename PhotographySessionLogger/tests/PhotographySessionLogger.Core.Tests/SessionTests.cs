// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class SessionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesSession()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(Guid.Empty));
            Assert.That(session.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(session.Title, Is.EqualTo(string.Empty));
            Assert.That(session.SessionType, Is.EqualTo(SessionType.Portrait));
            Assert.That(session.SessionDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.Location, Is.Null);
            Assert.That(session.Client, Is.Null);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.Photos, Is.Not.Null);
            Assert.That(session.Photos, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionDate = DateTime.UtcNow;

        // Act
        var session = new Session
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Wedding Shoot",
            SessionType = SessionType.Wedding,
            SessionDate = sessionDate,
            Location = "Central Park",
            Client = "John & Jane",
            Notes = "Great weather"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.Title, Is.EqualTo("Wedding Shoot"));
            Assert.That(session.SessionType, Is.EqualTo(SessionType.Wedding));
            Assert.That(session.SessionDate, Is.EqualTo(sessionDate));
            Assert.That(session.Location, Is.EqualTo("Central Park"));
            Assert.That(session.Client, Is.EqualTo("John & Jane"));
            Assert.That(session.Notes, Is.EqualTo("Great weather"));
        });
    }

    [Test]
    public void SessionType_CanBeSetToPortrait()
    {
        // Arrange & Act
        var session = new Session { SessionType = SessionType.Portrait };

        // Assert
        Assert.That(session.SessionType, Is.EqualTo(SessionType.Portrait));
    }

    [Test]
    public void SessionType_CanBeSetToLandscape()
    {
        // Arrange & Act
        var session = new Session { SessionType = SessionType.Landscape };

        // Assert
        Assert.That(session.SessionType, Is.EqualTo(SessionType.Landscape));
    }

    [Test]
    public void SessionType_CanBeSetToWedding()
    {
        // Arrange & Act
        var session = new Session { SessionType = SessionType.Wedding };

        // Assert
        Assert.That(session.SessionType, Is.EqualTo(SessionType.Wedding));
    }

    [Test]
    public void Photos_Collection_CanBeModified()
    {
        // Arrange
        var session = new Session();
        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            FileName = "photo1.jpg"
        };

        // Act
        session.Photos.Add(photo);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.Photos.Count, Is.EqualTo(1));
            Assert.That(session.Photos.First(), Is.EqualTo(photo));
        });
    }

    [Test]
    public void Location_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { Location = null };

        // Assert
        Assert.That(session.Location, Is.Null);
    }

    [Test]
    public void Client_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { Client = null };

        // Assert
        Assert.That(session.Client, Is.Null);
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var session = new Session { Notes = null };

        // Assert
        Assert.That(session.Notes, Is.Null);
    }
}
