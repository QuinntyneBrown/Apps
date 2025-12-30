// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Features.Contributions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeSavingsPlanner.Api.Controllers;

/// <summary>
/// Controller for managing plan contributions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContributionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContributionsController> _logger;

    public ContributionsController(IMediator mediator, ILogger<ContributionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all contributions, optionally filtered by plan ID.
    /// </summary>
    /// <param name="planId">Optional plan ID to filter by.</param>
    /// <returns>List of contributions.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ContributionDto>>> GetContributions([FromQuery] Guid? planId = null)
    {
        _logger.LogInformation("Getting contributions for plan {PlanId}", planId);
        var contributions = await _mediator.Send(new GetContributionsQuery { PlanId = planId });
        return Ok(contributions);
    }

    /// <summary>
    /// Gets a contribution by ID.
    /// </summary>
    /// <param name="id">The contribution ID.</param>
    /// <returns>The contribution.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContributionDto>> GetContribution(Guid id)
    {
        _logger.LogInformation("Getting contribution {ContributionId}", id);
        var contribution = await _mediator.Send(new GetContributionByIdQuery { ContributionId = id });

        if (contribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found", id);
            return NotFound();
        }

        return Ok(contribution);
    }

    /// <summary>
    /// Creates a new contribution.
    /// </summary>
    /// <param name="contribution">The contribution to create.</param>
    /// <returns>The created contribution.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContributionDto>> CreateContribution(CreateContributionDto contribution)
    {
        _logger.LogInformation("Creating new contribution of {Amount}", contribution.Amount);
        var createdContribution = await _mediator.Send(new CreateContributionCommand { Contribution = contribution });
        return CreatedAtAction(nameof(GetContribution), new { id = createdContribution.ContributionId }, createdContribution);
    }

    /// <summary>
    /// Updates an existing contribution.
    /// </summary>
    /// <param name="id">The contribution ID.</param>
    /// <param name="contribution">The updated contribution data.</param>
    /// <returns>The updated contribution.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContributionDto>> UpdateContribution(Guid id, UpdateContributionDto contribution)
    {
        _logger.LogInformation("Updating contribution {ContributionId}", id);
        var updatedContribution = await _mediator.Send(new UpdateContributionCommand { ContributionId = id, Contribution = contribution });

        if (updatedContribution == null)
        {
            _logger.LogWarning("Contribution {ContributionId} not found for update", id);
            return NotFound();
        }

        return Ok(updatedContribution);
    }

    /// <summary>
    /// Deletes a contribution.
    /// </summary>
    /// <param name="id">The contribution ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContribution(Guid id)
    {
        _logger.LogInformation("Deleting contribution {ContributionId}", id);
        var deleted = await _mediator.Send(new DeleteContributionCommand { ContributionId = id });

        if (!deleted)
        {
            _logger.LogWarning("Contribution {ContributionId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
