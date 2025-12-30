// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class RoomTests
{
    [Test]
    public void Room_LivingRoom_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.LivingRoom;

        // Assert
        Assert.That((int)room, Is.EqualTo(0));
    }

    [Test]
    public void Room_Bedroom_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Bedroom;

        // Assert
        Assert.That((int)room, Is.EqualTo(1));
    }

    [Test]
    public void Room_Kitchen_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Kitchen;

        // Assert
        Assert.That((int)room, Is.EqualTo(2));
    }

    [Test]
    public void Room_DiningRoom_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.DiningRoom;

        // Assert
        Assert.That((int)room, Is.EqualTo(3));
    }

    [Test]
    public void Room_Bathroom_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Bathroom;

        // Assert
        Assert.That((int)room, Is.EqualTo(4));
    }

    [Test]
    public void Room_Garage_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Garage;

        // Assert
        Assert.That((int)room, Is.EqualTo(5));
    }

    [Test]
    public void Room_Basement_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Basement;

        // Assert
        Assert.That((int)room, Is.EqualTo(6));
    }

    [Test]
    public void Room_Attic_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Attic;

        // Assert
        Assert.That((int)room, Is.EqualTo(7));
    }

    [Test]
    public void Room_Office_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Office;

        // Assert
        Assert.That((int)room, Is.EqualTo(8));
    }

    [Test]
    public void Room_Storage_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Storage;

        // Assert
        Assert.That((int)room, Is.EqualTo(9));
    }

    [Test]
    public void Room_Other_HasCorrectValue()
    {
        // Arrange & Act
        var room = Room.Other;

        // Assert
        Assert.That((int)room, Is.EqualTo(10));
    }

    [Test]
    public void Room_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var livingRoom = Room.LivingRoom;
        var bedroom = Room.Bedroom;
        var kitchen = Room.Kitchen;
        var diningRoom = Room.DiningRoom;
        var bathroom = Room.Bathroom;
        var garage = Room.Garage;
        var basement = Room.Basement;
        var attic = Room.Attic;
        var office = Room.Office;
        var storage = Room.Storage;
        var other = Room.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(livingRoom, Is.EqualTo(Room.LivingRoom));
            Assert.That(bedroom, Is.EqualTo(Room.Bedroom));
            Assert.That(kitchen, Is.EqualTo(Room.Kitchen));
            Assert.That(diningRoom, Is.EqualTo(Room.DiningRoom));
            Assert.That(bathroom, Is.EqualTo(Room.Bathroom));
            Assert.That(garage, Is.EqualTo(Room.Garage));
            Assert.That(basement, Is.EqualTo(Room.Basement));
            Assert.That(attic, Is.EqualTo(Room.Attic));
            Assert.That(office, Is.EqualTo(Room.Office));
            Assert.That(storage, Is.EqualTo(Room.Storage));
            Assert.That(other, Is.EqualTo(Room.Other));
        });
    }

    [Test]
    public void Room_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var names = new[]
        {
            Room.LivingRoom.ToString(),
            Room.Bedroom.ToString(),
            Room.Kitchen.ToString(),
            Room.DiningRoom.ToString(),
            Room.Bathroom.ToString(),
            Room.Garage.ToString(),
            Room.Basement.ToString(),
            Room.Attic.ToString(),
            Room.Office.ToString(),
            Room.Storage.ToString(),
            Room.Other.ToString()
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(names[0], Is.EqualTo("LivingRoom"));
            Assert.That(names[1], Is.EqualTo("Bedroom"));
            Assert.That(names[2], Is.EqualTo("Kitchen"));
            Assert.That(names[3], Is.EqualTo("DiningRoom"));
            Assert.That(names[4], Is.EqualTo("Bathroom"));
            Assert.That(names[5], Is.EqualTo("Garage"));
            Assert.That(names[6], Is.EqualTo("Basement"));
            Assert.That(names[7], Is.EqualTo("Attic"));
            Assert.That(names[8], Is.EqualTo("Office"));
            Assert.That(names[9], Is.EqualTo("Storage"));
            Assert.That(names[10], Is.EqualTo("Other"));
        });
    }

    [Test]
    public void Room_CanBeCompared()
    {
        // Arrange
        var room1 = Room.Office;
        var room2 = Room.Office;
        var room3 = Room.Garage;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(room1, Is.EqualTo(room2));
            Assert.That(room1, Is.Not.EqualTo(room3));
        });
    }

    [Test]
    public void Room_CanBeUsedInSwitch()
    {
        // Arrange
        var room = Room.Kitchen;
        string result;

        // Act
        result = room switch
        {
            Room.LivingRoom => "Living Room",
            Room.Bedroom => "Bedroom",
            Room.Kitchen => "Kitchen",
            Room.DiningRoom => "Dining Room",
            Room.Bathroom => "Bathroom",
            Room.Garage => "Garage",
            Room.Basement => "Basement",
            Room.Attic => "Attic",
            Room.Office => "Office",
            Room.Storage => "Storage",
            Room.Other => "Other",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Kitchen"));
    }

    [Test]
    public void Room_EnumParse_WorksCorrectly()
    {
        // Arrange
        var roomName = "Bedroom";

        // Act
        var parsed = Enum.Parse<Room>(roomName);

        // Assert
        Assert.That(parsed, Is.EqualTo(Room.Bedroom));
    }
}
