// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Api.Features.Contacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceEventManager.Api.Controllers;

/// <summary>
/// Controller for managing contacts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContactsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactsController"/> class.
    /// </summary>
    public ContactsController(IMediator mediator, ILogger<ContactsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all contacts, optionally filtered by event ID or user ID.
    /// </summary>
    /// <param name="eventId">Optional event ID to filter contacts.</param>
    /// <param name="userId">Optional user ID to filter contacts.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of contacts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ContactDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ContactDto>>> GetContacts([FromQuery] Guid? eventId, [FromQuery] Guid? userId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetContacts.Query { EventId = eventId, UserId = userId };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contacts");
            return StatusCode(500, "An error occurred while retrieving contacts");
        }
    }

    /// <summary>
    /// Gets a contact by ID.
    /// </summary>
    /// <param name="id">Contact ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Contact details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> GetContact(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetContactById.Query { ContactId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Contact with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving contact {ContactId}", id);
            return StatusCode(500, "An error occurred while retrieving the contact");
        }
    }

    /// <summary>
    /// Creates a new contact.
    /// </summary>
    /// <param name="command">Create contact command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created contact.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactDto>> CreateContact([FromBody] CreateContact.Command command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetContact), new { id = result.ContactId }, result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating contact");
            return StatusCode(500, "An error occurred while creating the contact");
        }
    }

    /// <summary>
    /// Updates an existing contact.
    /// </summary>
    /// <param name="id">Contact ID.</param>
    /// <param name="command">Update contact command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated contact.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> UpdateContact(Guid id, [FromBody] UpdateContact.Command command, CancellationToken cancellationToken)
    {
        if (id != command.ContactId)
        {
            return BadRequest("Contact ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Contact with ID {id} not found");
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating contact {ContactId}", id);
            return StatusCode(500, "An error occurred while updating the contact");
        }
    }

    /// <summary>
    /// Deletes a contact.
    /// </summary>
    /// <param name="id">Contact ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteContact.Command { ContactId = id };
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"Contact with ID {id} not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting contact {ContactId}", id);
            return StatusCode(500, "An error occurred while deleting the contact");
        }
    }
}
