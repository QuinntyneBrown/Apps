// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConversationStarterApp.Api;

/// <summary>
/// API controller for managing conversation starter prompts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PromptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PromptsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromptsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public PromptsController(IMediator mediator, ILogger<PromptsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a prompt by ID.
    /// </summary>
    /// <param name="id">The prompt ID.</param>
    /// <returns>The prompt.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PromptDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting prompt {PromptId}", id);

        var result = await _mediator.Send(new GetPromptByIdQuery { PromptId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets all prompts.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="category">Optional category filter.</param>
    /// <param name="depth">Optional depth filter.</param>
    /// <returns>The list of prompts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PromptDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PromptDto>>> GetAll(
        [FromQuery] Guid? userId = null,
        [FromQuery] int? category = null,
        [FromQuery] int? depth = null)
    {
        _logger.LogInformation("Getting prompts with filters - UserId: {UserId}, Category: {Category}, Depth: {Depth}", userId, category, depth);

        var result = await _mediator.Send(new GetPromptsQuery
        {
            UserId = userId,
            Category = category,
            Depth = depth,
        });

        return Ok(result);
    }

    /// <summary>
    /// Gets a random prompt based on optional filters.
    /// </summary>
    /// <param name="category">Optional category filter.</param>
    /// <param name="depth">Optional depth filter.</param>
    /// <returns>A random prompt.</returns>
    [HttpGet("random")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PromptDto>> GetRandom(
        [FromQuery] int? category = null,
        [FromQuery] int? depth = null)
    {
        _logger.LogInformation("Getting random prompt with filters - Category: {Category}, Depth: {Depth}", category, depth);

        var result = await _mediator.Send(new GetRandomPromptQuery
        {
            Category = category,
            Depth = depth,
        });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new prompt.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created prompt.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PromptDto>> Create([FromBody] CreatePromptCommand command)
    {
        _logger.LogInformation("Creating prompt for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.PromptId },
            result);
    }

    /// <summary>
    /// Updates an existing prompt.
    /// </summary>
    /// <param name="id">The prompt ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated prompt.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PromptDto>> Update(Guid id, [FromBody] UpdatePromptCommand command)
    {
        if (id != command.PromptId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating prompt {PromptId}", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a prompt.
    /// </summary>
    /// <param name="id">The prompt ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting prompt {PromptId}", id);

        var result = await _mediator.Send(new DeletePromptCommand { PromptId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Increments the usage count for a prompt.
    /// </summary>
    /// <param name="id">The prompt ID.</param>
    /// <returns>The updated prompt.</returns>
    [HttpPost("{id:guid}/use")]
    [ProducesResponseType(typeof(PromptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PromptDto>> IncrementUsage(Guid id)
    {
        _logger.LogInformation("Incrementing usage count for prompt {PromptId}", id);

        var result = await _mediator.Send(new IncrementPromptUsageCommand { PromptId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
