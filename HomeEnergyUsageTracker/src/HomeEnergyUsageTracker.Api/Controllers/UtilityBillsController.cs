// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Features.UtilityBills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilityBillsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UtilityBillsController> _logger;

    public UtilityBillsController(IMediator mediator, ILogger<UtilityBillsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UtilityBillDto>>> GetUtilityBills([FromQuery] Guid? userId)
    {
        try
        {
            var query = new GetUtilityBillsQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting utility bills");
            return StatusCode(500, "An error occurred while retrieving utility bills");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UtilityBillDto>> GetUtilityBillById(Guid id)
    {
        try
        {
            var query = new GetUtilityBillByIdQuery { UtilityBillId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Utility bill not found: {UtilityBillId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting utility bill: {UtilityBillId}", id);
            return StatusCode(500, "An error occurred while retrieving the utility bill");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UtilityBillDto>> CreateUtilityBill([FromBody] CreateUtilityBillCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUtilityBillById), new { id = result.UtilityBillId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating utility bill");
            return StatusCode(500, "An error occurred while creating the utility bill");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UtilityBillDto>> UpdateUtilityBill(Guid id, [FromBody] UpdateUtilityBillCommand command)
    {
        try
        {
            if (id != command.UtilityBillId)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Utility bill not found: {UtilityBillId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating utility bill: {UtilityBillId}", id);
            return StatusCode(500, "An error occurred while updating the utility bill");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUtilityBill(Guid id)
    {
        try
        {
            var command = new DeleteUtilityBillCommand { UtilityBillId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Utility bill not found: {UtilityBillId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting utility bill: {UtilityBillId}", id);
            return StatusCode(500, "An error occurred while deleting the utility bill");
        }
    }
}
