// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class LeaseCreatedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        // Arrange
        var leaseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var tenantName = "John Doe";
        var monthlyRent = 1500m;

        // Act
        var eventData = new LeaseCreatedEvent
        {
            LeaseId = leaseId,
            PropertyId = propertyId,
            TenantName = tenantName,
            MonthlyRent = monthlyRent
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.LeaseId, Is.EqualTo(leaseId));
            Assert.That(eventData.PropertyId, Is.EqualTo(propertyId));
            Assert.That(eventData.TenantName, Is.EqualTo(tenantName));
            Assert.That(eventData.MonthlyRent, Is.EqualTo(monthlyRent));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Timestamp_DefaultValue_IsSetToUtcNow()
    {
        // Arrange
        var beforeCreate = DateTime.UtcNow;

        // Act
        var eventData = new LeaseCreatedEvent
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            TenantName = "Jane Smith",
            MonthlyRent = 1200m
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
        var leaseId = Guid.NewGuid();
        var propertyId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new LeaseCreatedEvent
        {
            LeaseId = leaseId,
            PropertyId = propertyId,
            TenantName = "Bob Johnson",
            MonthlyRent = 1800m,
            Timestamp = timestamp
        };

        var event2 = new LeaseCreatedEvent
        {
            LeaseId = leaseId,
            PropertyId = propertyId,
            TenantName = "Bob Johnson",
            MonthlyRent = 1800m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        // Arrange
        var event1 = new LeaseCreatedEvent
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            TenantName = "Alice Brown",
            MonthlyRent = 2000m
        };

        var event2 = new LeaseCreatedEvent
        {
            LeaseId = Guid.NewGuid(),
            PropertyId = Guid.NewGuid(),
            TenantName = "Charlie Davis",
            MonthlyRent = 1500m
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
