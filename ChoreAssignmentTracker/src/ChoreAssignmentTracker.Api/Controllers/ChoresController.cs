// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Api.Features.Chores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChoreAssignmentTracker.Api.Controllers;

/// <summary>
/// Controller for managing chores.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ChoresController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ChoresController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChoresController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ChoresController(IMediator mediator, ILogger<ChoresController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all chores.
    /// </summary>
    /// <param name="userId">Optional user ID to filter by.</param>
    /// <param name="isActive">Optional active status to filter by.</param>
    /// <returns>A list of chores.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ChoreDto>>> GetChores([FromQuery] Guid? userId, [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting chores");

        var result = await _mediator.Send(new GetChores { UserId = userId, IsActive = isActive });
        return Ok(result);
    }

    /// <summary>
    /// Gets a chore by ID.
    /// </summary>
    /// <param name="id">The chore ID.</param>
    /// <returns>The chore.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ChoreDto>> GetChore(Guid id)
    {
        _logger.LogInformation("Getting chore {ChoreId}", id);

        var result = await _mediator.Send(new GetChoreById { ChoreId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new chore.
    /// </summary>
    /// <param name="dto">The chore data.</param>
    /// <returns>The created chore.</returns>
    [HttpPost]
    public async Task<ActionResult<ChoreDto>> CreateChore(CreateChoreDto dto)
    {
        _logger.LogInformation("Creating chore {Name}", dto.Name);

        var result = await _mediator.Send(new CreateChore { Chore = dto });
        return CreatedAtAction(nameof(GetChore), new { id = result.ChoreId }, result);
    }

    /// <summary>
    /// Updates a chore.
    /// </summary>
    /// <param name="id">The chore ID.</param>
    /// <param name="dto">The chore data.</param>
    /// <returns>The updated chore.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ChoreDto>> UpdateChore(Guid id, UpdateChoreDto dto)
    {
        _logger.LogInformation("Updating chore {ChoreId}", id);

        var result = await _mediator.Send(new UpdateChore { ChoreId = id, Chore = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a chore.
    /// </summary>
    /// <param name="id">The chore ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChore(Guid id)
    {
        _logger.LogInformation("Deleting chore {ChoreId}", id);

        var result = await _mediator.Send(new DeleteChore { ChoreId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
