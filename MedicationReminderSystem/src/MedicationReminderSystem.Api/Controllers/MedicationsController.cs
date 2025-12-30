// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Api.Features.Medication;
using Microsoft.AspNetCore.Mvc;

namespace MedicationReminderSystem.Api.Controllers;

/// <summary>
/// Controller for managing medications.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MedicationsController> _logger;

    public MedicationsController(IMediator mediator, ILogger<MedicationsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all medications.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<MedicationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MedicationDto>>> GetMedications(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMedicationsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a medication by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicationDto>> GetMedicationById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMedicationByIdQuery(id), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new medication.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicationDto>> CreateMedication(
        [FromBody] CreateMedicationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetMedicationById), new { id = result.MedicationId }, result);
    }

    /// <summary>
    /// Updates an existing medication.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicationDto>> UpdateMedication(
        Guid id,
        [FromBody] UpdateMedicationCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.MedicationId)
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
            _logger.LogWarning(ex, "Medication not found: {MedicationId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a medication.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedication(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteMedicationCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Medication not found: {MedicationId}", id);
            return NotFound(ex.Message);
        }
    }
}
