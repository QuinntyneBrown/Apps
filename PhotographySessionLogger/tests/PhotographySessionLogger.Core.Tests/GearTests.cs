// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class GearTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesGear()
    {
        // Arrange & Act
        var gear = new Gear();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gear.GearId, Is.EqualTo(Guid.Empty));
            Assert.That(gear.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(gear.Name, Is.EqualTo(string.Empty));
            Assert.That(gear.GearType, Is.EqualTo(string.Empty));
            Assert.That(gear.Brand, Is.Null);
            Assert.That(gear.Model, Is.Null);
            Assert.That(gear.PurchaseDate, Is.Null);
            Assert.That(gear.PurchasePrice, Is.Null);
            Assert.That(gear.Notes, Is.Null);
            Assert.That(gear.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var gearId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var purchaseDate = DateTime.UtcNow.AddYears(-1);

        // Act
        var gear = new Gear
        {
            GearId = gearId,
            UserId = userId,
            Name = "Primary Camera",
            GearType = "Camera",
            Brand = "Canon",
            Model = "EOS R5",
            PurchaseDate = purchaseDate,
            PurchasePrice = 3899.00m,
            Notes = "Excellent for portraits"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(gear.GearId, Is.EqualTo(gearId));
            Assert.That(gear.UserId, Is.EqualTo(userId));
            Assert.That(gear.Name, Is.EqualTo("Primary Camera"));
            Assert.That(gear.GearType, Is.EqualTo("Camera"));
            Assert.That(gear.Brand, Is.EqualTo("Canon"));
            Assert.That(gear.Model, Is.EqualTo("EOS R5"));
            Assert.That(gear.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(gear.PurchasePrice, Is.EqualTo(3899.00m));
            Assert.That(gear.Notes, Is.EqualTo("Excellent for portraits"));
        });
    }

    [Test]
    public void Brand_CanBeNull()
    {
        // Arrange & Act
        var gear = new Gear { Brand = null };

        // Assert
        Assert.That(gear.Brand, Is.Null);
    }

    [Test]
    public void Model_CanBeNull()
    {
        // Arrange & Act
        var gear = new Gear { Model = null };

        // Assert
        Assert.That(gear.Model, Is.Null);
    }

    [Test]
    public void PurchaseDate_CanBeNull()
    {
        // Arrange & Act
        var gear = new Gear { PurchaseDate = null };

        // Assert
        Assert.That(gear.PurchaseDate, Is.Null);
    }

    [Test]
    public void PurchasePrice_CanBeNull()
    {
        // Arrange & Act
        var gear = new Gear { PurchasePrice = null };

        // Assert
        Assert.That(gear.PurchasePrice, Is.Null);
    }

    [Test]
    public void PurchasePrice_AcceptsDecimalValues()
    {
        // Arrange & Act
        var gear = new Gear { PurchasePrice = 1299.99m };

        // Assert
        Assert.That(gear.PurchasePrice, Is.EqualTo(1299.99m));
    }

    [Test]
    public void Notes_CanBeNull()
    {
        // Arrange & Act
        var gear = new Gear { Notes = null };

        // Assert
        Assert.That(gear.Notes, Is.Null);
    }
}
