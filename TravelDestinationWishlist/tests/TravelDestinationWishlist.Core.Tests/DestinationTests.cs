// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TravelDestinationWishlist.Core.Tests;

public class DestinationTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDestination()
    {
        // Arrange
        var destinationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Paris";
        var country = "France";
        var type = DestinationType.City;
        var description = "City of lights";
        var priority = 5;
        var notes = "Visit the Eiffel Tower";

        // Act
        var destination = new Destination
        {
            DestinationId = destinationId,
            UserId = userId,
            Name = name,
            Country = country,
            DestinationType = type,
            Description = description,
            Priority = priority,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.DestinationId, Is.EqualTo(destinationId));
            Assert.That(destination.UserId, Is.EqualTo(userId));
            Assert.That(destination.Name, Is.EqualTo(name));
            Assert.That(destination.Country, Is.EqualTo(country));
            Assert.That(destination.DestinationType, Is.EqualTo(type));
            Assert.That(destination.Description, Is.EqualTo(description));
            Assert.That(destination.Priority, Is.EqualTo(priority));
            Assert.That(destination.IsVisited, Is.False);
            Assert.That(destination.VisitedDate, Is.Null);
            Assert.That(destination.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Priority_DefaultsTo3()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Rome",
            Country = "Italy",
            DestinationType = DestinationType.Historical
        };

        // Assert
        Assert.That(destination.Priority, Is.EqualTo(3));
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Tokyo",
            Country = "Japan",
            DestinationType = DestinationType.City
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(destination.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void DestinationType_AllValuesCanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Destination { DestinationType = DestinationType.City }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Beach }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Mountain }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Countryside }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Island }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Historical }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Adventure }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Cultural }, Throws.Nothing);
            Assert.That(() => new Destination { DestinationType = DestinationType.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void Destination_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Bali",
            Country = "Indonesia",
            DestinationType = DestinationType.Island
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.Description, Is.Null);
            Assert.That(destination.Notes, Is.Null);
            Assert.That(destination.VisitedDate, Is.Null);
        });
    }

    [Test]
    public void IsVisited_DefaultsToFalse()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Barcelona",
            Country = "Spain",
            DestinationType = DestinationType.City
        };

        // Assert
        Assert.That(destination.IsVisited, Is.False);
    }

    [Test]
    public void Destination_CanMarkAsVisited()
    {
        // Arrange
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "London",
            Country = "UK",
            DestinationType = DestinationType.City,
            IsVisited = false
        };

        var visitedDate = new DateTime(2024, 3, 15);

        // Act
        destination.IsVisited = true;
        destination.VisitedDate = visitedDate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.IsVisited, Is.True);
            Assert.That(destination.VisitedDate, Is.EqualTo(visitedDate));
        });
    }

    [Test]
    public void Trips_InitializesAsEmptyList()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Sydney",
            Country = "Australia",
            DestinationType = DestinationType.City
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(destination.Trips, Is.Not.Null);
            Assert.That(destination.Trips, Is.Empty);
        });
    }

    [Test]
    public void Priority_CanBeSetToLowValue()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Berlin",
            Country = "Germany",
            DestinationType = DestinationType.City,
            Priority = 1
        };

        // Assert
        Assert.That(destination.Priority, Is.EqualTo(1));
    }

    [Test]
    public void Priority_CanBeSetToHighValue()
    {
        // Arrange & Act
        var destination = new Destination
        {
            DestinationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Maldives",
            Country = "Maldives",
            DestinationType = DestinationType.Beach,
            Priority = 10
        };

        // Assert
        Assert.That(destination.Priority, Is.EqualTo(10));
    }
}
