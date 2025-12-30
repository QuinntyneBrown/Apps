// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LetterToFutureSelf.Api;

/// <summary>
/// API controller for managing delivery schedules.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DeliverySchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeliverySchedulesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeliverySchedulesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public DeliverySchedulesController(IMediator mediator, ILogger<DeliverySchedulesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a delivery schedule by ID.
    /// </summary>
    /// <param name="id">The delivery schedule ID.</param>
    /// <returns>The delivery schedule.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryScheduleDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting delivery schedule {DeliveryScheduleId}", id);

        var result = await _mediator.Send(new GetDeliveryScheduleByIdQuery { DeliveryScheduleId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all delivery schedules for a letter.
    /// </summary>
    /// <param name="letterId">The letter ID.</param>
    /// <returns>The list of delivery schedules.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeliveryScheduleDto>>> GetByLetterId([FromQuery] Guid letterId)
    {
        _logger.LogInformation("Getting delivery schedules for letter {LetterId}", letterId);

        var result = await _mediator.Send(new GetDeliverySchedulesQuery { LetterId = letterId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new delivery schedule.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created delivery schedule.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeliveryScheduleDto>> Create([FromBody] CreateDeliveryScheduleCommand command)
    {
        _logger.LogInformation(
            "Creating delivery schedule for letter {LetterId}",
            command.LetterId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.DeliveryScheduleId },
            result);
    }

    /// <summary>
    /// Updates an existing delivery schedule.
    /// </summary>
    /// <param name="id">The delivery schedule ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated delivery schedule.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(DeliveryScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryScheduleDto>> Update(Guid id, [FromBody] UpdateDeliveryScheduleCommand command)
    {
        if (id != command.DeliveryScheduleId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating delivery schedule {DeliveryScheduleId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a delivery schedule.
    /// </summary>
    /// <param name="id">The delivery schedule ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting delivery schedule {DeliveryScheduleId}", id);

        var result = await _mediator.Send(new DeleteDeliveryScheduleCommand { DeliveryScheduleId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
