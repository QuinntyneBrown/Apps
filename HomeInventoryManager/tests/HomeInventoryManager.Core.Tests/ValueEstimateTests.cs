// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class ValueEstimateTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesValueEstimate()
    {
        // Arrange
        var valueEstimateId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var estimatedValue = 1500m;
        var estimationDate = DateTime.UtcNow;
        var source = "Professional Appraisal";
        var notes = "Appraisal notes";

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate,
            Source = source,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimate.ValueEstimateId, Is.EqualTo(valueEstimateId));
            Assert.That(estimate.ItemId, Is.EqualTo(itemId));
            Assert.That(estimate.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(estimate.EstimationDate, Is.EqualTo(estimationDate));
            Assert.That(estimate.Source, Is.EqualTo(source));
            Assert.That(estimate.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void ValueEstimate_DefaultValues_AreSetCorrectly()
    {
        // Act
        var estimate = new ValueEstimate();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimate.EstimatedValue, Is.EqualTo(0m));
            Assert.That(estimate.EstimationDate, Is.EqualTo(default(DateTime)));
            Assert.That(estimate.Source, Is.Null);
            Assert.That(estimate.Notes, Is.Null);
            Assert.That(estimate.Item, Is.Null);
            Assert.That(estimate.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ValueEstimate_WithoutOptionalFields_IsValid()
    {
        // Arrange & Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 2000m,
            EstimationDate = DateTime.UtcNow
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimate.Source, Is.Null);
            Assert.That(estimate.Notes, Is.Null);
        });
    }

    [Test]
    public void ValueEstimate_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 1000m,
            EstimationDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(estimate.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void ValueEstimate_WithZeroValue_IsValid()
    {
        // Arrange & Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 0m,
            EstimationDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(estimate.EstimatedValue, Is.EqualTo(0m));
    }

    [Test]
    public void ValueEstimate_WithVariousSources_IsValid()
    {
        // Arrange
        var sources = new[] { "Professional Appraisal", "Online Tool", "Personal", "Insurance Company" };

        // Act & Assert
        foreach (var source in sources)
        {
            var estimate = new ValueEstimate
            {
                ValueEstimateId = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                EstimatedValue = 1000m,
                EstimationDate = DateTime.UtcNow,
                Source = source
            };

            Assert.That(estimate.Source, Is.EqualTo(source));
        }
    }

    [Test]
    public void ValueEstimate_WithPastEstimationDate_IsValid()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddYears(-1);

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 1200m,
            EstimationDate = pastDate
        };

        // Assert
        Assert.That(estimate.EstimationDate, Is.EqualTo(pastDate));
    }

    [Test]
    public void ValueEstimate_WithLargeValue_IsValid()
    {
        // Arrange
        var largeValue = 999999.99m;

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = largeValue,
            EstimationDate = DateTime.UtcNow
        };

        // Assert
        Assert.That(estimate.EstimatedValue, Is.EqualTo(largeValue));
    }

    [Test]
    public void ValueEstimate_WithNotes_IsValid()
    {
        // Arrange
        var notes = "Detailed notes about the valuation methodology and considerations";

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = Guid.NewGuid(),
            ItemId = Guid.NewGuid(),
            EstimatedValue = 1500m,
            EstimationDate = DateTime.UtcNow,
            Notes = notes
        };

        // Assert
        Assert.That(estimate.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void ValueEstimate_AllProperties_CanBeSet()
    {
        // Arrange
        var valueEstimateId = Guid.NewGuid();
        var itemId = Guid.NewGuid();
        var estimatedValue = 3500m;
        var estimationDate = new DateTime(2024, 1, 15);
        var source = "Certified Appraiser";
        var notes = "Comprehensive appraisal notes";
        var createdAt = DateTime.UtcNow.AddDays(-7);

        // Act
        var estimate = new ValueEstimate
        {
            ValueEstimateId = valueEstimateId,
            ItemId = itemId,
            EstimatedValue = estimatedValue,
            EstimationDate = estimationDate,
            Source = source,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(estimate.ValueEstimateId, Is.EqualTo(valueEstimateId));
            Assert.That(estimate.ItemId, Is.EqualTo(itemId));
            Assert.That(estimate.EstimatedValue, Is.EqualTo(estimatedValue));
            Assert.That(estimate.EstimationDate, Is.EqualTo(estimationDate));
            Assert.That(estimate.Source, Is.EqualTo(source));
            Assert.That(estimate.Notes, Is.EqualTo(notes));
            Assert.That(estimate.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
