// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WeeklyReviewSystem.Api.Controllers;
using WeeklyReviewSystem.Api.Features.Challenges;

namespace WeeklyReviewSystem.Api.Tests;

[TestFixture]
public class ChallengesControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<ChallengesController>> _loggerMock = null!;
    private ChallengesController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ChallengesController>>();
        _controller = new ChallengesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsOkResultWithChallenges()
    {
        // Arrange
        var challenges = new List<ChallengeDto>
        {
            new ChallengeDto { ChallengeId = Guid.NewGuid(), Title = "Challenge 1" },
            new ChallengeDto { ChallengeId = Guid.NewGuid(), Title = "Challenge 2" }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllChallengesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(challenges);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(challenges));
    }

    [Test]
    public async Task GetById_WithValidId_ReturnsOkResultWithChallenge()
    {
        // Arrange
        var challengeId = Guid.NewGuid();
        var challenge = new ChallengeDto { ChallengeId = challengeId, Title = "Test Challenge" };
        _mediatorMock.Setup(m => m.Send(It.Is<GetChallengeByIdQuery>(q => q.ChallengeId == challengeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(challenge);

        // Act
        var result = await _controller.GetById(challengeId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult!.Value, Is.EqualTo(challenge));
    }

    [Test]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var challengeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetChallengeByIdQuery>(q => q.ChallengeId == challengeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ChallengeDto?)null);

        // Act
        var result = await _controller.GetById(challengeId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Create_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var command = new CreateChallengeCommand
        {
            WeeklyReviewId = Guid.NewGuid(),
            Title = "New Challenge",
            Description = "Description"
        };
        var challenge = new ChallengeDto
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = command.WeeklyReviewId,
            Title = command.Title,
            Description = command.Description
        };
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(challenge);

        // Act
        var result = await _controller.Create(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult, Is.Not.Null);
        Assert.That(createdResult!.Value, Is.EqualTo(challenge));
    }

    [Test]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var challengeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteChallengeCommand>(c => c.ChallengeId == challengeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(challengeId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var challengeId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteChallengeCommand>(c => c.ChallengeId == challengeId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(challengeId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
