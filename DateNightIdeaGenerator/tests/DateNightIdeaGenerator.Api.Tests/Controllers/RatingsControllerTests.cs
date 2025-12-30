// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Controllers;
using DateNightIdeaGenerator.Api.Features.Ratings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DateNightIdeaGenerator.Api.Tests.Controllers;

/// <summary>
/// Tests for the RatingsController.
/// </summary>
[TestFixture]
public class RatingsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<RatingsController>> _loggerMock;
    private RatingsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RatingsController>>();
        _controller = new RatingsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithRatings()
    {
        // Arrange
        var ratings = new List<RatingDto>
        {
            new RatingDto { RatingId = Guid.NewGuid(), Score = 5, Review = "Excellent!" },
            new RatingDto { RatingId = Guid.NewGuid(), Score = 4, Review = "Very good" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllRatingsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ratings);

        // Act
        var result = await _controller.GetAll();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(ratings));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithRating()
    {
        // Arrange
        var ratingId = Guid.NewGuid();
        var rating = new RatingDto { RatingId = ratingId, Score = 5, Review = "Excellent!" };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetRatingByIdQuery>(q => q.RatingId == ratingId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rating);

        // Act
        var result = await _controller.GetById(ratingId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(rating));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var ratingId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetRatingByIdQuery>(q => q.RatingId == ratingId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((RatingDto?)null);

        // Act
        var result = await _controller.GetById(ratingId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateRatingCommand
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Score = 5,
            Review = "Amazing date!"
        };

        var createdRating = new RatingDto
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = command.DateIdeaId,
            UserId = command.UserId,
            Score = command.Score,
            Review = command.Review
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdRating);

        // Act
        var result = await _controller.Create(command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdRating));
        Assert.That(createdResult?.ActionName, Is.EqualTo(nameof(RatingsController.GetById)));
    }

    [Test]
    public async Task Update_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var ratingId = Guid.NewGuid();
        var command = new UpdateRatingCommand
        {
            RatingId = ratingId,
            Score = 4,
            Review = "Updated review"
        };

        var updatedRating = new RatingDto
        {
            RatingId = ratingId,
            Score = command.Score,
            Review = command.Review
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedRating);

        // Act
        var result = await _controller.Update(ratingId, command);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedRating));
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var ratingId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteRatingCommand>(c => c.RatingId == ratingId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(ratingId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var ratingId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteRatingCommand>(c => c.RatingId == ratingId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(ratingId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
