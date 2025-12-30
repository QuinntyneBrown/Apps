// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Api.Features.Maintenance;
using HomeGymEquipmentManager.Api.Features.Maintenance.Commands;
using HomeGymEquipmentManager.Api.Features.Maintenance.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeGymEquipmentManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MaintenanceController> _logger;

    public MaintenanceController(IMediator mediator, ILogger<MaintenanceController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<MaintenanceDto>>> GetAll([FromQuery] Guid? userId, [FromQuery] Guid? equipmentId)
    {
        try
        {
            var query = new GetMaintenanceListQuery { UserId = userId, EquipmentId = equipmentId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting maintenance list");
            return StatusCode(500, "An error occurred while retrieving maintenance records");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaintenanceDto>> GetById(Guid id)
    {
        try
        {
            var query = new GetMaintenanceByIdQuery { MaintenanceId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Maintenance record with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting maintenance by ID {MaintenanceId}", id);
            return StatusCode(500, "An error occurred while retrieving maintenance record");
        }
    }

    [HttpPost]
    public async Task<ActionResult<MaintenanceDto>> Create([FromBody] CreateMaintenanceCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.MaintenanceId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating maintenance record");
            return StatusCode(500, "An error occurred while creating maintenance record");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MaintenanceDto>> Update(Guid id, [FromBody] UpdateMaintenanceCommand command)
    {
        try
        {
            if (id != command.MaintenanceId)
            {
                return BadRequest("Maintenance ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Maintenance record not found for update: {MaintenanceId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating maintenance record {MaintenanceId}", id);
            return StatusCode(500, "An error occurred while updating maintenance record");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteMaintenanceCommand { MaintenanceId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Maintenance record not found for deletion: {MaintenanceId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting maintenance record {MaintenanceId}", id);
            return StatusCode(500, "An error occurred while deleting maintenance record");
        }
    }
}
