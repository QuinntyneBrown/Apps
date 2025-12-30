// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class TastingNoteTests
{
    [Test]
    public void TastingNote_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var tastingNoteId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var batchId = Guid.NewGuid();
        var tastingDate = DateTime.UtcNow.AddDays(-1);
        var rating = 4;
        var appearance = "Clear golden";
        var aroma = "Citrus and pine";
        var flavor = "Hoppy with caramel notes";
        var mouthfeel = "Medium body";
        var overallImpression = "Great beer!";
        var createdAt = DateTime.UtcNow;

        // Act
        var tastingNote = new TastingNote
        {
            TastingNoteId = tastingNoteId,
            UserId = userId,
            BatchId = batchId,
            TastingDate = tastingDate,
            Rating = rating,
            Appearance = appearance,
            Aroma = aroma,
            Flavor = flavor,
            Mouthfeel = mouthfeel,
            OverallImpression = overallImpression,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.TastingNoteId, Is.EqualTo(tastingNoteId));
            Assert.That(tastingNote.UserId, Is.EqualTo(userId));
            Assert.That(tastingNote.BatchId, Is.EqualTo(batchId));
            Assert.That(tastingNote.TastingDate, Is.EqualTo(tastingDate));
            Assert.That(tastingNote.Rating, Is.EqualTo(rating));
            Assert.That(tastingNote.Appearance, Is.EqualTo(appearance));
            Assert.That(tastingNote.Aroma, Is.EqualTo(aroma));
            Assert.That(tastingNote.Flavor, Is.EqualTo(flavor));
            Assert.That(tastingNote.Mouthfeel, Is.EqualTo(mouthfeel));
            Assert.That(tastingNote.OverallImpression, Is.EqualTo(overallImpression));
            Assert.That(tastingNote.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void TastingNote_TastingDate_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var tastingNote = new TastingNote();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(tastingNote.TastingDate, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void TastingNote_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var tastingNote = new TastingNote();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(tastingNote.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void TastingNote_NullableProperties_CanBeNull()
    {
        // Arrange & Act
        var tastingNote = new TastingNote
        {
            Appearance = null,
            Aroma = null,
            Flavor = null,
            Mouthfeel = null,
            OverallImpression = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.Appearance, Is.Null);
            Assert.That(tastingNote.Aroma, Is.Null);
            Assert.That(tastingNote.Flavor, Is.Null);
            Assert.That(tastingNote.Mouthfeel, Is.Null);
            Assert.That(tastingNote.OverallImpression, Is.Null);
        });
    }

    [Test]
    public void IsRatingValid_ReturnsTrue_WhenRatingIsOne()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = 1
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_ReturnsTrue_WhenRatingIsFive()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = 5
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_ReturnsTrue_WhenRatingIsInRange()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = 3
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_ReturnsFalse_WhenRatingIsZero()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = 0
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRatingValid_ReturnsFalse_WhenRatingIsSix()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = 6
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRatingValid_ReturnsFalse_WhenRatingIsNegative()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            Rating = -1
        };

        // Act
        var result = tastingNote.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void TastingNote_Batch_CanBeSet()
    {
        // Arrange
        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            BatchNumber = "2024-001"
        };

        var tastingNote = new TastingNote();

        // Act
        tastingNote.Batch = batch;

        // Assert
        Assert.That(tastingNote.Batch, Is.EqualTo(batch));
    }

    [Test]
    public void TastingNote_AllRatings_CanBeSet()
    {
        // Arrange & Act
        var note1 = new TastingNote { Rating = 1 };
        var note2 = new TastingNote { Rating = 2 };
        var note3 = new TastingNote { Rating = 3 };
        var note4 = new TastingNote { Rating = 4 };
        var note5 = new TastingNote { Rating = 5 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(note1.Rating, Is.EqualTo(1));
            Assert.That(note2.Rating, Is.EqualTo(2));
            Assert.That(note3.Rating, Is.EqualTo(3));
            Assert.That(note4.Rating, Is.EqualTo(4));
            Assert.That(note5.Rating, Is.EqualTo(5));
        });
    }

    [Test]
    public void TastingNote_AllProperties_CanBeModified()
    {
        // Arrange
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            Rating = 3
        };

        var newTastingNoteId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();
        var newBatchId = Guid.NewGuid();
        var newTastingDate = DateTime.UtcNow;

        // Act
        tastingNote.TastingNoteId = newTastingNoteId;
        tastingNote.UserId = newUserId;
        tastingNote.BatchId = newBatchId;
        tastingNote.TastingDate = newTastingDate;
        tastingNote.Rating = 5;
        tastingNote.Appearance = "Dark brown";
        tastingNote.Aroma = "Chocolate and coffee";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(tastingNote.TastingNoteId, Is.EqualTo(newTastingNoteId));
            Assert.That(tastingNote.UserId, Is.EqualTo(newUserId));
            Assert.That(tastingNote.BatchId, Is.EqualTo(newBatchId));
            Assert.That(tastingNote.TastingDate, Is.EqualTo(newTastingDate));
            Assert.That(tastingNote.Rating, Is.EqualTo(5));
            Assert.That(tastingNote.Appearance, Is.EqualTo("Dark brown"));
            Assert.That(tastingNote.Aroma, Is.EqualTo("Chocolate and coffee"));
        });
    }
}
