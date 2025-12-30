// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

public class EventTests
{
    [Test]
    public void PromptCreatedEvent_Properties_CanBeSet()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PromptCreatedEvent
        {
            PromptId = promptId,
            UserId = userId,
            Text = "What's your biggest dream?",
            Category = Category.DreamsAndAspirations,
            Depth = Depth.Deep,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PromptId, Is.EqualTo(promptId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Text, Is.EqualTo("What's your biggest dream?"));
            Assert.That(evt.Category, Is.EqualTo(Category.DreamsAndAspirations));
            Assert.That(evt.Depth, Is.EqualTo(Depth.Deep));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void PromptCreatedEvent_SystemPrompt_UserIdCanBeNull()
    {
        // Arrange & Act
        var evt = new PromptCreatedEvent
        {
            PromptId = Guid.NewGuid(),
            UserId = null,
            Text = "System prompt",
            Category = Category.Icebreaker,
            Depth = Depth.Surface
        };

        // Assert
        Assert.That(evt.UserId, Is.Null);
    }

    [Test]
    public void PromptUsedEvent_Properties_CanBeSet()
    {
        // Arrange
        var promptId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PromptUsedEvent
        {
            PromptId = promptId,
            UserId = userId,
            NewUsageCount = 10,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PromptId, Is.EqualTo(promptId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.NewUsageCount, Is.EqualTo(10));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void SessionStartedEvent_Properties_CanBeSet()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startTime = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new SessionStartedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            Title = "Date Night",
            StartTime = startTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SessionId, Is.EqualTo(sessionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("Date Night"));
            Assert.That(evt.StartTime, Is.EqualTo(startTime));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void SessionEndedEvent_Properties_CanBeSet()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var endTime = DateTime.UtcNow;
        var duration = TimeSpan.FromHours(2);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new SessionEndedEvent
        {
            SessionId = sessionId,
            UserId = userId,
            EndTime = endTime,
            Duration = duration,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SessionId, Is.EqualTo(sessionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.EndTime, Is.EqualTo(endTime));
            Assert.That(evt.Duration, Is.EqualTo(duration));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void SessionEndedEvent_DurationCanBeNull()
    {
        // Arrange & Act
        var evt = new SessionEndedEvent
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EndTime = DateTime.UtcNow,
            Duration = null
        };

        // Assert
        Assert.That(evt.Duration, Is.Null);
    }

    [Test]
    public void FavoriteAddedEvent_Properties_CanBeSet()
    {
        // Arrange
        var favoriteId = Guid.NewGuid();
        var promptId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new FavoriteAddedEvent
        {
            FavoriteId = favoriteId,
            PromptId = promptId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FavoriteId, Is.EqualTo(favoriteId));
            Assert.That(evt.PromptId, Is.EqualTo(promptId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void FavoriteRemovedEvent_Properties_CanBeSet()
    {
        // Arrange
        var favoriteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new FavoriteRemovedEvent
        {
            FavoriteId = favoriteId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FavoriteId, Is.EqualTo(favoriteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
