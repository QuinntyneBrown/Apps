// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// API controller for managing important dates.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DatesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public DatesController(IMediator mediator, ILogger<DatesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets an important date by ID.
    /// </summary>
    /// <param name="id">The important date ID.</param>
    /// <returns>The important date.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImportantDateDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting important date {ImportantDateId}", id);

        var result = await _mediator.Send(new GetDateByIdQuery { ImportantDateId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all important dates for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of important dates.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ImportantDateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ImportantDateDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting important dates for user {UserId}", userId);

        var result = await _mediator.Send(new GetDatesByUserIdQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Gets upcoming important dates for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="daysAhead">The number of days to look ahead (default 30).</param>
    /// <returns>The list of upcoming important dates.</returns>
    [HttpGet("upcoming")]
    [ProducesResponseType(typeof(IEnumerable<ImportantDateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ImportantDateDto>>> GetUpcoming(
        [FromQuery] Guid userId,
        [FromQuery] int daysAhead = 30)
    {
        _logger.LogInformation(
            "Getting upcoming dates for user {UserId} within {DaysAhead} days",
            userId,
            daysAhead);

        var result = await _mediator.Send(new GetUpcomingDatesQuery
        {
            UserId = userId,
            DaysAhead = daysAhead,
        });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new important date.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created important date.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ImportantDateDto>> Create([FromBody] CreateImportantDateCommand command)
    {
        _logger.LogInformation(
            "Creating important date for user {UserId}",
            command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.ImportantDateId },
            result);
    }

    /// <summary>
    /// Updates an existing important date.
    /// </summary>
    /// <param name="id">The important date ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated important date.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImportantDateDto>> Update(Guid id, [FromBody] UpdateImportantDateCommand command)
    {
        if (id != command.ImportantDateId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating important date {ImportantDateId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an important date.
    /// </summary>
    /// <param name="id">The important date ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting important date {ImportantDateId}", id);

        var result = await _mediator.Send(new DeleteImportantDateCommand { ImportantDateId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Toggles the active status of an important date.
    /// </summary>
    /// <param name="id">The important date ID.</param>
    /// <returns>The updated important date.</returns>
    [HttpPost("{id:guid}/toggle")]
    [ProducesResponseType(typeof(ImportantDateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ImportantDateDto>> ToggleActive(Guid id)
    {
        _logger.LogInformation("Toggling active status for important date {ImportantDateId}", id);

        var result = await _mediator.Send(new ToggleActiveDateCommand { ImportantDateId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
