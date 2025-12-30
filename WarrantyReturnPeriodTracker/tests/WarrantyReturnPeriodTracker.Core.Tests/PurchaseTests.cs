namespace WarrantyReturnPeriodTracker.Core.Tests;

public class PurchaseTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesPurchase()
    {
        // Arrange
        var purchaseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var productName = "Samsung TV";
        var category = ProductCategory.Electronics;
        var storeName = "Best Buy";
        var purchaseDate = new DateTime(2024, 1, 15);
        var price = 1299.99m;
        var modelNumber = "UN65TU8000";

        // Act
        var purchase = new Purchase
        {
            PurchaseId = purchaseId,
            UserId = userId,
            ProductName = productName,
            Category = category,
            StoreName = storeName,
            PurchaseDate = purchaseDate,
            Price = price,
            ModelNumber = modelNumber
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(purchase.UserId, Is.EqualTo(userId));
            Assert.That(purchase.ProductName, Is.EqualTo(productName));
            Assert.That(purchase.Category, Is.EqualTo(category));
            Assert.That(purchase.StoreName, Is.EqualTo(storeName));
            Assert.That(purchase.PurchaseDate, Is.EqualTo(purchaseDate));
            Assert.That(purchase.Price, Is.EqualTo(price));
            Assert.That(purchase.ModelNumber, Is.EqualTo(modelNumber));
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Active));
            Assert.That(purchase.Warranties, Is.Not.Null);
            Assert.That(purchase.ReturnWindows, Is.Not.Null);
            Assert.That(purchase.Receipts, Is.Not.Null);
        });
    }

    [Test]
    public void MarkAsReturned_ValidDate_UpdatesStatusAndNotes()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Laptop",
            Status = PurchaseStatus.Active
        };
        var returnDate = new DateTime(2024, 2, 15);

        // Act
        purchase.MarkAsReturned(returnDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Returned));
            Assert.That(purchase.Notes, Does.Contain("Returned on 2024-02-15"));
        });
    }

    [Test]
    public void MarkAsReturned_ExistingNotes_AppendsReturnInfo()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Laptop",
            Notes = "Original note",
            Status = PurchaseStatus.Active
        };
        var returnDate = new DateTime(2024, 2, 15);

        // Act
        purchase.MarkAsReturned(returnDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Returned));
            Assert.That(purchase.Notes, Does.Contain("Original note"));
            Assert.That(purchase.Notes, Does.Contain("Returned on 2024-02-15"));
        });
    }

    [Test]
    public void MarkAsDisposed_UpdatesStatus()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Old Phone",
            Status = PurchaseStatus.Active
        };

        // Act
        purchase.MarkAsDisposed();

        // Assert
        Assert.That(purchase.Status, Is.EqualTo(PurchaseStatus.Disposed));
    }

    [Test]
    public void HasActiveWarranty_WithActiveWarranty_ReturnsTrue()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Laptop"
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-1),
            EndDate = DateTime.UtcNow.AddMonths(11)
        };

        purchase.Warranties.Add(warranty);

        // Act
        var result = purchase.HasActiveWarranty();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void HasActiveWarranty_NoActiveWarranty_ReturnsFalse()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Laptop"
        };

        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            Status = WarrantyStatus.Expired,
            StartDate = DateTime.UtcNow.AddMonths(-13),
            EndDate = DateTime.UtcNow.AddMonths(-1)
        };

        purchase.Warranties.Add(warranty);

        // Act
        var result = purchase.HasActiveWarranty();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void CanBeReturned_WithOpenReturnWindow_ReturnsTrue()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "TV"
        };

        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(25)
        };

        purchase.ReturnWindows.Add(returnWindow);

        // Act
        var result = purchase.CanBeReturned();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeReturned_NoOpenReturnWindow_ReturnsFalse()
    {
        // Arrange
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "TV"
        };

        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = purchase.PurchaseId,
            Status = ReturnWindowStatus.Expired,
            StartDate = DateTime.UtcNow.AddDays(-35),
            EndDate = DateTime.UtcNow.AddDays(-5)
        };

        purchase.ReturnWindows.Add(returnWindow);

        // Act
        var result = purchase.CanBeReturned();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void DaysSincePurchase_ReturnsCorrectDays()
    {
        // Arrange
        var daysAgo = 30;
        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ProductName = "Phone",
            PurchaseDate = DateTime.UtcNow.AddDays(-daysAgo)
        };

        // Act
        var result = purchase.DaysSincePurchase();

        // Assert
        Assert.That(result, Is.EqualTo(daysAgo));
    }

    [Test]
    public void ProductCategory_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var purchase1 = new Purchase { Category = ProductCategory.Electronics };
            Assert.That(purchase1.Category, Is.EqualTo(ProductCategory.Electronics));

            var purchase2 = new Purchase { Category = ProductCategory.Appliances };
            Assert.That(purchase2.Category, Is.EqualTo(ProductCategory.Appliances));

            var purchase3 = new Purchase { Category = ProductCategory.Furniture };
            Assert.That(purchase3.Category, Is.EqualTo(ProductCategory.Furniture));

            var purchase4 = new Purchase { Category = ProductCategory.Clothing };
            Assert.That(purchase4.Category, Is.EqualTo(ProductCategory.Clothing));

            var purchase5 = new Purchase { Category = ProductCategory.Tools };
            Assert.That(purchase5.Category, Is.EqualTo(ProductCategory.Tools));

            var purchase6 = new Purchase { Category = ProductCategory.Automotive };
            Assert.That(purchase6.Category, Is.EqualTo(ProductCategory.Automotive));

            var purchase7 = new Purchase { Category = ProductCategory.Sports };
            Assert.That(purchase7.Category, Is.EqualTo(ProductCategory.Sports));

            var purchase8 = new Purchase { Category = ProductCategory.Other };
            Assert.That(purchase8.Category, Is.EqualTo(ProductCategory.Other));
        });
    }

    [Test]
    public void PurchaseStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var purchase1 = new Purchase { Status = PurchaseStatus.Active };
            Assert.That(purchase1.Status, Is.EqualTo(PurchaseStatus.Active));

            var purchase2 = new Purchase { Status = PurchaseStatus.Returned };
            Assert.That(purchase2.Status, Is.EqualTo(PurchaseStatus.Returned));

            var purchase3 = new Purchase { Status = PurchaseStatus.Disposed };
            Assert.That(purchase3.Status, Is.EqualTo(PurchaseStatus.Disposed));

            var purchase4 = new Purchase { Status = PurchaseStatus.UnderWarrantyClaim };
            Assert.That(purchase4.Status, Is.EqualTo(PurchaseStatus.UnderWarrantyClaim));
        });
    }
}
