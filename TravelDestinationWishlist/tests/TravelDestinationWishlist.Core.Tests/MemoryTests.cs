// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core.Tests;

public class MemoryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMemory()
    {
        // Arrange
        var memoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var title = "Eiffel Tower Visit";
        var description = "Amazing experience at the top";
        var memoryDate = new DateTime(2024, 6, 5);
        var photoUrl = "https://example.com/photo.jpg";

        // Act
        var memory = new Memory
        {
            MemoryId = memoryId,
            UserId = userId,
            TripId = tripId,
            Title = title,
            Description = description,
            MemoryDate = memoryDate,
            PhotoUrl = photoUrl
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.MemoryId, Is.EqualTo(memoryId));
            Assert.That(memory.UserId, Is.EqualTo(userId));
            Assert.That(memory.TripId, Is.EqualTo(tripId));
            Assert.That(memory.Title, Is.EqualTo(title));
            Assert.That(memory.Description, Is.EqualTo(description));
            Assert.That(memory.MemoryDate, Is.EqualTo(memoryDate));
            Assert.That(memory.PhotoUrl, Is.EqualTo(photoUrl));
        });
    }

    [Test]
    public void MemoryDate_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Test Memory"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.MemoryDate, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(memory.MemoryDate, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Test Memory"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(memory.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Memory_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Simple Memory"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.Description, Is.Null);
            Assert.That(memory.PhotoUrl, Is.Null);
        });
    }

    [Test]
    public void Trip_NavigationProperty_CanBeSet()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7)
        };

        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = trip.TripId,
            Title = "Trip Memory"
        };

        // Act
        memory.Trip = trip;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(memory.Trip, Is.Not.Null);
            Assert.That(memory.Trip.TripId, Is.EqualTo(trip.TripId));
        });
    }

    [Test]
    public void Memory_WithLongDescription_StoresCorrectly()
    {
        // Arrange
        var longDescription = "This is a very long description of a memorable moment during the trip. " +
                             "It contains many details about the experience, the people, the food, and the overall atmosphere. " +
                             "We will cherish this memory forever.";

        // Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Amazing Day",
            Description = longDescription
        };

        // Assert
        Assert.That(memory.Description, Is.EqualTo(longDescription));
    }

    [Test]
    public void PhotoUrl_CanStoreComplexUrl()
    {
        // Arrange
        var complexUrl = "https://example.com/photos/2024/trip/paris/day5/eiffel_tower_sunset.jpg?quality=high&size=large";

        // Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Sunset Photo",
            PhotoUrl = complexUrl
        };

        // Assert
        Assert.That(memory.PhotoUrl, Is.EqualTo(complexUrl));
    }

    [Test]
    public void Memory_WithPastDate_IsValid()
    {
        // Arrange
        var pastDate = new DateTime(2020, 1, 1);

        // Act
        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            Title = "Old Memory",
            MemoryDate = pastDate
        };

        // Assert
        Assert.That(memory.MemoryDate, Is.EqualTo(pastDate));
    }
}
