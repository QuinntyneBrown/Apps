// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaxDeductionOrganizer.Core.Tests;

public class TaxYearCreatedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEvent()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var year = 2024;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new TaxYearCreatedEvent
        {
            TaxYearId = taxYearId,
            Year = year,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.TaxYearId, Is.EqualTo(taxYearId));
            Assert.That(eventData.Year, Is.EqualTo(year));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new TaxYearCreatedEvent
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2024
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
    public void Event_WithHistoricalYear_IsValid()
    {
        // Arrange & Act
        var eventData = new TaxYearCreatedEvent
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2015
        };

        // Assert
        Assert.That(eventData.Year, Is.EqualTo(2015));
    }

    [Test]
    public void Event_WithFutureYear_IsValid()
    {
        // Arrange & Act
        var eventData = new TaxYearCreatedEvent
        {
            TaxYearId = Guid.NewGuid(),
            Year = 2030
        };

        // Assert
        Assert.That(eventData.Year, Is.EqualTo(2030));
    }

    [Test]
    public void Event_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var taxYearId = Guid.NewGuid();
        var year = 2024;
        var timestamp = DateTime.UtcNow;

        var event1 = new TaxYearCreatedEvent
        {
            TaxYearId = taxYearId,
            Year = year,
            Timestamp = timestamp
        };

        var event2 = new TaxYearCreatedEvent
        {
            TaxYearId = taxYearId,
            Year = year,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithCurrentYear_IsValid()
    {
        // Arrange
        var currentYear = DateTime.Now.Year;

        // Act
        var eventData = new TaxYearCreatedEvent
        {
            TaxYearId = Guid.NewGuid(),
            Year = currentYear
        };

        // Assert
        Assert.That(eventData.Year, Is.EqualTo(currentYear));
    }
}
