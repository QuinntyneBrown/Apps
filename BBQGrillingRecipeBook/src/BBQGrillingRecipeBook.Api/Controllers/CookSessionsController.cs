// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BBQGrillingRecipeBook.Api.Features.CookSessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BBQGrillingRecipeBook.Api.Controllers;

/// <summary>
/// Controller for managing cook sessions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CookSessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CookSessionsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookSessionsController"/> class.
    /// </summary>
    public CookSessionsController(IMediator mediator, ILogger<CookSessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all cook sessions.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="recipeId">Optional recipe ID filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of cook sessions.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CookSessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CookSessionDto>>> GetCookSessions(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? recipeId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all cook sessions");
        var query = new GetCookSessionsQuery { UserId = userId, RecipeId = recipeId };
        var sessions = await _mediator.Send(query, cancellationToken);
        return Ok(sessions);
    }

    /// <summary>
    /// Gets a cook session by ID.
    /// </summary>
    /// <param name="id">Cook session ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Cook session details.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CookSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CookSessionDto>> GetCookSessionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting cook session with ID: {CookSessionId}", id);
        var query = new GetCookSessionByIdQuery { CookSessionId = id };
        var session = await _mediator.Send(query, cancellationToken);

        if (session == null)
        {
            return NotFound();
        }

        return Ok(session);
    }

    /// <summary>
    /// Creates a new cook session.
    /// </summary>
    /// <param name="command">Create cook session command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Created cook session.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CookSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CookSessionDto>> CreateCookSession(
        [FromBody] CreateCookSessionCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new cook session for recipe: {RecipeId}", command.RecipeId);
        var session = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCookSessionById), new { id = session.CookSessionId }, session);
    }

    /// <summary>
    /// Updates an existing cook session.
    /// </summary>
    /// <param name="id">Cook session ID.</param>
    /// <param name="command">Update cook session command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated cook session.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CookSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CookSessionDto>> UpdateCookSession(
        Guid id,
        [FromBody] UpdateCookSessionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CookSessionId)
        {
            return BadRequest("Cook session ID mismatch");
        }

        _logger.LogInformation("Updating cook session with ID: {CookSessionId}", id);
        var session = await _mediator.Send(command, cancellationToken);

        if (session == null)
        {
            return NotFound();
        }

        return Ok(session);
    }

    /// <summary>
    /// Deletes a cook session.
    /// </summary>
    /// <param name="id">Cook session ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCookSession(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting cook session with ID: {CookSessionId}", id);
        var command = new DeleteCookSessionCommand { CookSessionId = id };
        var result = await _mediator.Send(command, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
