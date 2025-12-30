// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Api.Tests;

/// <summary>
/// Unit tests for PartDto mapping.
/// </summary>
[TestFixture]
public class PartDtoTests
{
    /// <summary>
    /// Tests that ToDto maps all properties correctly.
    /// </summary>
    [Test]
    public void ToDto_MapsAllProperties()
    {
        // Arrange
        var part = new Part
        {
            PartId = Guid.NewGuid(),
            Name = "High-Flow Air Filter",
            PartNumber = "KN-33-2304",
            Manufacturer = "K&N",
            Description = "Washable and reusable air filter",
            Price = 59.99m,
            Category = ModCategory.Engine,
            CompatibleVehicles = new List<string> { "Honda Civic 2016-2023" },
            WarrantyInfo = "10-year/million-mile limited warranty",
            Weight = 1.2m,
            Dimensions = "14 x 9 x 2 inches",
            InStock = true,
            Supplier = "K&N Engineering",
            Notes = "Direct replacement",
        };

        // Act
        var dto = part.ToDto();

        // Assert
        Assert.That(dto.PartId, Is.EqualTo(part.PartId));
        Assert.That(dto.Name, Is.EqualTo(part.Name));
        Assert.That(dto.PartNumber, Is.EqualTo(part.PartNumber));
        Assert.That(dto.Manufacturer, Is.EqualTo(part.Manufacturer));
        Assert.That(dto.Description, Is.EqualTo(part.Description));
        Assert.That(dto.Price, Is.EqualTo(part.Price));
        Assert.That(dto.Category, Is.EqualTo(part.Category));
        Assert.That(dto.CompatibleVehicles, Is.EqualTo(part.CompatibleVehicles));
        Assert.That(dto.WarrantyInfo, Is.EqualTo(part.WarrantyInfo));
        Assert.That(dto.Weight, Is.EqualTo(part.Weight));
        Assert.That(dto.Dimensions, Is.EqualTo(part.Dimensions));
        Assert.That(dto.InStock, Is.EqualTo(part.InStock));
        Assert.That(dto.Supplier, Is.EqualTo(part.Supplier));
        Assert.That(dto.Notes, Is.EqualTo(part.Notes));
    }
}
