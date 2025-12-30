// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class CookSessionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesCookSession()
    {
        // Arrange
        var cookSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var cookDate = DateTime.UtcNow.AddDays(-1);
        var actualCookTime = 180;
        var tempUsed = 250;
        var rating = 4;
        var notes = "Came out great";
        var modifications = "Added extra garlic";

        // Act
        var session = new CookSession
        {
            CookSessionId = cookSessionId,
            UserId = userId,
            RecipeId = recipeId,
            CookDate = cookDate,
            ActualCookTimeMinutes = actualCookTime,
            TemperatureUsed = tempUsed,
            Rating = rating,
            Notes = notes,
            Modifications = modifications,
            WasSuccessful = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.CookSessionId, Is.EqualTo(cookSessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.RecipeId, Is.EqualTo(recipeId));
            Assert.That(session.CookDate, Is.EqualTo(cookDate));
            Assert.That(session.ActualCookTimeMinutes, Is.EqualTo(actualCookTime));
            Assert.That(session.TemperatureUsed, Is.EqualTo(tempUsed));
            Assert.That(session.Rating, Is.EqualTo(rating));
            Assert.That(session.Notes, Is.EqualTo(notes));
            Assert.That(session.Modifications, Is.EqualTo(modifications));
            Assert.That(session.WasSuccessful, Is.True);
            Assert.That(session.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var session = new CookSession();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.CookDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(session.TemperatureUsed, Is.Null);
            Assert.That(session.Rating, Is.Null);
            Assert.That(session.Notes, Is.Null);
            Assert.That(session.Modifications, Is.Null);
            Assert.That(session.WasSuccessful, Is.True);
            Assert.That(session.Recipe, Is.Null);
            Assert.That(session.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsRatingValid_WithNoRating_ReturnsTrue()
    {
        // Arrange
        var session = new CookSession { Rating = null };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating1_ReturnsTrue()
    {
        // Arrange
        var session = new CookSession { Rating = 1 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating5_ReturnsTrue()
    {
        // Arrange
        var session = new CookSession { Rating = 5 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating3_ReturnsTrue()
    {
        // Arrange
        var session = new CookSession { Rating = 3 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRatingValid_WithRating0_ReturnsFalse()
    {
        // Arrange
        var session = new CookSession { Rating = 0 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRatingValid_WithRating6_ReturnsFalse()
    {
        // Arrange
        var session = new CookSession { Rating = 6 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRatingValid_WithNegativeRating_ReturnsFalse()
    {
        // Arrange
        var session = new CookSession { Rating = -1 };

        // Act
        var result = session.IsRatingValid();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void WasSuccessful_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var session = new CookSession();

        // Act
        session.WasSuccessful = false;

        // Assert
        Assert.That(session.WasSuccessful, Is.False);
    }

    [Test]
    public void Recipe_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var session = new CookSession();
        var recipe = new Recipe { RecipeId = Guid.NewGuid() };

        // Act
        session.Recipe = recipe;

        // Assert
        Assert.That(session.Recipe, Is.EqualTo(recipe));
    }

    [Test]
    public void TemperatureUsed_CanBeSetAndCleared_UpdatesCorrectly()
    {
        // Arrange
        var session = new CookSession { TemperatureUsed = 225 };

        // Act
        session.TemperatureUsed = null;

        // Assert
        Assert.That(session.TemperatureUsed, Is.Null);
    }

    [Test]
    public void Notes_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var session = new CookSession();
        var notes = "Great results!";

        // Act
        session.Notes = notes;

        // Assert
        Assert.That(session.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Modifications_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var session = new CookSession();
        var mods = "Used hickory instead of oak";

        // Act
        session.Modifications = mods;

        // Assert
        Assert.That(session.Modifications, Is.EqualTo(mods));
    }
}
