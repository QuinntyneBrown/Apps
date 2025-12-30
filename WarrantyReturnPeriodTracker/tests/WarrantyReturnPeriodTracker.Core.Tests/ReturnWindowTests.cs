namespace WarrantyReturnPeriodTracker.Core.Tests;

public class ReturnWindowTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReturnWindow()
    {
        // Arrange
        var returnWindowId = Guid.NewGuid();
        var purchaseId = Guid.NewGuid();
        var startDate = new DateTime(2024, 1, 15);
        var endDate = new DateTime(2024, 2, 14);
        var durationDays = 30;
        var policyDetails = "Full refund within 30 days";
        var restockingFeePercent = 10m;

        // Act
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = returnWindowId,
            PurchaseId = purchaseId,
            StartDate = startDate,
            EndDate = endDate,
            DurationDays = durationDays,
            PolicyDetails = policyDetails,
            RestockingFeePercent = restockingFeePercent
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(returnWindow.ReturnWindowId, Is.EqualTo(returnWindowId));
            Assert.That(returnWindow.PurchaseId, Is.EqualTo(purchaseId));
            Assert.That(returnWindow.StartDate, Is.EqualTo(startDate));
            Assert.That(returnWindow.EndDate, Is.EqualTo(endDate));
            Assert.That(returnWindow.DurationDays, Is.EqualTo(durationDays));
            Assert.That(returnWindow.PolicyDetails, Is.EqualTo(policyDetails));
            Assert.That(returnWindow.RestockingFeePercent, Is.EqualTo(restockingFeePercent));
            Assert.That(returnWindow.Status, Is.EqualTo(ReturnWindowStatus.Open));
        });
    }

    [Test]
    public void IsOpen_OpenStatusWithinPeriod_ReturnsTrue()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(25)
        };

        // Act
        var result = returnWindow.IsOpen();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsOpen_OpenStatusBeforeStartDate_ReturnsFalse()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(5),
            EndDate = DateTime.UtcNow.AddDays(35)
        };

        // Act
        var result = returnWindow.IsOpen();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOpen_OpenStatusAfterEndDate_ReturnsFalse()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-35),
            EndDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var result = returnWindow.IsOpen();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsOpen_ExpiredStatus_ReturnsFalse()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Expired,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(25)
        };

        // Act
        var result = returnWindow.IsOpen();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsClosingSoon_OpenAndClosingWithin7Days_ReturnsTrue()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-23),
            EndDate = DateTime.UtcNow.AddDays(5)
        };

        // Act
        var result = returnWindow.IsClosingSoon();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsClosingSoon_OpenButNotClosingSoon_ReturnsFalse()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-5),
            EndDate = DateTime.UtcNow.AddDays(20)
        };

        // Act
        var result = returnWindow.IsClosingSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsClosingSoon_NotOpen_ReturnsFalse()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Expired,
            StartDate = DateTime.UtcNow.AddDays(-35),
            EndDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var result = returnWindow.IsClosingSoon();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetDaysRemaining_OpenWindow_ReturnsCorrectDays()
    {
        // Arrange
        var daysRemaining = 15;
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            StartDate = DateTime.UtcNow.AddDays(-15),
            EndDate = DateTime.UtcNow.AddDays(daysRemaining)
        };

        // Act
        var result = returnWindow.GetDaysRemaining();

        // Assert
        Assert.That(result, Is.EqualTo(daysRemaining));
    }

    [Test]
    public void GetDaysRemaining_ClosedWindow_ReturnsZero()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Expired,
            StartDate = DateTime.UtcNow.AddDays(-35),
            EndDate = DateTime.UtcNow.AddDays(-5)
        };

        // Act
        var result = returnWindow.GetDaysRemaining();

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void MarkAsUsed_UpdatesStatus()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open
        };

        // Act
        returnWindow.MarkAsUsed();

        // Assert
        Assert.That(returnWindow.Status, Is.EqualTo(ReturnWindowStatus.Used));
    }

    [Test]
    public void MarkAsExpired_UpdatesStatus()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open
        };

        // Act
        returnWindow.MarkAsExpired();

        // Assert
        Assert.That(returnWindow.Status, Is.EqualTo(ReturnWindowStatus.Expired));
    }

    [Test]
    public void VoidWindow_ValidReason_UpdatesStatusAndNotes()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open
        };
        var reason = "Policy changed";

        // Act
        returnWindow.VoidWindow(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(returnWindow.Status, Is.EqualTo(ReturnWindowStatus.Voided));
            Assert.That(returnWindow.Notes, Does.Contain("Voided: Policy changed"));
        });
    }

    [Test]
    public void VoidWindow_ExistingNotes_AppendsVoidReason()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            Status = ReturnWindowStatus.Open,
            Notes = "Original note"
        };
        var reason = "Policy changed";

        // Act
        returnWindow.VoidWindow(reason);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(returnWindow.Status, Is.EqualTo(ReturnWindowStatus.Voided));
            Assert.That(returnWindow.Notes, Does.Contain("Original note"));
            Assert.That(returnWindow.Notes, Does.Contain("Voided: Policy changed"));
        });
    }

    [Test]
    public void CalculateRestockingFee_WithRestockingFeePercent_ReturnsCorrectAmount()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            RestockingFeePercent = 15m
        };
        var purchasePrice = 1000m;

        // Act
        var result = returnWindow.CalculateRestockingFee(purchasePrice);

        // Assert
        Assert.That(result, Is.EqualTo(150m));
    }

    [Test]
    public void CalculateRestockingFee_NoRestockingFeePercent_ReturnsZero()
    {
        // Arrange
        var returnWindow = new ReturnWindow
        {
            ReturnWindowId = Guid.NewGuid(),
            PurchaseId = Guid.NewGuid(),
            RestockingFeePercent = null
        };
        var purchasePrice = 1000m;

        // Act
        var result = returnWindow.CalculateRestockingFee(purchasePrice);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void ReturnWindowStatus_AllValues_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var window1 = new ReturnWindow { Status = ReturnWindowStatus.Open };
            Assert.That(window1.Status, Is.EqualTo(ReturnWindowStatus.Open));

            var window2 = new ReturnWindow { Status = ReturnWindowStatus.Used };
            Assert.That(window2.Status, Is.EqualTo(ReturnWindowStatus.Used));

            var window3 = new ReturnWindow { Status = ReturnWindowStatus.Expired };
            Assert.That(window3.Status, Is.EqualTo(ReturnWindowStatus.Expired));

            var window4 = new ReturnWindow { Status = ReturnWindowStatus.Voided };
            Assert.That(window4.Status, Is.EqualTo(ReturnWindowStatus.Voided));
        });
    }
}
