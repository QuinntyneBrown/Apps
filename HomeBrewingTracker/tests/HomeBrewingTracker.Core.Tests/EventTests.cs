// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void RecipeCreatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "American IPA";
        var beerStyle = BeerStyle.IPA;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            BeerStyle = beerStyle,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.BeerStyle, Is.EqualTo(beerStyle));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RecipeCreatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Stout",
            BeerStyle = BeerStyle.Stout
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void RecipeUpdatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RecipeUpdatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void RecipeUpdatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new RecipeUpdatedEvent
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void BatchStartedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var batchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var batchNumber = "2024-001";
        var brewDate = DateTime.UtcNow.AddDays(-7);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BatchStartedEvent
        {
            BatchId = batchId,
            UserId = userId,
            RecipeId = recipeId,
            BatchNumber = batchNumber,
            BrewDate = brewDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BatchId, Is.EqualTo(batchId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.BatchNumber, Is.EqualTo(batchNumber));
            Assert.That(evt.BrewDate, Is.EqualTo(brewDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void BatchStartedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new BatchStartedEvent
        {
            BatchId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            BatchNumber = "001",
            BrewDate = DateTime.UtcNow
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void BatchCompletedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var batchId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new BatchCompletedEvent
        {
            BatchId = batchId,
            UserId = userId,
            RecipeId = recipeId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BatchId, Is.EqualTo(batchId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void BatchCompletedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new BatchCompletedEvent
        {
            BatchId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void TastingNoteCreatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var batchId = Guid.NewGuid();
        var rating = 5;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new TastingNoteCreatedEvent
        {
            TastingNoteId = tastingNoteId,
            UserId = userId,
            BatchId = batchId,
            Rating = rating,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TastingNoteId, Is.EqualTo(tastingNoteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BatchId, Is.EqualTo(batchId));
            Assert.That(evt.Rating, Is.EqualTo(rating));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TastingNoteCreatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new TastingNoteCreatedEvent
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BatchId = Guid.NewGuid(),
            Rating = 4
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void TastingNoteUpdatedEvent_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var batchId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new TastingNoteUpdatedEvent
        {
            TastingNoteId = tastingNoteId,
            UserId = userId,
            BatchId = batchId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TastingNoteId, Is.EqualTo(tastingNoteId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.BatchId, Is.EqualTo(batchId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TastingNoteUpdatedEvent_Timestamp_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var evt = new TastingNoteUpdatedEvent
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            BatchId = Guid.NewGuid()
        };
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(evt.Timestamp, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Events_AreRecords_AndSupportValueEquality()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new RecipeUpdatedEvent
        {
            RecipeId = id,
            UserId = userId,
            Timestamp = timestamp
        };

        var event2 = new RecipeUpdatedEvent
        {
            RecipeId = id,
            UserId = userId,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
