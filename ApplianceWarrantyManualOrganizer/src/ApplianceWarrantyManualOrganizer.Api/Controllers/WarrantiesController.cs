// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Api.Features.Warranties;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceWarrantyManualOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarrantiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WarrantiesController> _logger;

    public WarrantiesController(IMediator mediator, ILogger<WarrantiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("appliance/{applianceId}")]
    public async Task<ActionResult<List<WarrantyDto>>> GetWarrantiesByAppliance(Guid applianceId)
    {
        try
        {
            var query = new GetWarrantiesByAppliance.Query { ApplianceId = applianceId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting warranties for appliance {ApplianceId}", applianceId);
            return StatusCode(500, "An error occurred while getting warranties");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WarrantyDto>> GetWarrantyById(Guid id)
    {
        try
        {
            var query = new GetWarrantyById.Query { WarrantyId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting warranty {WarrantyId}", id);
            return StatusCode(500, "An error occurred while getting the warranty");
        }
    }

    [HttpPost]
    public async Task<ActionResult<WarrantyDto>> CreateWarranty([FromBody] CreateWarranty.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWarrantyById), new { id = result.WarrantyId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating warranty");
            return StatusCode(500, "An error occurred while creating the warranty");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WarrantyDto>> UpdateWarranty(Guid id, [FromBody] UpdateWarranty.Command command)
    {
        try
        {
            command.WarrantyId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating warranty {WarrantyId}", id);
            return StatusCode(500, "An error occurred while updating the warranty");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteWarranty(Guid id)
    {
        try
        {
            var command = new DeleteWarranty.Command { WarrantyId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting warranty {WarrantyId}", id);
            return StatusCode(500, "An error occurred while deleting the warranty");
        }
    }
}
