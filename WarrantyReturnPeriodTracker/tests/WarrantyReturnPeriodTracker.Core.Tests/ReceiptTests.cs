namespace WarrantyReturnPeriodTracker.Core.Tests;

public class ReceiptTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReceipt()
    {
        // Arrange
        var receiptId = Guid.NewGuid();
        var purchaseId = Guid.NewGuid();
        var receiptNumber = "REC-12345";
        var receiptType = ReceiptType.Purchase;
        var format = ReceiptFormat.Pdf;
        var storageLocation = "/receipts/2024/receipt.pdf";
        var receiptDate = new DateTime(2024, 1, 15);
        var storeName = "Best Buy";
        var totalAmount = 1299.99m;
        var paymentMethod = PaymentMethod.CreditCard;

        // Act
        var receipt = new Receipt
        {
            ReceiptId = receiptId,
            PurchaseId = purchaseId,
            ReceiptNumber = receiptNumber,
            ReceiptType = receiptType,
            Format = format,
            StorageLocation = storageLocation,
            ReceiptDate = receiptDate,
            StoreName = storeName,
            TotalAmount = totalAmount,
            PaymentMethod = paymentMethod
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.ReceiptId, Is.EqualTo(receiptId));
            Assert.That(receipt.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(receipt.ReceiptNumber, Is.EqualTo(receiptNumber));
            Assert.That(receipt.ReceiptType, Is.EqualTo(receiptType));
            Assert.That(receipt.Format, Is.EqualTo(format));
            Assert.That(receipt.StorageLocation, Is.EqualTo(storageLocation));
            Assert.That(receipt.ReceiptDate, Is.EqualTo(receiptDate));
            Assert.That(receipt.StoreName, Is.EqualTo(storeName));
            Assert.That(receipt.TotalAmount, Is.EqualTo(totalAmount));
            Assert.That(receipt.PaymentMethod, Is.EqualTo(paymentMethod));
            Assert.That(receipt.Status, Is.EqualTo(ReceiptStatus.Active));
            Assert.That(receipt.IsVerified, Is.False);
        });
    }

    [Test]
    public void Verify_SetsIsVerifiedToTrue()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            IsVerified = false
        };

        // Act
        receipt.Verify();

        // Assert
        Assert.That(receipt.IsVerified, Is.True);
    }

    [Test]
    public void Archive_UpdatesStatus()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Active
        };

        // Act
        receipt.Archive();

        // Assert
        Assert.That(receipt.Status, Is.EqualTo(ReceiptStatus.Archived));
    }

    [Test]
    public void MarkAsLost_UpdatesStatus()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Active
        };

        // Act
        receipt.MarkAsLost();

        // Assert
        Assert.That(receipt.Status, Is.EqualTo(ReceiptStatus.Lost));
    }

    [Test]
    public void MarkAsInvalid_ValidReason_UpdatesStatusAndNotes()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Active
        };
        var reason = "Receipt is forged";

        // Act
        receipt.MarkAsInvalid(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Status, Is.EqualTo(ReceiptStatus.Invalid));
            Assert.That(receipt.Notes, Does.Contain("Invalid: Receipt is forged"));
        });
    }

    [Test]
    public void MarkAsInvalid_ExistingNotes_AppendsInvalidReason()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Notes = "Original note",
            Status = ReceiptStatus.Active
        };
        var reason = "Duplicate receipt";

        // Act
        receipt.MarkAsInvalid(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(receipt.Status, Is.EqualTo(ReceiptStatus.Invalid));
            Assert.That(receipt.Notes, Does.Contain("Original note"));
            Assert.That(receipt.Notes, Does.Contain("Invalid: Duplicate receipt"));
        });
    }

    [Test]
    public void UpdateStorageLocation_ValidLocation_UpdatesStorageLocation()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            StorageLocation = "/old/location.pdf"
        };
        var newLocation = "/new/location.pdf";

        // Act
        receipt.UpdateStorageLocation(newLocation);

        // Assert
        Assert.That(receipt.StorageLocation, Is.EqualTo(newLocation));
    }

    [Test]
    public void IsAccessible_ActiveStatusWithStorageLocation_ReturnsTrue()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Active,
            StorageLocation = "/receipts/receipt.pdf"
        };

        // Act
        var result = receipt.IsAccessible();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsAccessible_ActiveStatusNoStorageLocation_ReturnsFalse()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Active,
            StorageLocation = null
        };

        // Act
        var result = receipt.IsAccessible();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsAccessible_ArchivedStatus_ReturnsFalse()
    {
        // Arrange
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            ReceiptNumber = "REC-001",
            Status = ReceiptStatus.Archived,
            StorageLocation = "/receipts/receipt.pdf"
        };

        // Act
        var result = receipt.IsAccessible();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void ReceiptType_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var receipt1 = new Receipt { ReceiptType = ReceiptType.Purchase };
            Assert.That(receipt1.ReceiptType, Is.EqualTo(ReceiptType.Purchase));

            var receipt2 = new Receipt { ReceiptType = ReceiptType.Return };
            Assert.That(receipt2.ReceiptType, Is.EqualTo(ReceiptType.Return));

            var receipt3 = new Receipt { ReceiptType = ReceiptType.Exchange };
            Assert.That(receipt3.ReceiptType, Is.EqualTo(ReceiptType.Exchange));

            var receipt4 = new Receipt { ReceiptType = ReceiptType.WarrantyRegistration };
            Assert.That(receipt4.ReceiptType, Is.EqualTo(ReceiptType.WarrantyRegistration));

            var receipt5 = new Receipt { ReceiptType = ReceiptType.Refund };
            Assert.That(receipt5.ReceiptType, Is.EqualTo(ReceiptType.Refund));
        });
    }

    [Test]
    public void ReceiptFormat_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var receipt1 = new Receipt { Format = ReceiptFormat.Paper };
            Assert.That(receipt1.Format, Is.EqualTo(ReceiptFormat.Paper));

            var receipt2 = new Receipt { Format = ReceiptFormat.Pdf };
            Assert.That(receipt2.Format, Is.EqualTo(ReceiptFormat.Pdf));

            var receipt3 = new Receipt { Format = ReceiptFormat.Image };
            Assert.That(receipt3.Format, Is.EqualTo(ReceiptFormat.Image));

            var receipt4 = new Receipt { Format = ReceiptFormat.Email };
            Assert.That(receipt4.Format, Is.EqualTo(ReceiptFormat.Email));

            var receipt5 = new Receipt { Format = ReceiptFormat.Digital };
            Assert.That(receipt5.Format, Is.EqualTo(ReceiptFormat.Digital));
        });
    }

    [Test]
    public void PaymentMethod_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var receipt1 = new Receipt { PaymentMethod = PaymentMethod.Cash };
            Assert.That(receipt1.PaymentMethod, Is.EqualTo(PaymentMethod.Cash));

            var receipt2 = new Receipt { PaymentMethod = PaymentMethod.CreditCard };
            Assert.That(receipt2.PaymentMethod, Is.EqualTo(PaymentMethod.CreditCard));

            var receipt3 = new Receipt { PaymentMethod = PaymentMethod.DebitCard };
            Assert.That(receipt3.PaymentMethod, Is.EqualTo(PaymentMethod.DebitCard));

            var receipt4 = new Receipt { PaymentMethod = PaymentMethod.PayPal };
            Assert.That(receipt4.PaymentMethod, Is.EqualTo(PaymentMethod.PayPal));

            var receipt5 = new Receipt { PaymentMethod = PaymentMethod.BankTransfer };
            Assert.That(receipt5.PaymentMethod, Is.EqualTo(PaymentMethod.BankTransfer));

            var receipt6 = new Receipt { PaymentMethod = PaymentMethod.DigitalWallet };
            Assert.That(receipt6.PaymentMethod, Is.EqualTo(PaymentMethod.DigitalWallet));

            var receipt7 = new Receipt { PaymentMethod = PaymentMethod.Check };
            Assert.That(receipt7.PaymentMethod, Is.EqualTo(PaymentMethod.Check));

            var receipt8 = new Receipt { PaymentMethod = PaymentMethod.Other };
            Assert.That(receipt8.PaymentMethod, Is.EqualTo(PaymentMethod.Other));
        });
    }

    [Test]
    public void ReceiptStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var receipt1 = new Receipt { Status = ReceiptStatus.Active };
            Assert.That(receipt1.Status, Is.EqualTo(ReceiptStatus.Active));

            var receipt2 = new Receipt { Status = ReceiptStatus.Archived };
            Assert.That(receipt2.Status, Is.EqualTo(ReceiptStatus.Archived));

            var receipt3 = new Receipt { Status = ReceiptStatus.Lost };
            Assert.That(receipt3.Status, Is.EqualTo(ReceiptStatus.Lost));

            var receipt4 = new Receipt { Status = ReceiptStatus.Invalid };
            Assert.That(receipt4.Status, Is.EqualTo(ReceiptStatus.Invalid));
        });
    }
}
