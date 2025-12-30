// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeEnergyUsageTracker.Core.Tests;

public class UsageTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesUsage()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var utilityBillId = Guid.NewGuid();
        var date = DateTime.UtcNow;
        var amount = 123.45m;

        // Act
        var usage = new Usage
        {
            UsageId = usageId,
            UtilityBillId = utilityBillId,
            Date = date,
            Amount = amount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usage.UsageId, Is.EqualTo(usageId));
            Assert.That(usage.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(usage.Date, Is.EqualTo(date));
            Assert.That(usage.Amount, Is.EqualTo(amount));
        });
    }

    [Test]
    public void Usage_DefaultValues_AreSetCorrectly()
    {
        // Act
        var usage = new Usage();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usage.UsageId, Is.EqualTo(Guid.Empty));
            Assert.That(usage.UtilityBillId, Is.EqualTo(Guid.Empty));
            Assert.That(usage.Date, Is.EqualTo(default(DateTime)));
            Assert.That(usage.Amount, Is.EqualTo(0m));
            Assert.That(usage.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Usage_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 100m
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(usage.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void Usage_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 0m
        };

        // Assert
        Assert.That(usage.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Usage_WithNegativeAmount_CanBeSet()
    {
        // Arrange & Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = -50m
        };

        // Assert
        Assert.That(usage.Amount, Is.EqualTo(-50m));
    }

    [Test]
    public void Usage_WithLargeAmount_IsValid()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = largeAmount
        };

        // Assert
        Assert.That(usage.Amount, Is.EqualTo(largeAmount));
    }

    [Test]
    public void Usage_WithPastDate_IsValid()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddMonths(-3);

        // Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = pastDate,
            Amount = 100m
        };

        // Assert
        Assert.That(usage.Date, Is.EqualTo(pastDate));
    }

    [Test]
    public void Usage_WithFutureDate_CanBeSet()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = futureDate,
            Amount = 100m
        };

        // Assert
        Assert.That(usage.Date, Is.EqualTo(futureDate));
    }

    [Test]
    public void Usage_CanUpdateAmount()
    {
        // Arrange
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = Guid.NewGuid(),
            Date = DateTime.UtcNow,
            Amount = 100m
        };
        var newAmount = 150m;

        // Act
        usage.Amount = newAmount;

        // Assert
        Assert.That(usage.Amount, Is.EqualTo(newAmount));
    }

    [Test]
    public void Usage_AllProperties_CanBeSet()
    {
        // Arrange
        var usageId = Guid.NewGuid();
        var utilityBillId = Guid.NewGuid();
        var date = new DateTime(2024, 1, 15);
        var amount = 456.78m;
        var createdAt = DateTime.UtcNow.AddDays(-10);

        // Act
        var usage = new Usage
        {
            UsageId = usageId,
            UtilityBillId = utilityBillId,
            Date = date,
            Amount = amount,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(usage.UsageId, Is.EqualTo(usageId));
            Assert.That(usage.UtilityBillId, Is.EqualTo(utilityBillId));
            Assert.That(usage.Date, Is.EqualTo(date));
            Assert.That(usage.Amount, Is.EqualTo(amount));
            Assert.That(usage.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
