// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Controllers;
using GroceryShoppingListApp.Api.Features.Stores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GroceryShoppingListApp.Api.Tests.Controllers;

[TestFixture]
public class StoresControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private StoresController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new StoresController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetStores_ReturnsOkResult_WithListOfStores()
    {
        // Arrange
        var stores = new List<StoreDto>
        {
            new StoreDto { StoreId = Guid.NewGuid(), Name = "Walmart", Address = "123 Main St" },
            new StoreDto { StoreId = Guid.NewGuid(), Name = "Target", Address = "456 Oak Ave" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetStoresQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(stores);

        // Act
        var result = await _controller.GetStores(null);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(stores));
    }

    [Test]
    public async Task GetStore_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var storeId = Guid.NewGuid();
        var store = new StoreDto { StoreId = storeId, Name = "Kroger" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetStoreQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(store);

        // Act
        var result = await _controller.GetStore(storeId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(store));
    }

    [Test]
    public async Task GetStore_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetStoreQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((StoreDto?)null);

        // Act
        var result = await _controller.GetStore(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateStore_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateStoreCommand { Name = "Whole Foods", Address = "789 Pine Rd", UserId = Guid.NewGuid() };
        var store = new StoreDto { StoreId = Guid.NewGuid(), Name = "Whole Foods" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(store);

        // Act
        var result = await _controller.CreateStore(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(store));
    }

    [Test]
    public async Task DeleteStore_ReturnsNoContent()
    {
        // Arrange
        var storeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteStoreCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteStore(storeId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
