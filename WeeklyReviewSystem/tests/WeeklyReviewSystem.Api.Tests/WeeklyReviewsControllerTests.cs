// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Controllers;
using WeeklyReviewSystem.Api.Features.WeeklyReviews;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class WeeklyReviewsControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<WeeklyReviewsController>> _loggerMock = null!;
    private WeeklyReviewsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<WeeklyReviewsController>>();
        _controller = new WeeklyReviewsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithReviews()
    {
        // Arrange
        var reviews = new List<WeeklyReviewDto>
        {
            new WeeklyReviewDto { WeeklyReviewId = Guid.NewGuid(), UserId = Guid.NewGuid() },
            new WeeklyReviewDto { WeeklyReviewId = Guid.NewGuid(), UserId = Guid.NewGuid() }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllWeeklyReviewsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(reviews);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(reviews));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithReview()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var review = new WeeklyReviewDto { WeeklyReviewId = reviewId, UserId = Guid.NewGuid() };
        _mediatorMock.Setup(m => m.Send(It.Is<GetWeeklyReviewByIdQuery>(q => q.WeeklyReviewId == reviewId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(review);

        // Act
        var result = await _controller.GetById(reviewId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(review));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetWeeklyReviewByIdQuery>(q => q.WeeklyReviewId == reviewId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((WeeklyReviewDto?)null);

        // Act
        var result = await _controller.GetById(reviewId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateWeeklyReviewCommand
        {
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow,
            WeekEndDate = DateTime.UtcNow.AddDays(7)
        };
        var review = new WeeklyReviewDto
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = command.UserId,
            WeekStartDate = command.WeekStartDate,
            WeekEndDate = command.WeekEndDate
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(review);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.Value, Is.EqualTo(review));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var command = new UpdateWeeklyReviewCommand
        {
            WeeklyReviewId = reviewId,
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow,
            WeekEndDate = DateTime.UtcNow.AddDays(7)
        };
        var review = new WeeklyReviewDto
        {
            WeeklyReviewId = reviewId,
            UserId = command.UserId,
            WeekStartDate = command.WeekStartDate,
            WeekEndDate = command.WeekEndDate
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(review);

        // Act
        var result = await _controller.Update(reviewId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(review));
    }

    [Test]
    public async Task Update_WithMismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var urlId = Guid.NewGuid();
        var commandId = Guid.NewGuid();
        var command = new UpdateWeeklyReviewCommand { WeeklyReviewId = commandId };

        // Act
        var result = await _controller.Update(urlId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Update_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        var command = new UpdateWeeklyReviewCommand { WeeklyReviewId = reviewId };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((WeeklyReviewDto?)null);

        // Act
        var result = await _controller.Update(reviewId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWeeklyReviewCommand>(c => c.WeeklyReviewId == reviewId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(reviewId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var reviewId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteWeeklyReviewCommand>(c => c.WeeklyReviewId == reviewId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(reviewId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
