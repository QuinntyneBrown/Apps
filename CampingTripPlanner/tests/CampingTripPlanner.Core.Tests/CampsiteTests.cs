// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core.Tests;

public class CampsiteTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            CampsiteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Yosemite Valley",
            Location = "California",
            CampsiteType = CampsiteType.Tent,
            HasElectricity = true,
            HasWater = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(campsite.CampsiteId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(campsite.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(campsite.Name, Is.EqualTo("Yosemite Valley"));
            Assert.That(campsite.Location, Is.EqualTo("California"));
            Assert.That(campsite.CampsiteType, Is.EqualTo(CampsiteType.Tent));
            Assert.That(campsite.HasElectricity, Is.True);
            Assert.That(campsite.HasWater, Is.True);
            Assert.That(campsite.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(campsite.Trips, Is.Not.Null);
            Assert.That(campsite.Reviews, Is.Not.Null);
        });
    }

    [Test]
    public void Campsite_WithDescription_StoresDescriptionCorrectly()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            Description = "Beautiful mountain views"
        };

        // Assert
        Assert.That(campsite.Description, Is.EqualTo("Beautiful mountain views"));
    }

    [Test]
    public void Campsite_WithNullDescription_AllowsNull()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            Description = null
        };

        // Assert
        Assert.That(campsite.Description, Is.Null);
    }

    [Test]
    public void Campsite_WithCostPerNight_StoresCostCorrectly()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            CostPerNight = 45.50m
        };

        // Assert
        Assert.That(campsite.CostPerNight, Is.EqualTo(45.50m));
    }

    [Test]
    public void Campsite_WithNullCostPerNight_AllowsNull()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            CostPerNight = null
        };

        // Assert
        Assert.That(campsite.CostPerNight, Is.Null);
    }

    [Test]
    public void Campsite_CanBeMarkedAsFavorite()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            IsFavorite = true
        };

        // Assert
        Assert.That(campsite.IsFavorite, Is.True);
    }

    [Test]
    public void Campsite_DefaultIsFavorite_IsFalse()
    {
        // Arrange & Act
        var campsite = new Campsite();

        // Assert
        Assert.That(campsite.IsFavorite, Is.False);
    }

    [Test]
    public void Campsite_AllCampsiteTypesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var types = Enum.GetValues<CampsiteType>();
        foreach (var type in types)
        {
            var campsite = new Campsite { CampsiteType = type };
            Assert.That(campsite.CampsiteType, Is.EqualTo(type));
        }
    }

    [Test]
    public void Campsite_CanAddMultipleTrips()
    {
        // Arrange
        var campsite = new Campsite();
        var trip1 = new Trip { TripId = Guid.NewGuid() };
        var trip2 = new Trip { TripId = Guid.NewGuid() };

        // Act
        campsite.Trips.Add(trip1);
        campsite.Trips.Add(trip2);

        // Assert
        Assert.That(campsite.Trips, Has.Count.EqualTo(2));
    }

    [Test]
    public void Campsite_CanAddMultipleReviews()
    {
        // Arrange
        var campsite = new Campsite();
        var review1 = new Review { ReviewId = Guid.NewGuid() };
        var review2 = new Review { ReviewId = Guid.NewGuid() };

        // Act
        campsite.Reviews.Add(review1);
        campsite.Reviews.Add(review2);

        // Assert
        Assert.That(campsite.Reviews, Has.Count.EqualTo(2));
    }

    [Test]
    public void Campsite_WithoutElectricity_HasElectricityIsFalse()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            HasElectricity = false
        };

        // Assert
        Assert.That(campsite.HasElectricity, Is.False);
    }

    [Test]
    public void Campsite_WithoutWater_HasWaterIsFalse()
    {
        // Arrange & Act
        var campsite = new Campsite
        {
            HasWater = false
        };

        // Assert
        Assert.That(campsite.HasWater, Is.False);
    }
}
