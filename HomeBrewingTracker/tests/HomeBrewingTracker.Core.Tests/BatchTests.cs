// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class BatchTests
{
    [Test]
    public void Batch_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var batchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var batchNumber = "2024-001";
        var status = BatchStatus.Fermenting;
        var brewDate = DateTime.UtcNow.AddDays(-7);
        var bottlingDate = DateTime.UtcNow.AddDays(-1);
        var actualOriginalGravity = 1.065m;
        var actualFinalGravity = 1.012m;
        var actualABV = 6.9m;
        var notes = "Fermentation went well";
        var createdAt = DateTime.UtcNow;

        // Act
        var batch = new Batch
        {
            BatchId = batchId,
            UserId = userId,
            RecipeId = recipeId,
            BatchNumber = batchNumber,
            Status = status,
            BrewDate = brewDate,
            BottlingDate = bottlingDate,
            ActualOriginalGravity = actualOriginalGravity,
            ActualFinalGravity = actualFinalGravity,
            ActualABV = actualABV,
            Notes = notes,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(batch.BatchId, Is.EqualTo(batchId));
            Assert.That(batch.UserId, Is.EqualTo(userId));
            Assert.That(batch.RecipeId, Is.EqualTo(recipeId));
            Assert.That(batch.BatchNumber, Is.EqualTo(batchNumber));
            Assert.That(batch.Status, Is.EqualTo(status));
            Assert.That(batch.BrewDate, Is.EqualTo(brewDate));
            Assert.That(batch.BottlingDate, Is.EqualTo(bottlingDate));
            Assert.That(batch.ActualOriginalGravity, Is.EqualTo(actualOriginalGravity));
            Assert.That(batch.ActualFinalGravity, Is.EqualTo(actualFinalGravity));
            Assert.That(batch.ActualABV, Is.EqualTo(actualABV));
            Assert.That(batch.Notes, Is.EqualTo(notes));
            Assert.That(batch.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Batch_DefaultBatchNumber_IsEmptyString()
    {
        // Arrange & Act
        var batch = new Batch();

        // Assert
        Assert.That(batch.BatchNumber, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Batch_BrewDate_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var batch = new Batch();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(batch.BrewDate, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Batch_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var batch = new Batch();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(batch.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Batch_NullableProperties_CanBeNull()
    {
        // Arrange & Act
        var batch = new Batch
        {
            BottlingDate = null,
            ActualOriginalGravity = null,
            ActualFinalGravity = null,
            ActualABV = null,
            Notes = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(batch.BottlingDate, Is.Null);
            Assert.That(batch.ActualOriginalGravity, Is.Null);
            Assert.That(batch.ActualFinalGravity, Is.Null);
            Assert.That(batch.ActualABV, Is.Null);
            Assert.That(batch.Notes, Is.Null);
        });
    }

    [Test]
    public void Batch_TastingNotes_DefaultsToEmptyList()
    {
        // Arrange & Act
        var batch = new Batch();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(batch.TastingNotes, Is.Not.Null);
            Assert.That(batch.TastingNotes, Is.Empty);
        });
    }

    [Test]
    public void MarkAsBottled_SetsStatusToBottled()
    {
        // Arrange
        var batch = new Batch
        {
            Status = BatchStatus.Fermenting
        };

        // Act
        batch.MarkAsBottled();

        // Assert
        Assert.That(batch.Status, Is.EqualTo(BatchStatus.Bottled));
    }

    [Test]
    public void MarkAsBottled_SetsBottlingDate()
    {
        // Arrange
        var beforeBottling = DateTime.UtcNow.AddSeconds(-1);
        var batch = new Batch
        {
            Status = BatchStatus.Fermenting,
            BottlingDate = null
        };

        // Act
        batch.MarkAsBottled();
        var afterBottling = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(batch.BottlingDate, Is.InRange(beforeBottling, afterBottling));
    }

    [Test]
    public void MarkAsCompleted_SetsStatusToCompleted()
    {
        // Arrange
        var batch = new Batch
        {
            Status = BatchStatus.Bottled
        };

        // Act
        batch.MarkAsCompleted();

        // Assert
        Assert.That(batch.Status, Is.EqualTo(BatchStatus.Completed));
    }

    [Test]
    public void Batch_Recipe_CanBeSet()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            Name = "American IPA"
        };

        var batch = new Batch();

        // Act
        batch.Recipe = recipe;

        // Assert
        Assert.That(batch.Recipe, Is.EqualTo(recipe));
    }

    [Test]
    public void Batch_CanHaveMultipleTastingNotes()
    {
        // Arrange
        var batch = new Batch();
        var note1 = new TastingNote { Rating = 4 };
        var note2 = new TastingNote { Rating = 5 };

        // Act
        batch.TastingNotes.Add(note1);
        batch.TastingNotes.Add(note2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(batch.TastingNotes, Has.Count.EqualTo(2));
            Assert.That(batch.TastingNotes, Contains.Item(note1));
            Assert.That(batch.TastingNotes, Contains.Item(note2));
        });
    }

    [Test]
    public void Batch_CanHaveDifferentStatuses()
    {
        // Arrange & Act
        var planned = new Batch { Status = BatchStatus.Planned };
        var fermenting = new Batch { Status = BatchStatus.Fermenting };
        var bottled = new Batch { Status = BatchStatus.Bottled };
        var completed = new Batch { Status = BatchStatus.Completed };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(planned.Status, Is.EqualTo(BatchStatus.Planned));
            Assert.That(fermenting.Status, Is.EqualTo(BatchStatus.Fermenting));
            Assert.That(bottled.Status, Is.EqualTo(BatchStatus.Bottled));
            Assert.That(completed.Status, Is.EqualTo(BatchStatus.Completed));
        });
    }

    [Test]
    public void Batch_AllProperties_CanBeModified()
    {
        // Arrange
        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            BatchNumber = "001"
        };

        var newBatchId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newRecipeId = Guid.NewGuid();
        var newBrewDate = DateTime.UtcNow;

        // Act
        batch.BatchId = newBatchId;
        batch.UserId = newUserId;
        batch.RecipeId = newRecipeId;
        batch.BatchNumber = "002";
        batch.Status = BatchStatus.Completed;
        batch.BrewDate = newBrewDate;
        batch.ActualABV = 7.5m;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(batch.BatchId, Is.EqualTo(newBatchId));
            Assert.That(batch.UserId, Is.EqualTo(newUserId));
            Assert.That(batch.RecipeId, Is.EqualTo(newRecipeId));
            Assert.That(batch.BatchNumber, Is.EqualTo("002"));
            Assert.That(batch.Status, Is.EqualTo(BatchStatus.Completed));
            Assert.That(batch.BrewDate, Is.EqualTo(newBrewDate));
            Assert.That(batch.ActualABV, Is.EqualTo(7.5m));
        });
    }
}
