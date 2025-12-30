// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class ExpenseRecordedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var amount = 2500m;

        // Act
        var eventData = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            PropertyId = propertyId,
            Amount = amount
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ExpenseId, Is.EqualTo(expenseId));
            Assert.That(eventData.PropertyId, Is.EqualTo(propertyId));
            Assert.That(eventData.Amount, Is.EqualTo(amount));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Amount = 1500m
        };

        var afterCreate = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreate));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreate));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            PropertyId = propertyId,
            Amount = 3000m,
            Timestamp = timestamp
        };

        var event2 = new ExpenseRecordedEvent
        {
            ExpenseId = expenseId,
            PropertyId = propertyId,
            Amount = 3000m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Amount = 2500m
        };

        var event2 = new ExpenseRecordedEvent
        {
            ExpenseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            Amount = 3500m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
