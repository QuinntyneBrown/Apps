// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CampingTripPlanner.Core.Tests;

public class GearChecklistTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var gearItem = new GearChecklist
        {
            GearChecklistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TripId = Guid.NewGuid(),
            ItemName = "Tent",
            IsPacked = false,
            Quantity = 1
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gearItem.GearChecklistId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(gearItem.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(gearItem.TripId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(gearItem.ItemName, Is.EqualTo("Tent"));
            Assert.That(gearItem.IsPacked, Is.False);
            Assert.That(gearItem.Quantity, Is.EqualTo(1));
            Assert.That(gearItem.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GearChecklist_CanBeMarkedAsPacked()
    {
        // Arrange
        var gearItem = new GearChecklist
        {
            IsPacked = false
        };

        // Act
        gearItem.IsPacked = true;

        // Assert
        Assert.That(gearItem.IsPacked, Is.True);
    }

    [Test]
    public void GearChecklist_WithQuantity_StoresQuantityCorrectly()
    {
        // Arrange & Act
        var gearItem = new GearChecklist
        {
            ItemName = "Water bottles",
            Quantity = 4
        };

        // Assert
        Assert.That(gearItem.Quantity, Is.EqualTo(4));
    }

    [Test]
    public void GearChecklist_DefaultQuantity_Is1()
    {
        // Arrange & Act
        var gearItem = new GearChecklist();

        // Assert
        Assert.That(gearItem.Quantity, Is.EqualTo(1));
    }

    [Test]
    public void GearChecklist_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var gearItem = new GearChecklist
        {
            Notes = "Pack in main backpack"
        };

        // Assert
        Assert.That(gearItem.Notes, Is.EqualTo("Pack in main backpack"));
    }

    [Test]
    public void GearChecklist_WithNullNotes_AllowsNull()
    {
        // Arrange & Act
        var gearItem = new GearChecklist
        {
            Notes = null
        };

        // Assert
        Assert.That(gearItem.Notes, Is.Null);
    }

    [Test]
    public void GearChecklist_WithTrip_AssociatesTripCorrectly()
    {
        // Arrange
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            Name = "Yosemite Weekend"
        };
        var gearItem = new GearChecklist
        {
            TripId = trip.TripId,
            Trip = trip
        };

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(gearItem.Trip, Is.Not.Null);
            Assert.That(gearItem.Trip.TripId, Is.EqualTo(trip.TripId));
            Assert.That(gearItem.Trip.Name, Is.EqualTo("Yosemite Weekend"));
        });
    }

    [Test]
    public void GearChecklist_WithNullTrip_AllowsNull()
    {
        // Arrange & Act
        var gearItem = new GearChecklist
        {
            TripId = Guid.NewGuid(),
            Trip = null
        };

        // Assert
        Assert.That(gearItem.Trip, Is.Null);
    }

    [Test]
    public void GearChecklist_CanBeUnpacked()
    {
        // Arrange
        var gearItem = new GearChecklist
        {
            IsPacked = true
        };

        // Act
        gearItem.IsPacked = false;

        // Assert
        Assert.That(gearItem.IsPacked, Is.False);
    }

    [Test]
    public void GearChecklist_DefaultIsPacked_IsFalse()
    {
        // Arrange & Act
        var gearItem = new GearChecklist();

        // Assert
        Assert.That(gearItem.IsPacked, Is.False);
    }
}
