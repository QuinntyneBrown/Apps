// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Core.Tests;

public class EventTypeTests
{
    [Test]
    public void EventType_Social_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Social, Is.EqualTo((EventType)0));
    }

    [Test]
    public void EventType_Meal_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Meal, Is.EqualTo((EventType)1));
    }

    [Test]
    public void EventType_Sports_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Sports, Is.EqualTo((EventType)2));
    }

    [Test]
    public void EventType_Outdoor_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Outdoor, Is.EqualTo((EventType)3));
    }

    [Test]
    public void EventType_Cultural_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Cultural, Is.EqualTo((EventType)4));
    }

    [Test]
    public void EventType_GameNight_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.GameNight, Is.EqualTo((EventType)5));
    }

    [Test]
    public void EventType_Travel_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Travel, Is.EqualTo((EventType)6));
    }

    [Test]
    public void EventType_Celebration_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Celebration, Is.EqualTo((EventType)7));
    }

    [Test]
    public void EventType_Meeting_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Meeting, Is.EqualTo((EventType)8));
    }

    [Test]
    public void EventType_Other_HasCorrectValue()
    {
        // Assert
        Assert.That(EventType.Other, Is.EqualTo((EventType)9));
    }

    [Test]
    public void EventType_AllValues_CanBeAssigned()
    {
        // Arrange
        var allEventTypes = new[]
        {
            EventType.Social,
            EventType.Meal,
            EventType.Sports,
            EventType.Outdoor,
            EventType.Cultural,
            EventType.GameNight,
            EventType.Travel,
            EventType.Celebration,
            EventType.Meeting,
            EventType.Other
        };

        // Act & Assert
        foreach (var eventType in allEventTypes)
        {
            var evt = new Event { EventType = eventType };
            Assert.That(evt.EventType, Is.EqualTo(eventType));
        }
    }

    [Test]
    public void EventType_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(EventType));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(10));
    }
}
