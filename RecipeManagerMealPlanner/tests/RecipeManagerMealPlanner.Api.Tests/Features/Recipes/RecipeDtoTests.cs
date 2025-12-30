using RecipeManagerMealPlanner.Api.Features.Recipes;
using RecipeManagerMealPlanner.Core;

namespace RecipeManagerMealPlanner.Api.Tests.Features.Recipes;

[TestFixture]
public class RecipeDtoTests
{
    [Test]
    public void ToDto_ValidRecipe_MapsAllProperties()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;

        var recipe = new Recipe
        {
            RecipeId = recipeId,
            UserId = userId,
            Name = "Test Recipe",
            Description = "Test Description",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Medium,
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30,
            Servings = 4,
            Instructions = "Test Instructions",
            PhotoUrl = "http://example.com/photo.jpg",
            Source = "Test Source",
            Rating = 5,
            Notes = "Test Notes",
            IsFavorite = true,
            CreatedAt = createdAt,
        };

        // Act
        var dto = recipe.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.RecipeId, Is.EqualTo(recipeId));
        Assert.That(dto.UserId, Is.EqualTo(userId));
        Assert.That(dto.Name, Is.EqualTo("Test Recipe"));
        Assert.That(dto.Description, Is.EqualTo("Test Description"));
        Assert.That(dto.Cuisine, Is.EqualTo(Cuisine.Italian));
        Assert.That(dto.DifficultyLevel, Is.EqualTo(DifficultyLevel.Medium));
        Assert.That(dto.PrepTimeMinutes, Is.EqualTo(15));
        Assert.That(dto.CookTimeMinutes, Is.EqualTo(30));
        Assert.That(dto.Servings, Is.EqualTo(4));
        Assert.That(dto.Instructions, Is.EqualTo("Test Instructions"));
        Assert.That(dto.PhotoUrl, Is.EqualTo("http://example.com/photo.jpg"));
        Assert.That(dto.Source, Is.EqualTo("Test Source"));
        Assert.That(dto.Rating, Is.EqualTo(5));
        Assert.That(dto.Notes, Is.EqualTo("Test Notes"));
        Assert.That(dto.IsFavorite, Is.True);
        Assert.That(dto.CreatedAt, Is.EqualTo(createdAt));
    }

    [Test]
    public void ToDto_RecipeWithNullableFields_MapsCorrectly()
    {
        // Arrange
        var recipe = new Recipe
        {
            RecipeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Simple Recipe",
            Cuisine = Cuisine.American,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 5,
            CookTimeMinutes = 10,
            Servings = 2,
            Instructions = "Cook it",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = recipe.ToDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
        Assert.That(dto.Description, Is.Null);
        Assert.That(dto.PhotoUrl, Is.Null);
        Assert.That(dto.Source, Is.Null);
        Assert.That(dto.Rating, Is.Null);
        Assert.That(dto.Notes, Is.Null);
        Assert.That(dto.IsFavorite, Is.False);
    }
}
