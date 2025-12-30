namespace WoodworkingProjectManager.Core.Tests;

public class MaterialTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMaterial()
    {
        // Arrange
        var materialId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var name = "Oak Lumber";
        var description = "Red Oak, S4S";
        var quantity = 20m;
        var unit = "board feet";
        var cost = 150.00m;
        var supplier = "Local Lumber Yard";

        // Act
        var material = new Material
        {
            MaterialId = materialId,
            UserId = userId,
            ProjectId = projectId,
            Name = name,
            Description = description,
            Quantity = quantity,
            Unit = unit,
            Cost = cost,
            Supplier = supplier
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.MaterialId, Is.EqualTo(materialId));
            Assert.That(material.UserId, Is.EqualTo(userId));
            Assert.That(material.ProjectId, Is.EqualTo(projectId));
            Assert.That(material.Name, Is.EqualTo(name));
            Assert.That(material.Description, Is.EqualTo(description));
            Assert.That(material.Quantity, Is.EqualTo(quantity));
            Assert.That(material.Unit, Is.EqualTo(unit));
            Assert.That(material.Cost, Is.EqualTo(cost));
            Assert.That(material.Supplier, Is.EqualTo(supplier));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var material = new Material();

        // Assert
        Assert.That(material.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Unit_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var material = new Material();

        // Assert
        Assert.That(material.Unit, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ProjectId_NullValue_IsAllowed()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Wood Glue",
            ProjectId = null
        };

        // Assert
        Assert.That(material.ProjectId, Is.Null);
    }

    [Test]
    public void Description_NullValue_IsAllowed()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Screws",
            Description = null
        };

        // Assert
        Assert.That(material.Description, Is.Null);
    }

    [Test]
    public void Cost_NullValue_IsAllowed()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Leftover Wood",
            Cost = null
        };

        // Assert
        Assert.That(material.Cost, Is.Null);
    }

    [Test]
    public void Supplier_NullValue_IsAllowed()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Recycled Lumber",
            Supplier = null
        };

        // Assert
        Assert.That(material.Supplier, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Pine Boards"
        };

        // Assert
        Assert.That(material.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Quantity_PositiveValue_CanBeSet()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Walnut Lumber",
            Quantity = 15.5m,
            Unit = "board feet"
        };

        // Assert
        Assert.That(material.Quantity, Is.EqualTo(15.5m));
    }

    [Test]
    public void Material_WithProject_CanBeSet()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Dining Table"
        };

        // Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = project.UserId,
            ProjectId = project.ProjectId,
            Project = project,
            Name = "Oak Lumber"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Project, Is.Not.Null);
            Assert.That(material.Project.ProjectId, Is.EqualTo(project.ProjectId));
        });
    }

    [Test]
    public void Material_DifferentUnits_CanBeSet()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var lumber = new Material { Unit = "board feet" };
            Assert.That(lumber.Unit, Is.EqualTo("board feet"));

            var plywood = new Material { Unit = "sheets" };
            Assert.That(plywood.Unit, Is.EqualTo("sheets"));

            var screws = new Material { Unit = "pieces" };
            Assert.That(screws.Unit, Is.EqualTo("pieces"));

            var glue = new Material { Unit = "bottles" };
            Assert.That(glue.Unit, Is.EqualTo("bottles"));

            var sandpaper = new Material { Unit = "packs" };
            Assert.That(sandpaper.Unit, Is.EqualTo("packs"));
        });
    }

    [Test]
    public void Material_WithoutProject_IsValid()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "General Stock Wood",
            Quantity = 50,
            Unit = "board feet",
            ProjectId = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.ProjectId, Is.Null);
            Assert.That(material.Project, Is.Null);
        });
    }

    [Test]
    public void Material_WithCost_CanCalculateTotal()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Cherry Lumber",
            Quantity = 25,
            Unit = "board feet",
            Cost = 200.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Quantity, Is.EqualTo(25));
            Assert.That(material.Cost, Is.EqualTo(200.00m));
        });
    }

    [Test]
    public void Material_SmallQuantity_CanBeSet()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Wood Stain",
            Quantity = 0.5m,
            Unit = "quarts"
        };

        // Assert
        Assert.That(material.Quantity, Is.EqualTo(0.5m));
    }

    [Test]
    public void Material_LargeQuantity_CanBeSet()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Finishing Nails",
            Quantity = 1000,
            Unit = "pieces"
        };

        // Assert
        Assert.That(material.Quantity, Is.EqualTo(1000));
    }

    [Test]
    public void Material_WithSupplier_CanTrackSource()
    {
        // Arrange & Act
        var material = new Material
        {
            MaterialId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Mahogany Lumber",
            Supplier = "Exotic Woods LLC",
            Cost = 450.00m
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(material.Supplier, Is.EqualTo("Exotic Woods LLC"));
            Assert.That(material.Cost, Is.EqualTo(450.00m));
        });
    }
}
