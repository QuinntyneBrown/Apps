// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// API controller for managing budgets.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BudgetsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BudgetsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public BudgetsController(IMediator mediator, ILogger<BudgetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all budgets for a project.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <returns>The list of budgets.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BudgetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetByProjectId([FromQuery] Guid projectId)
    {
        _logger.LogInformation("Getting budgets for project {ProjectId}", projectId);

        var result = await _mediator.Send(new GetBudgetsByProjectIdQuery { ProjectId = projectId });

        return Ok(result);
    }

    /// <summary>
    /// Creates a new budget.
    /// </summary>
    /// <param name="command">The create command.</param>
    /// <returns>The created budget.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BudgetDto>> Create([FromBody] CreateBudgetCommand command)
    {
        _logger.LogInformation(
            "Creating budget for project {ProjectId}",
            command.ProjectId);

        var result = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetByProjectId),
            new { projectId = result.ProjectId },
            result);
    }
}
