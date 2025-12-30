// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Api.Features.Clients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceProjectManager.Api.Controllers;

/// <summary>
/// Controller for managing clients.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public ClientsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all clients for the current user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of clients.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetClients([FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetClientsQuery { UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a client by ID.
    /// </summary>
    /// <param name="id">The client ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The client.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClient(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetClientByIdQuery { ClientId = id, UserId = userId };
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new client.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created client.</returns>
    [HttpPost]
    public async Task<ActionResult<ClientDto>> CreateClient([FromBody] CreateClientCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetClient), new { id = result.ClientId, userId = result.UserId }, result);
    }

    /// <summary>
    /// Updates a client.
    /// </summary>
    /// <param name="id">The client ID.</param>
    /// <param name="command">The update command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated client.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ClientDto>> UpdateClient(Guid id, [FromBody] UpdateClientCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ClientId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a client.
    /// </summary>
    /// <param name="id">The client ID.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClient(Guid id, [FromQuery] Guid userId, CancellationToken cancellationToken)
    {
        var command = new DeleteClientCommand { ClientId = id, UserId = userId };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
