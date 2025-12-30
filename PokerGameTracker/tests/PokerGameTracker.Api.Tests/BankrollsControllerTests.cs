// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentAssertions;
using PokerGameTracker.Api.Controllers;
using PokerGameTracker.Api.Features.Bankrolls;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PokerGameTracker.Api.Tests;

public class BankrollsControllerTests
{
    private Mock<IMediator> _mockMediator;
    private BankrollsController _controller;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
        _controller = new BankrollsController(_mockMediator.Object);
    }

    [Test]
    public async Task GetBankrolls_ShouldReturnOkResultWithBankrolls()
    {
        // Arrange
        var bankrolls = new List<BankrollDto>
        {
            new BankrollDto
            {
                BankrollId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Amount = 5000m,
                RecordedDate = DateTime.UtcNow,
                Notes = "Initial bankroll"
            }
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetBankrollsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bankrolls);

        // Act
        var result = await _controller.GetBankrolls();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(bankrolls);
    }

    [Test]
    public async Task GetBankrollById_ShouldReturnOkResultWhenBankrollExists()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();
        var bankroll = new BankrollDto
        {
            BankrollId = bankrollId,
            UserId = Guid.NewGuid(),
            Amount = 6500m,
            RecordedDate = DateTime.UtcNow,
            Notes = "After big win"
        };

        _mockMediator.Setup(m => m.Send(It.IsAny<GetBankrollByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bankroll);

        // Act
        var result = await _controller.GetBankrollById(bankrollId);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult!.Value.Should().BeEquivalentTo(bankroll);
    }

    [Test]
    public async Task GetBankrollById_ShouldReturnNotFoundWhenBankrollDoesNotExist()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<GetBankrollByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BankrollDto?)null);

        // Act
        var result = await _controller.GetBankrollById(bankrollId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task CreateBankroll_ShouldReturnCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateBankrollCommand
        {
            UserId = Guid.NewGuid(),
            Amount = 10000m,
            RecordedDate = DateTime.UtcNow,
            Notes = "New bankroll record"
        };

        var createdBankroll = new BankrollDto
        {
            BankrollId = Guid.NewGuid(),
            UserId = command.UserId,
            Amount = command.Amount,
            RecordedDate = command.RecordedDate,
            Notes = command.Notes
        };

        _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdBankroll);

        // Act
        var result = await _controller.CreateBankroll(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult!.Value.Should().BeEquivalentTo(createdBankroll);
    }

    [Test]
    public async Task DeleteBankroll_ShouldReturnNoContentWhenBankrollExists()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteBankrollCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteBankroll(bankrollId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Test]
    public async Task DeleteBankroll_ShouldReturnNotFoundWhenBankrollDoesNotExist()
    {
        // Arrange
        var bankrollId = Guid.NewGuid();

        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteBankrollCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteBankroll(bankrollId);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
