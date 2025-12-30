// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class UtilityBillTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesUtilityBill()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var utilityType = UtilityType.Electricity;
        var billingDate = DateTime.UtcNow;
        var amount = 150.50m;
        var usageAmount = 500m;
        var unit = "kWh";

        // Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            BillingDate = billingDate,
            Amount = amount,
            UsageAmount = usageAmount,
            Unit = unit
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(utilityBill.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(utilityBill.UserId, Is.EqualTo(userId));
            Assert.That(utilityBill.UtilityType, Is.EqualTo(utilityType));
            Assert.That(utilityBill.BillingDate, Is.EqualTo(billingDate));
            Assert.That(utilityBill.Amount, Is.EqualTo(amount));
            Assert.That(utilityBill.UsageAmount, Is.EqualTo(usageAmount));
            Assert.That(utilityBill.Unit, Is.EqualTo(unit));
        });
    }

    [Test]
    public void UtilityBill_DefaultValues_AreSetCorrectly()
    {
        // Act
        var utilityBill = new UtilityBill();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(utilityBill.UtilityBillId, Is.EqualTo(Guid.Empty));
            Assert.That(utilityBill.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(utilityBill.UtilityType, Is.EqualTo(UtilityType.Electricity));
            Assert.That(utilityBill.BillingDate, Is.EqualTo(default(DateTime)));
            Assert.That(utilityBill.Amount, Is.EqualTo(0m));
            Assert.That(utilityBill.UsageAmount, Is.Null);
            Assert.That(utilityBill.Unit, Is.Null);
            Assert.That(utilityBill.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UtilityBill_ElectricityType_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Electricity,
            BillingDate = DateTime.UtcNow,
            Amount = 100m
        };

        // Assert
        Assert.That(utilityBill.UtilityType, Is.EqualTo(UtilityType.Electricity));
    }

    [Test]
    public void UtilityBill_GasType_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Gas,
            BillingDate = DateTime.UtcNow,
            Amount = 75m
        };

        // Assert
        Assert.That(utilityBill.UtilityType, Is.EqualTo(UtilityType.Gas));
    }

    [Test]
    public void UtilityBill_WaterType_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Water,
            BillingDate = DateTime.UtcNow,
            Amount = 50m
        };

        // Assert
        Assert.That(utilityBill.UtilityType, Is.EqualTo(UtilityType.Water));
    }

    [Test]
    public void UtilityBill_WithoutUsageAmount_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Internet,
            BillingDate = DateTime.UtcNow,
            Amount = 60m
        };

        // Assert
        Assert.That(utilityBill.UsageAmount, Is.Null);
    }

    [Test]
    public void UtilityBill_WithUsageAmount_IsValid()
    {
        // Arrange
        var usageAmount = 750.5m;

        // Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Electricity,
            BillingDate = DateTime.UtcNow,
            Amount = 120m,
            UsageAmount = usageAmount
        };

        // Assert
        Assert.That(utilityBill.UsageAmount, Is.EqualTo(usageAmount));
    }

    [Test]
    public void UtilityBill_WithoutUnit_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Other,
            BillingDate = DateTime.UtcNow,
            Amount = 40m
        };

        // Assert
        Assert.That(utilityBill.Unit, Is.Null);
    }

    [Test]
    public void UtilityBill_WithUnit_IsValid()
    {
        // Arrange
        var unit = "Gallons";

        // Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Water,
            BillingDate = DateTime.UtcNow,
            Amount = 55m,
            Unit = unit
        };

        // Assert
        Assert.That(utilityBill.Unit, Is.EqualTo(unit));
    }

    [Test]
    public void UtilityBill_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Electricity,
            BillingDate = DateTime.UtcNow,
            Amount = 100m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(utilityBill.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void UtilityBill_CanUpdateAmount()
    {
        // Arrange
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Gas,
            BillingDate = DateTime.UtcNow,
            Amount = 80m
        };
        var newAmount = 95m;

        // Act
        utilityBill.Amount = newAmount;

        // Assert
        Assert.That(utilityBill.Amount, Is.EqualTo(newAmount));
    }

    [Test]
    public void UtilityBill_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            UtilityType = UtilityType.Water,
            BillingDate = DateTime.UtcNow,
            Amount = 0m
        };

        // Assert
        Assert.That(utilityBill.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void UtilityBill_AllUtilityTypes_CanBeAssigned()
    {
        // Arrange
        var utilityTypes = new[]
        {
            UtilityType.Electricity,
            UtilityType.Gas,
            UtilityType.Water,
            UtilityType.Internet,
            UtilityType.Other
        };

        // Act & Assert
        foreach (var type in utilityTypes)
        {
            var utilityBill = new UtilityBill
            {
                UtilityBillId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UtilityType = type,
                BillingDate = DateTime.UtcNow,
                Amount = 100m
            };

            Assert.That(utilityBill.UtilityType, Is.EqualTo(type));
        }
    }

    [Test]
    public void UtilityBill_AllProperties_CanBeSet()
    {
        // Arrange
        var utilityBillId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var utilityType = UtilityType.Electricity;
        var billingDate = new DateTime(2024, 1, 1);
        var amount = 175.25m;
        var usageAmount = 875m;
        var unit = "kWh";
        var createdAt = DateTime.UtcNow.AddDays(-7);

        // Act
        var utilityBill = new UtilityBill
        {
            UtilityBillId = utilityBillId,
            UserId = userId,
            UtilityType = utilityType,
            BillingDate = billingDate,
            Amount = amount,
            UsageAmount = usageAmount,
            Unit = unit,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(utilityBill.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(utilityBill.UserId, Is.EqualTo(userId));
            Assert.That(utilityBill.UtilityType, Is.EqualTo(utilityType));
            Assert.That(utilityBill.BillingDate, Is.EqualTo(billingDate));
            Assert.That(utilityBill.Amount, Is.EqualTo(amount));
            Assert.That(utilityBill.UsageAmount, Is.EqualTo(usageAmount));
            Assert.That(utilityBill.Unit, Is.EqualTo(unit));
            Assert.That(utilityBill.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
