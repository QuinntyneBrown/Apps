// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class PackingListTests
{
    [Test]
    public void Constructor_CreatesPackingList_WithDefaultValues()
    {
        // Arrange & Act
        var packingItem = new PackingList();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(packingItem.PackingListId, Is.EqualTo(Guid.Empty));
            Assert.That(packingItem.TripId, Is.EqualTo(Guid.Empty));
            Assert.That(packingItem.Trip, Is.Null);
            Assert.That(packingItem.ItemName, Is.EqualTo(string.Empty));
            Assert.That(packingItem.IsPacked, Is.False);
            Assert.That(packingItem.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void PackingListId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var packingItem = new PackingList();
        var expectedId = Guid.NewGuid();

        // Act
        packingItem.PackingListId = expectedId;

        // Assert
        Assert.That(packingItem.PackingListId, Is.EqualTo(expectedId));
    }

    [Test]
    public void ItemName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var packingItem = new PackingList();
        var expectedName = "Passport";

        // Act
        packingItem.ItemName = expectedName;

        // Assert
        Assert.That(packingItem.ItemName, Is.EqualTo(expectedName));
    }

    [Test]
    public void IsPacked_DefaultsToFalse()
    {
        // Arrange & Act
        var packingItem = new PackingList();

        // Assert
        Assert.That(packingItem.IsPacked, Is.False);
    }

    [Test]
    public void IsPacked_CanBeSet_ToTrue()
    {
        // Arrange
        var packingItem = new PackingList();

        // Act
        packingItem.IsPacked = true;

        // Assert
        Assert.That(packingItem.IsPacked, Is.True);
    }

    [Test]
    public void IsPacked_CanBeToggled()
    {
        // Arrange
        var packingItem = new PackingList { IsPacked = false };

        // Act
        packingItem.IsPacked = true;
        var firstState = packingItem.IsPacked;
        packingItem.IsPacked = false;
        var secondState = packingItem.IsPacked;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstState, Is.True);
            Assert.That(secondState, Is.False);
        });
    }

    [Test]
    public void PackingList_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var packingListId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var itemName = "Sunscreen";
        var isPacked = true;
        var createdAt = DateTime.UtcNow;

        // Act
        var packingItem = new PackingList
        {
            PackingListId = packingListId,
            TripId = tripId,
            ItemName = itemName,
            IsPacked = isPacked,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(packingItem.PackingListId, Is.EqualTo(packingListId));
            Assert.That(packingItem.TripId, Is.EqualTo(tripId));
            Assert.That(packingItem.ItemName, Is.EqualTo(itemName));
            Assert.That(packingItem.IsPacked, Is.EqualTo(isPacked));
            Assert.That(packingItem.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void PackingList_ForMultipleItems_CanBeCreated()
    {
        // Arrange & Act
        var passport = new PackingList { ItemName = "Passport", IsPacked = true };
        var swimsuit = new PackingList { ItemName = "Swimsuit", IsPacked = false };
        var camera = new PackingList { ItemName = "Camera", IsPacked = true };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(passport.ItemName, Is.EqualTo("Passport"));
            Assert.That(passport.IsPacked, Is.True);
            Assert.That(swimsuit.ItemName, Is.EqualTo("Swimsuit"));
            Assert.That(swimsuit.IsPacked, Is.False);
            Assert.That(camera.ItemName, Is.EqualTo("Camera"));
            Assert.That(camera.IsPacked, Is.True);
        });
    }
}
