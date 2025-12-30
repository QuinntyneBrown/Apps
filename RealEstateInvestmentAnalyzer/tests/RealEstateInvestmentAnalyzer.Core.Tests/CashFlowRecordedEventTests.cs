// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class CashFlowRecordedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var cashFlowId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var netCashFlow = 700m;

        // Act
        var eventData = new CashFlowRecordedEvent
        {
            CashFlowId = cashFlowId,
            PropertyId = propertyId,
            NetCashFlow = netCashFlow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.CashFlowId, Is.EqualTo(cashFlowId));
            Assert.That(eventData.PropertyId, Is.EqualTo(propertyId));
            Assert.That(eventData.NetCashFlow, Is.EqualTo(netCashFlow));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_NegativeCashFlow_StoresCorrectly()
    {
        // Arrange & Act
        var eventData = new CashFlowRecordedEvent
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            NetCashFlow = -500m
        };

        // Assert
        Assert.That(eventData.NetCashFlow, Is.EqualTo(-500m));
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new CashFlowRecordedEvent
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            NetCashFlow = 800m
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
        var cashFlowId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new CashFlowRecordedEvent
        {
            CashFlowId = cashFlowId,
            PropertyId = propertyId,
            NetCashFlow = 1200m,
            Timestamp = timestamp
        };

        var event2 = new CashFlowRecordedEvent
        {
            CashFlowId = cashFlowId,
            PropertyId = propertyId,
            NetCashFlow = 1200m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new CashFlowRecordedEvent
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            NetCashFlow = 900m
        };

        var event2 = new CashFlowRecordedEvent
        {
            CashFlowId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            NetCashFlow = 1100m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
