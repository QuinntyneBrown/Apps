// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Features.Ratings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DateNightIdeaGenerator.Api.Controllers;

/// <summary>
/// API controller for managing ratings.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RatingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RatingsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RatingsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public RatingsController(IMediator mediator, ILogger<RatingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all ratings.
    /// </summary>
    /// <param name="dateIdeaId">Optional date idea ID filter.</param>
    /// <param name="experienceId">Optional experience ID filter.</param>
    /// <param name="userId">Optional user ID filter.</param>
    /// <returns>A list of ratings.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<RatingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RatingDto>>> GetAll(
        [FromQuery] Guid? dateIdeaId = null,
        [FromQuery] Guid? experienceId = null,
        [FromQuery] Guid? userId = null)
    {
        _logger.LogInformation("Getting all ratings");

        var query = new GetAllRatingsQuery
        {
            DateIdeaId = dateIdeaId,
            ExperienceId = experienceId,
            UserId = userId
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a rating by ID.
    /// </summary>
    /// <param name="id">The rating ID.</param>
    /// <returns>The rating.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RatingDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting rating with ID: {RatingId}", id);

        var query = new GetRatingByIdQuery { RatingId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new rating.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created rating.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RatingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RatingDto>> Create([FromBody] CreateRatingCommand command)
    {
        _logger.LogInformation("Creating new rating");

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.RatingId }, result);
    }

    /// <summary>
    /// Updates an existing rating.
    /// </summary>
    /// <param name="id">The rating ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated rating.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(RatingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RatingDto>> Update(Guid id, [FromBody] UpdateRatingCommand command)
    {
        _logger.LogInformation("Updating rating with ID: {RatingId}", id);

        if (id != command.RatingId)
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
    /// Deletes a rating.
    /// </summary>
    /// <param name="id">The rating ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting rating with ID: {RatingId}", id);

        var command = new DeleteRatingCommand { RatingId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
