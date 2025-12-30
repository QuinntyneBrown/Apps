using RecipeManagerMealPlanner.Api.Features.Recipes;
using RecipeManagerMealPlanner.Core;
using Microsoft.Extensions.Logging;
using Moq;

namespace RecipeManagerMealPlanner.Api.Tests.Features.Recipes;

[TestFixture]
public class CreateRecipeCommandTests
{
    private Mock<IRecipeManagerMealPlannerContext> _contextMock = null!;
    private Mock<ILogger<CreateRecipeCommandHandler>> _loggerMock = null!;
    private CreateRecipeCommandHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IRecipeManagerMealPlannerContext>();
        _loggerMock = new Mock<ILogger<CreateRecipeCommandHandler>>();
        _handler = new CreateRecipeCommandHandler(_contextMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesRecipe()
    {
        // Arrange
        var command = new CreateRecipeCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Spaghetti Carbonara",
            Description = "Classic Italian pasta",
            Cuisine = Cuisine.Italian,
            DifficultyLevel = DifficultyLevel.Medium,
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Instructions = "Cook pasta, mix with sauce",
            PhotoUrl = "http://example.com/photo.jpg",
            Source = "Italian Cookbook",
            Notes = "Use fresh eggs",
        };

        var recipes = new List<Recipe>();
        var mockDbSet = TestHelpers.CreateMockDbSet(recipes);
        _contextMock.Setup(c => c.Recipes).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.EqualTo(command.Description));
        Assert.That(result.Cuisine, Is.EqualTo(command.Cuisine));
        Assert.That(result.DifficultyLevel, Is.EqualTo(command.DifficultyLevel));
        Assert.That(result.PrepTimeMinutes, Is.EqualTo(command.PrepTimeMinutes));
        Assert.That(result.CookTimeMinutes, Is.EqualTo(command.CookTimeMinutes));
        Assert.That(result.Servings, Is.EqualTo(command.Servings));
        Assert.That(result.Instructions, Is.EqualTo(command.Instructions));
        Assert.That(result.UserId, Is.EqualTo(command.UserId));
        Assert.That(result.IsFavorite, Is.False);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_MinimalCommand_CreatesRecipeWithDefaults()
    {
        // Arrange
        var command = new CreateRecipeCommand
        {
            UserId = Guid.NewGuid(),
            Name = "Simple Salad",
            Cuisine = Cuisine.American,
            DifficultyLevel = DifficultyLevel.Easy,
            PrepTimeMinutes = 5,
            CookTimeMinutes = 0,
            Servings = 2,
            Instructions = "Mix ingredients",
        };

        var recipes = new List<Recipe>();
        var mockDbSet = TestHelpers.CreateMockDbSet(recipes);
        _contextMock.Setup(c => c.Recipes).Returns(mockDbSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(command.Name));
        Assert.That(result.Description, Is.Null);
        Assert.That(result.PhotoUrl, Is.Null);
        Assert.That(result.Source, Is.Null);
        Assert.That(result.Notes, Is.Null);
        Assert.That(result.IsFavorite, Is.False);
    }
}
