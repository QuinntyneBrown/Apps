// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NeighborhoodSocialNetwork.Core.Tests;

public class EventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithDefaultValues()
    {
        // Arrange & Act
        var evt = new Event();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.CreatedByNeighborId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Title, Is.EqualTo(string.Empty));
            Assert.That(evt.Description, Is.EqualTo(string.Empty));
            Assert.That(evt.EventDateTime, Is.EqualTo(default(DateTime)));
            Assert.That(evt.Location, Is.Null);
            Assert.That(evt.IsPublic, Is.True);
            Assert.That(evt.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(evt.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void EventId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedId = Guid.NewGuid();

        // Act
        evt.EventId = expectedId;

        // Assert
        Assert.That(evt.EventId, Is.EqualTo(expectedId));
    }

    [Test]
    public void CreatedByNeighborId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedNeighborId = Guid.NewGuid();

        // Act
        evt.CreatedByNeighborId = expectedNeighborId;

        // Assert
        Assert.That(evt.CreatedByNeighborId, Is.EqualTo(expectedNeighborId));
    }

    [Test]
    public void Title_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedTitle = "Summer Block Party";

        // Act
        evt.Title = expectedTitle;

        // Assert
        Assert.That(evt.Title, Is.EqualTo(expectedTitle));
    }

    [Test]
    public void Description_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedDescription = "Join us for food, music, and fun!";

        // Act
        evt.Description = expectedDescription;

        // Assert
        Assert.That(evt.Description, Is.EqualTo(expectedDescription));
    }

    [Test]
    public void EventDateTime_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedDateTime = new DateTime(2024, 7, 15, 14, 0, 0);

        // Act
        evt.EventDateTime = expectedDateTime;

        // Assert
        Assert.That(evt.EventDateTime, Is.EqualTo(expectedDateTime));
    }

    [Test]
    public void Location_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedLocation = "Community Park";

        // Act
        evt.Location = expectedLocation;

        // Assert
        Assert.That(evt.Location, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void IsPublic_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();

        // Act
        evt.IsPublic = false;

        // Assert
        Assert.That(evt.IsPublic, Is.False);
    }

    [Test]
    public void IsPublic_DefaultsToTrue()
    {
        // Arrange & Act
        var evt = new Event();

        // Assert
        Assert.That(evt.IsPublic, Is.True);
    }

    [Test]
    public void CreatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedDate = new DateTime(2024, 1, 1);

        // Act
        evt.CreatedAt = expectedDate;

        // Assert
        Assert.That(evt.CreatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void UpdatedAt_CanBeSet_AndRetrieved()
    {
        // Arrange
        var evt = new Event();
        var expectedDate = new DateTime(2024, 2, 1);

        // Act
        evt.UpdatedAt = expectedDate;

        // Assert
        Assert.That(evt.UpdatedAt, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Event_WithAllPropertiesSet_IsValid()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var neighborId = Guid.NewGuid();
        var eventDateTime = new DateTime(2024, 7, 15, 14, 0, 0);

        // Act
        var evt = new Event
        {
            EventId = eventId,
            CreatedByNeighborId = neighborId,
            Title = "Test Event",
            Description = "Test Description",
            EventDateTime = eventDateTime,
            Location = "Test Location",
            IsPublic = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.EventId, Is.EqualTo(eventId));
            Assert.That(evt.CreatedByNeighborId, Is.EqualTo(neighborId));
            Assert.That(evt.Title, Is.EqualTo("Test Event"));
            Assert.That(evt.Description, Is.EqualTo("Test Description"));
            Assert.That(evt.EventDateTime, Is.EqualTo(eventDateTime));
            Assert.That(evt.Location, Is.EqualTo("Test Location"));
            Assert.That(evt.IsPublic, Is.True);
        });
    }
}
