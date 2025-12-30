// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Core.Tests;

public class EventTests
{
    [Test]
    public void Event_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var groupId = Guid.NewGuid();
        var createdByUserId = Guid.NewGuid();
        var title = "Summer BBQ Party";
        var description = "Annual summer barbecue";
        var eventType = EventType.Social;
        var startDateTime = new DateTime(2024, 7, 15, 14, 0, 0);
        var endDateTime = new DateTime(2024, 7, 15, 18, 0, 0);

        // Act
        var evt = new Event
        {
            EventId = eventId,
            GroupId = groupId,
            CreatedByUserId = createdByUserId,
            Title = title,
            Description = description,
            EventType = eventType,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Location = "Central Park",
            MaxAttendees = 20,
            IsCancelled = false
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.GroupId, Is.EqualTo(groupId));
            Assert.That(evt.CreatedByUserId, Is.EqualTo(createdByUserId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Description, Is.EqualTo(description));
            Assert.That(evt.EventType, Is.EqualTo(eventType));
            Assert.That(evt.StartDateTime, Is.EqualTo(startDateTime));
            Assert.That(evt.EndDateTime, Is.EqualTo(endDateTime));
            Assert.That(evt.Location, Is.EqualTo("Central Park"));
            Assert.That(evt.MaxAttendees, Is.EqualTo(20));
            Assert.That(evt.IsCancelled, Is.False);
            Assert.That(evt.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(evt.RSVPs, Is.Not.Null);
        });
    }

    [Test]
    public void Event_DefaultValues_AreSetCorrectly()
    {
        // Act
        var evt = new Event();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.Title, Is.EqualTo(string.Empty));
            Assert.That(evt.Description, Is.EqualTo(string.Empty));
            Assert.That(evt.IsCancelled, Is.False);
            Assert.That(evt.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(evt.RSVPs, Is.Not.Null);
            Assert.That(evt.RSVPs.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void Cancel_SetsIsCancelledToTrueAndUpdatesTimestamp()
    {
        // Arrange
        var evt = new Event { IsCancelled = false };
        var beforeCall = DateTime.UtcNow;

        // Act
        evt.Cancel();
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.IsCancelled, Is.True);
            Assert.That(evt.UpdatedAt, Is.Not.Null);
            Assert.That(evt.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void Cancel_WhenAlreadyCancelled_RemainsCancelled()
    {
        // Arrange
        var evt = new Event { IsCancelled = true };

        // Act
        evt.Cancel();

        // Assert
        Assert.That(evt.IsCancelled, Is.True);
    }

    [Test]
    public void GetConfirmedAttendeeCount_WithNoRSVPs_ReturnsZero()
    {
        // Arrange
        var evt = new Event();

        // Act
        var count = evt.GetConfirmedAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetConfirmedAttendeeCount_WithOnlyYesResponses_ReturnsCorrectCount()
    {
        // Arrange
        var evt = new Event
        {
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var count = evt.GetConfirmedAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void GetConfirmedAttendeeCount_WithMixedResponses_ReturnsOnlyYesCount()
    {
        // Arrange
        var evt = new Event
        {
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.No },
                new RSVP { Response = RSVPResponse.Maybe },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Pending },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var count = evt.GetConfirmedAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void GetConfirmedAttendeeCount_WithNoYesResponses_ReturnsZero()
    {
        // Arrange
        var evt = new Event
        {
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.No },
                new RSVP { Response = RSVPResponse.Maybe },
                new RSVP { Response = RSVPResponse.Pending }
            }
        };

        // Act
        var count = evt.GetConfirmedAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void IsAtCapacity_WhenMaxAttendeesIsNull_ReturnsFalse()
    {
        // Arrange
        var evt = new Event
        {
            MaxAttendees = null,
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var isAtCapacity = evt.IsAtCapacity();

        // Assert
        Assert.That(isAtCapacity, Is.False);
    }

    [Test]
    public void IsAtCapacity_WhenBelowCapacity_ReturnsFalse()
    {
        // Arrange
        var evt = new Event
        {
            MaxAttendees = 10,
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var isAtCapacity = evt.IsAtCapacity();

        // Assert
        Assert.That(isAtCapacity, Is.False);
    }

    [Test]
    public void IsAtCapacity_WhenAtExactCapacity_ReturnsTrue()
    {
        // Arrange
        var evt = new Event
        {
            MaxAttendees = 3,
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var isAtCapacity = evt.IsAtCapacity();

        // Assert
        Assert.That(isAtCapacity, Is.True);
    }

    [Test]
    public void IsAtCapacity_WhenOverCapacity_ReturnsTrue()
    {
        // Arrange
        var evt = new Event
        {
            MaxAttendees = 2,
            RSVPs = new List<RSVP>
            {
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes },
                new RSVP { Response = RSVPResponse.Yes }
            }
        };

        // Act
        var isAtCapacity = evt.IsAtCapacity();

        // Assert
        Assert.That(isAtCapacity, Is.True);
    }

    [Test]
    public void Event_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var evt = new Event
        {
            EndDateTime = null,
            Location = null,
            MaxAttendees = null,
            UpdatedAt = null,
            Group = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EndDateTime, Is.Null);
            Assert.That(evt.Location, Is.Null);
            Assert.That(evt.MaxAttendees, Is.Null);
            Assert.That(evt.UpdatedAt, Is.Null);
            Assert.That(evt.Group, Is.Null);
        });
    }

    [Test]
    public void Event_EventType_CanBeSetToAllValues()
    {
        // Arrange
        var evt = new Event();

        // Act & Assert
        foreach (EventType eventType in Enum.GetValues(typeof(EventType)))
        {
            evt.EventType = eventType;
            Assert.That(evt.EventType, Is.EqualTo(eventType));
        }
    }

    [Test]
    public void Event_MaxAttendees_CanBeZero()
    {
        // Arrange & Act
        var evt = new Event { MaxAttendees = 0 };

        // Assert
        Assert.That(evt.MaxAttendees, Is.EqualTo(0));
    }

    [Test]
    public void Event_MaxAttendees_CanBeLargeNumber()
    {
        // Arrange & Act
        var evt = new Event { MaxAttendees = 1000 };

        // Assert
        Assert.That(evt.MaxAttendees, Is.EqualTo(1000));
    }
}
