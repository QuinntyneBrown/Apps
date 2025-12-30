using BBQGrillingRecipeBook.Api.Controllers;
using BBQGrillingRecipeBook.Api.Features.Recipes;
using BBQGrillingRecipeBook.Api.Features.Techniques;
using BBQGrillingRecipeBook.Api.Features.CookSessions;
using BBQGrillingRecipeBook.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BBQGrillingRecipeBook.Api.Tests;

/// <summary>
/// Tests for RecipesController.
/// </summary>
[TestFixture]
public class RecipesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<RecipesController>> _loggerMock;
    private RecipesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RecipesController>>();
        _controller = new RecipesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetRecipes_ReturnsOkResultWithRecipes()
    {
        // Arrange
        var recipes = new List<RecipeDto>
        {
            new RecipeDto { RecipeId = Guid.NewGuid(), Name = "Grilled Chicken" },
            new RecipeDto { RecipeId = Guid.NewGuid(), Name = "BBQ Ribs" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecipesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipes);

        // Act
        var result = await _controller.GetRecipes(null, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<List<RecipeDto>>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(recipes, okResult.Value);
    }

    [Test]
    public async Task GetRecipeById_ExistingId_ReturnsOkResultWithRecipe()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        var recipe = new RecipeDto { RecipeId = recipeId, Name = "Grilled Chicken" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecipeByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(recipe);

        // Act
        var result = await _controller.GetRecipeById(recipeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<RecipeDto>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(recipe, okResult.Value);
    }

    [Test]
    public async Task GetRecipeById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetRecipeByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RecipeDto?)null);

        // Act
        var result = await _controller.GetRecipeById(recipeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<RecipeDto>>(result);
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateRecipe_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateRecipeCommand { Name = "New Recipe" };
        var createdRecipe = new RecipeDto { RecipeId = Guid.NewGuid(), Name = "New Recipe" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdRecipe);

        // Act
        var result = await _controller.CreateRecipe(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<RecipeDto>>(result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(201, createdResult.StatusCode);
        Assert.AreEqual(createdRecipe, createdResult.Value);
    }

    [Test]
    public async Task DeleteRecipe_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRecipeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task DeleteRecipe_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var recipeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRecipeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteRecipe(recipeId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}

/// <summary>
/// Tests for TechniquesController.
/// </summary>
[TestFixture]
public class TechniquesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<TechniquesController>> _loggerMock;
    private TechniquesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<TechniquesController>>();
        _controller = new TechniquesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetTechniques_ReturnsOkResultWithTechniques()
    {
        // Arrange
        var techniques = new List<TechniqueDto>
        {
            new TechniqueDto { TechniqueId = Guid.NewGuid(), Name = "Smoking" },
            new TechniqueDto { TechniqueId = Guid.NewGuid(), Name = "Direct Grilling" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTechniquesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(techniques);

        // Act
        var result = await _controller.GetTechniques(null, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<List<TechniqueDto>>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(techniques, okResult.Value);
    }

    [Test]
    public async Task GetTechniqueById_ExistingId_ReturnsOkResultWithTechnique()
    {
        // Arrange
        var techniqueId = Guid.NewGuid();
        var technique = new TechniqueDto { TechniqueId = techniqueId, Name = "Smoking" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTechniqueByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(technique);

        // Act
        var result = await _controller.GetTechniqueById(techniqueId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<TechniqueDto>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(technique, okResult.Value);
    }

    [Test]
    public async Task CreateTechnique_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateTechniqueCommand { Name = "New Technique" };
        var createdTechnique = new TechniqueDto { TechniqueId = Guid.NewGuid(), Name = "New Technique" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdTechnique);

        // Act
        var result = await _controller.CreateTechnique(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<TechniqueDto>>(result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(201, createdResult.StatusCode);
    }
}

/// <summary>
/// Tests for CookSessionsController.
/// </summary>
[TestFixture]
public class CookSessionsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<CookSessionsController>> _loggerMock;
    private CookSessionsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<CookSessionsController>>();
        _controller = new CookSessionsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetCookSessions_ReturnsOkResultWithSessions()
    {
        // Arrange
        var sessions = new List<CookSessionDto>
        {
            new CookSessionDto { CookSessionId = Guid.NewGuid(), RecipeId = Guid.NewGuid() },
            new CookSessionDto { CookSessionId = Guid.NewGuid(), RecipeId = Guid.NewGuid() }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCookSessionsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sessions);

        // Act
        var result = await _controller.GetCookSessions(null, null, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<List<CookSessionDto>>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(sessions, okResult.Value);
    }

    [Test]
    public async Task GetCookSessionById_ExistingId_ReturnsOkResultWithSession()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new CookSessionDto { CookSessionId = sessionId, RecipeId = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCookSessionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);

        // Act
        var result = await _controller.GetCookSessionById(sessionId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<CookSessionDto>>(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(session, okResult.Value);
    }

    [Test]
    public async Task CreateCookSession_ValidCommand_ReturnsCreatedResult()
    {
        // Arrange
        var command = new CreateCookSessionCommand { RecipeId = Guid.NewGuid() };
        var createdSession = new CookSessionDto { CookSessionId = Guid.NewGuid(), RecipeId = command.RecipeId };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdSession);

        // Act
        var result = await _controller.CreateCookSession(command, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<ActionResult<CookSessionDto>>(result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(201, createdResult.StatusCode);
    }

    [Test]
    public async Task DeleteCookSession_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCookSessionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCookSession(sessionId, CancellationToken.None);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
