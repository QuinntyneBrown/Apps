// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void VehicleAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var make = "Tesla";
        var model = "Model S";
        var year = 2022;
        var purchasePrice = 85000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new VehicleAddedEvent
        {
            VehicleId = vehicleId,
            Make = make,
            Model = model,
            Year = year,
            PurchasePrice = purchasePrice,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.Make, Is.EqualTo(make));
            Assert.That(eventData.Model, Is.EqualTo(model));
            Assert.That(eventData.Year, Is.EqualTo(year));
            Assert.That(eventData.PurchasePrice, Is.EqualTo(purchasePrice));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void VehicleSoldEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var salePrice = 75000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new VehicleSoldEvent
        {
            VehicleId = vehicleId,
            SalePrice = salePrice,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.SalePrice, Is.EqualTo(salePrice));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ValueAssessmentCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var assessmentId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var assessmentDate = new DateTime(2024, 3, 15);
        var estimatedValue = 28000m;
        var conditionGrade = ConditionGrade.Good;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new ValueAssessmentCreatedEvent
        {
            ValueAssessmentId = assessmentId,
            VehicleId = vehicleId,
            AssessmentDate = assessmentDate,
            EstimatedValue = estimatedValue,
            ConditionGrade = conditionGrade,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ValueAssessmentId, Is.EqualTo(assessmentId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.AssessmentDate, Is.EqualTo(assessmentDate));
            Assert.That(eventData.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(eventData.ConditionGrade, Is.EqualTo(conditionGrade));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ValueUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var assessmentId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var previousValue = 30000m;
        var newValue = 28000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new ValueUpdatedEvent
        {
            ValueAssessmentId = assessmentId,
            VehicleId = vehicleId,
            PreviousValue = previousValue,
            NewValue = newValue,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.ValueAssessmentId, Is.EqualTo(assessmentId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.PreviousValue, Is.EqualTo(previousValue));
            Assert.That(eventData.NewValue, Is.EqualTo(newValue));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MarketComparisonAddedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var comparisonId = Guid.NewGuid();
        var vehicleId = Guid.NewGuid();
        var listingSource = "Autotrader";
        var comparableDescription = "2020 BMW M3 Competition";
        var askingPrice = 52000m;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new MarketComparisonAddedEvent
        {
            MarketComparisonId = comparisonId,
            VehicleId = vehicleId,
            ListingSource = listingSource,
            ComparableDescription = comparableDescription,
            AskingPrice = askingPrice,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.MarketComparisonId, Is.EqualTo(comparisonId));
            Assert.That(eventData.VehicleId, Is.EqualTo(vehicleId));
            Assert.That(eventData.ListingSource, Is.EqualTo(listingSource));
            Assert.That(eventData.ComparableDescription, Is.EqualTo(comparableDescription));
            Assert.That(eventData.AskingPrice, Is.EqualTo(askingPrice));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Events_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var event1 = new VehicleAddedEvent { VehicleId = Guid.NewGuid(), Make = "Tesla", Model = "Model 3", Year = 2022 };
        var event2 = new ValueAssessmentCreatedEvent { ValueAssessmentId = Guid.NewGuid(), VehicleId = Guid.NewGuid(), AssessmentDate = DateTime.UtcNow, EstimatedValue = 30000m, ConditionGrade = ConditionGrade.Good };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(event1.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
            Assert.That(event2.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Events_AreRecords_SupportValueEquality()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new VehicleAddedEvent
        {
            VehicleId = vehicleId,
            Make = "BMW",
            Model = "M3",
            Year = 2020,
            PurchasePrice = 50000m,
            Timestamp = timestamp
        };

        var event2 = new VehicleAddedEvent
        {
            VehicleId = vehicleId,
            Make = "BMW",
            Model = "M3",
            Year = 2020,
            PurchasePrice = 50000m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
