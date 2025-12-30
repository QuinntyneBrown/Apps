// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VideoGameCollectionManager.Api.Controllers;
using VideoGameCollectionManager.Api.Features.Games;
using VideoGameCollectionManager.Core;

namespace VideoGameCollectionManager.Api.Tests.Controllers;

[TestFixture]
public class GamesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<GamesController>> _loggerMock;
    private GamesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<GamesController>>();
        _controller = new GamesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkWithGames()
    {
        // Arrange
        var games = new List<GameDto>
        {
            new GameDto { GameId = Guid.NewGuid(), Title = "Game 1", Platform = Platform.PC, Genre = Genre.Action, Status = CompletionStatus.InProgress },
            new GameDto { GameId = Guid.NewGuid(), Title = "Game 2", Platform = Platform.PlayStation5, Genre = Genre.RPG, Status = CompletionStatus.Completed }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(games);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(games));
    }

    [Test]
    public async Task GetById_ExistingId_ReturnsOkWithGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var game = new GameDto { GameId = gameId, Title = "Test Game", Platform = Platform.XboxSeriesX, Genre = Genre.Shooter, Status = CompletionStatus.NotStarted };
        _mediatorMock.Setup(m => m.Send(It.Is<GetGameByIdQuery>(q => q.GameId == gameId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(game);

        // Act
        var result = await _controller.GetById(gameId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(game));
    }

    [Test]
    public async Task GetById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetGameByIdQuery>(q => q.GameId == gameId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GameDto?)null);

        // Act
        var result = await _controller.GetById(gameId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ValidCommand_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateGameCommand
        {
            UserId = Guid.NewGuid(),
            Title = "New Game",
            Platform = Platform.NintendoSwitch,
            Genre = Genre.Adventure,
            Status = CompletionStatus.NotStarted
        };
        var createdGame = new GameDto { GameId = Guid.NewGuid(), Title = command.Title, Platform = command.Platform, Genre = command.Genre, Status = command.Status };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdGame);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdGame));
    }

    [Test]
    public async Task Update_ValidCommand_ReturnsOkWithUpdatedGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var command = new UpdateGameCommand
        {
            GameId = gameId,
            UserId = Guid.NewGuid(),
            Title = "Updated Game",
            Platform = Platform.PC,
            Genre = Genre.Strategy,
            Status = CompletionStatus.InProgress
        };
        var updatedGame = new GameDto { GameId = gameId, Title = command.Title, Platform = command.Platform, Genre = command.Genre, Status = command.Status };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedGame);

        // Act
        var result = await _controller.Update(gameId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedGame));
    }

    [Test]
    public async Task Update_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var command = new UpdateGameCommand { GameId = Guid.NewGuid() };

        // Act
        var result = await _controller.Update(gameId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_ExistingId_ReturnsNoContent()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteGameCommand>(c => c.GameId == gameId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(gameId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteGameCommand>(c => c.GameId == gameId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(gameId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
