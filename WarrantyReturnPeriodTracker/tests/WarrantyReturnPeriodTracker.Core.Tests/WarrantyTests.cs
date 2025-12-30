namespace WarrantyReturnPeriodTracker.Core.Tests;

public class WarrantyTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWarranty()
    {
        // Arrange
        var warrantyId = Guid.NewGuid();
        var purchaseId = Guid.NewGuid();
        var warrantyType = WarrantyType.Manufacturer;
        var provider = "Samsung";
        var startDate = new DateTime(2024, 1, 15);
        var endDate = new DateTime(2025, 1, 15);
        var durationMonths = 12;
        var coverageDetails = "Parts and labor";
        var registrationNumber = "WRNTY-12345";

        // Act
        var warranty = new Warranty
        {
            WarrantyId = warrantyId,
            PurchaseId = purchaseId,
            WarrantyType = warrantyType,
            Provider = provider,
            StartDate = startDate,
            EndDate = endDate,
            DurationMonths = durationMonths,
            CoverageDetails = coverageDetails,
            RegistrationNumber = registrationNumber
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.WarrantyId, Is.EqualTo(warrantyId));
            Assert.That(warranty.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(warranty.WarrantyType, Is.EqualTo(warrantyType));
            Assert.That(warranty.Provider, Is.EqualTo(provider));
            Assert.That(warranty.StartDate, Is.EqualTo(startDate));
            Assert.That(warranty.EndDate, Is.EqualTo(endDate));
            Assert.That(warranty.DurationMonths, Is.EqualTo(durationMonths));
            Assert.That(warranty.CoverageDetails, Is.EqualTo(coverageDetails));
            Assert.That(warranty.RegistrationNumber, Is.EqualTo(registrationNumber));
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.Active));
            Assert.That(warranty.ClaimFiledDate, Is.Null);
        });
    }

    [Test]
    public void IsActive_ActiveStatusNotExpired_ReturnsTrue()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow.AddMonths(6)
        };

        // Act
        var result = warranty.IsActive();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsActive_ActiveStatusButExpired_ReturnsFalse()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-13),
            EndDate = DateTime.UtcNow.AddMonths(-1)
        };

        // Act
        var result = warranty.IsActive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsActive_ExpiredStatus_ReturnsFalse()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Expired,
            StartDate = DateTime.UtcNow.AddMonths(-13),
            EndDate = DateTime.UtcNow.AddMonths(-1)
        };

        // Act
        var result = warranty.IsActive();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsExpiringSoon_ActiveAndExpiresWithin30Days_ReturnsTrue()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-11),
            EndDate = DateTime.UtcNow.AddDays(15)
        };

        // Act
        var result = warranty.IsExpiringSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsExpiringSoon_ActiveButNotExpiringSoon_ReturnsFalse()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-6),
            EndDate = DateTime.UtcNow.AddMonths(6)
        };

        // Act
        var result = warranty.IsExpiringSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsExpiringSoon_NotActive_ReturnsFalse()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Expired,
            StartDate = DateTime.UtcNow.AddMonths(-13),
            EndDate = DateTime.UtcNow.AddMonths(-1)
        };

        // Act
        var result = warranty.IsExpiringSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetDaysRemaining_NotExpired_ReturnsCorrectDays()
    {
        // Arrange
        var daysRemaining = 90;
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            StartDate = DateTime.UtcNow.AddMonths(-9),
            EndDate = DateTime.UtcNow.AddDays(daysRemaining)
        };

        // Act
        var result = warranty.GetDaysRemaining();

        // Assert
        Assert.That(result, Is.EqualTo(daysRemaining));
    }

    [Test]
    public void GetDaysRemaining_Expired_ReturnsZero()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Expired,
            StartDate = DateTime.UtcNow.AddMonths(-13),
            EndDate = DateTime.UtcNow.AddMonths(-1)
        };

        // Act
        var result = warranty.GetDaysRemaining();

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void FileClaim_UpdatesStatusAndSetsClaimDate()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active
        };

        // Act
        warranty.FileClaim();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.ClaimFiled));
            Assert.That(warranty.ClaimFiledDate, Is.Not.Null);
            Assert.That(warranty.ClaimFiledDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ApproveClaim_UpdatesStatus()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.ClaimFiled
        };

        // Act
        warranty.ApproveClaim();

        // Assert
        Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.ClaimApproved));
    }

    [Test]
    public void RejectClaim_ValidReason_UpdatesStatusAndNotes()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.ClaimFiled
        };
        var reason = "Not covered under warranty";

        // Act
        warranty.RejectClaim(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.ClaimRejected));
            Assert.That(warranty.Notes, Does.Contain("Claim rejected: Not covered under warranty"));
        });
    }

    [Test]
    public void RejectClaim_ExistingNotes_AppendsRejectionReason()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.ClaimFiled,
            Notes = "Original note"
        };
        var reason = "User error";

        // Act
        warranty.RejectClaim(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.ClaimRejected));
            Assert.That(warranty.Notes, Does.Contain("Original note"));
            Assert.That(warranty.Notes, Does.Contain("Claim rejected: User error"));
        });
    }

    [Test]
    public void MarkAsExpired_UpdatesStatus()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active
        };

        // Act
        warranty.MarkAsExpired();

        // Assert
        Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.Expired));
    }

    [Test]
    public void VoidWarranty_ValidReason_UpdatesStatusAndNotes()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active
        };
        var reason = "Product returned";

        // Act
        warranty.VoidWarranty(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.Voided));
            Assert.That(warranty.Notes, Does.Contain("Voided: Product returned"));
        });
    }

    [Test]
    public void VoidWarranty_ExistingNotes_AppendsVoidReason()
    {
        // Arrange
        var warranty = new Warranty
        {
            WarrantyId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = WarrantyStatus.Active,
            Notes = "Original note"
        };
        var reason = "Product returned";

        // Act
        warranty.VoidWarranty(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(warranty.Status, Is.EqualTo(WarrantyStatus.Voided));
            Assert.That(warranty.Notes, Does.Contain("Original note"));
            Assert.That(warranty.Notes, Does.Contain("Voided: Product returned"));
        });
    }

    [Test]
    public void WarrantyType_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var warranty1 = new Warranty { WarrantyType = WarrantyType.Manufacturer };
            Assert.That(warranty1.WarrantyType, Is.EqualTo(WarrantyType.Manufacturer));

            var warranty2 = new Warranty { WarrantyType = WarrantyType.Extended };
            Assert.That(warranty2.WarrantyType, Is.EqualTo(WarrantyType.Extended));

            var warranty3 = new Warranty { WarrantyType = WarrantyType.Store };
            Assert.That(warranty3.WarrantyType, Is.EqualTo(WarrantyType.Store));

            var warranty4 = new Warranty { WarrantyType = WarrantyType.ThirdParty };
            Assert.That(warranty4.WarrantyType, Is.EqualTo(WarrantyType.ThirdParty));

            var warranty5 = new Warranty { WarrantyType = WarrantyType.Limited };
            Assert.That(warranty5.WarrantyType, Is.EqualTo(WarrantyType.Limited));

            var warranty6 = new Warranty { WarrantyType = WarrantyType.Lifetime };
            Assert.That(warranty6.WarrantyType, Is.EqualTo(WarrantyType.Lifetime));
        });
    }

    [Test]
    public void WarrantyStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var warranty1 = new Warranty { Status = WarrantyStatus.Active };
            Assert.That(warranty1.Status, Is.EqualTo(WarrantyStatus.Active));

            var warranty2 = new Warranty { Status = WarrantyStatus.Expired };
            Assert.That(warranty2.Status, Is.EqualTo(WarrantyStatus.Expired));

            var warranty3 = new Warranty { Status = WarrantyStatus.ClaimFiled };
            Assert.That(warranty3.Status, Is.EqualTo(WarrantyStatus.ClaimFiled));

            var warranty4 = new Warranty { Status = WarrantyStatus.ClaimApproved };
            Assert.That(warranty4.Status, Is.EqualTo(WarrantyStatus.ClaimApproved));

            var warranty5 = new Warranty { Status = WarrantyStatus.ClaimRejected };
            Assert.That(warranty5.Status, Is.EqualTo(WarrantyStatus.ClaimRejected));

            var warranty6 = new Warranty { Status = WarrantyStatus.Voided };
            Assert.That(warranty6.Status, Is.EqualTo(WarrantyStatus.Voided));
        });
    }
}
