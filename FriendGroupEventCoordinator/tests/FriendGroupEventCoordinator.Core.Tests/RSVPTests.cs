// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;

namespace FriendGroupEventCoordinator.Core.Tests;

public class RSVPTests
{
    [Test]
    public void RSVP_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var rsvpId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var memberId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var response = RSVPResponse.Yes;
        var additionalGuests = 2;

        // Act
        var rsvp = new RSVP
        {
            RSVPId = rsvpId,
            EventId = eventId,
            MemberId = memberId,
            UserId = userId,
            Response = response,
            AdditionalGuests = additionalGuests,
            Notes = "Looking forward to it!"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rsvp.RSVPId, Is.EqualTo(rsvpId));
            Assert.That(rsvp.EventId, Is.EqualTo(eventId));
            Assert.That(rsvp.MemberId, Is.EqualTo(memberId));
            Assert.That(rsvp.UserId, Is.EqualTo(userId));
            Assert.That(rsvp.Response, Is.EqualTo(response));
            Assert.That(rsvp.AdditionalGuests, Is.EqualTo(additionalGuests));
            Assert.That(rsvp.Notes, Is.EqualTo("Looking forward to it!"));
            Assert.That(rsvp.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void RSVP_DefaultValues_AreSetCorrectly()
    {
        // Act
        var rsvp = new RSVP();

        // Assert
        Assert.That(rsvp.CreatedAt, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void UpdateResponse_ChangesResponseAndUpdatesTimestamp()
    {
        // Arrange
        var rsvp = new RSVP { Response = RSVPResponse.Pending };
        var beforeCall = DateTime.UtcNow;

        // Act
        rsvp.UpdateResponse(RSVPResponse.Yes);
        var afterCall = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rsvp.Response, Is.EqualTo(RSVPResponse.Yes));
            Assert.That(rsvp.UpdatedAt, Is.Not.Null);
            Assert.That(rsvp.UpdatedAt!.Value, Is.GreaterThanOrEqualTo(beforeCall).And.LessThanOrEqualTo(afterCall));
        });
    }

    [Test]
    public void UpdateResponse_ToNo_ChangesResponse()
    {
        // Arrange
        var rsvp = new RSVP { Response = RSVPResponse.Yes };

        // Act
        rsvp.UpdateResponse(RSVPResponse.No);

        // Assert
        Assert.That(rsvp.Response, Is.EqualTo(RSVPResponse.No));
    }

    [Test]
    public void UpdateResponse_ToMaybe_ChangesResponse()
    {
        // Arrange
        var rsvp = new RSVP { Response = RSVPResponse.No };

        // Act
        rsvp.UpdateResponse(RSVPResponse.Maybe);

        // Assert
        Assert.That(rsvp.Response, Is.EqualTo(RSVPResponse.Maybe));
    }

    [Test]
    public void GetTotalAttendeeCount_WhenResponseIsYes_ReturnsOnePlusGuests()
    {
        // Arrange
        var rsvp = new RSVP
        {
            Response = RSVPResponse.Yes,
            AdditionalGuests = 3
        };

        // Act
        var count = rsvp.GetTotalAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(4));
    }

    [Test]
    public void GetTotalAttendeeCount_WhenResponseIsYesWithNoGuests_ReturnsOne()
    {
        // Arrange
        var rsvp = new RSVP
        {
            Response = RSVPResponse.Yes,
            AdditionalGuests = 0
        };

        // Act
        var count = rsvp.GetTotalAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void GetTotalAttendeeCount_WhenResponseIsNo_ReturnsZero()
    {
        // Arrange
        var rsvp = new RSVP
        {
            Response = RSVPResponse.No,
            AdditionalGuests = 2
        };

        // Act
        var count = rsvp.GetTotalAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalAttendeeCount_WhenResponseIsMaybe_ReturnsZero()
    {
        // Arrange
        var rsvp = new RSVP
        {
            Response = RSVPResponse.Maybe,
            AdditionalGuests = 1
        };

        // Act
        var count = rsvp.GetTotalAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void GetTotalAttendeeCount_WhenResponseIsPending_ReturnsZero()
    {
        // Arrange
        var rsvp = new RSVP
        {
            Response = RSVPResponse.Pending,
            AdditionalGuests = 5
        };

        // Act
        var count = rsvp.GetTotalAttendeeCount();

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public void RSVP_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var rsvp = new RSVP
        {
            Notes = null,
            UpdatedAt = null,
            Event = null,
            Member = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(rsvp.Notes, Is.Null);
            Assert.That(rsvp.UpdatedAt, Is.Null);
            Assert.That(rsvp.Event, Is.Null);
            Assert.That(rsvp.Member, Is.Null);
        });
    }

    [Test]
    public void RSVP_AdditionalGuests_CanBeZero()
    {
        // Arrange & Act
        var rsvp = new RSVP { AdditionalGuests = 0 };

        // Assert
        Assert.That(rsvp.AdditionalGuests, Is.EqualTo(0));
    }

    [Test]
    public void RSVP_AdditionalGuests_CanBeLargeNumber()
    {
        // Arrange & Act
        var rsvp = new RSVP { AdditionalGuests = 10 };

        // Assert
        Assert.That(rsvp.AdditionalGuests, Is.EqualTo(10));
    }

    [Test]
    public void RSVP_Response_CanBeSetToAllValues()
    {
        // Arrange
        var rsvp = new RSVP();

        // Act & Assert
        foreach (RSVPResponse response in Enum.GetValues(typeof(RSVPResponse)))
        {
            rsvp.Response = response;
            Assert.That(rsvp.Response, Is.EqualTo(response));
        }
    }

    [Test]
    public void RSVPResponse_Pending_HasCorrectValue()
    {
        // Assert
        Assert.That(RSVPResponse.Pending, Is.EqualTo((RSVPResponse)0));
    }

    [Test]
    public void RSVPResponse_Yes_HasCorrectValue()
    {
        // Assert
        Assert.That(RSVPResponse.Yes, Is.EqualTo((RSVPResponse)1));
    }

    [Test]
    public void RSVPResponse_No_HasCorrectValue()
    {
        // Assert
        Assert.That(RSVPResponse.No, Is.EqualTo((RSVPResponse)2));
    }

    [Test]
    public void RSVPResponse_Maybe_HasCorrectValue()
    {
        // Assert
        Assert.That(RSVPResponse.Maybe, Is.EqualTo((RSVPResponse)3));
    }

    [Test]
    public void RSVPResponse_HasExpectedNumberOfValues()
    {
        // Arrange
        var enumValues = Enum.GetValues(typeof(RSVPResponse));

        // Assert
        Assert.That(enumValues.Length, Is.EqualTo(4));
    }
}
