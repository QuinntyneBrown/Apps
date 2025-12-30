// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class MealPlanTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesMealPlan()
    {
        var mealPlan = new MealPlan();
        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.MealPlanId, Is.EqualTo(Guid.Empty));
            Assert.That(mealPlan.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(mealPlan.Name, Is.EqualTo(string.Empty));
            Assert.That(mealPlan.MealDate, Is.EqualTo(default(DateTime)));
            Assert.That(mealPlan.MealType, Is.EqualTo(string.Empty));
            Assert.That(mealPlan.RecipeId, Is.Null);
            Assert.That(mealPlan.Recipe, Is.Null);
            Assert.That(mealPlan.Notes, Is.Null);
            Assert.That(mealPlan.IsPrepared, Is.False);
            Assert.That(mealPlan.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesMealPlan()
    {
        var mealPlanId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recipeId = Guid.NewGuid();
        var name = "Monday Dinner";
        var mealDate = new DateTime(2024, 6, 15);
        var mealType = "Dinner";

        var mealPlan = new MealPlan
        {
            MealPlanId = mealPlanId,
            UserId = userId,
            Name = name,
            MealDate = mealDate,
            MealType = mealType,
            RecipeId = recipeId
        };

        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.MealPlanId, Is.EqualTo(mealPlanId));
            Assert.That(mealPlan.UserId, Is.EqualTo(userId));
            Assert.That(mealPlan.Name, Is.EqualTo(name));
            Assert.That(mealPlan.MealDate, Is.EqualTo(mealDate));
            Assert.That(mealPlan.MealType, Is.EqualTo(mealType));
            Assert.That(mealPlan.RecipeId, Is.EqualTo(recipeId));
        });
    }

    [Test]
    public void MealPlan_Breakfast_SetsCorrectly()
    {
        var mealPlan = new MealPlan { Name = "Morning Meal", MealType = "Breakfast" };
        Assert.That(mealPlan.MealType, Is.EqualTo("Breakfast"));
    }

    [Test]
    public void MealPlan_Lunch_SetsCorrectly()
    {
        var mealPlan = new MealPlan { Name = "Midday Meal", MealType = "Lunch" };
        Assert.That(mealPlan.MealType, Is.EqualTo("Lunch"));
    }

    [Test]
    public void MealPlan_Dinner_SetsCorrectly()
    {
        var mealPlan = new MealPlan { Name = "Evening Meal", MealType = "Dinner" };
        Assert.That(mealPlan.MealType, Is.EqualTo("Dinner"));
    }

    [Test]
    public void MealPlan_Snack_SetsCorrectly()
    {
        var mealPlan = new MealPlan { Name = "Light Snack", MealType = "Snack" };
        Assert.That(mealPlan.MealType, Is.EqualTo("Snack"));
    }

    [Test]
    public void MealPlan_IsPrepared_SetsCorrectly()
    {
        var mealPlan = new MealPlan { Name = "Prepared Meal", IsPrepared = true };
        Assert.That(mealPlan.IsPrepared, Is.True);
    }

    [Test]
    public void MealPlan_WithNotes_SetsCorrectly()
    {
        var notes = "Add extra vegetables";
        var mealPlan = new MealPlan { Name = "Healthy Meal", Notes = notes };
        Assert.That(mealPlan.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void MealPlan_WithoutRecipe_AllowsNullRecipeId()
    {
        var mealPlan = new MealPlan { Name = "No Recipe Meal", MealType = "Lunch", RecipeId = null };
        Assert.That(mealPlan.RecipeId, Is.Null);
    }

    [Test]
    public void MealPlan_WithAllProperties_SetsAllCorrectly()
    {
        var mealPlan = new MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Complete Meal Plan",
            MealDate = new DateTime(2024, 7, 1),
            MealType = "Dinner",
            RecipeId = Guid.NewGuid(),
            Notes = "Special occasion",
            IsPrepared = false
        };

        Assert.Multiple(() =>
        {
            Assert.That(mealPlan.Name, Is.EqualTo("Complete Meal Plan"));
            Assert.That(mealPlan.MealDate, Is.EqualTo(new DateTime(2024, 7, 1)));
            Assert.That(mealPlan.MealType, Is.EqualTo("Dinner"));
            Assert.That(mealPlan.RecipeId, Is.Not.Null);
            Assert.That(mealPlan.Notes, Is.EqualTo("Special occasion"));
            Assert.That(mealPlan.IsPrepared, Is.False);
        });
    }
}
