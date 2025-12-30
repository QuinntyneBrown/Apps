// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Features.Usages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsagesController> _logger;

    public UsagesController(IMediator mediator, ILogger<UsagesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UsageDto>>> GetUsages([FromQuery] Guid? utilityBillId)
    {
        try
        {
            var query = new GetUsagesQuery { UtilityBillId = utilityBillId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting usages");
            return StatusCode(500, "An error occurred while retrieving usages");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsageDto>> GetUsageById(Guid id)
    {
        try
        {
            var query = new GetUsageByIdQuery { UsageId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Usage not found: {UsageId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting usage: {UsageId}", id);
            return StatusCode(500, "An error occurred while retrieving the usage");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UsageDto>> CreateUsage([FromBody] CreateUsageCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUsageById), new { id = result.UsageId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating usage");
            return StatusCode(500, "An error occurred while creating the usage");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsageDto>> UpdateUsage(Guid id, [FromBody] UpdateUsageCommand command)
    {
        try
        {
            if (id != command.UsageId)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Usage not found: {UsageId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating usage: {UsageId}", id);
            return StatusCode(500, "An error occurred while updating the usage");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUsage(Guid id)
    {
        try
        {
            var command = new DeleteUsageCommand { UsageId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Usage not found: {UsageId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting usage: {UsageId}", id);
            return StatusCode(500, "An error occurred while deleting the usage");
        }
    }
}
