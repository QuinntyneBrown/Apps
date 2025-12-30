// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Api.Features.SavingsTips;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeEnergyUsageTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SavingsTipsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SavingsTipsController> _logger;

    public SavingsTipsController(IMediator mediator, ILogger<SavingsTipsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<SavingsTipDto>>> GetSavingsTips()
    {
        try
        {
            var query = new GetSavingsTipsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting savings tips");
            return StatusCode(500, "An error occurred while retrieving savings tips");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SavingsTipDto>> GetSavingsTipById(Guid id)
    {
        try
        {
            var query = new GetSavingsTipByIdQuery { SavingsTipId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Savings tip not found: {SavingsTipId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting savings tip: {SavingsTipId}", id);
            return StatusCode(500, "An error occurred while retrieving the savings tip");
        }
    }

    [HttpPost]
    public async Task<ActionResult<SavingsTipDto>> CreateSavingsTip([FromBody] CreateSavingsTipCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSavingsTipById), new { id = result.SavingsTipId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating savings tip");
            return StatusCode(500, "An error occurred while creating the savings tip");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SavingsTipDto>> UpdateSavingsTip(Guid id, [FromBody] UpdateSavingsTipCommand command)
    {
        try
        {
            if (id != command.SavingsTipId)
            {
                return BadRequest("ID in URL does not match ID in request body");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Savings tip not found: {SavingsTipId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating savings tip: {SavingsTipId}", id);
            return StatusCode(500, "An error occurred while updating the savings tip");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSavingsTip(Guid id)
    {
        try
        {
            var command = new DeleteSavingsTipCommand { SavingsTipId = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Savings tip not found: {SavingsTipId}", id);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting savings tip: {SavingsTipId}", id);
            return StatusCode(500, "An error occurred while deleting the savings tip");
        }
    }
}
