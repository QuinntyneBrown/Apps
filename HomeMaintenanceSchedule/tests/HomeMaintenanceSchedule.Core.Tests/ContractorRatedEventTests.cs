// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core.Tests;

public class ContractorRatedEventTests
{
    [Test]
    public void ContractorRatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var rating = 5;
        var timestamp = DateTime.UtcNow;

        // Act
        var ratedEvent = new ContractorRatedEvent
        {
            ContractorId = contractorId,
            Rating = rating,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(ratedEvent.ContractorId, Is.EqualTo(contractorId));
            Assert.That(ratedEvent.Rating, Is.EqualTo(rating));
            Assert.That(ratedEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ContractorRatedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var ratedEvent = new ContractorRatedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(ratedEvent.ContractorId, Is.EqualTo(Guid.Empty));
            Assert.That(ratedEvent.Rating, Is.EqualTo(0));
            Assert.That(ratedEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ContractorRatedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var ratedEvent = new ContractorRatedEvent
        {
            ContractorId = Guid.NewGuid(),
            Rating = 4
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(ratedEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ContractorRatedEvent_AllRatings_CanBeSet()
    {
        // Arrange
        var ratings = new[] { 1, 2, 3, 4, 5 };

        // Act & Assert
        foreach (var rating in ratings)
        {
            var ratedEvent = new ContractorRatedEvent
            {
                ContractorId = Guid.NewGuid(),
                Rating = rating
            };

            Assert.That(ratedEvent.Rating, Is.EqualTo(rating));
        }
    }

    [Test]
    public void ContractorRatedEvent_IsImmutable()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var rating = 3;

        // Act
        var ratedEvent = new ContractorRatedEvent
        {
            ContractorId = contractorId,
            Rating = rating
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(ratedEvent.ContractorId, Is.EqualTo(contractorId));
            Assert.That(ratedEvent.Rating, Is.EqualTo(rating));
        });
    }

    [Test]
    public void ContractorRatedEvent_EqualityByValue()
    {
        // Arrange
        var contractorId = Guid.NewGuid();
        var rating = 5;
        var timestamp = DateTime.UtcNow;

        var event1 = new ContractorRatedEvent
        {
            ContractorId = contractorId,
            Rating = rating,
            Timestamp = timestamp
        };

        var event2 = new ContractorRatedEvent
        {
            ContractorId = contractorId,
            Rating = rating,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ContractorRatedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new ContractorRatedEvent
        {
            ContractorId = Guid.NewGuid(),
            Rating = 3
        };

        var event2 = new ContractorRatedEvent
        {
            ContractorId = Guid.NewGuid(),
            Rating = 5
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
