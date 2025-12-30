// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConversationStarterApp.Api;

/// <summary>
/// API controller for managing favorite prompts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FavoritesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FavoritesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public FavoritesController(IMediator mediator, ILogger<FavoritesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a favorite by ID.
    /// </summary>
    /// <param name="id">The favorite ID.</param>
    /// <returns>The favorite.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FavoriteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FavoriteDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting favorite {FavoriteId}", id);

        var result = await _mediator.Send(new GetFavoriteByIdQuery { FavoriteId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all favorites for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of favorites.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FavoriteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FavoriteDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting favorites for user {UserId}", userId);

        var result = await _mediator.Send(new GetFavoritesByUserIdQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new favorite.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created favorite.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(FavoriteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FavoriteDto>> Create([FromBody] CreateFavoriteCommand command)
    {
        _logger.LogInformation("Creating favorite for user {UserId}, prompt {PromptId}", command.UserId, command.PromptId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.FavoriteId },
            result);
    }

    /// <summary>
    /// Updates an existing favorite.
    /// </summary>
    /// <param name="id">The favorite ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated favorite.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(FavoriteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FavoriteDto>> Update(Guid id, [FromBody] UpdateFavoriteCommand command)
    {
        if (id != command.FavoriteId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating favorite {FavoriteId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a favorite.
    /// </summary>
    /// <param name="id">The favorite ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting favorite {FavoriteId}", id);

        var result = await _mediator.Send(new DeleteFavoriteCommand { FavoriteId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
