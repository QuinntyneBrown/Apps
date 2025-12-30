// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class IngredientTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesIngredient()
    {
        var ingredient = new Ingredient();
        Assert.Multiple(() =>
        {
            Assert.That(ingredient.IngredientId, Is.EqualTo(Guid.Empty));
            Assert.That(ingredient.RecipeId, Is.EqualTo(Guid.Empty));
            Assert.That(ingredient.Recipe, Is.Null);
            Assert.That(ingredient.Name, Is.EqualTo(string.Empty));
            Assert.That(ingredient.Quantity, Is.EqualTo(string.Empty));
            Assert.That(ingredient.Unit, Is.Null);
            Assert.That(ingredient.Notes, Is.Null);
            Assert.That(ingredient.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesIngredient()
    {
        var ingredientId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var name = "Flour";
        var quantity = "2";
        var unit = "cups";

        var ingredient = new Ingredient
        {
            IngredientId = ingredientId,
            RecipeId = recipeId,
            Name = name,
            Quantity = quantity,
            Unit = unit
        };

        Assert.Multiple(() =>
        {
            Assert.That(ingredient.IngredientId, Is.EqualTo(ingredientId));
            Assert.That(ingredient.RecipeId, Is.EqualTo(recipeId));
            Assert.That(ingredient.Name, Is.EqualTo(name));
            Assert.That(ingredient.Quantity, Is.EqualTo(quantity));
            Assert.That(ingredient.Unit, Is.EqualTo(unit));
        });
    }

    [Test]
    public void Ingredient_WithNotes_SetsCorrectly()
    {
        var notes = "Use all-purpose flour";
        var ingredient = new Ingredient { Name = "Flour", Quantity = "2", Unit = "cups", Notes = notes };
        Assert.That(ingredient.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Ingredient_WithoutUnit_AllowsNullUnit()
    {
        var ingredient = new Ingredient { Name = "Salt", Quantity = "1", Unit = null };
        Assert.That(ingredient.Unit, Is.Null);
    }

    [Test]
    public void Ingredient_DifferentUnits_SetCorrectly()
    {
        var cups = new Ingredient { Name = "Sugar", Quantity = "1", Unit = "cup" };
        var tablespoons = new Ingredient { Name = "Butter", Quantity = "2", Unit = "tablespoons" };
        var grams = new Ingredient { Name = "Salt", Quantity = "5", Unit = "grams" };

        Assert.Multiple(() =>
        {
            Assert.That(cups.Unit, Is.EqualTo("cup"));
            Assert.That(tablespoons.Unit, Is.EqualTo("tablespoons"));
            Assert.That(grams.Unit, Is.EqualTo("grams"));
        });
    }

    [Test]
    public void Ingredient_FractionalQuantity_StoresCorrectly()
    {
        var ingredient = new Ingredient { Name = "Milk", Quantity = "1/2", Unit = "cup" };
        Assert.That(ingredient.Quantity, Is.EqualTo("1/2"));
    }

    [Test]
    public void Ingredient_DecimalQuantity_StoresCorrectly()
    {
        var ingredient = new Ingredient { Name = "Oil", Quantity = "2.5", Unit = "tablespoons" };
        Assert.That(ingredient.Quantity, Is.EqualTo("2.5"));
    }

    [Test]
    public void Ingredient_WithAllProperties_SetsAllCorrectly()
    {
        var ingredient = new Ingredient
        {
            IngredientId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            Name = "Tomatoes",
            Quantity = "3",
            Unit = "large",
            Notes = "Fresh and ripe"
        };

        Assert.Multiple(() =>
        {
            Assert.That(ingredient.Name, Is.EqualTo("Tomatoes"));
            Assert.That(ingredient.Quantity, Is.EqualTo("3"));
            Assert.That(ingredient.Unit, Is.EqualTo("large"));
            Assert.That(ingredient.Notes, Is.EqualTo("Fresh and ripe"));
        });
    }
}
