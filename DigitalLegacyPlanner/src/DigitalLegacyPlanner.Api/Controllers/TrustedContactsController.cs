// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Api.Features.TrustedContacts;
using DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;
using DigitalLegacyPlanner.Api.Features.TrustedContacts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLegacyPlanner.Api.Controllers;

/// <summary>
/// Controller for managing trusted contacts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrustedContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TrustedContactsController> _logger;

    public TrustedContactsController(IMediator mediator, ILogger<TrustedContactsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all trusted contacts.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of trusted contacts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TrustedContactDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TrustedContactDto>>> GetTrustedContacts(
        [FromQuery] Guid? userId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trusted contacts for user {UserId}", userId);
        var query = new GetTrustedContactsQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a trusted contact by ID.
    /// </summary>
    /// <param name="id">Trusted contact ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The trusted contact.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TrustedContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrustedContactDto>> GetTrustedContactById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting trusted contact {TrustedContactId}", id);
        var query = new GetTrustedContactByIdQuery { TrustedContactId = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new trusted contact.
    /// </summary>
    /// <param name="command">Create command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created trusted contact.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TrustedContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrustedContactDto>> CreateTrustedContact(
        [FromBody] CreateTrustedContactCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating trusted contact for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTrustedContactById), new { id = result.TrustedContactId }, result);
    }

    /// <summary>
    /// Updates an existing trusted contact.
    /// </summary>
    /// <param name="id">Trusted contact ID.</param>
    /// <param name="command">Update command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated trusted contact.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TrustedContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrustedContactDto>> UpdateTrustedContact(
        Guid id,
        [FromBody] UpdateTrustedContactCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.TrustedContactId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating trusted contact {TrustedContactId}", id);
        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a trusted contact.
    /// </summary>
    /// <param name="id">Trusted contact ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrustedContact(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting trusted contact {TrustedContactId}", id);
        var command = new DeleteTrustedContactCommand { TrustedContactId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
