// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Api.Features.Carpool;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KidsActivitySportsTracker.Api.Controllers;

/// <summary>
/// Controller for managing carpools.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarpoolsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CarpoolsController> _logger;

    public CarpoolsController(IMediator mediator, ILogger<CarpoolsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all carpools.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<CarpoolDto>>> GetCarpools([FromQuery] Guid? userId)
    {
        try
        {
            var query = new GetCarpoolsQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting carpools");
            return StatusCode(500, "An error occurred while retrieving carpools");
        }
    }

    /// <summary>
    /// Gets a carpool by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CarpoolDto>> GetCarpoolById(Guid id)
    {
        try
        {
            var query = new GetCarpoolByIdQuery { CarpoolId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound($"Carpool with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting carpool by ID {CarpoolId}", id);
            return StatusCode(500, "An error occurred while retrieving the carpool");
        }
    }

    /// <summary>
    /// Creates a new carpool.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CarpoolDto>> CreateCarpool([FromBody] CreateCarpoolCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCarpoolById), new { id = result.CarpoolId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating carpool");
            return StatusCode(500, "An error occurred while creating the carpool");
        }
    }

    /// <summary>
    /// Updates an existing carpool.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CarpoolDto>> UpdateCarpool(Guid id, [FromBody] UpdateCarpoolCommand command)
    {
        try
        {
            if (id != command.CarpoolId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Carpool not found for update: {CarpoolId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating carpool {CarpoolId}", id);
            return StatusCode(500, "An error occurred while updating the carpool");
        }
    }

    /// <summary>
    /// Deletes a carpool.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCarpool(Guid id)
    {
        try
        {
            var command = new DeleteCarpoolCommand { CarpoolId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Carpool not found for deletion: {CarpoolId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting carpool {CarpoolId}", id);
            return StatusCode(500, "An error occurred while deleting the carpool");
        }
    }
}
