// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Api.Controllers;
using DocumentVaultOrganizer.Api.Features.DocumentCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DocumentVaultOrganizer.Api.Tests.Controllers;

[TestFixture]
public class DocumentCategoriesControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<DocumentCategoriesController>> _loggerMock;
    private DocumentCategoriesController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<DocumentCategoriesController>>();
        _controller = new DocumentCategoriesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetDocumentCategories_ReturnsOkResult_WithListOfCategories()
    {
        // Arrange
        var categories = new List<DocumentCategoryDto>
        {
            new DocumentCategoryDto
            {
                DocumentCategoryId = Guid.NewGuid(),
                Name = "Test Category",
                Description = "Test Description",
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocumentCategories.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(categories);

        // Act
        var result = await _controller.GetDocumentCategories();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(categories));
    }

    [Test]
    public async Task GetDocumentCategoryById_ReturnsOkResult_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var category = new DocumentCategoryDto
        {
            DocumentCategoryId = categoryId,
            Name = "Test Category",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocumentCategoryById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        // Act
        var result = await _controller.GetDocumentCategoryById(categoryId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(category));
    }

    [Test]
    public async Task GetDocumentCategoryById_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocumentCategoryById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DocumentCategoryDto?)null);

        // Act
        var result = await _controller.GetDocumentCategoryById(categoryId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateDocumentCategory_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateDocumentCategory.Command
        {
            Name = "New Category",
            Description = "New Description"
        };

        var createdCategory = new DocumentCategoryDto
        {
            DocumentCategoryId = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdCategory);

        // Act
        var result = await _controller.CreateDocumentCategory(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdCategory));
    }

    [Test]
    public async Task UpdateDocumentCategory_ReturnsOkResult_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateDocumentCategory.Command
        {
            DocumentCategoryId = categoryId,
            Name = "Updated Category",
            Description = "Updated Description"
        };

        var updatedCategory = new DocumentCategoryDto
        {
            DocumentCategoryId = categoryId,
            Name = command.Name,
            Description = command.Description,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedCategory);

        // Act
        var result = await _controller.UpdateDocumentCategory(categoryId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedCategory));
    }

    [Test]
    public async Task UpdateDocumentCategory_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var command = new UpdateDocumentCategory.Command
        {
            DocumentCategoryId = Guid.NewGuid(), // Different ID
            Name = "Updated Category",
            Description = "Updated Description"
        };

        // Act
        var result = await _controller.UpdateDocumentCategory(categoryId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task DeleteDocumentCategory_ReturnsNoContent_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDocumentCategory.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteDocumentCategory(categoryId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteDocumentCategory_ReturnsNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDocumentCategory.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteDocumentCategory(categoryId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
