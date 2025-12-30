// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Features.Projections;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeSavingsPlanner.Api.Controllers;

/// <summary>
/// Controller for managing college savings projections.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProjectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectionsController> _logger;

    public ProjectionsController(IMediator mediator, ILogger<ProjectionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all projections, optionally filtered by plan ID.
    /// </summary>
    /// <param name="planId">Optional plan ID to filter by.</param>
    /// <returns>List of projections.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProjectionDto>>> GetProjections([FromQuery] Guid? planId = null)
    {
        _logger.LogInformation("Getting projections for plan {PlanId}", planId);
        var projections = await _mediator.Send(new GetProjectionsQuery { PlanId = planId });
        return Ok(projections);
    }

    /// <summary>
    /// Gets a projection by ID.
    /// </summary>
    /// <param name="id">The projection ID.</param>
    /// <returns>The projection.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectionDto>> GetProjection(Guid id)
    {
        _logger.LogInformation("Getting projection {ProjectionId}", id);
        var projection = await _mediator.Send(new GetProjectionByIdQuery { ProjectionId = id });

        if (projection == null)
        {
            _logger.LogWarning("Projection {ProjectionId} not found", id);
            return NotFound();
        }

        return Ok(projection);
    }

    /// <summary>
    /// Creates a new projection.
    /// </summary>
    /// <param name="projection">The projection to create.</param>
    /// <returns>The created projection.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProjectionDto>> CreateProjection(CreateProjectionDto projection)
    {
        _logger.LogInformation("Creating new projection: {ProjectionName}", projection.Name);
        var createdProjection = await _mediator.Send(new CreateProjectionCommand { Projection = projection });
        return CreatedAtAction(nameof(GetProjection), new { id = createdProjection.ProjectionId }, createdProjection);
    }

    /// <summary>
    /// Updates an existing projection.
    /// </summary>
    /// <param name="id">The projection ID.</param>
    /// <param name="projection">The updated projection data.</param>
    /// <returns>The updated projection.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectionDto>> UpdateProjection(Guid id, UpdateProjectionDto projection)
    {
        _logger.LogInformation("Updating projection {ProjectionId}", id);
        var updatedProjection = await _mediator.Send(new UpdateProjectionCommand { ProjectionId = id, Projection = projection });

        if (updatedProjection == null)
        {
            _logger.LogWarning("Projection {ProjectionId} not found for update", id);
            return NotFound();
        }

        return Ok(updatedProjection);
    }

    /// <summary>
    /// Deletes a projection.
    /// </summary>
    /// <param name="id">The projection ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProjection(Guid id)
    {
        _logger.LogInformation("Deleting projection {ProjectionId}", id);
        var deleted = await _mediator.Send(new DeleteProjectionCommand { ProjectionId = id });

        if (!deleted)
        {
            _logger.LogWarning("Projection {ProjectionId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
