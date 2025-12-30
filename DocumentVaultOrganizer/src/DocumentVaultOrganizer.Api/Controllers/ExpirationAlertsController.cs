// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Api.Features.ExpirationAlerts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentVaultOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpirationAlertsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExpirationAlertsController> _logger;

    public ExpirationAlertsController(IMediator mediator, ILogger<ExpirationAlertsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpirationAlertDto>>> GetExpirationAlerts([FromQuery] bool? onlyUnacknowledged)
    {
        try
        {
            var query = new GetExpirationAlerts.Query { OnlyUnacknowledged = onlyUnacknowledged };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expiration alerts");
            return StatusCode(500, "An error occurred while retrieving expiration alerts");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpirationAlertDto>> GetExpirationAlertById(Guid id)
    {
        try
        {
            var query = new GetExpirationAlertById.Query { ExpirationAlertId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expiration alert {AlertId}", id);
            return StatusCode(500, "An error occurred while retrieving the expiration alert");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ExpirationAlertDto>> CreateExpirationAlert([FromBody] CreateExpirationAlert.Command command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetExpirationAlertById), new { id = result.ExpirationAlertId }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating expiration alert");
            return StatusCode(500, "An error occurred while creating the expiration alert");
        }
    }

    [HttpPut("{id}/acknowledge")]
    public async Task<ActionResult<ExpirationAlertDto>> AcknowledgeExpirationAlert(Guid id)
    {
        try
        {
            var command = new AcknowledgeExpirationAlert.Command { ExpirationAlertId = id };
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error acknowledging expiration alert {AlertId}", id);
            return StatusCode(500, "An error occurred while acknowledging the expiration alert");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteExpirationAlert(Guid id)
    {
        try
        {
            var command = new DeleteExpirationAlert.Command { ExpirationAlertId = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting expiration alert {AlertId}", id);
            return StatusCode(500, "An error occurred while deleting the expiration alert");
        }
    }
}
