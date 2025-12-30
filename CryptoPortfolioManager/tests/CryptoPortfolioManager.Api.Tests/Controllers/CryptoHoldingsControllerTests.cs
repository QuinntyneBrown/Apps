// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CryptoPortfolioManager.Api.Controllers;
using CryptoPortfolioManager.Api.Features.CryptoHoldings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CryptoPortfolioManager.Api.Tests.Controllers;

[TestFixture]
public class CryptoHoldingsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private CryptoHoldingsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CryptoHoldingsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetCryptoHoldings_ReturnsOkResultWithHoldings()
    {
        // Arrange
        var holdings = new List<CryptoHoldingDto>
        {
            new CryptoHoldingDto { CryptoHoldingId = Guid.NewGuid(), Symbol = "BTC" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCryptoHoldingsQuery>(), default))
            .ReturnsAsync(holdings);

        // Act
        var result = await _controller.GetCryptoHoldings(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(holdings));
    }

    [Test]
    public async Task GetCryptoHolding_WithValidId_ReturnsOkResultWithHolding()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var holding = new CryptoHoldingDto { CryptoHoldingId = holdingId, Symbol = "BTC" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCryptoHoldingByIdQuery>(), default))
            .ReturnsAsync(holding);

        // Act
        var result = await _controller.GetCryptoHolding(holdingId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(holding));
    }

    [Test]
    public async Task GetCryptoHolding_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCryptoHoldingByIdQuery>(), default))
            .ReturnsAsync((CryptoHoldingDto?)null);

        // Act
        var result = await _controller.GetCryptoHolding(Guid.NewGuid());

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateCryptoHolding_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateCryptoHoldingCommand { Symbol = "BTC" };
        var holding = new CryptoHoldingDto { CryptoHoldingId = Guid.NewGuid(), Symbol = "BTC" };
        _mediatorMock.Setup(m => m.Send(command, default))
            .ReturnsAsync(holding);

        // Act
        var result = await _controller.CreateCryptoHolding(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(holding));
    }

    [Test]
    public async Task UpdateCryptoHolding_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var holdingId = Guid.NewGuid();
        var command = new UpdateCryptoHoldingCommand { Symbol = "ETH" };
        var holding = new CryptoHoldingDto { CryptoHoldingId = holdingId, Symbol = "ETH" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCryptoHoldingCommand>(), default))
            .ReturnsAsync(holding);

        // Act
        var result = await _controller.UpdateCryptoHolding(holdingId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task UpdateCryptoHolding_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var command = new UpdateCryptoHoldingCommand { Symbol = "ETH" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCryptoHoldingCommand>(), default))
            .ReturnsAsync((CryptoHoldingDto?)null);

        // Act
        var result = await _controller.UpdateCryptoHolding(Guid.NewGuid(), command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteCryptoHolding_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCryptoHoldingCommand>(), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCryptoHolding(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteCryptoHolding_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCryptoHoldingCommand>(), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteCryptoHolding(Guid.NewGuid());

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
