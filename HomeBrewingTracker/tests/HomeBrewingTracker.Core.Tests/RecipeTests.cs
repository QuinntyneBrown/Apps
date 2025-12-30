// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class RecipeTests
{
    [Test]
    public void Recipe_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "American IPA";
        var beerStyle = BeerStyle.IPA;
        var description = "Hoppy and citrusy IPA";
        var originalGravity = 1.065m;
        var finalGravity = 1.012m;
        var abv = 6.9m;
        var ibu = 65;
        var batchSize = 5.0m;
        var ingredients = "Pale malt, Cascade hops, US-05 yeast";
        var instructions = "Mash at 152F for 60 minutes";
        var notes = "Great recipe!";
        var isFavorite = true;
        var createdAt = DateTime.UtcNow;

        // Act
        var recipe = new Recipe
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = name,
            BeerStyle = beerStyle,
            Description = description,
            OriginalGravity = originalGravity,
            FinalGravity = finalGravity,
            ABV = abv,
            IBU = ibu,
            BatchSize = batchSize,
            Ingredients = ingredients,
            Instructions = instructions,
            Notes = notes,
            IsFavorite = isFavorite,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(recipeId));
            Assert.That(recipe.UserId, Is.EqualTo(userId));
            Assert.That(recipe.Name, Is.EqualTo(name));
            Assert.That(recipe.BeerStyle, Is.EqualTo(beerStyle));
            Assert.That(recipe.Description, Is.EqualTo(description));
            Assert.That(recipe.OriginalGravity, Is.EqualTo(originalGravity));
            Assert.That(recipe.FinalGravity, Is.EqualTo(finalGravity));
            Assert.That(recipe.ABV, Is.EqualTo(abv));
            Assert.That(recipe.IBU, Is.EqualTo(ibu));
            Assert.That(recipe.BatchSize, Is.EqualTo(batchSize));
            Assert.That(recipe.Ingredients, Is.EqualTo(ingredients));
            Assert.That(recipe.Instructions, Is.EqualTo(instructions));
            Assert.That(recipe.Notes, Is.EqualTo(notes));
            Assert.That(recipe.IsFavorite, Is.True);
            Assert.That(recipe.CreatedAt, Is.EqualTo(createdAt));
        });
    }

    [Test]
    public void Recipe_DefaultName_IsEmptyString()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Recipe_DefaultDescription_IsEmptyString()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.Description, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Recipe_DefaultBatchSize_IsFiveGallons()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.BatchSize, Is.EqualTo(5.0m));
    }

    [Test]
    public void Recipe_DefaultIngredients_IsEmptyString()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.Ingredients, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Recipe_DefaultInstructions_IsEmptyString()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.Instructions, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Recipe_DefaultIsFavorite_IsFalse()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void Recipe_CreatedAt_HasDefaultValue()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var recipe = new Recipe();
        var afterCreation = DateTime.UtcNow.AddSeconds(1);

        // Assert
        Assert.That(recipe.CreatedAt, Is.InRange(beforeCreation, afterCreation));
    }

    [Test]
    public void Recipe_NullableProperties_CanBeNull()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            OriginalGravity = null,
            FinalGravity = null,
            ABV = null,
            IBU = null,
            Notes = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.OriginalGravity, Is.Null);
            Assert.That(recipe.FinalGravity, Is.Null);
            Assert.That(recipe.ABV, Is.Null);
            Assert.That(recipe.IBU, Is.Null);
            Assert.That(recipe.Notes, Is.Null);
        });
    }

    [Test]
    public void Recipe_Batches_DefaultsToEmptyList()
    {
        // Arrange & Act
        var recipe = new Recipe();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.Batches, Is.Not.Null);
            Assert.That(recipe.Batches, Is.Empty);
        });
    }

    [Test]
    public void ToggleFavorite_SwitchesFromFalseToTrue()
    {
        // Arrange
        var recipe = new Recipe
        {
            IsFavorite = false
        };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_SwitchesFromTrueToFalse()
    {
        // Arrange
        var recipe = new Recipe
        {
            IsFavorite = true
        };

        // Act
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_CanBeCalledMultipleTimes()
    {
        // Arrange
        var recipe = new Recipe
        {
            IsFavorite = false
        };

        // Act
        recipe.ToggleFavorite();
        recipe.ToggleFavorite();

        // Assert
        Assert.That(recipe.IsFavorite, Is.False);
    }

    [Test]
    public void Recipe_CanHaveDifferentBeerStyles()
    {
        // Arrange & Act
        var ipaRecipe = new Recipe { BeerStyle = BeerStyle.IPA };
        var stoutRecipe = new Recipe { BeerStyle = BeerStyle.Stout };
        var lagerRecipe = new Recipe { BeerStyle = BeerStyle.Lager };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(ipaRecipe.BeerStyle, Is.EqualTo(BeerStyle.IPA));
            Assert.That(stoutRecipe.BeerStyle, Is.EqualTo(BeerStyle.Stout));
            Assert.That(lagerRecipe.BeerStyle, Is.EqualTo(BeerStyle.Lager));
        });
    }

    [Test]
    public void Recipe_CanHaveMultipleBatches()
    {
        // Arrange
        var recipe = new Recipe();
        var batch1 = new Batch { BatchNumber = "001" };
        var batch2 = new Batch { BatchNumber = "002" };

        // Act
        recipe.Batches.Add(batch1);
        recipe.Batches.Add(batch2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.Batches, Has.Count.EqualTo(2));
            Assert.That(recipe.Batches, Contains.Item(batch1));
            Assert.That(recipe.Batches, Contains.Item(batch2));
        });
    }

    [Test]
    public void Recipe_AllProperties_CanBeModified()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            Name = "Initial Name"
        };

        var newRecipeId = Guid.NewGuid();
        var newUserId = Guid.NewGuid();

        // Act
        recipe.RecipeId = newRecipeId;
        recipe.UserId = newUserId;
        recipe.Name = "Updated Name";
        recipe.BeerStyle = BeerStyle.Porter;
        recipe.Description = "New Description";
        recipe.BatchSize = 10m;
        recipe.IsFavorite = true;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(recipe.RecipeId, Is.EqualTo(newRecipeId));
            Assert.That(recipe.UserId, Is.EqualTo(newUserId));
            Assert.That(recipe.Name, Is.EqualTo("Updated Name"));
            Assert.That(recipe.BeerStyle, Is.EqualTo(BeerStyle.Porter));
            Assert.That(recipe.Description, Is.EqualTo("New Description"));
            Assert.That(recipe.BatchSize, Is.EqualTo(10m));
            Assert.That(recipe.IsFavorite, Is.True);
        });
    }
}
