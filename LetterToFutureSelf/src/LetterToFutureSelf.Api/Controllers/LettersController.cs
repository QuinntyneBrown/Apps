// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LetterToFutureSelf.Api;

/// <summary>
/// API controller for managing letters.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LettersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LettersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LettersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public LettersController(IMediator mediator, ILogger<LettersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a letter by ID.
    /// </summary>
    /// <param name="id">The letter ID.</param>
    /// <returns>The letter.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting letter {LetterId}", id);

        var result = await _mediator.Send(new GetLetterByIdQuery { LetterId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all letters for a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The list of letters.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LetterDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LetterDto>>> GetByUserId([FromQuery] Guid userId)
    {
        _logger.LogInformation("Getting letters for user {UserId}", userId);

        var result = await _mediator.Send(new GetLettersQuery { UserId = userId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new letter.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created letter.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LetterDto>> Create([FromBody] CreateLetterCommand command)
    {
        _logger.LogInformation(
            "Creating letter for user {UserId}",
            command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.LetterId },
            result);
    }

    /// <summary>
    /// Updates an existing letter.
    /// </summary>
    /// <param name="id">The letter ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated letter.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LetterDto>> Update(Guid id, [FromBody] UpdateLetterCommand command)
    {
        if (id != command.LetterId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating letter {LetterId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a letter.
    /// </summary>
    /// <param name="id">The letter ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting letter {LetterId}", id);

        var result = await _mediator.Send(new DeleteLetterCommand { LetterId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
