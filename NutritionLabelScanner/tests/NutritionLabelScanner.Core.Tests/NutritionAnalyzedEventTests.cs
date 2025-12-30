// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core.Tests;

public class NutritionAnalyzedEventTests
{
    [Test]
    public void NutritionAnalyzedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var nutritionInfoId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var calories = 250;
        var protein = 15m;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new NutritionAnalyzedEvent
        {
            NutritionInfoId = nutritionInfoId,
            ProductId = productId,
            Calories = calories,
            Protein = protein,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.NutritionInfoId, Is.EqualTo(nutritionInfoId));
            Assert.That(evt.ProductId, Is.EqualTo(productId));
            Assert.That(evt.Calories, Is.EqualTo(calories));
            Assert.That(evt.Protein, Is.EqualTo(protein));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void NutritionAnalyzedEvent_DefaultTimestamp_IsNotDefault()
    {
        // Arrange & Act
        var evt = new NutritionAnalyzedEvent
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 200,
            Protein = 10m
        };

        // Assert
        Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void NutritionAnalyzedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var nutritionInfoId = Guid.NewGuid();
        var productId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new NutritionAnalyzedEvent
        {
            NutritionInfoId = nutritionInfoId,
            ProductId = productId,
            Calories = 250,
            Protein = 15m,
            Timestamp = timestamp
        };

        var evt2 = new NutritionAnalyzedEvent
        {
            NutritionInfoId = nutritionInfoId,
            ProductId = productId,
            Calories = 250,
            Protein = 15m,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void NutritionAnalyzedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var evt1 = new NutritionAnalyzedEvent
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 200,
            Protein = 10m
        };

        var evt2 = new NutritionAnalyzedEvent
        {
            NutritionInfoId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Calories = 300,
            Protein = 20m
        };

        // Act & Assert
        Assert.That(evt1, Is.Not.EqualTo(evt2));
    }
}
