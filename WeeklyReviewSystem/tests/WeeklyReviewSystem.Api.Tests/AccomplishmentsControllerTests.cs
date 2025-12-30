// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Controllers;
using WeeklyReviewSystem.Api.Features.Accomplishments;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class AccomplishmentsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<AccomplishmentsController>> _loggerMock = null!;
    private AccomplishmentsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<AccomplishmentsController>>();
        _controller = new AccomplishmentsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithAccomplishments()
    {
        // Arrange
        var accomplishments = new List<AccomplishmentDto>
        {
            new AccomplishmentDto { AccomplishmentId = Guid.NewGuid(), Title = "Test 1" },
            new AccomplishmentDto { AccomplishmentId = Guid.NewGuid(), Title = "Test 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllAccomplishmentsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(accomplishments);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(accomplishments));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithAccomplishment()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        var accomplishment = new AccomplishmentDto { AccomplishmentId = accomplishmentId, Title = "Test" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetAccomplishmentByIdQuery>(q => q.AccomplishmentId == accomplishmentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(accomplishment);

        // Act
        var result = await _controller.GetById(accomplishmentId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(accomplishment));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetAccomplishmentByIdQuery>(q => q.AccomplishmentId == accomplishmentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AccomplishmentDto?)null);

        // Act
        var result = await _controller.GetById(accomplishmentId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateAccomplishmentCommand
        {
            WeeklyReviewId = Guid.NewGuid(),
            Title = "New Accomplishment",
            Description = "Description",
            ImpactLevel = 8
        };
        var accomplishment = new AccomplishmentDto
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = command.WeeklyReviewId,
            Title = command.Title,
            Description = command.Description,
            ImpactLevel = command.ImpactLevel
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accomplishment);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.Value, Is.EqualTo(accomplishment));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        var command = new UpdateAccomplishmentCommand
        {
            AccomplishmentId = accomplishmentId,
            WeeklyReviewId = Guid.NewGuid(),
            Title = "Updated Accomplishment"
        };
        var accomplishment = new AccomplishmentDto
        {
            AccomplishmentId = accomplishmentId,
            WeeklyReviewId = command.WeeklyReviewId,
            Title = command.Title
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accomplishment);

        // Act
        var result = await _controller.Update(accomplishmentId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(accomplishment));
    }

    [Test]
    public async Task Update_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateAccomplishmentCommand { AccomplishmentId = commandId };

        // Act
        var result = await _controller.Update(urlId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteAccomplishmentCommand>(c => c.AccomplishmentId == accomplishmentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(accomplishmentId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var accomplishmentId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteAccomplishmentCommand>(c => c.AccomplishmentId == accomplishmentId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(accomplishmentId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
