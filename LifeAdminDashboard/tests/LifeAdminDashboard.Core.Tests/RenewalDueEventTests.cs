// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LifeAdminDashboard.Core.Tests;

public class RenewalDueEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var renewalId = Guid.NewGuid();
        var name = "Netflix Subscription";
        var renewalDate = DateTime.UtcNow.AddDays(7);
        var cost = 15.99m;

        // Act
        var evt = new RenewalDueEvent
        {
            RenewalId = renewalId,
            Name = name,
            RenewalDate = renewalDate,
            Cost = cost
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RenewalId, Is.EqualTo(renewalId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.RenewalDate, Is.EqualTo(renewalDate));
            Assert.That(evt.Cost, Is.EqualTo(cost));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Timestamp_IsSetAutomatically()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var evt = new RenewalDueEvent
        {
            RenewalId = Guid.NewGuid()
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(evt.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
        Assert.That(evt.Timestamp, Is.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void RenewalId_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var evt = new RenewalDueEvent { RenewalId = expectedId };

        // Assert
        Assert.That(evt.RenewalId, Is.EqualTo(expectedId));
    }

    [Test]
    public void Name_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedName = "Car Insurance";

        // Act
        var evt = new RenewalDueEvent { Name = expectedName };

        // Assert
        Assert.That(evt.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void RenewalDate_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedDate = DateTime.UtcNow.AddMonths(1);

        // Act
        var evt = new RenewalDueEvent { RenewalDate = expectedDate };

        // Assert
        Assert.That(evt.RenewalDate, Is.EqualTo(expectedDate));
    }

    [Test]
    public void Cost_CanBeNull()
    {
        // Arrange & Act
        var evt = new RenewalDueEvent { Cost = null };

        // Assert
        Assert.That(evt.Cost, Is.Null);
    }

    [Test]
    public void Cost_CanBeInitialized_ReturnsCorrectValue()
    {
        // Arrange
        var expectedCost = 99.99m;

        // Act
        var evt = new RenewalDueEvent { Cost = expectedCost };

        // Assert
        Assert.That(evt.Cost, Is.EqualTo(expectedCost));
    }
}
