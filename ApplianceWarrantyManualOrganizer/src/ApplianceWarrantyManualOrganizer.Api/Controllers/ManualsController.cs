// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Api.Features.Manuals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApplianceWarrantyManualOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManualsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ManualsController> _logger;

    public ManualsController(IMediator mediator, ILogger<ManualsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("appliance/{applianceId}")]
    public async Task<ActionResult<List<ManualDto>>> GetManualsByAppliance(Guid applianceId)
    {
        try
        {
            var query = new GetManualsByAppliance.Query { ApplianceId = applianceId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting manuals for appliance {ApplianceId}", applianceId);
            return StatusCode(500, "An error occurred while getting manuals");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManualDto>> GetManualById(Guid id)
    {
        try
        {
            var query = new GetManualById.Query { ManualId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting manual {ManualId}", id);
            return StatusCode(500, "An error occurred while getting the manual");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ManualDto>> CreateManual([FromBody] CreateManual.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetManualById), new { id = result.ManualId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating manual");
            return StatusCode(500, "An error occurred while creating the manual");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ManualDto>> UpdateManual(Guid id, [FromBody] UpdateManual.Command command)
    {
        try
        {
            command.ManualId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating manual {ManualId}", id);
            return StatusCode(500, "An error occurred while updating the manual");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManual(Guid id)
    {
        try
        {
            var command = new DeleteManual.Command { ManualId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting manual {ManualId}", id);
            return StatusCode(500, "An error occurred while deleting the manual");
        }
    }
}
