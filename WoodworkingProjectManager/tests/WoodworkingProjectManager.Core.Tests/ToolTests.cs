namespace WoodworkingProjectManager.Core.Tests;

public class ToolTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTool()
    {
        // Arrange
        var toolId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Table Saw";
        var brand = "DeWalt";
        var model = "DWE7491RS";
        var description = "10-inch jobsite table saw with rolling stand";
        var purchasePrice = 599.00m;
        var purchaseDate = new DateTime(2023, 6, 15);
        var location = "Workshop - Main Bench";
        var notes = "Excellent condition, blade needs sharpening";

        // Act
        var tool = new Tool
        {
            ToolId = toolId,
            UserId = userId,
            Name = name,
            Brand = brand,
            Model = model,
            Description = description,
            PurchasePrice = purchasePrice,
            PurchaseDate = purchaseDate,
            Location = location,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tool.ToolId, Is.EqualTo(toolId));
            Assert.That(tool.UserId, Is.EqualTo(userId));
            Assert.That(tool.Name, Is.EqualTo(name));
            Assert.That(tool.Brand, Is.EqualTo(brand));
            Assert.That(tool.Model, Is.EqualTo(model));
            Assert.That(tool.Description, Is.EqualTo(description));
            Assert.That(tool.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(tool.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(tool.Location, Is.EqualTo(location));
            Assert.That(tool.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var tool = new Tool();

        // Assert
        Assert.That(tool.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Brand_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Hand Plane",
            Brand = null
        };

        // Assert
        Assert.That(tool.Brand, Is.Null);
    }

    [Test]
    public void Model_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Chisel Set",
            Model = null
        };

        // Assert
        Assert.That(tool.Model, Is.Null);
    }

    [Test]
    public void Description_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Hammer",
            Description = null
        };

        // Assert
        Assert.That(tool.Description, Is.Null);
    }

    [Test]
    public void PurchasePrice_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Inherited Miter Saw",
            PurchasePrice = null
        };

        // Assert
        Assert.That(tool.PurchasePrice, Is.Null);
    }

    [Test]
    public void PurchaseDate_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Old Clamps",
            PurchaseDate = null
        };

        // Assert
        Assert.That(tool.PurchaseDate, Is.Null);
    }

    [Test]
    public void Location_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Portable Drill",
            Location = null
        };

        // Assert
        Assert.That(tool.Location, Is.Null);
    }

    [Test]
    public void Notes_NullValue_IsAllowed()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Router",
            Notes = null
        };

        // Assert
        Assert.That(tool.Notes, Is.Null);
    }

    [Test]
    public void CreatedAt_DefaultValue_IsSetToUtcNow()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Circular Saw"
        };

        // Assert
        Assert.That(tool.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Tool_PowerTools_CanBeSet()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var tablesaw = new Tool { Name = "Table Saw", Brand = "DeWalt" };
            Assert.That(tablesaw.Name, Is.EqualTo("Table Saw"));

            var mitersaw = new Tool { Name = "Miter Saw", Brand = "Makita" };
            Assert.That(mitersaw.Name, Is.EqualTo("Miter Saw"));

            var router = new Tool { Name = "Router", Brand = "Bosch" };
            Assert.That(router.Name, Is.EqualTo("Router"));

            var drill = new Tool { Name = "Cordless Drill", Brand = "Milwaukee" };
            Assert.That(drill.Name, Is.EqualTo("Cordless Drill"));
        });
    }

    [Test]
    public void Tool_HandTools_CanBeSet()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var handplane = new Tool { Name = "Hand Plane" };
            Assert.That(handplane.Name, Is.EqualTo("Hand Plane"));

            var chisel = new Tool { Name = "Chisel Set" };
            Assert.That(chisel.Name, Is.EqualTo("Chisel Set"));

            var square = new Tool { Name = "Combination Square" };
            Assert.That(square.Name, Is.EqualTo("Combination Square"));

            var clamps = new Tool { Name = "Bar Clamps" };
            Assert.That(clamps.Name, Is.EqualTo("Bar Clamps"));
        });
    }

    [Test]
    public void Tool_WithLocation_CanBeTracked()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Jointer",
            Location = "Workshop - North Wall"
        };

        // Assert
        Assert.That(tool.Location, Is.EqualTo("Workshop - North Wall"));
    }

    [Test]
    public void Tool_ExpensiveTool_CanTrackPrice()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Cabinet Table Saw",
            Brand = "SawStop",
            PurchasePrice = 3299.00m,
            PurchaseDate = new DateTime(2024, 1, 15)
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tool.PurchasePrice, Is.EqualTo(3299.00m));
            Assert.That(tool.PurchaseDate, Is.EqualTo(new DateTime(2024, 1, 15)));
        });
    }

    [Test]
    public void Tool_InexpensiveTool_CanTrackPrice()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Wood Glue",
            PurchasePrice = 8.99m
        };

        // Assert
        Assert.That(tool.PurchasePrice, Is.EqualTo(8.99m));
    }

    [Test]
    public void Tool_WithDetailedNotes_CanTrackMaintenance()
    {
        // Arrange
        var notes = "Last blade change: 2024-01-10. " +
                   "Due for calibration check. " +
                   "Fence needs adjustment.";

        // Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Band Saw",
            Notes = notes
        };

        // Assert
        Assert.That(tool.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Tool_WithBrandAndModel_CanBeIdentified()
    {
        // Arrange & Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Random Orbital Sander",
            Brand = "Festool",
            Model = "ETS 150/3 EQ"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tool.Brand, Is.EqualTo("Festool"));
            Assert.That(tool.Model, Is.EqualTo("ETS 150/3 EQ"));
        });
    }

    [Test]
    public void Tool_OldPurchaseDate_CanBeSet()
    {
        // Arrange
        var oldDate = new DateTime(2010, 3, 20);

        // Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Vintage Hand Plane",
            PurchaseDate = oldDate
        };

        // Assert
        Assert.That(tool.PurchaseDate, Is.EqualTo(oldDate));
    }

    [Test]
    public void Tool_WithDescription_ProvidesDetails()
    {
        // Arrange
        var description = "Professional grade 12-inch planer with helical cutterhead. " +
                         "Three knife design for smooth finish. Dust collection port included.";

        // Act
        var tool = new Tool
        {
            ToolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Thickness Planer",
            Description = description
        };

        // Assert
        Assert.That(tool.Description, Is.EqualTo(description));
    }
}
