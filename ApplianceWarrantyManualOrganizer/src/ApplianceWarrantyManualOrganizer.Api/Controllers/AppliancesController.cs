// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Api.Features.Appliances;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceWarrantyManualOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppliancesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AppliancesController> _logger;

    public AppliancesController(IMediator mediator, ILogger<AppliancesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ApplianceDto>>> GetAppliances([FromQuery] Guid? userId)
    {
        try
        {
            var query = new GetAppliances.Query { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appliances");
            return StatusCode(500, "An error occurred while getting appliances");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApplianceDto>> GetApplianceById(Guid id)
    {
        try
        {
            var query = new GetApplianceById.Query { ApplianceId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appliance {ApplianceId}", id);
            return StatusCode(500, "An error occurred while getting the appliance");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ApplianceDto>> CreateAppliance([FromBody] CreateAppliance.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetApplianceById), new { id = result.ApplianceId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating appliance");
            return StatusCode(500, "An error occurred while creating the appliance");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApplianceDto>> UpdateAppliance(Guid id, [FromBody] UpdateAppliance.Command command)
    {
        try
        {
            command.ApplianceId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appliance {ApplianceId}", id);
            return StatusCode(500, "An error occurred while updating the appliance");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAppliance(Guid id)
    {
        try
        {
            var command = new DeleteAppliance.Command { ApplianceId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appliance {ApplianceId}", id);
            return StatusCode(500, "An error occurred while deleting the appliance");
        }
    }
}
