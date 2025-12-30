// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Controllers;
using GroceryShoppingListApp.Api.Features.PriceHistories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GroceryShoppingListApp.Api.Tests.Controllers;

[TestFixture]
public class PriceHistoriesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private PriceHistoriesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PriceHistoriesController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetPriceHistories_ReturnsOkResult_WithListOfPriceHistories()
    {
        // Arrange
        var priceHistories = new List<PriceHistoryDto>
        {
            new PriceHistoryDto { PriceHistoryId = Guid.NewGuid(), Price = 3.99m, Date = DateTime.UtcNow },
            new PriceHistoryDto { PriceHistoryId = Guid.NewGuid(), Price = 4.25m, Date = DateTime.UtcNow.AddDays(-7) }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPriceHistoriesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(priceHistories);

        // Act
        var result = await _controller.GetPriceHistories(null, null);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(priceHistories));
    }

    [Test]
    public async Task GetPriceHistory_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var priceHistoryId = Guid.NewGuid();
        var priceHistory = new PriceHistoryDto { PriceHistoryId = priceHistoryId, Price = 5.99m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPriceHistoryQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(priceHistory);

        // Act
        var result = await _controller.GetPriceHistory(priceHistoryId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(priceHistory));
    }

    [Test]
    public async Task GetPriceHistory_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPriceHistoryQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PriceHistoryDto?)null);

        // Act
        var result = await _controller.GetPriceHistory(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreatePriceHistory_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreatePriceHistoryCommand
        {
            GroceryItemId = Guid.NewGuid(),
            StoreId = Guid.NewGuid(),
            Price = 2.99m,
            Date = DateTime.UtcNow
        };
        var priceHistory = new PriceHistoryDto { PriceHistoryId = Guid.NewGuid(), Price = 2.99m };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(priceHistory);

        // Act
        var result = await _controller.CreatePriceHistory(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(priceHistory));
    }

    [Test]
    public async Task DeletePriceHistory_ReturnsNoContent()
    {
        // Arrange
        var priceHistoryId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePriceHistoryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeletePriceHistory(priceHistoryId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
