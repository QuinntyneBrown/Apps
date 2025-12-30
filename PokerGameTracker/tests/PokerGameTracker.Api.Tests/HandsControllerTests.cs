// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentAssertions;
using PokerGameTracker.Api.Controllers;
using PokerGameTracker.Api.Features.Hands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PokerGameTracker.Api.Tests;

public class HandsControllerTests
{
    private Mock<IMediator> _mockMediator;
    private HandsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new HandsController(_mockMediator.Object);
    }

    [Test]
    public async Task GetHands_ShouldReturnOkResultWithHands()
    {
        // Arrange
        var hands = new List<HandDto>
        {
            new HandDto
            {
                HandId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                SessionId = Guid.NewGuid(),
                StartingCards = "AK",
                PotSize = 50m,
                WasWon = true
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetHandsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(hands);

        // Act
        var result = await _controller.GetHands();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(hands);
    }

    [Test]
    public async Task GetHandById_ShouldReturnOkResultWhenHandExists()
    {
        // Arrange
        var handId = Guid.NewGuid();
        var hand = new HandDto
        {
            HandId = handId,
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            StartingCards = "QQ",
            PotSize = 75m,
            WasWon = false
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetHandByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(hand);

        // Act
        var result = await _controller.GetHandById(handId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(hand);
    }

    [Test]
    public async Task GetHandById_ShouldReturnNotFoundWhenHandDoesNotExist()
    {
        // Arrange
        var handId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetHandByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((HandDto?)null);

        // Act
        var result = await _controller.GetHandById(handId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateHand_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateHandCommand
        {
            UserId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            StartingCards = "AA",
            PotSize = 100m,
            WasWon = true,
            Notes = "Great hand!"
        };

        var createdHand = new HandDto
        {
            HandId = Guid.NewGuid(),
            UserId = command.UserId,
            SessionId = command.SessionId,
            StartingCards = command.StartingCards,
            PotSize = command.PotSize,
            WasWon = command.WasWon,
            Notes = command.Notes
        };

        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdHand);

        // Act
        var result = await _controller.CreateHand(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(createdHand);
    }

    [Test]
    public async Task DeleteHand_ShouldReturnNoContentWhenHandExists()
    {
        // Arrange
        var handId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteHandCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteHand(handId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task DeleteHand_ShouldReturnNotFoundWhenHandDoesNotExist()
    {
        // Arrange
        var handId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteHandCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteHand(handId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
