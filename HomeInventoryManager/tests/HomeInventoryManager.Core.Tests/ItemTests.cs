// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class ItemTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesItem()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "MacBook Pro";
        var description = "Laptop computer";
        var category = Category.Electronics;
        var room = Room.Office;
        var brand = "Apple";
        var modelNumber = "MBP2024";
        var serialNumber = "SN12345";
        var purchaseDate = DateTime.UtcNow.AddYears(-1);
        var purchasePrice = 2500m;
        var currentValue = 2000m;
        var quantity = 1;
        var photoUrl = "photo.jpg";
        var receiptUrl = "receipt.pdf";
        var notes = "Great condition";

        // Act
        var item = new Item
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Description = description,
            Category = category,
            Room = room,
            Brand = brand,
            ModelNumber = modelNumber,
            SerialNumber = serialNumber,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            CurrentValue = currentValue,
            Quantity = quantity,
            PhotoUrl = photoUrl,
            ReceiptUrl = receiptUrl,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(item.ItemId, Is.EqualTo(itemId));
            Assert.That(item.UserId, Is.EqualTo(userId));
            Assert.That(item.Name, Is.EqualTo(name));
            Assert.That(item.Description, Is.EqualTo(description));
            Assert.That(item.Category, Is.EqualTo(category));
            Assert.That(item.Room, Is.EqualTo(room));
            Assert.That(item.Brand, Is.EqualTo(brand));
            Assert.That(item.ModelNumber, Is.EqualTo(modelNumber));
            Assert.That(item.SerialNumber, Is.EqualTo(serialNumber));
            Assert.That(item.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(item.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(item.CurrentValue, Is.EqualTo(currentValue));
            Assert.That(item.Quantity, Is.EqualTo(quantity));
            Assert.That(item.PhotoUrl, Is.EqualTo(photoUrl));
            Assert.That(item.ReceiptUrl, Is.EqualTo(receiptUrl));
            Assert.That(item.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void Item_DefaultValues_AreSetCorrectly()
    {
        // Act
        var item = new Item();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(item.Name, Is.EqualTo(string.Empty));
            Assert.That(item.Description, Is.Null);
            Assert.That(item.Category, Is.EqualTo(Category.Electronics));
            Assert.That(item.Room, Is.EqualTo(Room.LivingRoom));
            Assert.That(item.Brand, Is.Null);
            Assert.That(item.ModelNumber, Is.Null);
            Assert.That(item.SerialNumber, Is.Null);
            Assert.That(item.PurchaseDate, Is.Null);
            Assert.That(item.PurchasePrice, Is.Null);
            Assert.That(item.CurrentValue, Is.Null);
            Assert.That(item.Quantity, Is.EqualTo(1));
            Assert.That(item.PhotoUrl, Is.Null);
            Assert.That(item.ReceiptUrl, Is.Null);
            Assert.That(item.Notes, Is.Null);
            Assert.That(item.ValueEstimates, Is.Not.Null);
        });
    }

    [Test]
    public void CalculateDepreciation_WithValidPriceAndValue_ReturnsCorrectPercentage()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            PurchasePrice = 1000m,
            CurrentValue = 750m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.EqualTo(25.0).Within(0.01));
    }

    [Test]
    public void CalculateDepreciation_WithNoDepreciation_ReturnsZero()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            PurchasePrice = 1000m,
            CurrentValue = 1000m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.EqualTo(0.0).Within(0.01));
    }

    [Test]
    public void CalculateDepreciation_WithAppreciation_ReturnsNegative()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Collectible",
            PurchasePrice = 1000m,
            CurrentValue = 1500m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.EqualTo(-50.0).Within(0.01));
    }

    [Test]
    public void CalculateDepreciation_WithoutPurchasePrice_ReturnsNull()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            CurrentValue = 500m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.Null);
    }

    [Test]
    public void CalculateDepreciation_WithoutCurrentValue_ReturnsNull()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Item",
            PurchasePrice = 1000m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.Null);
    }

    [Test]
    public void CalculateDepreciation_WithZeroPurchasePrice_ReturnsNull()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Free Item",
            PurchasePrice = 0m,
            CurrentValue = 100m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.Null);
    }

    [Test]
    public void CalculateDepreciation_WithZeroCurrentValue_Returns100Percent()
    {
        // Arrange
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Worthless Item",
            PurchasePrice = 1000m,
            CurrentValue = 0m
        };

        // Act
        var depreciation = item.CalculateDepreciation();

        // Assert
        Assert.That(depreciation, Is.EqualTo(100.0).Within(0.01));
    }

    [Test]
    public void Item_Quantity_DefaultsToOne()
    {
        // Act
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item"
        };

        // Assert
        Assert.That(item.Quantity, Is.EqualTo(1));
    }

    [Test]
    public void Item_ValueEstimates_InitializesAsEmptyList()
    {
        // Act
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(item.ValueEstimates, Is.Not.Null);
            Assert.That(item.ValueEstimates, Is.Empty);
        });
    }

    [Test]
    public void Item_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(item.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Item_UpdatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var item = new Item
        {
            ItemId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Item"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(item.UpdatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Item_AllCategories_CanBeAssigned()
    {
        // Arrange
        var categories = new[]
        {
            Category.Electronics,
            Category.Furniture,
            Category.Appliances,
            Category.Jewelry,
            Category.Collectibles,
            Category.Tools,
            Category.Clothing,
            Category.Books,
            Category.Sports,
            Category.Other
        };

        // Act & Assert
        foreach (var category in categories)
        {
            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Item",
                Category = category
            };

            Assert.That(item.Category, Is.EqualTo(category));
        }
    }

    [Test]
    public void Item_AllRooms_CanBeAssigned()
    {
        // Arrange
        var rooms = new[]
        {
            Room.LivingRoom,
            Room.Bedroom,
            Room.Kitchen,
            Room.DiningRoom,
            Room.Bathroom,
            Room.Garage,
            Room.Basement,
            Room.Attic,
            Room.Office,
            Room.Storage,
            Room.Other
        };

        // Act & Assert
        foreach (var room in rooms)
        {
            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Item",
                Room = room
            };

            Assert.That(item.Room, Is.EqualTo(room));
        }
    }

    [Test]
    public void Item_AllProperties_CanBeSet()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Complete Item";
        var description = "Full description";
        var category = Category.Furniture;
        var room = Room.Bedroom;
        var brand = "BrandName";
        var modelNumber = "MODEL123";
        var serialNumber = "SERIAL456";
        var purchaseDate = new DateTime(2023, 1, 1);
        var purchasePrice = 3000m;
        var currentValue = 2500m;
        var quantity = 2;
        var photoUrl = "photo.jpg";
        var receiptUrl = "receipt.pdf";
        var notes = "Important notes";
        var createdAt = DateTime.UtcNow.AddDays(-30);
        var updatedAt = DateTime.UtcNow.AddDays(-1);

        // Act
        var item = new Item
        {
            ItemId = itemId,
            UserId = userId,
            Name = name,
            Description = description,
            Category = category,
            Room = room,
            Brand = brand,
            ModelNumber = modelNumber,
            SerialNumber = serialNumber,
            PurchaseDate = purchaseDate,
            PurchasePrice = purchasePrice,
            CurrentValue = currentValue,
            Quantity = quantity,
            PhotoUrl = photoUrl,
            ReceiptUrl = receiptUrl,
            Notes = notes,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(item.ItemId, Is.EqualTo(itemId));
            Assert.That(item.UserId, Is.EqualTo(userId));
            Assert.That(item.Name, Is.EqualTo(name));
            Assert.That(item.Description, Is.EqualTo(description));
            Assert.That(item.Category, Is.EqualTo(category));
            Assert.That(item.Room, Is.EqualTo(room));
            Assert.That(item.Brand, Is.EqualTo(brand));
            Assert.That(item.ModelNumber, Is.EqualTo(modelNumber));
            Assert.That(item.SerialNumber, Is.EqualTo(serialNumber));
            Assert.That(item.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(item.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(item.CurrentValue, Is.EqualTo(currentValue));
            Assert.That(item.Quantity, Is.EqualTo(quantity));
            Assert.That(item.PhotoUrl, Is.EqualTo(photoUrl));
            Assert.That(item.ReceiptUrl, Is.EqualTo(receiptUrl));
            Assert.That(item.Notes, Is.EqualTo(notes));
            Assert.That(item.CreatedAt, Is.EqualTo(createdAt));
            Assert.That(item.UpdatedAt, Is.EqualTo(updatedAt));
        });
    }
}
