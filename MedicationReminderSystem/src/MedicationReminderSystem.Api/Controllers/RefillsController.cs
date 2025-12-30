// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Api.Features.Refill;
using Microsoft.AspNetCore.Mvc;

namespace MedicationReminderSystem.Api.Controllers;

/// <summary>
/// Controller for managing refills.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RefillsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RefillsController> _logger;

    public RefillsController(IMediator mediator, ILogger<RefillsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all refills.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<RefillDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RefillDto>>> GetRefills(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRefillsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a refill by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RefillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RefillDto>> GetRefillById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRefillByIdQuery(id), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new refill.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RefillDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RefillDto>> CreateRefill(
        [FromBody] CreateRefillCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRefillById), new { id = result.RefillId }, result);
    }

    /// <summary>
    /// Updates an existing refill.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RefillDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RefillDto>> UpdateRefill(
        Guid id,
        [FromBody] UpdateRefillCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RefillId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Refill not found: {RefillId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a refill.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRefill(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteRefillCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Refill not found: {RefillId}", id);
            return NotFound(ex.Message);
        }
    }
}
