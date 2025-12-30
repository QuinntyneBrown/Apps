// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Features.DateIdeas;
using DateNightIdeaGenerator.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DateNightIdeaGenerator.Api.Controllers;

/// <summary>
/// API controller for managing date ideas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DateIdeasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DateIdeasController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateIdeasController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public DateIdeasController(IMediator mediator, ILogger<DateIdeasController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all date ideas.
    /// </summary>
    /// <param name="userId">Optional user ID filter.</param>
    /// <param name="category">Optional category filter.</param>
    /// <param name="budgetRange">Optional budget range filter.</param>
    /// <param name="favoritesOnly">Optional favorites filter.</param>
    /// <returns>A list of date ideas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<DateIdeaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DateIdeaDto>>> GetAll(
        [FromQuery] Guid? userId = null,
        [FromQuery] Category? category = null,
        [FromQuery] BudgetRange? budgetRange = null,
        [FromQuery] bool? favoritesOnly = null)
    {
        _logger.LogInformation("Getting all date ideas");

        var query = new GetAllDateIdeasQuery
        {
            UserId = userId,
            Category = category,
            BudgetRange = budgetRange,
            FavoritesOnly = favoritesOnly
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a date idea by ID.
    /// </summary>
    /// <param name="id">The date idea ID.</param>
    /// <returns>The date idea.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DateIdeaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DateIdeaDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting date idea with ID: {DateIdeaId}", id);

        var query = new GetDateIdeaByIdQuery { DateIdeaId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new date idea.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created date idea.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(DateIdeaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DateIdeaDto>> Create([FromBody] CreateDateIdeaCommand command)
    {
        _logger.LogInformation("Creating new date idea");

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.DateIdeaId }, result);
    }

    /// <summary>
    /// Updates an existing date idea.
    /// </summary>
    /// <param name="id">The date idea ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated date idea.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DateIdeaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DateIdeaDto>> Update(Guid id, [FromBody] UpdateDateIdeaCommand command)
    {
        _logger.LogInformation("Updating date idea with ID: {DateIdeaId}", id);

        if (id != command.DateIdeaId)
        {
            return BadRequest("ID mismatch");
        }

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a date idea.
    /// </summary>
    /// <param name="id">The date idea ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting date idea with ID: {DateIdeaId}", id);

        var command = new DeleteDateIdeaCommand { DateIdeaId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
