// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Core.Tests;

public class DistractionTests
{
    [Test]
    public void Distraction_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var distractionId = Guid.NewGuid();
        var sessionId = Guid.NewGuid();
        var type = "Phone call";
        var description = "Unexpected call from client";
        var occurredAt = DateTime.UtcNow;
        var duration = 5.5;

        // Act
        var distraction = new Distraction
        {
            DistractionId = distractionId,
            FocusSessionId = sessionId,
            Type = type,
            Description = description,
            OccurredAt = occurredAt,
            DurationMinutes = duration,
            IsInternal = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(distraction.DistractionId, Is.EqualTo(distractionId));
            Assert.That(distraction.FocusSessionId, Is.EqualTo(sessionId));
            Assert.That(distraction.Type, Is.EqualTo(type));
            Assert.That(distraction.Description, Is.EqualTo(description));
            Assert.That(distraction.OccurredAt, Is.EqualTo(occurredAt));
            Assert.That(distraction.DurationMinutes, Is.EqualTo(duration));
            Assert.That(distraction.IsInternal, Is.False);
            Assert.That(distraction.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Distraction_DefaultValues_AreSetCorrectly()
    {
        // Act
        var distraction = new Distraction();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(distraction.Type, Is.EqualTo(string.Empty));
            Assert.That(distraction.IsInternal, Is.False);
            Assert.That(distraction.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Distraction_IsInternal_CanBeSetToTrue()
    {
        // Arrange & Act
        var distraction = new Distraction { IsInternal = true };

        // Assert
        Assert.That(distraction.IsInternal, Is.True);
    }

    [Test]
    public void Distraction_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var distraction = new Distraction
        {
            Description = null,
            DurationMinutes = null,
            Session = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(distraction.Description, Is.Null);
            Assert.That(distraction.DurationMinutes, Is.Null);
            Assert.That(distraction.Session, Is.Null);
        });
    }

    [Test]
    public void Distraction_DurationMinutes_CanBeZero()
    {
        // Arrange & Act
        var distraction = new Distraction { DurationMinutes = 0 };

        // Assert
        Assert.That(distraction.DurationMinutes, Is.EqualTo(0));
    }

    [Test]
    public void Distraction_DurationMinutes_CanBeSmallFraction()
    {
        // Arrange & Act
        var distraction = new Distraction { DurationMinutes = 0.5 };

        // Assert
        Assert.That(distraction.DurationMinutes, Is.EqualTo(0.5));
    }

    [Test]
    public void Distraction_DurationMinutes_CanBeLargeValue()
    {
        // Arrange & Act
        var distraction = new Distraction { DurationMinutes = 120.5 };

        // Assert
        Assert.That(distraction.DurationMinutes, Is.EqualTo(120.5));
    }

    [Test]
    public void Distraction_Type_CanBeSetToVariousValues()
    {
        // Arrange
        var types = new[] { "Phone", "Email", "Social Media", "Mind Wandering", "Noise", "Other" };

        // Act & Assert
        foreach (var type in types)
        {
            var distraction = new Distraction { Type = type };
            Assert.That(distraction.Type, Is.EqualTo(type));
        }
    }

    [Test]
    public void Distraction_OccurredAt_CanBeSetToSpecificDateTime()
    {
        // Arrange
        var specificTime = new DateTime(2024, 6, 15, 14, 30, 0);
        var distraction = new Distraction();

        // Act
        distraction.OccurredAt = specificTime;

        // Assert
        Assert.That(distraction.OccurredAt, Is.EqualTo(specificTime));
    }

    [Test]
    public void Distraction_Session_CanBeSetToFocusSession()
    {
        // Arrange
        var session = new FocusSession { FocusSessionId = Guid.NewGuid() };
        var distraction = new Distraction();

        // Act
        distraction.Session = session;

        // Assert
        Assert.That(distraction.Session, Is.EqualTo(session));
    }
}
