// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Api.Features.WorkoutMapping;
using HomeGymEquipmentManager.Api.Features.WorkoutMapping.Commands;
using HomeGymEquipmentManager.Api.Features.WorkoutMapping.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeGymEquipmentManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkoutMappingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WorkoutMappingController> _logger;

    public WorkoutMappingController(IMediator mediator, ILogger<WorkoutMappingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkoutMappingDto>>> GetAll([FromQuery] Guid? userId, [FromQuery] Guid? equipmentId, [FromQuery] bool? isFavorite)
    {
        try
        {
            var query = new GetWorkoutMappingListQuery { UserId = userId, EquipmentId = equipmentId, IsFavorite = isFavorite };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting workout mapping list");
            return StatusCode(500, "An error occurred while retrieving workout mappings");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutMappingDto>> GetById(Guid id)
    {
        try
        {
            var query = new GetWorkoutMappingByIdQuery { WorkoutMappingId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Workout mapping with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting workout mapping by ID {WorkoutMappingId}", id);
            return StatusCode(500, "An error occurred while retrieving workout mapping");
        }
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutMappingDto>> Create([FromBody] CreateWorkoutMappingCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.WorkoutMappingId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating workout mapping");
            return StatusCode(500, "An error occurred while creating workout mapping");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WorkoutMappingDto>> Update(Guid id, [FromBody] UpdateWorkoutMappingCommand command)
    {
        try
        {
            if (id != command.WorkoutMappingId)
            {
                return BadRequest("Workout mapping ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Workout mapping not found for update: {WorkoutMappingId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating workout mapping {WorkoutMappingId}", id);
            return StatusCode(500, "An error occurred while updating workout mapping");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var command = new DeleteWorkoutMappingCommand { WorkoutMappingId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Workout mapping not found for deletion: {WorkoutMappingId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting workout mapping {WorkoutMappingId}", id);
            return StatusCode(500, "An error occurred while deleting workout mapping");
        }
    }
}
