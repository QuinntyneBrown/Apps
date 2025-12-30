// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodPressureMonitor.Api;

/// <summary>
/// API controller for managing blood pressure readings.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReadingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReadingsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadingsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ReadingsController(IMediator mediator, ILogger<ReadingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a reading by ID.
    /// </summary>
    /// <param name="id">The reading ID.</param>
    /// <returns>The reading.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReadingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReadingDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting reading {ReadingId}", id);

        var result = await _mediator.Send(new GetReadingByIdQuery { ReadingId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all readings for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="startDate">Optional start date for filtering.</param>
    /// <param name="endDate">Optional end date for filtering.</param>
    /// <returns>The list of readings.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReadingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReadingDto>>> GetByUserId(
        [FromQuery] Guid userId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        _logger.LogInformation("Getting readings for user {UserId}", userId);

        var result = await _mediator.Send(new GetReadingsByUserIdQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    /// <summary>
    /// Gets critical readings for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="daysBack">The number of days to look back (default 30).</param>
    /// <returns>The list of critical readings.</returns>
    [HttpGet("critical")]
    [ProducesResponseType(typeof(IEnumerable<ReadingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReadingDto>>> GetCritical(
        [FromQuery] Guid userId,
        [FromQuery] int daysBack = 30)
    {
        _logger.LogInformation(
            "Getting critical readings for user {UserId} within {DaysBack} days",
            userId,
            daysBack);

        var result = await _mediator.Send(new GetCriticalReadingsQuery
        {
            UserId = userId,
            DaysBack = daysBack,
        });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new reading.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created reading.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ReadingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadingDto>> Create([FromBody] CreateReadingCommand command)
    {
        _logger.LogInformation(
            "Creating reading for user {UserId}",
            command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.ReadingId },
            result);
    }

    /// <summary>
    /// Updates an existing reading.
    /// </summary>
    /// <param name="id">The reading ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated reading.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ReadingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadingDto>> Update(Guid id, [FromBody] UpdateReadingCommand command)
    {
        if (id != command.ReadingId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating reading {ReadingId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a reading.
    /// </summary>
    /// <param name="id">The reading ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting reading {ReadingId}", id);

        var result = await _mediator.Send(new DeleteReadingCommand { ReadingId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
