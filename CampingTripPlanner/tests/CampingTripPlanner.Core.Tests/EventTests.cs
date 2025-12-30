// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core.Tests;

public class EventTests
{
    [Test]
    public void CampsiteAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var campsiteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var beforeCreate = DateTime.UtcNow;

        // Act
        var evt = new CampsiteAddedEvent
        {
            CampsiteId = campsiteId,
            UserId = userId,
            Name = "Redwood Grove",
            CampsiteType = CampsiteType.Tent
        };
        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CampsiteId, Is.EqualTo(campsiteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Redwood Grove"));
            Assert.That(evt.CampsiteType, Is.EqualTo(CampsiteType.Tent));
            Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void CampsiteUpdatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var campsiteId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new CampsiteUpdatedEvent
        {
            CampsiteId = campsiteId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CampsiteId, Is.EqualTo(campsiteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GearItemAddedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        // Act
        var evt = new GearItemAddedEvent
        {
            GearChecklistId = gearId,
            UserId = userId,
            TripId = tripId,
            ItemName = "Sleeping bag"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GearChecklistId, Is.EqualTo(gearId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.ItemName, Is.EqualTo("Sleeping bag"));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GearItemPackedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new GearItemPackedEvent
        {
            GearChecklistId = gearId,
            UserId = userId,
            IsPacked = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GearChecklistId, Is.EqualTo(gearId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.IsPacked, Is.True);
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GearItemPackedEvent_WhenUnpacked_SetsIsPackedToFalse()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new GearItemPackedEvent
        {
            GearChecklistId = gearId,
            UserId = userId,
            IsPacked = false
        };

        // Assert
        Assert.That(evt.IsPacked, Is.False);
    }

    [Test]
    public void ReviewCreatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var campsiteId = Guid.NewGuid();

        // Act
        var evt = new ReviewCreatedEvent
        {
            ReviewId = reviewId,
            UserId = userId,
            CampsiteId = campsiteId,
            Rating = 4
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReviewId, Is.EqualTo(reviewId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CampsiteId, Is.EqualTo(campsiteId));
            Assert.That(evt.Rating, Is.EqualTo(4));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ReviewUpdatedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new ReviewUpdatedEvent
        {
            ReviewId = reviewId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReviewId, Is.EqualTo(reviewId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TripPlannedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var startDate = new DateTime(2024, 8, 15);

        // Act
        var evt = new TripPlannedEvent
        {
            TripId = tripId,
            UserId = userId,
            Name = "Weekend Getaway",
            StartDate = startDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Weekend Getaway"));
            Assert.That(evt.StartDate, Is.EqualTo(startDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void TripCompletedEvent_CreatesEventWithCorrectProperties()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var endDate = new DateTime(2024, 8, 20);

        // Act
        var evt = new TripCompletedEvent
        {
            TripId = tripId,
            UserId = userId,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.EndDate, Is.EqualTo(endDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }
}
