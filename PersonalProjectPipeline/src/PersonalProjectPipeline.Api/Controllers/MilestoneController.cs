// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalProjectPipeline.Api.Features.Milestone;

namespace PersonalProjectPipeline.Api.Controllers;

/// <summary>
/// Controller for managing milestones.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MilestoneController : ControllerBase
{
    private readonly IMediator _mediator;

    public MilestoneController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Gets all milestones.
    /// </summary>
    /// <param name="projectId">Optional project ID to filter milestones.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of milestones.</returns>
    [HttpGet]
    public async Task<ActionResult<List<MilestoneDto>>> GetMilestones(
        [FromQuery] Guid? projectId,
        CancellationToken cancellationToken)
    {
        var query = new GetMilestonesQuery { ProjectId = projectId };
        var milestones = await _mediator.Send(query, cancellationToken);
        return Ok(milestones);
    }

    /// <summary>
    /// Gets a milestone by ID.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The milestone.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<MilestoneDto>> GetMilestone(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMilestoneByIdQuery { MilestoneId = id };
        var milestone = await _mediator.Send(query, cancellationToken);

        if (milestone == null)
        {
            return NotFound();
        }

        return Ok(milestone);
    }

    /// <summary>
    /// Creates a new milestone.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created milestone.</returns>
    [HttpPost]
    public async Task<ActionResult<MilestoneDto>> CreateMilestone(
        CreateMilestoneCommand command,
        CancellationToken cancellationToken)
    {
        var milestone = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetMilestone), new { id = milestone.MilestoneId }, milestone);
    }

    /// <summary>
    /// Updates an existing milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated milestone.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<MilestoneDto>> UpdateMilestone(
        Guid id,
        UpdateMilestoneCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.MilestoneId)
        {
            return BadRequest("Milestone ID mismatch.");
        }

        try
        {
            var milestone = await _mediator.Send(command, cancellationToken);
            return Ok(milestone);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a milestone.
    /// </summary>
    /// <param name="id">The milestone ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMilestone(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteMilestoneCommand { MilestoneId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
