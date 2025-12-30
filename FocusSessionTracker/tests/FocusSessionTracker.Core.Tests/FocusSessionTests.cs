// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Core.Tests;

public class FocusSessionTests
{
    [Test]
    public void FocusSession_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var sessionType = SessionType.DeepWork;
        var name = "Write report";
        var plannedDuration = 90;
        var startTime = DateTime.UtcNow;

        // Act
        var session = new FocusSession
        {
            FocusSessionId = sessionId,
            UserId = userId,
            SessionType = sessionType,
            Name = name,
            PlannedDurationMinutes = plannedDuration,
            StartTime = startTime,
            Notes = "Important task",
            FocusScore = 8,
            IsCompleted = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.FocusSessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.SessionType, Is.EqualTo(sessionType));
            Assert.That(session.Name, Is.EqualTo(name));
            Assert.That(session.PlannedDurationMinutes, Is.EqualTo(plannedDuration));
            Assert.That(session.StartTime, Is.EqualTo(startTime));
            Assert.That(session.Notes, Is.EqualTo("Important task"));
            Assert.That(session.FocusScore, Is.EqualTo(8));
            Assert.That(session.IsCompleted, Is.True);
            Assert.That(session.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.Distractions, Is.Not.Null);
        });
    }

    [Test]
    public void FocusSession_DefaultValues_AreSetCorrectly()
    {
        // Act
        var session = new FocusSession();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.Name, Is.EqualTo(string.Empty));
            Assert.That(session.IsCompleted, Is.False);
            Assert.That(session.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(session.Distractions, Is.Not.Null);
            Assert.That(session.Distractions.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void GetActualDurationMinutes_WithEndTime_ReturnsCorrectDuration()
    {
        // Arrange
        var startTime = new DateTime(2024, 6, 15, 9, 0, 0);
        var endTime = new DateTime(2024, 6, 15, 10, 30, 0);
        var session = new FocusSession
        {
            StartTime = startTime,
            EndTime = endTime
        };

        // Act
        var duration = session.GetActualDurationMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(90.0).Within(0.01));
    }

    [Test]
    public void GetActualDurationMinutes_WithoutEndTime_ReturnsNull()
    {
        // Arrange
        var session = new FocusSession
        {
            StartTime = DateTime.UtcNow,
            EndTime = null
        };

        // Act
        var duration = session.GetActualDurationMinutes();

        // Assert
        Assert.That(duration, Is.Null);
    }

    [Test]
    public void GetActualDurationMinutes_WithZeroDuration_ReturnsZero()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var session = new FocusSession
        {
            StartTime = startTime,
            EndTime = startTime
        };

        // Act
        var duration = session.GetActualDurationMinutes();

        // Assert
        Assert.That(duration, Is.EqualTo(0.0));
    }

    [Test]
    public void EndSession_WithValidEndTime_SetsEndTimeAndCompletes()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddMinutes(60);
        var session = new FocusSession
        {
            StartTime = startTime,
            IsCompleted = false
        };

        // Act
        session.EndSession(endTime);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.EndTime, Is.EqualTo(endTime));
            Assert.That(session.IsCompleted, Is.True);
        });
    }

    [Test]
    public void EndSession_WithEndTimeBeforeStartTime_ThrowsInvalidOperationException()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var endTime = startTime.AddMinutes(-30);
        var session = new FocusSession { StartTime = startTime };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => session.EndSession(endTime));
        Assert.That(ex.Message, Is.EqualTo("End time must be after start time."));
    }

    [Test]
    public void EndSession_WithEndTimeEqualToStartTime_ThrowsInvalidOperationException()
    {
        // Arrange
        var startTime = DateTime.UtcNow;
        var session = new FocusSession { StartTime = startTime };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => session.EndSession(startTime));
        Assert.That(ex.Message, Is.EqualTo("End time must be after start time."));
    }

    [Test]
    public void GetDistractionCount_WithNoDistractions_ReturnsZero()
    {
        // Arrange
        var session = new FocusSession();

        // Act
        var count = session.GetDistractionCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetDistractionCount_WithMultipleDistractions_ReturnsCorrectCount()
    {
        // Arrange
        var session = new FocusSession
        {
            Distractions = new List<Distraction>
            {
                new Distraction { DistractionId = Guid.NewGuid(), Type = "Phone" },
                new Distraction { DistractionId = Guid.NewGuid(), Type = "Email" },
                new Distraction { DistractionId = Guid.NewGuid(), Type = "Social Media" }
            }
        };

        // Act
        var count = session.GetDistractionCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void FocusSession_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var session = new FocusSession
        {
            EndTime = null,
            Notes = null,
            FocusScore = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.EndTime, Is.Null);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.FocusScore, Is.Null);
        });
    }

    [Test]
    public void FocusSession_FocusScore_CanBeSetToMinimum()
    {
        // Arrange & Act
        var session = new FocusSession { FocusScore = 1 };

        // Assert
        Assert.That(session.FocusScore, Is.EqualTo(1));
    }

    [Test]
    public void FocusSession_FocusScore_CanBeSetToMaximum()
    {
        // Arrange & Act
        var session = new FocusSession { FocusScore = 10 };

        // Assert
        Assert.That(session.FocusScore, Is.EqualTo(10));
    }

    [Test]
    public void FocusSession_SessionType_CanBeSetToAllValues()
    {
        // Arrange
        var session = new FocusSession();

        // Act & Assert
        foreach (SessionType sessionType in Enum.GetValues(typeof(SessionType)))
        {
            session.SessionType = sessionType;
            Assert.That(session.SessionType, Is.EqualTo(sessionType));
        }
    }

    [Test]
    public void FocusSession_PlannedDurationMinutes_CanBeSetToZero()
    {
        // Arrange & Act
        var session = new FocusSession { PlannedDurationMinutes = 0 };

        // Assert
        Assert.That(session.PlannedDurationMinutes, Is.EqualTo(0));
    }

    [Test]
    public void FocusSession_PlannedDurationMinutes_CanBeSetToLargeValue()
    {
        // Arrange & Act
        var session = new FocusSession { PlannedDurationMinutes = 480 };

        // Assert
        Assert.That(session.PlannedDurationMinutes, Is.EqualTo(480));
    }
}
