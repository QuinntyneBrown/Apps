// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;
using AnnualHealthScreeningReminder.Api.Features.Appointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnnualHealthScreeningReminder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IMediator mediator, ILogger<AppointmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all appointments, optionally filtered by user ID or screening ID.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? userId, [FromQuery] Guid? screeningId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllAppointments.Query(userId, screeningId), cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all appointments");
            return StatusCode(500, "An error occurred while retrieving appointments.");
        }
    }

    /// <summary>
    /// Gets an appointment by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAppointmentById.Query(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Appointment not found: {AppointmentId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointment by ID: {AppointmentId}", id);
            return StatusCode(500, "An error occurred while retrieving the appointment.");
        }
    }

    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointment.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.AppointmentId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error creating appointment");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating appointment");
            return StatusCode(500, "An error occurred while creating the appointment.");
        }
    }

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointment.Command command, CancellationToken cancellationToken)
    {
        if (id != command.AppointmentId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Appointment not found: {AppointmentId}", id);
            return NotFound(ex.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            _logger.LogWarning(ex, "Validation error updating appointment");
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment: {AppointmentId}", id);
            return StatusCode(500, "An error occurred while updating the appointment.");
        }
    }

    /// <summary>
    /// Deletes an appointment.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteAppointment.Command(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Appointment not found: {AppointmentId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment: {AppointmentId}", id);
            return StatusCode(500, "An error occurred while deleting the appointment.");
        }
    }
}
