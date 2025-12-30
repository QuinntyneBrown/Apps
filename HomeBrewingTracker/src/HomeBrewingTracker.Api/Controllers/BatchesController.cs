// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Api.Features.Batches;
using HomeBrewingTracker.Api.Features.Batches.Commands;
using HomeBrewingTracker.Api.Features.Batches.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeBrewingTracker.Api.Controllers;

/// <summary>
/// Controller for managing batches.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BatchesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BatchesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BatchesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public BatchesController(IMediator mediator, ILogger<BatchesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all batches.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="recipeId">Optional recipe ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>List of batches.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<BatchDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BatchDto>>> GetBatches(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? recipeId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all batches");
        var query = new GetBatchesQuery { UserId = userId, RecipeId = recipeId };
        var batches = await _mediator.Send(query, cancellationToken);
        return Ok(batches);
    }

    /// <summary>
    /// Gets a batch by ID.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The batch.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BatchDto>> GetBatch(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting batch with ID: {BatchId}", id);
        var query = new GetBatchByIdQuery { BatchId = id };
        var batch = await _mediator.Send(query, cancellationToken);

        if (batch == null)
        {
            return NotFound();
        }

        return Ok(batch);
    }

    /// <summary>
    /// Creates a new batch.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created batch.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BatchDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BatchDto>> CreateBatch(
        [FromBody] CreateBatchCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new batch: {BatchNumber}", command.BatchNumber);
        var batch = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBatch), new { id = batch.BatchId }, batch);
    }

    /// <summary>
    /// Updates an existing batch.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated batch.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BatchDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BatchDto>> UpdateBatch(
        Guid id,
        [FromBody] UpdateBatchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.BatchId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating batch with ID: {BatchId}", id);

        try
        {
            var batch = await _mediator.Send(command, cancellationToken);
            return Ok(batch);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a batch.
    /// </summary>
    /// <param name="id">The batch ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBatch(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting batch with ID: {BatchId}", id);

        try
        {
            var command = new DeleteBatchCommand { BatchId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
