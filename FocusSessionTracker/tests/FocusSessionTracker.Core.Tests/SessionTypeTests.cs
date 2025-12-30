// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Core.Tests;

public class SessionTypeTests
{
    [Test]
    public void SessionType_DeepWork_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.DeepWork, Is.EqualTo((SessionType)0));
    }

    [Test]
    public void SessionType_Pomodoro_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Pomodoro, Is.EqualTo((SessionType)1));
    }

    [Test]
    public void SessionType_Study_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Study, Is.EqualTo((SessionType)2));
    }

    [Test]
    public void SessionType_Creative_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Creative, Is.EqualTo((SessionType)3));
    }

    [Test]
    public void SessionType_Meeting_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Meeting, Is.EqualTo((SessionType)4));
    }

    [Test]
    public void SessionType_Learning_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Learning, Is.EqualTo((SessionType)5));
    }

    [Test]
    public void SessionType_Planning_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Planning, Is.EqualTo((SessionType)6));
    }

    [Test]
    public void SessionType_Other_HasCorrectValue()
    {
        // Assert
        Assert.That(SessionType.Other, Is.EqualTo((SessionType)7));
    }

    [Test]
    public void SessionType_AllValues_CanBeAssigned()
    {
        // Arrange
        var allSessionTypes = new[]
        {
            SessionType.DeepWork,
            SessionType.Pomodoro,
            SessionType.Study,
            SessionType.Creative,
            SessionType.Meeting,
            SessionType.Learning,
            SessionType.Planning,
            SessionType.Other
        };

        // Act & Assert
        foreach (var sessionType in allSessionTypes)
        {
            var session = new FocusSession { SessionType = sessionType };
            Assert.That(session.SessionType, Is.EqualTo(sessionType));
        }
    }

    [Test]
    public void SessionType_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(SessionType));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(8));
    }
}
