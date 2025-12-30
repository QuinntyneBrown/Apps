// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Controllers;
using HomeBrewingTracker.Api.Features.Batches;
using HomeBrewingTracker.Api.Features.Batches.Commands;
using HomeBrewingTracker.Api.Features.Batches.Queries;
using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HomeBrewingTracker.Api.Tests.Controllers;

/// <summary>
/// Tests for BatchesController.
/// </summary>
[TestFixture]
public class BatchesControllerTests
{
    private Mock<IMediator> _mediatorMock = null!;
    private Mock<ILogger<BatchesController>> _loggerMock = null!;
    private BatchesController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<BatchesController>>();
        _controller = new BatchesController(_mediatorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetBatches_ReturnsOkResult_WithListOfBatches()
    {
        // Arrange
        var batches = new List<BatchDto>
        {
            new() { BatchId = Guid.NewGuid(), BatchNumber = "Batch-001", Status = BatchStatus.Fermenting },
            new() { BatchId = Guid.NewGuid(), BatchNumber = "Batch-002", Status = BatchStatus.Completed },
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetBatchesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(batches);

        // Act
        var result = await _controller.GetBatches(null, null, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(batches));
    }

    [Test]
    public async Task GetBatch_WithValidId_ReturnsOkResult_WithBatch()
    {
        // Arrange
        var batchId = Guid.NewGuid();
        var batch = new BatchDto { BatchId = batchId, BatchNumber = "Batch-001", Status = BatchStatus.Fermenting };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetBatchByIdQuery>(q => q.BatchId == batchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(batch);

        // Act
        var result = await _controller.GetBatch(batchId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(batch));
    }

    [Test]
    public async Task GetBatch_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var batchId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetBatchByIdQuery>(q => q.BatchId == batchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BatchDto?)null);

        // Act
        var result = await _controller.GetBatch(batchId, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task CreateBatch_ReturnsCreatedAtAction_WithBatch()
    {
        // Arrange
        var command = new CreateBatchCommand
        {
            UserId = Guid.NewGuid(),
            RecipeId = Guid.NewGuid(),
            BatchNumber = "Batch-001",
        };

        var batch = new BatchDto
        {
            BatchId = Guid.NewGuid(),
            UserId = command.UserId,
            RecipeId = command.RecipeId,
            BatchNumber = command.BatchNumber,
            Status = BatchStatus.Planned,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateBatchCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(batch);

        // Act
        var result = await _controller.CreateBatch(command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.That(createdResult!.Value, Is.EqualTo(batch));
    }

    [Test]
    public async Task UpdateBatch_WithValidId_ReturnsOkResult_WithUpdatedBatch()
    {
        // Arrange
        var batchId = Guid.NewGuid();
        var command = new UpdateBatchCommand
        {
            BatchId = batchId,
            BatchNumber = "Batch-001-Updated",
            Status = BatchStatus.Bottled,
        };

        var batch = new BatchDto
        {
            BatchId = batchId,
            BatchNumber = command.BatchNumber,
            Status = command.Status,
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateBatchCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(batch);

        // Act
        var result = await _controller.UpdateBatch(batchId, command, CancellationToken.None);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var okResult = result.Result as OkObjectResult;
        Assert.That(okResult!.Value, Is.EqualTo(batch));
    }

    [Test]
    public async Task DeleteBatch_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var batchId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeleteBatchCommand>(c => c.BatchId == batchId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.DeleteBatch(batchId, CancellationToken.None);

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }
}
