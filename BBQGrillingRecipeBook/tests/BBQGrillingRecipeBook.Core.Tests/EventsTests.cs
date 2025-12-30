// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class EventsTests
{
    [Test]
    public void RecipeCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Smoked Brisket";
        var meatType = MeatType.Beef;
        var cookingMethod = CookingMethod.Smoking;

        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            MeatType = meatType,
            CookingMethod = cookingMethod
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.MeatType, Is.EqualTo(meatType));
            Assert.That(evt.CookingMethod, Is.EqualTo(cookingMethod));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void RecipeCreatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Grilled Chicken";
        var timestamp = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var evt = new RecipeCreatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void RecipeUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new RecipeUpdatedEvent
        {
            RecipeId = recipeId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void RecipeUpdatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow.AddMinutes(-10);

        // Act
        var evt = new RecipeUpdatedEvent
        {
            RecipeId = recipeId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void CookSessionStartedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var cookSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var cookDate = DateTime.UtcNow.AddHours(-2);

        // Act
        var evt = new CookSessionStartedEvent
        {
            CookSessionId = cookSessionId,
            UserId = userId,
            RecipeId = recipeId,
            CookDate = cookDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CookSessionId, Is.EqualTo(cookSessionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.CookDate, Is.EqualTo(cookDate));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void CookSessionStartedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var cookSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var cookDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow.AddMinutes(-3);

        // Act
        var evt = new CookSessionStartedEvent
        {
            CookSessionId = cookSessionId,
            UserId = userId,
            RecipeId = recipeId,
            CookDate = cookDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void CookSessionCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var cookSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var rating = 5;
        var wasSuccessful = true;

        // Act
        var evt = new CookSessionCompletedEvent
        {
            CookSessionId = cookSessionId,
            UserId = userId,
            RecipeId = recipeId,
            Rating = rating,
            WasSuccessful = wasSuccessful
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.CookSessionId, Is.EqualTo(cookSessionId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.RecipeId, Is.EqualTo(recipeId));
            Assert.That(evt.Rating, Is.EqualTo(rating));
            Assert.That(evt.WasSuccessful, Is.EqualTo(wasSuccessful));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void CookSessionCompletedEvent_NullRating_CreatesEvent()
    {
        // Arrange
        var cookSessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();

        // Act
        var evt = new CookSessionCompletedEvent
        {
            CookSessionId = cookSessionId,
            UserId = userId,
            RecipeId = recipeId,
            Rating = null,
            WasSuccessful = false
        };

        // Assert
        Assert.That(evt.Rating, Is.Null);
        Assert.That(evt.WasSuccessful, Is.False);
    }

    [Test]
    public void TechniqueCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Reverse Sear";
        var category = "Beef Techniques";

        // Act
        var evt = new TechniqueCreatedEvent
        {
            TechniqueId = techniqueId,
            UserId = userId,
            Name = name,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TechniqueId, Is.EqualTo(techniqueId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void TechniqueCreatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Low and Slow";
        var category = "General";
        var timestamp = DateTime.UtcNow.AddMinutes(-15);

        // Act
        var evt = new TechniqueCreatedEvent
        {
            TechniqueId = techniqueId,
            UserId = userId,
            Name = name,
            Category = category,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void TechniqueUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new TechniqueUpdatedEvent
        {
            TechniqueId = techniqueId,
            UserId = userId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TechniqueId, Is.EqualTo(techniqueId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void TechniqueUpdatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow.AddMinutes(-7);

        // Act
        var evt = new TechniqueUpdatedEvent
        {
            TechniqueId = techniqueId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void RecipeCreatedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new RecipeCreatedEvent
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Old Name"
        };
        var newName = "New Name";

        // Act
        var newEvt = evt with { Name = newName };

        // Assert
        Assert.That(newEvt.Name, Is.EqualTo(newName));
        Assert.That(newEvt.RecipeId, Is.EqualTo(evt.RecipeId));
    }

    [Test]
    public void CookSessionCompletedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new CookSessionCompletedEvent
        {
            CookSessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            Rating = 3
        };
        var newRating = 5;

        // Act
        var newEvt = evt with { Rating = newRating };

        // Assert
        Assert.That(newEvt.Rating, Is.EqualTo(newRating));
        Assert.That(newEvt.CookSessionId, Is.EqualTo(evt.CookSessionId));
    }
}
