// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Api.Features.Experiences;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DateNightIdeaGenerator.Api.Controllers;

/// <summary>
/// API controller for managing experiences.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ExperiencesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ExperiencesController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExperiencesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ExperiencesController(IMediator mediator, ILogger<ExperiencesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all experiences.
    /// </summary>
    /// <param name="dateIdeaId">Optional date idea ID filter.</param>
    /// <param name="userId">Optional user ID filter.</param>
    /// <returns>A list of experiences.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ExperienceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ExperienceDto>>> GetAll(
        [FromQuery] Guid? dateIdeaId = null,
        [FromQuery] Guid? userId = null)
    {
        _logger.LogInformation("Getting all experiences");

        var query = new GetAllExperiencesQuery
        {
            DateIdeaId = dateIdeaId,
            UserId = userId
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets an experience by ID.
    /// </summary>
    /// <param name="id">The experience ID.</param>
    /// <returns>The experience.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ExperienceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ExperienceDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting experience with ID: {ExperienceId}", id);

        var query = new GetExperienceByIdQuery { ExperienceId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new experience.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created experience.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ExperienceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperienceDto>> Create([FromBody] CreateExperienceCommand command)
    {
        _logger.LogInformation("Creating new experience");

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.ExperienceId }, result);
    }

    /// <summary>
    /// Updates an existing experience.
    /// </summary>
    /// <param name="id">The experience ID.</param>
    /// <param name="command">The update command.</param>
    /// <returns>The updated experience.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ExperienceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExperienceDto>> Update(Guid id, [FromBody] UpdateExperienceCommand command)
    {
        _logger.LogInformation("Updating experience with ID: {ExperienceId}", id);

        if (id != command.ExperienceId)
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
    /// Deletes an experience.
    /// </summary>
    /// <param name="id">The experience ID.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting experience with ID: {ExperienceId}", id);

        var command = new DeleteExperienceCommand { ExperienceId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
