// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Api.Controllers;
using DocumentVaultOrganizer.Api.Features.Documents;
using DocumentVaultOrganizer.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DocumentVaultOrganizer.Api.Tests.Controllers;

[TestFixture]
public class DocumentsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<DocumentsController>> _loggerMock;
    private DocumentsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<DocumentsController>>();
        _controller = new DocumentsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetDocuments_ReturnsOkResult_WithListOfDocuments()
    {
        // Arrange
        var documents = new List<DocumentDto>
        {
            new DocumentDto
            {
                DocumentId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Name = "Test Document",
                Category = DocumentCategoryEnum.Personal,
                CreatedAt = DateTime.UtcNow
            }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocuments.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(documents);

        // Act
        var result = await _controller.GetDocuments(null);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(documents));
    }

    [Test]
    public async Task GetDocumentById_ReturnsOkResult_WhenDocumentExists()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var document = new DocumentDto
        {
            DocumentId = documentId,
            UserId = Guid.NewGuid(),
            Name = "Test Document",
            Category = DocumentCategoryEnum.Financial,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocumentById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(document);

        // Act
        var result = await _controller.GetDocumentById(documentId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(document));
    }

    [Test]
    public async Task GetDocumentById_ReturnsNotFound_WhenDocumentDoesNotExist()
    {
        // Arrange
        var documentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetDocumentById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((DocumentDto?)null);

        // Act
        var result = await _controller.GetDocumentById(documentId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateDocument_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateDocument.Command
        {
            UserId = Guid.NewGuid(),
            Name = "New Document",
            Category = DocumentCategoryEnum.Legal
        };

        var createdDocument = new DocumentDto
        {
            DocumentId = Guid.NewGuid(),
            UserId = command.UserId,
            Name = command.Name,
            Category = command.Category,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdDocument);

        // Act
        var result = await _controller.CreateDocument(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(createdDocument));
    }

    [Test]
    public async Task UpdateDocument_ReturnsOkResult_WhenDocumentExists()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var command = new UpdateDocument.Command
        {
            DocumentId = documentId,
            Name = "Updated Document",
            Category = DocumentCategoryEnum.Medical
        };

        var updatedDocument = new DocumentDto
        {
            DocumentId = documentId,
            UserId = Guid.NewGuid(),
            Name = command.Name,
            Category = command.Category,
            CreatedAt = DateTime.UtcNow
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedDocument);

        // Act
        var result = await _controller.UpdateDocument(documentId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(updatedDocument));
    }

    [Test]
    public async Task UpdateDocument_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var command = new UpdateDocument.Command
        {
            DocumentId = Guid.NewGuid(), // Different ID
            Name = "Updated Document",
            Category = DocumentCategoryEnum.Tax
        };

        // Act
        var result = await _controller.UpdateDocument(documentId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task UpdateDocument_ReturnsNotFound_WhenDocumentDoesNotExist()
    {
        // Arrange
        var documentId = Guid.NewGuid();
        var command = new UpdateDocument.Command
        {
            DocumentId = documentId,
            Name = "Updated Document",
            Category = DocumentCategoryEnum.Insurance
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((DocumentDto?)null);

        // Act
        var result = await _controller.UpdateDocument(documentId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task DeleteDocument_ReturnsNoContent_WhenDocumentExists()
    {
        // Arrange
        var documentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDocument.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteDocument(documentId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteDocument_ReturnsNotFound_WhenDocumentDoesNotExist()
    {
        // Arrange
        var documentId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeleteDocument.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteDocument(documentId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
