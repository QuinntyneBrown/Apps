// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core.Tests;

public class PartTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            PartId = Guid.NewGuid(),
            Name = "Performance Air Filter",
            PartNumber = "33-2304",
            Manufacturer = "K&N",
            Description = "High-flow air filter",
            Price = 49.99m,
            Category = ModCategory.Engine
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.PartId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(part.Name, Is.EqualTo("Performance Air Filter"));
            Assert.That(part.PartNumber, Is.EqualTo("33-2304"));
            Assert.That(part.Manufacturer, Is.EqualTo("K&N"));
            Assert.That(part.Description, Is.EqualTo("High-flow air filter"));
            Assert.That(part.Price, Is.EqualTo(49.99m));
            Assert.That(part.Category, Is.EqualTo(ModCategory.Engine));
            Assert.That(part.InStock, Is.True);
            Assert.That(part.CompatibleVehicles, Is.Not.Null);
        });
    }

    [Test]
    public void UpdatePrice_ValidPrice_UpdatesPrice()
    {
        // Arrange
        var part = new Part { Price = 50m };

        // Act
        part.UpdatePrice(75.50m);

        // Assert
        Assert.That(part.Price, Is.EqualTo(75.50m));
    }

    [Test]
    public void UpdatePrice_ZeroPrice_IsValid()
    {
        // Arrange
        var part = new Part { Price = 50m };

        // Act
        part.UpdatePrice(0m);

        // Assert
        Assert.That(part.Price, Is.EqualTo(0m));
    }

    [Test]
    public void UpdatePrice_NegativePrice_ThrowsArgumentException()
    {
        // Arrange
        var part = new Part { Price = 50m };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => part.UpdatePrice(-10m));
    }

    [Test]
    public void MarkOutOfStock_SetsInStockToFalse()
    {
        // Arrange
        var part = new Part { InStock = true };

        // Act
        part.MarkOutOfStock();

        // Assert
        Assert.That(part.InStock, Is.False);
    }

    [Test]
    public void MarkInStock_SetsInStockToTrue()
    {
        // Arrange
        var part = new Part { InStock = false };

        // Act
        part.MarkInStock();

        // Assert
        Assert.That(part.InStock, Is.True);
    }

    [Test]
    public void AddCompatibleVehicles_AddsSingleVehicle()
    {
        // Arrange
        var part = new Part();
        var vehicles = new[] { "2018-2021 Honda Civic Type R" };

        // Act
        part.AddCompatibleVehicles(vehicles);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.CompatibleVehicles, Has.Count.EqualTo(1));
            Assert.That(part.CompatibleVehicles[0], Is.EqualTo("2018-2021 Honda Civic Type R"));
        });
    }

    [Test]
    public void AddCompatibleVehicles_AddsMultipleVehicles()
    {
        // Arrange
        var part = new Part();
        var vehicles = new[] { "2016-2020 Civic Si", "2017-2021 CR-V", "2018-2022 Accord" };

        // Act
        part.AddCompatibleVehicles(vehicles);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(part.CompatibleVehicles, Has.Count.EqualTo(3));
            Assert.That(part.CompatibleVehicles, Contains.Item("2016-2020 Civic Si"));
            Assert.That(part.CompatibleVehicles, Contains.Item("2017-2021 CR-V"));
        });
    }

    [Test]
    public void Part_WithWarrantyInfo_StoresWarrantyCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            WarrantyInfo = "Limited lifetime warranty"
        };

        // Assert
        Assert.That(part.WarrantyInfo, Is.EqualTo("Limited lifetime warranty"));
    }

    [Test]
    public void Part_WithWeight_StoresWeightCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            Weight = 2.5m
        };

        // Assert
        Assert.That(part.Weight, Is.EqualTo(2.5m));
    }

    [Test]
    public void Part_WithDimensions_StoresDimensionsCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            Dimensions = "12\" x 8\" x 3\""
        };

        // Assert
        Assert.That(part.Dimensions, Is.EqualTo("12\" x 8\" x 3\""));
    }

    [Test]
    public void Part_WithSupplier_StoresSupplierCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            Supplier = "AutoZone"
        };

        // Assert
        Assert.That(part.Supplier, Is.EqualTo("AutoZone"));
    }

    [Test]
    public void Part_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var part = new Part
        {
            Notes = "Free shipping available"
        };

        // Assert
        Assert.That(part.Notes, Is.EqualTo("Free shipping available"));
    }

    [Test]
    public void Part_DefaultInStock_IsTrue()
    {
        // Arrange & Act
        var part = new Part();

        // Assert
        Assert.That(part.InStock, Is.True);
    }

    [Test]
    public void Part_AllCategoriesCanBeAssigned()
    {
        // Arrange & Act & Assert
        var categories = Enum.GetValues<ModCategory>();
        foreach (var category in categories)
        {
            var part = new Part { Category = category };
            Assert.That(part.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void Part_CanToggleStockStatus()
    {
        // Arrange
        var part = new Part { InStock = true };

        // Act
        part.MarkOutOfStock();
        var outOfStock = part.InStock;
        part.MarkInStock();
        var backInStock = part.InStock;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(outOfStock, Is.False);
            Assert.That(backInStock, Is.True);
        });
    }
}
