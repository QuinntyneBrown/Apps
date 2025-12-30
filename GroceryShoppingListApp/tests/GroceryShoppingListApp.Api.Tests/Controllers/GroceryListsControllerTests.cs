// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GroceryShoppingListApp.Api.Controllers;
using GroceryShoppingListApp.Api.Features.GroceryLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GroceryShoppingListApp.Api.Tests.Controllers;

[TestFixture]
public class GroceryListsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private GroceryListsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new GroceryListsController(_mediatorMock.Object);
    }

    [Test]
    public async Task GetGroceryLists_ReturnsOkResult_WithListOfGroceryLists()
    {
        // Arrange
        var groceryLists = new List<GroceryListDto>
        {
            new GroceryListDto { GroceryListId = Guid.NewGuid(), Name = "Weekly Shopping" },
            new GroceryListDto { GroceryListId = Guid.NewGuid(), Name = "Party Supplies" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryListsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryLists);

        // Act
        var result = await _controller.GetGroceryLists(null);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(groceryLists));
    }

    [Test]
    public async Task GetGroceryList_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var groceryList = new GroceryListDto { GroceryListId = groceryListId, Name = "Test List" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryList);

        // Act
        var result = await _controller.GetGroceryList(groceryListId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(groceryList));
    }

    [Test]
    public async Task GetGroceryList_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGroceryListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GroceryListDto?)null);

        // Act
        var result = await _controller.GetGroceryList(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreateGroceryList_ReturnsCreatedAtAction()
    {
        // Arrange
        var command = new CreateGroceryListCommand { Name = "New List", UserId = Guid.NewGuid() };
        var groceryList = new GroceryListDto { GroceryListId = Guid.NewGuid(), Name = "New List" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryList);

        // Act
        var result = await _controller.CreateGroceryList(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(groceryList));
    }

    [Test]
    public async Task UpdateGroceryList_WithMatchingIds_ReturnsOkResult()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        var command = new UpdateGroceryListCommand { GroceryListId = groceryListId, Name = "Updated List" };
        var groceryList = new GroceryListDto { GroceryListId = groceryListId, Name = "Updated List" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(groceryList);

        // Act
        var result = await _controller.UpdateGroceryList(groceryListId, command);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(groceryList));
    }

    [Test]
    public async Task UpdateGroceryList_WithMismatchedIds_ReturnsBadRequest()
    {
        // Arrange
        var command = new UpdateGroceryListCommand { GroceryListId = Guid.NewGuid(), Name = "Updated List" };

        // Act
        var result = await _controller.UpdateGroceryList(Guid.NewGuid(), command);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
    }

    [Test]
    public async Task DeleteGroceryList_ReturnsNoContent()
    {
        // Arrange
        var groceryListId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroceryListCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteGroceryList(groceryListId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
