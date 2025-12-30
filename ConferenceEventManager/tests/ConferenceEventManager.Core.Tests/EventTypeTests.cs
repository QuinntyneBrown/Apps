// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConferenceEventManager.Core.Tests;

public class EventTypeTests
{
    [Test]
    public void EventType_Conference_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Conference;

        // Assert
        Assert.That((int)type, Is.EqualTo(0));
    }

    [Test]
    public void EventType_Workshop_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Workshop;

        // Assert
        Assert.That((int)type, Is.EqualTo(1));
    }

    [Test]
    public void EventType_Seminar_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Seminar;

        // Assert
        Assert.That((int)type, Is.EqualTo(2));
    }

    [Test]
    public void EventType_Webinar_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Webinar;

        // Assert
        Assert.That((int)type, Is.EqualTo(3));
    }

    [Test]
    public void EventType_Networking_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Networking;

        // Assert
        Assert.That((int)type, Is.EqualTo(4));
    }

    [Test]
    public void EventType_Meetup_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Meetup;

        // Assert
        Assert.That((int)type, Is.EqualTo(5));
    }

    [Test]
    public void EventType_TradeShow_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.TradeShow;

        // Assert
        Assert.That((int)type, Is.EqualTo(6));
    }

    [Test]
    public void EventType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var type = EventType.Other;

        // Assert
        Assert.That((int)type, Is.EqualTo(7));
    }

    [Test]
    public void EventType_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var conference = EventType.Conference;
        var workshop = EventType.Workshop;
        var seminar = EventType.Seminar;
        var webinar = EventType.Webinar;
        var networking = EventType.Networking;
        var meetup = EventType.Meetup;
        var tradeShow = EventType.TradeShow;
        var other = EventType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(conference, Is.EqualTo(EventType.Conference));
            Assert.That(workshop, Is.EqualTo(EventType.Workshop));
            Assert.That(seminar, Is.EqualTo(EventType.Seminar));
            Assert.That(webinar, Is.EqualTo(EventType.Webinar));
            Assert.That(networking, Is.EqualTo(EventType.Networking));
            Assert.That(meetup, Is.EqualTo(EventType.Meetup));
            Assert.That(tradeShow, Is.EqualTo(EventType.TradeShow));
            Assert.That(other, Is.EqualTo(EventType.Other));
        });
    }
}
