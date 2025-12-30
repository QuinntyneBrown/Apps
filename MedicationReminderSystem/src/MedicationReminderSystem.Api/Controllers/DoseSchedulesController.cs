// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Api.Features.DoseSchedule;
using Microsoft.AspNetCore.Mvc;

namespace MedicationReminderSystem.Api.Controllers;

/// <summary>
/// Controller for managing dose schedules.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DoseSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DoseSchedulesController> _logger;

    public DoseSchedulesController(IMediator mediator, ILogger<DoseSchedulesController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all dose schedules.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DoseScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DoseScheduleDto>>> GetDoseSchedules(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDoseSchedulesQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a dose schedule by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DoseScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DoseScheduleDto>> GetDoseScheduleById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDoseScheduleByIdQuery(id), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new dose schedule.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DoseScheduleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DoseScheduleDto>> CreateDoseSchedule(
        [FromBody] CreateDoseScheduleCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDoseScheduleById), new { id = result.DoseScheduleId }, result);
    }

    /// <summary>
    /// Updates an existing dose schedule.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DoseScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DoseScheduleDto>> UpdateDoseSchedule(
        Guid id,
        [FromBody] UpdateDoseScheduleCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.DoseScheduleId)
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
            _logger.LogWarning(ex, "Dose schedule not found: {DoseScheduleId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a dose schedule.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDoseSchedule(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteDoseScheduleCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Dose schedule not found: {DoseScheduleId}", id);
            return NotFound(ex.Message);
        }
    }
}
