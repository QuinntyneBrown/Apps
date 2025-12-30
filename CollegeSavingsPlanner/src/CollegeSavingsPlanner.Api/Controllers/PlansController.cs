// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Features.Plans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeSavingsPlanner.Api.Controllers;

/// <summary>
/// Controller for managing 529 college savings plans.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PlansController> _logger;

    public PlansController(IMediator mediator, ILogger<PlansController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all plans.
    /// </summary>
    /// <returns>List of plans.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<PlanDto>>> GetPlans()
    {
        _logger.LogInformation("Getting all plans");
        var plans = await _mediator.Send(new GetPlansQuery());
        return Ok(plans);
    }

    /// <summary>
    /// Gets a plan by ID.
    /// </summary>
    /// <param name="id">The plan ID.</param>
    /// <returns>The plan.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanDto>> GetPlan(Guid id)
    {
        _logger.LogInformation("Getting plan {PlanId}", id);
        var plan = await _mediator.Send(new GetPlanByIdQuery { PlanId = id });

        if (plan == null)
        {
            _logger.LogWarning("Plan {PlanId} not found", id);
            return NotFound();
        }

        return Ok(plan);
    }

    /// <summary>
    /// Creates a new plan.
    /// </summary>
    /// <param name="plan">The plan to create.</param>
    /// <returns>The created plan.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlanDto>> CreatePlan(CreatePlanDto plan)
    {
        _logger.LogInformation("Creating new plan: {PlanName}", plan.Name);
        var createdPlan = await _mediator.Send(new CreatePlanCommand { Plan = plan });
        return CreatedAtAction(nameof(GetPlan), new { id = createdPlan.PlanId }, createdPlan);
    }

    /// <summary>
    /// Updates an existing plan.
    /// </summary>
    /// <param name="id">The plan ID.</param>
    /// <param name="plan">The updated plan data.</param>
    /// <returns>The updated plan.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlanDto>> UpdatePlan(Guid id, UpdatePlanDto plan)
    {
        _logger.LogInformation("Updating plan {PlanId}", id);
        var updatedPlan = await _mediator.Send(new UpdatePlanCommand { PlanId = id, Plan = plan });

        if (updatedPlan == null)
        {
            _logger.LogWarning("Plan {PlanId} not found for update", id);
            return NotFound();
        }

        return Ok(updatedPlan);
    }

    /// <summary>
    /// Deletes a plan.
    /// </summary>
    /// <param name="id">The plan ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        _logger.LogInformation("Deleting plan {PlanId}", id);
        var deleted = await _mediator.Send(new DeletePlanCommand { PlanId = id });

        if (!deleted)
        {
            _logger.LogWarning("Plan {PlanId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
