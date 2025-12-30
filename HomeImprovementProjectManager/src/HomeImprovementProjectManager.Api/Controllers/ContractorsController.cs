// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// API controller for managing contractors.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContractorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractorsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public ContractorsController(IMediator mediator, ILogger<ContractorsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all contractors for a project.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <returns>The list of contractors.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContractorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContractorDto>>> GetByProjectId([FromQuery] Guid projectId)
    {
        _logger.LogInformation("Getting contractors for project {ProjectId}", projectId);

        var result = await _mediator.Send(new GetContractorsByProjectIdQuery { ProjectId = projectId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new contractor.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created contractor.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ContractorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContractorDto>> Create([FromBody] CreateContractorCommand command)
    {
        _logger.LogInformation(
            "Creating contractor {Name}",
            command.Name);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetByProjectId),
            new { projectId = result.ProjectId },
            result);
    }
}
