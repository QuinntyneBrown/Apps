// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// API controller for managing milestones.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MilestonesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MilestonesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MilestonesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public MilestonesController(IMediator mediator, ILogger<MilestonesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a milestone by ID.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <returns>The milestone.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MilestoneDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting milestone {MilestoneId}", id);

        var result = await _mediator.Send(new GetMilestoneByIdQuery { MilestoneId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all milestones for an injury.
    /// </summary>
    /// <param name="injuryId">The injury ID.</param>
    /// <returns>The list of milestones.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MilestoneDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MilestoneDto>>> GetByInjuryId([FromQuery] Guid injuryId)
    {
        _logger.LogInformation("Getting milestones for injury {InjuryId}", injuryId);

        var result = await _mediator.Send(new GetMilestonesQuery { InjuryId = injuryId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new milestone.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created milestone.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MilestoneDto>> Create([FromBody] CreateMilestoneCommand command)
    {
        _logger.LogInformation(
            "Creating milestone for injury {InjuryId}",
            command.InjuryId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.MilestoneId },
            result);
    }

    /// <summary>
    /// Updates an existing milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated milestone.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(MilestoneDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MilestoneDto>> Update(Guid id, [FromBody] UpdateMilestoneCommand command)
    {
        if (id != command.MilestoneId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating milestone {MilestoneId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting milestone {MilestoneId}", id);

        var result = await _mediator.Send(new DeleteMilestoneCommand { MilestoneId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
