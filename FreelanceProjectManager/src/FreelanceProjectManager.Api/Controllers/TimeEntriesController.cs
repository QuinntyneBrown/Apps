// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Features.TimeEntries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProjectManager.Api.Controllers;

/// <summary>
/// Controller for managing time entries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TimeEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeEntriesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public TimeEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all time entries for the current user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="projectId">Optional project ID filter.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of time entries.</returns>
    [HttpGet]
    public async Task<ActionResult<List<TimeEntryDto>>> GetTimeEntries(
        [FromQuery] Guid userId,
        [FromQuery] Guid? projectId,
        CancellationToken cancellationToken)
    {
        var query = new GetTimeEntriesQuery { UserId = userId, ProjectId = projectId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a time entry by ID.
    /// </summary>
    /// <param name="id">The time entry ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The time entry.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TimeEntryDto>> GetTimeEntry(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetTimeEntryByIdQuery { TimeEntryId = id, UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new time entry.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created time entry.</returns>
    [HttpPost]
    public async Task<ActionResult<TimeEntryDto>> CreateTimeEntry([FromBody] CreateTimeEntryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTimeEntry), new { id = result.TimeEntryId, userId = result.UserId }, result);
    }

    /// <summary>
    /// Updates a time entry.
    /// </summary>
    /// <param name="id">The time entry ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated time entry.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<TimeEntryDto>> UpdateTimeEntry(Guid id, [FromBody] UpdateTimeEntryCommand command, CancellationToken cancellationToken)
    {
        if (id != command.TimeEntryId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a time entry.
    /// </summary>
    /// <param name="id">The time entry ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTimeEntry(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var command = new DeleteTimeEntryCommand { TimeEntryId = id, UserId = userId };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
