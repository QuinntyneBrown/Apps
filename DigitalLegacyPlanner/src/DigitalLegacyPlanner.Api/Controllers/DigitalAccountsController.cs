// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.DigitalAccounts;
using DigitalLegacyPlanner.Api.Features.DigitalAccounts.Commands;
using DigitalLegacyPlanner.Api.Features.DigitalAccounts.Queries;
using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLegacyPlanner.Api.Controllers;

/// <summary>
/// Controller for managing digital accounts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DigitalAccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DigitalAccountsController> _logger;

    public DigitalAccountsController(IMediator mediator, ILogger<DigitalAccountsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all digital accounts.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="accountType">Optional account type filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of digital accounts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<DigitalAccountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DigitalAccountDto>>> GetDigitalAccounts(
        [FromQuery] Guid? userId,
        [FromQuery] AccountType? accountType,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting digital accounts for user {UserId}", userId);
        var query = new GetDigitalAccountsQuery { UserId = userId, AccountType = accountType };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a digital account by ID.
    /// </summary>
    /// <param name="id">Digital account ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The digital account.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DigitalAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DigitalAccountDto>> GetDigitalAccountById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting digital account {DigitalAccountId}", id);
        var query = new GetDigitalAccountByIdQuery { DigitalAccountId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new digital account.
    /// </summary>
    /// <param name="command">Create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created digital account.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DigitalAccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DigitalAccountDto>> CreateDigitalAccount(
        [FromBody] CreateDigitalAccountCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating digital account for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetDigitalAccountById), new { id = result.DigitalAccountId }, result);
    }

    /// <summary>
    /// Updates an existing digital account.
    /// </summary>
    /// <param name="id">Digital account ID.</param>
    /// <param name="command">Update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated digital account.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DigitalAccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DigitalAccountDto>> UpdateDigitalAccount(
        Guid id,
        [FromBody] UpdateDigitalAccountCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.DigitalAccountId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating digital account {DigitalAccountId}", id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a digital account.
    /// </summary>
    /// <param name="id">Digital account ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDigitalAccount(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting digital account {DigitalAccountId}", id);
        var command = new DeleteDigitalAccountCommand { DigitalAccountId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
