// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Api.Controllers;
using ClassicCarRestorationLog.Api.Features.Parts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ClassicCarRestorationLog.Api.Tests.Controllers;

[TestFixture]
public class PartsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<PartsController>> _loggerMock;
    private PartsController _controller;

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<PartsController>>();
        _controller = new PartsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetParts_ReturnsOkResult_WithListOfParts()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var parts = new List<PartDto>
        {
            new PartDto { PartId = Guid.NewGuid(), ProjectId = projectId, Name = "Carburetor" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetParts.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(parts);

        // Act
        var result = await _controller.GetParts(projectId, null);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(parts, okResult.Value);
    }

    [Test]
    public async Task GetPart_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var partId = Guid.NewGuid();
        var part = new PartDto { PartId = partId, Name = "Carburetor" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPartById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(part);

        // Act
        var result = await _controller.GetPart(partId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(part, okResult.Value);
    }

    [Test]
    public async Task GetPart_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var partId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPartById.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PartDto?)null);

        // Act
        var result = await _controller.GetPart(partId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task CreatePart_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreatePart.Command
        {
            UserId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            Name = "Carburetor"
        };
        var part = new PartDto { PartId = Guid.NewGuid(), Name = "Carburetor" };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(part);

        // Act
        var result = await _controller.CreatePart(command);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(nameof(PartsController.GetPart), createdResult.ActionName);
        Assert.AreEqual(part, createdResult.Value);
    }

    [Test]
    public async Task DeletePart_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var partId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePart.Command>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeletePart(partId);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }
}
