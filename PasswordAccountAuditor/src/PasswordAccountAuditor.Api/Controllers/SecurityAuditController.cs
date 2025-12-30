// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PasswordAccountAuditor.Api.Features.SecurityAudit;

namespace PasswordAccountAuditor.Api.Controllers;

/// <summary>
/// Controller for managing security audits.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SecurityAuditController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SecurityAuditController> _logger;

    public SecurityAuditController(IMediator mediator, ILogger<SecurityAuditController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all security audits.
    /// </summary>
    /// <returns>List of security audits.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<SecurityAuditDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SecurityAuditDto>>> GetSecurityAudits()
    {
        _logger.LogInformation("Getting all security audits");
        var securityAudits = await _mediator.Send(new GetSecurityAuditsQuery());
        return Ok(securityAudits);
    }

    /// <summary>
    /// Gets a security audit by ID.
    /// </summary>
    /// <param name="id">The security audit ID.</param>
    /// <returns>The security audit.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SecurityAuditDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SecurityAuditDto>> GetSecurityAuditById(Guid id)
    {
        _logger.LogInformation("Getting security audit with ID: {SecurityAuditId}", id);
        var securityAudit = await _mediator.Send(new GetSecurityAuditByIdQuery(id));

        if (securityAudit == null)
        {
            _logger.LogWarning("Security audit with ID {SecurityAuditId} not found", id);
            return NotFound();
        }

        return Ok(securityAudit);
    }

    /// <summary>
    /// Creates a new security audit.
    /// </summary>
    /// <param name="command">The create security audit command.</param>
    /// <returns>The created security audit.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SecurityAuditDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SecurityAuditDto>> CreateSecurityAudit([FromBody] CreateSecurityAuditCommand command)
    {
        _logger.LogInformation("Creating new security audit for account: {AccountId}", command.AccountId);
        var securityAudit = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSecurityAuditById), new { id = securityAudit.SecurityAuditId }, securityAudit);
    }

    /// <summary>
    /// Updates an existing security audit.
    /// </summary>
    /// <param name="id">The security audit ID.</param>
    /// <param name="command">The update security audit command.</param>
    /// <returns>The updated security audit.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SecurityAuditDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SecurityAuditDto>> UpdateSecurityAudit(Guid id, [FromBody] UpdateSecurityAuditCommand command)
    {
        if (id != command.SecurityAuditId)
        {
            _logger.LogWarning("Security audit ID mismatch: {UrlId} vs {CommandId}", id, command.SecurityAuditId);
            return BadRequest("Security audit ID mismatch");
        }

        _logger.LogInformation("Updating security audit with ID: {SecurityAuditId}", id);

        try
        {
            var securityAudit = await _mediator.Send(command);
            return Ok(securityAudit);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to update security audit with ID: {SecurityAuditId}", id);
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Deletes a security audit.
    /// </summary>
    /// <param name="id">The security audit ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSecurityAudit(Guid id)
    {
        _logger.LogInformation("Deleting security audit with ID: {SecurityAuditId}", id);

        try
        {
            await _mediator.Send(new DeleteSecurityAuditCommand(id));
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, "Failed to delete security audit with ID: {SecurityAuditId}", id);
            return NotFound(ex.Message);
        }
    }
}
