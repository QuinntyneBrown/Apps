// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Controllers;
using CollegeSavingsPlanner.Api.Features.Contributions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CollegeSavingsPlanner.Api.Tests;

/// <summary>
/// Tests for ContributionsController.
/// </summary>
[TestFixture]
public class ContributionsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private Mock<ILogger<ContributionsController>> _loggerMock;
    private ContributionsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<ContributionsController>>();
        _controller = new ContributionsController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetContributions_ReturnsOkWithListOfContributions()
    {
        // Arrange
        var contributions = new List<ContributionDto>
        {
            new ContributionDto { ContributionId = Guid.NewGuid(), Amount = 500m },
            new ContributionDto { ContributionId = Guid.NewGuid(), Amount = 1000m }
        };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetContributionsQuery>(), default))
            .ReturnsAsync(contributions);

        // Act
        var result = await _controller.GetContributions();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(contributions));
    }

    [Test]
    public async Task GetContribution_WithValidId_ReturnsOkWithContribution()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var contribution = new ContributionDto { ContributionId = contributionId, Amount = 500m };
        _mediatorMock.Setup(m => m.Send(It.Is<GetContributionByIdQuery>(q => q.ContributionId == contributionId), default))
            .ReturnsAsync(contribution);

        // Act
        var result = await _controller.GetContribution(contributionId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(contribution));
    }

    [Test]
    public async Task GetContribution_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<GetContributionByIdQuery>(q => q.ContributionId == contributionId), default))
            .ReturnsAsync((ContributionDto?)null);

        // Act
        var result = await _controller.GetContribution(contributionId);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateContribution_ReturnsCreatedAtAction()
    {
        // Arrange
        var createContributionDto = new CreateContributionDto { Amount = 500m };
        var createdContribution = new ContributionDto { ContributionId = Guid.NewGuid(), Amount = 500m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateContributionCommand>(), default))
            .ReturnsAsync(createdContribution);

        // Act
        var result = await _controller.CreateContribution(createContributionDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult?.Value, Is.EqualTo(createdContribution));
    }

    [Test]
    public async Task UpdateContribution_WithValidId_ReturnsOkWithUpdatedContribution()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        var updateContributionDto = new UpdateContributionDto { Amount = 750m };
        var updatedContribution = new ContributionDto { ContributionId = contributionId, Amount = 750m };
        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateContributionCommand>(), default))
            .ReturnsAsync(updatedContribution);

        // Act
        var result = await _controller.UpdateContribution(contributionId, updateContributionDto);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult?.Value, Is.EqualTo(updatedContribution));
    }

    [Test]
    public async Task DeleteContribution_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteContributionCommand>(c => c.ContributionId == contributionId), default))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteContribution(contributionId);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Test]
    public async Task DeleteContribution_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var contributionId = Guid.NewGuid();
        _mediatorMock.Setup(m => m.Send(It.Is<DeleteContributionCommand>(c => c.ContributionId == contributionId), default))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteContribution(contributionId);

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
