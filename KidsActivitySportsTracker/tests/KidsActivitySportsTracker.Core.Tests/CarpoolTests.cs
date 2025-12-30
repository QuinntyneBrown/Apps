// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class CarpoolTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCarpool()
    {
        // Arrange
        var carpoolId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Soccer Carpool";
        var driverName = "Jane Doe";

        // Act
        var carpool = new Carpool
        {
            CarpoolId = carpoolId,
            UserId = userId,
            Name = name,
            DriverName = driverName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(carpool.CarpoolId, Is.EqualTo(carpoolId));
            Assert.That(carpool.UserId, Is.EqualTo(userId));
            Assert.That(carpool.Name, Is.EqualTo(name));
            Assert.That(carpool.DriverName, Is.EqualTo(driverName));
            Assert.That(carpool.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void CarpoolId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedId = Guid.NewGuid();

        // Act
        carpool.CarpoolId = expectedId;

        // Assert
        Assert.That(carpool.CarpoolId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedUserId = Guid.NewGuid();

        // Act
        carpool.UserId = expectedUserId;

        // Assert
        Assert.That(carpool.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void Name_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedName = "Basketball Carpool";

        // Act
        carpool.Name = expectedName;

        // Assert
        Assert.That(carpool.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void DriverName_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedDriverName = "John Smith";

        // Act
        carpool.DriverName = expectedDriverName;

        // Assert
        Assert.That(carpool.DriverName, Is.EqualTo(expectedDriverName));
    }

    [Test]
    public void DriverContact_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedContact = "555-0123";

        // Act
        carpool.DriverContact = expectedContact;

        // Assert
        Assert.That(carpool.DriverContact, Is.EqualTo(expectedContact));
    }

    [Test]
    public void PickupTime_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedTime = DateTime.UtcNow.AddHours(1);

        // Act
        carpool.PickupTime = expectedTime;

        // Assert
        Assert.That(carpool.PickupTime, Is.EqualTo(expectedTime));
    }

    [Test]
    public void PickupLocation_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedLocation = "123 Main St";

        // Act
        carpool.PickupLocation = expectedLocation;

        // Assert
        Assert.That(carpool.PickupLocation, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void DropoffTime_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedTime = DateTime.UtcNow.AddHours(2);

        // Act
        carpool.DropoffTime = expectedTime;

        // Assert
        Assert.That(carpool.DropoffTime, Is.EqualTo(expectedTime));
    }

    [Test]
    public void DropoffLocation_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedLocation = "456 Oak Ave";

        // Act
        carpool.DropoffLocation = expectedLocation;

        // Assert
        Assert.That(carpool.DropoffLocation, Is.EqualTo(expectedLocation));
    }

    [Test]
    public void Participants_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedParticipants = "John, Jane, Bob";

        // Act
        carpool.Participants = expectedParticipants;

        // Assert
        Assert.That(carpool.Participants, Is.EqualTo(expectedParticipants));
    }

    [Test]
    public void Notes_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var carpool = new Carpool();
        var expectedNotes = "Pickup from school parking lot";

        // Act
        carpool.Notes = expectedNotes;

        // Assert
        Assert.That(carpool.Notes, Is.EqualTo(expectedNotes));
    }
}
