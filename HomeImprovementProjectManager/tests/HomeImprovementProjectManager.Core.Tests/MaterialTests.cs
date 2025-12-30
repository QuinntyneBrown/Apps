// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeImprovementProjectManager.Core.Tests;

public class MaterialTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMaterial()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "2x4 Lumber";
        var quantity = 100;
        var unit = "pieces";
        var unitCost = 8.50m;
        var totalCost = 850m;
        var supplier = "Home Depot";

        // Act
        var material = new Material
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name,
            Quantity = quantity,
            Unit = unit,
            UnitCost = unitCost,
            TotalCost = totalCost,
            Supplier = supplier
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.MaterialId, Is.EqualTo(materialId));
            Assert.That(material.ProjectId, Is.EqualTo(projectId));
            Assert.That(material.Name, Is.EqualTo(name));
            Assert.That(material.Quantity, Is.EqualTo(quantity));
            Assert.That(material.Unit, Is.EqualTo(unit));
            Assert.That(material.UnitCost, Is.EqualTo(unitCost));
            Assert.That(material.TotalCost, Is.EqualTo(totalCost));
            Assert.That(material.Supplier, Is.EqualTo(supplier));
        });
    }

    [Test]
    public void Material_DefaultValues_AreSetCorrectly()
    {
        // Act
        var material = new Material();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Name, Is.EqualTo(string.Empty));
            Assert.That(material.Quantity, Is.EqualTo(0));
            Assert.That(material.Unit, Is.Null);
            Assert.That(material.UnitCost, Is.Null);
            Assert.That(material.TotalCost, Is.Null);
            Assert.That(material.Supplier, Is.Null);
            Assert.That(material.Project, Is.Null);
            Assert.That(material.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Material_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Simple Material",
            Quantity = 50
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Unit, Is.Null);
            Assert.That(material.UnitCost, Is.Null);
            Assert.That(material.TotalCost, Is.Null);
            Assert.That(material.Supplier, Is.Null);
        });
    }

    [Test]
    public void Material_WithZeroQuantity_IsValid()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material",
            Quantity = 0
        };

        // Assert
        Assert.That(material.Quantity, Is.EqualTo(0));
    }

    [Test]
    public void Material_WithVariousUnits_IsValid()
    {
        // Arrange
        var units = new[] { "pieces", "feet", "yards", "gallons", "pounds" };

        // Act & Assert
        foreach (var unit in units)
        {
            var material = new Material
            {
                MaterialId = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                Name = "Material",
                Quantity = 10,
                Unit = unit
            };

            Assert.That(material.Unit, Is.EqualTo(unit));
        }
    }

    [Test]
    public void Material_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Test Material",
            Quantity = 1
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(material.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Material_WithUnitCost_CanCalculateTotalCost()
    {
        // Arrange
        var quantity = 100;
        var unitCost = 5.50m;
        var expectedTotalCost = 550m;

        // Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material",
            Quantity = quantity,
            UnitCost = unitCost,
            TotalCost = expectedTotalCost
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Quantity, Is.EqualTo(quantity));
            Assert.That(material.UnitCost, Is.EqualTo(unitCost));
            Assert.That(material.TotalCost, Is.EqualTo(expectedTotalCost));
        });
    }

    [Test]
    public void Material_WithSupplier_IsValid()
    {
        // Arrange
        var supplier = "Lowe's";

        // Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Material",
            Quantity = 25,
            Supplier = supplier
        };

        // Assert
        Assert.That(material.Supplier, Is.EqualTo(supplier));
    }

    [Test]
    public void Material_WithLargeQuantity_IsValid()
    {
        // Arrange
        var largeQuantity = 10000;

        // Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Bulk Material",
            Quantity = largeQuantity
        };

        // Assert
        Assert.That(material.Quantity, Is.EqualTo(largeQuantity));
    }

    [Test]
    public void Material_AllProperties_CanBeSet()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "Complete Material";
        var quantity = 200;
        var unit = "sq ft";
        var unitCost = 12.75m;
        var totalCost = 2550m;
        var supplier = "Local Supplier";
        var createdAt = DateTime.UtcNow.AddDays(-5);

        // Act
        var material = new Material
        {
            MaterialId = materialId,
            ProjectId = projectId,
            Name = name,
            Quantity = quantity,
            Unit = unit,
            UnitCost = unitCost,
            TotalCost = totalCost,
            Supplier = supplier,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.MaterialId, Is.EqualTo(materialId));
            Assert.That(material.ProjectId, Is.EqualTo(projectId));
            Assert.That(material.Name, Is.EqualTo(name));
            Assert.That(material.Quantity, Is.EqualTo(quantity));
            Assert.That(material.Unit, Is.EqualTo(unit));
            Assert.That(material.UnitCost, Is.EqualTo(unitCost));
            Assert.That(material.TotalCost, Is.EqualTo(totalCost));
            Assert.That(material.Supplier, Is.EqualTo(supplier));
            Assert.That(material.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
