// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Controllers;
using HomeBrewingTracker.Api.Features.Recipes;
using HomeBrewingTracker.Api.Features.Recipes.Commands;
using HomeBrewingTracker.Api.Features.Recipes.Queries;
using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeBrewingTracker.Api.Tests.Controllers;

/// <summary>
/// Tests for RecipesController.
/// </summary>
[TestFixture]
public class RecipesControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<RecipesController>> _loggerMock = null!;
    private RecipesController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RecipesController>>();
        _controller = new RecipesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetRecipes_ReturnsOkResult_WithListOfRecipes()
    {
        // Arrange
        var recipes = new List<RecipeDto>
        {
            new() { RecipeId = Guid.NewGuid(), Name = "Test Recipe 1", BeerStyle = BeerStyle.IPA },
            new() { RecipeId = Guid.NewGuid(), Name = "Test Recipe 2", BeerStyle = BeerStyle.Stout },
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetRecipesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipes);

        // Act
        var result = await _controller.GetRecipes(null, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(recipes));
    }

    [Test]
    public async Task GetRecipe_WithValidId_ReturnsOkResult_WithRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var recipe = new RecipeDto { RecipeId = recipeId, Name = "Test Recipe", BeerStyle = BeerStyle.IPA };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetRecipeByIdQuery>(q => q.RecipeId == recipeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipe);

        // Act
        var result = await _controller.GetRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(recipe));
    }

    [Test]
    public async Task GetRecipe_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetRecipeByIdQuery>(q => q.RecipeId == recipeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RecipeDto?)null);

        // Act
        var result = await _controller.GetRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateRecipe_ReturnsCreatedAtAction_WithRecipe()
    {
        // Arrange
        var command = new CreateRecipeCommand
        {
            UserId = Guid.NewGuid(),
            Name = "New Recipe",
            BeerStyle = BeerStyle.IPA,
            Description = "Test description",
        };

        var recipe = new RecipeDto
        {
            RecipeId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            BeerStyle = command.BeerStyle,
            Description = command.Description,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateRecipeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipe);

        // Act
        var result = await _controller.CreateRecipe(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(recipe));
    }

    [Test]
    public async Task UpdateRecipe_WithValidId_ReturnsOkResult_WithUpdatedRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var command = new UpdateRecipeCommand
        {
            RecipeId = recipeId,
            Name = "Updated Recipe",
            BeerStyle = BeerStyle.Stout,
        };

        var recipe = new RecipeDto
        {
            RecipeId = recipeId,
            Name = command.Name,
            BeerStyle = command.BeerStyle,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateRecipeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipe);

        // Act
        var result = await _controller.UpdateRecipe(recipeId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(recipe));
    }

    [Test]
    public async Task UpdateRecipe_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var command = new UpdateRecipeCommand
        {
            RecipeId = Guid.NewGuid(), // Different ID
            Name = "Updated Recipe",
        };

        // Act
        var result = await _controller.UpdateRecipe(recipeId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteRecipe_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteRecipeCommand>(c => c.RecipeId == recipeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteRecipe_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteRecipeCommand>(c => c.RecipeId == recipeId), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Recipe not found"));

        // Act
        var result = await _controller.DeleteRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
