// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class DeductionAddedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var deductionId = Guid.NewGuid();
        var amount = 500.50m;
        var category = DeductionCategory.MedicalExpenses;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new DeductionAddedEvent
        {
            DeductionId = deductionId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.DeductionId, Is.EqualTo(deductionId));
            Assert.That(eventData.Amount, Is.EqualTo(amount));
            Assert.That(eventData.Category, Is.EqualTo(category));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new DeductionAddedEvent
        {
            DeductionId = Guid.NewGuid(),
            Amount = 100m,
            Category = DeductionCategory.Other
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Event_WithAllCategories_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.MedicalExpenses }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.CharitableDonations }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.MortgageInterest }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.StateAndLocalTaxes }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.BusinessExpenses }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.EducationExpenses }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.HomeOffice }, Throws.Nothing);
            Assert.That(() => new DeductionAddedEvent { DeductionId = Guid.NewGuid(), Amount = 100m, Category = DeductionCategory.Other }, Throws.Nothing);
        });
    }

    [Test]
    public void Event_WithLargeAmount_StoresCorrectly()
    {
        // Arrange
        var largeAmount = 999999.99m;

        // Act
        var eventData = new DeductionAddedEvent
        {
            DeductionId = Guid.NewGuid(),
            Amount = largeAmount,
            Category = DeductionCategory.Other
        };

        // Assert
        Assert.That(eventData.Amount, Is.EqualTo(largeAmount));
    }

    [Test]
    public void Event_WithZeroAmount_IsValid()
    {
        // Arrange & Act
        var eventData = new DeductionAddedEvent
        {
            DeductionId = Guid.NewGuid(),
            Amount = 0m,
            Category = DeductionCategory.Other
        };

        // Assert
        Assert.That(eventData.Amount, Is.EqualTo(0m));
    }

    [Test]
    public void Event_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var deductionId = Guid.NewGuid();
        var amount = 100m;
        var category = DeductionCategory.MedicalExpenses;
        var timestamp = DateTime.UtcNow;

        var event1 = new DeductionAddedEvent
        {
            DeductionId = deductionId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        var event2 = new DeductionAddedEvent
        {
            DeductionId = deductionId,
            Amount = amount,
            Category = category,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
