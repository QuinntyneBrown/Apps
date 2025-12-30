// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Controllers;
using GroceryShoppingListApp.Api.Features.GroceryItems;
using GroceryShoppingListApp.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GroceryShoppingListApp.Api.Tests.Controllers;

[TestFixture]
public class GroceryItemsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private GroceryItemsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new GroceryItemsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetGroceryItems_ReturnsOkResult_WithListOfGroceryItems()
    {
        // Arrange
        var groceryItems = new List<GroceryItemDto>
        {
            new GroceryItemDto { GroceryItemId = Guid.NewGuid(), Name = "Milk", Category = Category.Dairy },
            new GroceryItemDto { GroceryItemId = Guid.NewGuid(), Name = "Apples", Category = Category.Produce }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryItemsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryItems);

        // Act
        var result = await _controller.GetGroceryItems(null, null);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(groceryItems));
    }

    [Test]
    public async Task GetGroceryItem_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var groceryItemId = Guid.NewGuid();
        var groceryItem = new GroceryItemDto { GroceryItemId = groceryItemId, Name = "Bread" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryItemQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryItem);

        // Act
        var result = await _controller.GetGroceryItem(groceryItemId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(groceryItem));
    }

    [Test]
    public async Task GetGroceryItem_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryItemQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GroceryItemDto?)null);

        // Act
        var result = await _controller.GetGroceryItem(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateGroceryItem_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateGroceryItemCommand { Name = "Eggs", Category = Category.Dairy, Quantity = 12 };
        var groceryItem = new GroceryItemDto { GroceryItemId = Guid.NewGuid(), Name = "Eggs" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryItem);

        // Act
        var result = await _controller.CreateGroceryItem(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(groceryItem));
    }

    [Test]
    public async Task DeleteGroceryItem_ReturnsNoContent()
    {
        // Arrange
        var groceryItemId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroceryItemCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteGroceryItem(groceryItemId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
