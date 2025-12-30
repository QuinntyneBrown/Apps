// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

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
            Assert.That(session.EndTime, Is.Null);
            Assert.That(session.Participants, Is.Null);
            Assert.That(session.PromptsUsed, Is.Null);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.WasSuccessful, Is.True);
            Assert.That(session.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void Properties_CanBeSet()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startTime = DateTime.UtcNow;
        var endTime = DateTime.UtcNow.AddHours(2);

        // Act
        var session = new Session
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Date Night Conversation",
            StartTime = startTime,
            EndTime = endTime,
            Participants = "John and Jane",
            PromptsUsed = "prompt1,prompt2,prompt3",
            Notes = "Great conversation about dreams",
            WasSuccessful = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.Title, Is.EqualTo("Date Night Conversation"));
            Assert.That(session.StartTime, Is.EqualTo(startTime));
            Assert.That(session.EndTime, Is.EqualTo(endTime));
            Assert.That(session.Participants, Is.EqualTo("John and Jane"));
            Assert.That(session.PromptsUsed, Is.EqualTo("prompt1,prompt2,prompt3"));
            Assert.That(session.Notes, Is.EqualTo("Great conversation about dreams"));
            Assert.That(session.WasSuccessful, Is.True);
        });
    }

    [Test]
    public void EndSession_SetsEndTimeAndUpdatesTimestamp()
    {
        // Arrange
        var session = new Session
        {
            EndTime = null,
            UpdatedAt = null
        };

        // Act
        session.EndSession();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.EndTime, Is.Not.Null);
            Assert.That(session.EndTime.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(session.UpdatedAt, Is.Not.Null);
            Assert.That(session.UpdatedAt.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void EndSession_AlreadyEnded_UpdatesEndTime()
    {
        // Arrange
        var originalEndTime = DateTime.UtcNow.AddHours(-1);
        var session = new Session
        {
            EndTime = originalEndTime
        };

        // Act
        session.EndSession();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.EndTime, Is.Not.EqualTo(originalEndTime));
            Assert.That(session.EndTime.Value, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetDuration_SessionEnded_ReturnsTimeSpan()
    {
        // Arrange
        var startTime = DateTime.UtcNow.AddHours(-2);
        var endTime = DateTime.UtcNow;
        var session = new Session
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = session.GetDuration();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(duration, Is.Not.Null);
            Assert.That(duration.Value.TotalHours, Is.EqualTo(2).Within(0.01));
        });
    }

    [Test]
    public void GetDuration_SessionNotEnded_ReturnsNull()
    {
        // Arrange
        var session = new Session
        {
            StartTime = DateTime.UtcNow.AddHours(-1),
            EndTime = null
        };

        // Act
        var duration = session.GetDuration();

        // Assert
        Assert.That(duration, Is.Null);
    }

    [Test]
    public void GetDuration_ZeroDuration_ReturnsZeroTimeSpan()
    {
        // Arrange
        var time = DateTime.UtcNow;
        var session = new Session
        {
            StartTime = time,
            EndTime = time
        };

        // Act
        var duration = session.GetDuration();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(duration, Is.Not.Null);
            Assert.That(duration.Value, Is.EqualTo(TimeSpan.Zero));
        });
    }

    [Test]
    public void WasSuccessful_DefaultsToTrue()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        Assert.That(session.WasSuccessful, Is.True);
    }

    [Test]
    public void WasSuccessful_CanBeSetToFalse()
    {
        // Arrange & Act
        var session = new Session
        {
            WasSuccessful = false
        };

        // Assert
        Assert.That(session.WasSuccessful, Is.False);
    }
}
