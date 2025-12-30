// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Api.Features.Equipment;
using HomeGymEquipmentManager.Api.Features.Equipment.Commands;
using HomeGymEquipmentManager.Api.Features.Equipment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeGymEquipmentManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EquipmentController> _logger;

    public EquipmentController(IMediator mediator, ILogger<EquipmentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<EquipmentDto>>> GetAll([FromQuery] Guid? userId, [FromQuery] bool? isActive)
    {
        try
        {
            var query = new GetEquipmentListQuery { UserId = userId, IsActive = isActive };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting equipment list");
            return StatusCode(500, "An error occurred while retrieving equipment");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetById(Guid id)
    {
        try
        {
            var query = new GetEquipmentByIdQuery { EquipmentId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Equipment with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting equipment by ID {EquipmentId}", id);
            return StatusCode(500, "An error occurred while retrieving equipment");
        }
    }

    [HttpPost]
    public async Task<ActionResult<EquipmentDto>> Create([FromBody] CreateEquipmentCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.EquipmentId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating equipment");
            return StatusCode(500, "An error occurred while creating equipment");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EquipmentDto>> Update(Guid id, [FromBody] UpdateEquipmentCommand command)
    {
        try
        {
            if (id != command.EquipmentId)
            {
                return BadRequest("Equipment ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Equipment not found for update: {EquipmentId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating equipment {EquipmentId}", id);
            return StatusCode(500, "An error occurred while updating equipment");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteEquipmentCommand { EquipmentId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Equipment not found for deletion: {EquipmentId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting equipment {EquipmentId}", id);
            return StatusCode(500, "An error occurred while deleting equipment");
        }
    }
}
