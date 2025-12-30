// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentAssertions;
using PokerGameTracker.Api.Controllers;
using PokerGameTracker.Api.Features.Sessions;
using PokerGameTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PokerGameTracker.Api.Tests;

public class SessionsControllerTests
{
    private Mock<IMediator> _mockMediator;
    private SessionsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new SessionsController(_mockMediator.Object);
    }

    [Test]
    public async Task GetSessions_ShouldReturnOkResultWithSessions()
    {
        // Arrange
        var sessions = new List<SessionDto>
        {
            new SessionDto
            {
                SessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                GameType = (int)GameType.TexasHoldem,
                StartTime = DateTime.UtcNow,
                BuyIn = 100m,
                Location = "Home Game"
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetSessionsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(sessions);

        // Act
        var result = await _controller.GetSessions();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(sessions);
    }

    [Test]
    public async Task GetSessionById_ShouldReturnOkResultWhenSessionExists()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var session = new SessionDto
        {
            SessionId = sessionId,
            UserId = Guid.NewGuid(),
            GameType = (int)GameType.OmahaHoldem,
            StartTime = DateTime.UtcNow,
            BuyIn = 200m,
            Location = "Casino"
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetSessionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);

        // Act
        var result = await _controller.GetSessionById(sessionId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(session);
    }

    [Test]
    public async Task GetSessionById_ShouldReturnNotFoundWhenSessionDoesNotExist()
    {
        // Arrange
        var sessionId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetSessionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((SessionDto?)null);

        // Act
        var result = await _controller.GetSessionById(sessionId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateSession_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateSessionCommand
        {
            UserId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem,
            StartTime = DateTime.UtcNow,
            BuyIn = 150m,
            Location = "Online"
        };

        var createdSession = new SessionDto
        {
            SessionId = Guid.NewGuid(),
            UserId = command.UserId,
            GameType = (int)command.GameType,
            StartTime = command.StartTime,
            BuyIn = command.BuyIn,
            Location = command.Location
        };

        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdSession);

        // Act
        var result = await _controller.CreateSession(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(createdSession);
    }

    [Test]
    public async Task DeleteSession_ShouldReturnNoContentWhenSessionExists()
    {
        // Arrange
        var sessionId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteSessionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteSession(sessionId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task DeleteSession_ShouldReturnNotFoundWhenSessionDoesNotExist()
    {
        // Arrange
        var sessionId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteSessionCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteSession(sessionId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
