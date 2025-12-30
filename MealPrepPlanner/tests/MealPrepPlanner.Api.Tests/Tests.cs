using MealPrepPlanner.Api.Features.MealPlans;
using MealPrepPlanner.Api.Features.Recipes;
using MealPrepPlanner.Api.Features.GroceryLists;
using MealPrepPlanner.Api.Features.Nutritions;

namespace MealPrepPlanner.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void MealPlanDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var mealPlan = new Core.MealPlan
        {
            MealPlanId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Weekly Plan",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            DailyCalorieTarget = 2000,
            DietaryPreferences = "Vegetarian",
            IsActive = true,
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = mealPlan.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.MealPlanId, Is.EqualTo(mealPlan.MealPlanId));
            Assert.That(dto.UserId, Is.EqualTo(mealPlan.UserId));
            Assert.That(dto.Name, Is.EqualTo(mealPlan.Name));
            Assert.That(dto.StartDate, Is.EqualTo(mealPlan.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(mealPlan.EndDate));
            Assert.That(dto.DailyCalorieTarget, Is.EqualTo(mealPlan.DailyCalorieTarget));
            Assert.That(dto.DietaryPreferences, Is.EqualTo(mealPlan.DietaryPreferences));
            Assert.That(dto.IsActive, Is.EqualTo(mealPlan.IsActive));
            Assert.That(dto.Notes, Is.EqualTo(mealPlan.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(mealPlan.CreatedAt));
            Assert.That(dto.Duration, Is.EqualTo(mealPlan.GetDuration()));
            Assert.That(dto.IsCurrentlyActive, Is.EqualTo(mealPlan.IsCurrentlyActive()));
        });
    }

    [Test]
    public void RecipeDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var recipe = new Core.Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MealPlanId = Guid.NewGuid(),
            Name = "Test Recipe",
            Description = "Test Description",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30,
            Servings = 4,
            Ingredients = "[\"ingredient1\", \"ingredient2\"]",
            Instructions = "Test Instructions",
            MealType = "Dinner",
            Tags = "quick,easy",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = recipe.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.RecipeId, Is.EqualTo(recipe.RecipeId));
            Assert.That(dto.UserId, Is.EqualTo(recipe.UserId));
            Assert.That(dto.MealPlanId, Is.EqualTo(recipe.MealPlanId));
            Assert.That(dto.Name, Is.EqualTo(recipe.Name));
            Assert.That(dto.Description, Is.EqualTo(recipe.Description));
            Assert.That(dto.PrepTimeMinutes, Is.EqualTo(recipe.PrepTimeMinutes));
            Assert.That(dto.CookTimeMinutes, Is.EqualTo(recipe.CookTimeMinutes));
            Assert.That(dto.Servings, Is.EqualTo(recipe.Servings));
            Assert.That(dto.Ingredients, Is.EqualTo(recipe.Ingredients));
            Assert.That(dto.Instructions, Is.EqualTo(recipe.Instructions));
            Assert.That(dto.MealType, Is.EqualTo(recipe.MealType));
            Assert.That(dto.Tags, Is.EqualTo(recipe.Tags));
            Assert.That(dto.IsFavorite, Is.EqualTo(recipe.IsFavorite));
            Assert.That(dto.CreatedAt, Is.EqualTo(recipe.CreatedAt));
            Assert.That(dto.TotalTime, Is.EqualTo(recipe.GetTotalTime()));
            Assert.That(dto.IsQuickMeal, Is.EqualTo(recipe.IsQuickMeal()));
        });
    }

    [Test]
    public void GroceryListDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var groceryList = new Core.GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MealPlanId = Guid.NewGuid(),
            Name = "Weekly Groceries",
            Items = "[\"item1\", \"item2\"]",
            ShoppingDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false,
            EstimatedCost = 150.00m,
            Notes = "Test Notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = groceryList.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.GroceryListId, Is.EqualTo(groceryList.GroceryListId));
            Assert.That(dto.UserId, Is.EqualTo(groceryList.UserId));
            Assert.That(dto.MealPlanId, Is.EqualTo(groceryList.MealPlanId));
            Assert.That(dto.Name, Is.EqualTo(groceryList.Name));
            Assert.That(dto.Items, Is.EqualTo(groceryList.Items));
            Assert.That(dto.ShoppingDate, Is.EqualTo(groceryList.ShoppingDate));
            Assert.That(dto.IsCompleted, Is.EqualTo(groceryList.IsCompleted));
            Assert.That(dto.EstimatedCost, Is.EqualTo(groceryList.EstimatedCost));
            Assert.That(dto.Notes, Is.EqualTo(groceryList.Notes));
            Assert.That(dto.CreatedAt, Is.EqualTo(groceryList.CreatedAt));
            Assert.That(dto.IsScheduledForToday, Is.EqualTo(groceryList.IsScheduledForToday()));
        });
    }

    [Test]
    public void NutritionDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var nutrition = new Core.Nutrition
        {
            NutritionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            Calories = 500,
            Protein = 30.5m,
            Carbohydrates = 45.0m,
            Fat = 20.0m,
            Fiber = 8.5m,
            Sugar = 10.0m,
            Sodium = 450.0m,
            AdditionalNutrients = "{\"vitamin_c\": 25}",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = nutrition.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.NutritionId, Is.EqualTo(nutrition.NutritionId));
            Assert.That(dto.UserId, Is.EqualTo(nutrition.UserId));
            Assert.That(dto.RecipeId, Is.EqualTo(nutrition.RecipeId));
            Assert.That(dto.Calories, Is.EqualTo(nutrition.Calories));
            Assert.That(dto.Protein, Is.EqualTo(nutrition.Protein));
            Assert.That(dto.Carbohydrates, Is.EqualTo(nutrition.Carbohydrates));
            Assert.That(dto.Fat, Is.EqualTo(nutrition.Fat));
            Assert.That(dto.Fiber, Is.EqualTo(nutrition.Fiber));
            Assert.That(dto.Sugar, Is.EqualTo(nutrition.Sugar));
            Assert.That(dto.Sodium, Is.EqualTo(nutrition.Sodium));
            Assert.That(dto.AdditionalNutrients, Is.EqualTo(nutrition.AdditionalNutrients));
            Assert.That(dto.CreatedAt, Is.EqualTo(nutrition.CreatedAt));
            Assert.That(dto.ProteinCaloriesPercentage, Is.EqualTo(nutrition.GetProteinCaloriesPercentage()));
            Assert.That(dto.IsHighProtein, Is.EqualTo(nutrition.IsHighProtein()));
        });
    }
}
