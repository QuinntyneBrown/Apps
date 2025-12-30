// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core.Tests;

public class TripTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange
        var startDate = new DateTime(2024, 8, 1);
        var endDate = new DateTime(2024, 8, 5);

        // Act
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Summer Camping Trip",
            StartDate = startDate,
            EndDate = endDate,
            NumberOfPeople = 4
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.TripId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(trip.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(trip.Name, Is.EqualTo("Summer Camping Trip"));
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
            Assert.That(trip.NumberOfPeople, Is.EqualTo(4));
            Assert.That(trip.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(trip.GearChecklists, Is.Not.Null);
        });
    }

    [Test]
    public void Trip_WithCampsite_AssociatesCampsiteCorrectly()
    {
        // Arrange
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            Name = "Pine Grove"
        };

        // Act
        var trip = new Trip
        {
            CampsiteId = campsite.CampsiteId,
            Campsite = campsite
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.CampsiteId, Is.EqualTo(campsite.CampsiteId));
            Assert.That(trip.Campsite, Is.Not.Null);
            Assert.That(trip.Campsite.Name, Is.EqualTo("Pine Grove"));
        });
    }

    [Test]
    public void Trip_WithNullCampsite_AllowsNull()
    {
        // Arrange & Act
        var trip = new Trip
        {
            CampsiteId = null,
            Campsite = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.CampsiteId, Is.Null);
            Assert.That(trip.Campsite, Is.Null);
        });
    }

    [Test]
    public void Trip_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var trip = new Trip
        {
            Notes = "Bring extra firewood"
        };

        // Assert
        Assert.That(trip.Notes, Is.EqualTo("Bring extra firewood"));
    }

    [Test]
    public void Trip_WithNullNotes_AllowsNull()
    {
        // Arrange & Act
        var trip = new Trip
        {
            Notes = null
        };

        // Assert
        Assert.That(trip.Notes, Is.Null);
    }

    [Test]
    public void Trip_WithSinglePerson_StoresNumberCorrectly()
    {
        // Arrange & Act
        var trip = new Trip
        {
            NumberOfPeople = 1
        };

        // Assert
        Assert.That(trip.NumberOfPeople, Is.EqualTo(1));
    }

    [Test]
    public void Trip_WithLargeGroup_StoresNumberCorrectly()
    {
        // Arrange & Act
        var trip = new Trip
        {
            NumberOfPeople = 20
        };

        // Assert
        Assert.That(trip.NumberOfPeople, Is.EqualTo(20));
    }

    [Test]
    public void Trip_CanAddMultipleGearChecklists()
    {
        // Arrange
        var trip = new Trip();
        var gear1 = new GearChecklist { GearChecklistId = Guid.NewGuid() };
        var gear2 = new GearChecklist { GearChecklistId = Guid.NewGuid() };
        var gear3 = new GearChecklist { GearChecklistId = Guid.NewGuid() };

        // Act
        trip.GearChecklists.Add(gear1);
        trip.GearChecklists.Add(gear2);
        trip.GearChecklists.Add(gear3);

        // Assert
        Assert.That(trip.GearChecklists, Has.Count.EqualTo(3));
    }

    [Test]
    public void Trip_WithDateRange_StoresCorrectDates()
    {
        // Arrange
        var startDate = new DateTime(2024, 9, 10, 14, 0, 0);
        var endDate = new DateTime(2024, 9, 15, 12, 0, 0);

        // Act
        var trip = new Trip
        {
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trip.StartDate, Is.EqualTo(startDate));
            Assert.That(trip.EndDate, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void Trip_MultiDayTrip_StoresCorrectDates()
    {
        // Arrange
        var startDate = new DateTime(2024, 12, 20);
        var endDate = new DateTime(2024, 12, 27);

        // Act
        var trip = new Trip
        {
            StartDate = startDate,
            EndDate = endDate
        };

        // Assert
        Assert.That((trip.EndDate - trip.StartDate).Days, Is.EqualTo(7));
    }
}
