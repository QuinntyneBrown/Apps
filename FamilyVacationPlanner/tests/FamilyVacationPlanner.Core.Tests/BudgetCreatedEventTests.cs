// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyVacationPlanner.Core.Tests;

public class BudgetCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.VacationBudgetId, Is.EqualTo(budgetId));
            Assert.That(evt.TripId, Is.EqualTo(tripId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        // Act
        var evt = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId,
            TripId = tripId
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId,
            TripId = tripId,
            Timestamp = timestamp
        };

        var event2 = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var budgetId1 = Guid.NewGuid();
        var budgetId2 = Guid.NewGuid();
        var tripId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId1,
            TripId = tripId,
            Timestamp = timestamp
        };

        var event2 = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId2,
            TripId = tripId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var budgetId = Guid.NewGuid();
        var tripId = Guid.NewGuid();

        // Act
        var evt = new BudgetCreatedEvent
        {
            VacationBudgetId = budgetId,
            TripId = tripId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.VacationBudgetId, Is.EqualTo(budgetId));
            Assert.That(evt.TripId, Is.EqualTo(tripId));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new BudgetCreatedEvent
        {
            VacationBudgetId = Guid.NewGuid(),
            TripId = Guid.NewGuid()
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("BudgetCreatedEvent"));
    }
}
