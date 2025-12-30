// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// API controller for managing materials.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MaterialsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MaterialsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MaterialsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public MaterialsController(IMediator mediator, ILogger<MaterialsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all materials for a project.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <returns>The list of materials.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaterialDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetByProjectId([FromQuery] Guid projectId)
    {
        _logger.LogInformation("Getting materials for project {ProjectId}", projectId);

        var result = await _mediator.Send(new GetMaterialsByProjectIdQuery { ProjectId = projectId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new material.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created material.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MaterialDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaterialDto>> Create([FromBody] CreateMaterialCommand command)
    {
        _logger.LogInformation(
            "Creating material for project {ProjectId}",
            command.ProjectId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetByProjectId),
            new { projectId = result.ProjectId },
            result);
    }
}
