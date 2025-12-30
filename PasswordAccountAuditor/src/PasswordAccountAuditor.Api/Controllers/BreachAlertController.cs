// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PasswordAccountAuditor.Api.Features.BreachAlert;

namespace PasswordAccountAuditor.Api.Controllers;

/// <summary>
/// Controller for managing breach alerts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BreachAlertController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BreachAlertController> _logger;

    public BreachAlertController(IMediator mediator, ILogger<BreachAlertController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all breach alerts.
    /// </summary>
    /// <returns>List of breach alerts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<BreachAlertDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BreachAlertDto>>> GetBreachAlerts()
    {
        _logger.LogInformation("Getting all breach alerts");
        var breachAlerts = await _mediator.Send(new GetBreachAlertsQuery());
        return Ok(breachAlerts);
    }

    /// <summary>
    /// Gets a breach alert by ID.
    /// </summary>
    /// <param name="id">The breach alert ID.</param>
    /// <returns>The breach alert.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BreachAlertDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreachAlertDto>> GetBreachAlertById(Guid id)
    {
        _logger.LogInformation("Getting breach alert with ID: {BreachAlertId}", id);
        var breachAlert = await _mediator.Send(new GetBreachAlertByIdQuery(id));

        if (breachAlert == null)
        {
            _logger.LogWarning("Breach alert with ID {BreachAlertId} not found", id);
            return NotFound();
        }

        return Ok(breachAlert);
    }

    /// <summary>
    /// Creates a new breach alert.
    /// </summary>
    /// <param name="command">The create breach alert command.</param>
    /// <returns>The created breach alert.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BreachAlertDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BreachAlertDto>> CreateBreachAlert([FromBody] CreateBreachAlertCommand command)
    {
        _logger.LogInformation("Creating new breach alert for account: {AccountId}", command.AccountId);
        var breachAlert = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBreachAlertById), new { id = breachAlert.BreachAlertId }, breachAlert);
    }

    /// <summary>
    /// Updates an existing breach alert.
    /// </summary>
    /// <param name="id">The breach alert ID.</param>
    /// <param name="command">The update breach alert command.</param>
    /// <returns>The updated breach alert.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BreachAlertDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreachAlertDto>> UpdateBreachAlert(Guid id, [FromBody] UpdateBreachAlertCommand command)
    {
        if (id != command.BreachAlertId)
        {
            _logger.LogWarning("Breach alert ID mismatch: {UrlId} vs {CommandId}", id, command.BreachAlertId);
            return BadRequest("Breach alert ID mismatch");
        }

        _logger.LogInformation("Updating breach alert with ID: {BreachAlertId}", id);

        try
        {
            var breachAlert = await _mediator.Send(command);
            return Ok(breachAlert);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to update breach alert with ID: {BreachAlertId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a breach alert.
    /// </summary>
    /// <param name="id">The breach alert ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBreachAlert(Guid id)
    {
        _logger.LogInformation("Deleting breach alert with ID: {BreachAlertId}", id);

        try
        {
            await _mediator.Send(new DeleteBreachAlertCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to delete breach alert with ID: {BreachAlertId}", id);
            return NotFound(ex.Message);
        }
    }
}
