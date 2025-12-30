// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CollegeSavingsPlanner.Api.Features.Beneficiaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollegeSavingsPlanner.Api.Controllers;

/// <summary>
/// Controller for managing plan beneficiaries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BeneficiariesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BeneficiariesController> _logger;

    public BeneficiariesController(IMediator mediator, ILogger<BeneficiariesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all beneficiaries, optionally filtered by plan ID.
    /// </summary>
    /// <param name="planId">Optional plan ID to filter by.</param>
    /// <returns>List of beneficiaries.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BeneficiaryDto>>> GetBeneficiaries([FromQuery] Guid? planId = null)
    {
        _logger.LogInformation("Getting beneficiaries for plan {PlanId}", planId);
        var beneficiaries = await _mediator.Send(new GetBeneficiariesQuery { PlanId = planId });
        return Ok(beneficiaries);
    }

    /// <summary>
    /// Gets a beneficiary by ID.
    /// </summary>
    /// <param name="id">The beneficiary ID.</param>
    /// <returns>The beneficiary.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BeneficiaryDto>> GetBeneficiary(Guid id)
    {
        _logger.LogInformation("Getting beneficiary {BeneficiaryId}", id);
        var beneficiary = await _mediator.Send(new GetBeneficiaryByIdQuery { BeneficiaryId = id });

        if (beneficiary == null)
        {
            _logger.LogWarning("Beneficiary {BeneficiaryId} not found", id);
            return NotFound();
        }

        return Ok(beneficiary);
    }

    /// <summary>
    /// Creates a new beneficiary.
    /// </summary>
    /// <param name="beneficiary">The beneficiary to create.</param>
    /// <returns>The created beneficiary.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BeneficiaryDto>> CreateBeneficiary(CreateBeneficiaryDto beneficiary)
    {
        _logger.LogInformation("Creating new beneficiary: {FirstName} {LastName}", beneficiary.FirstName, beneficiary.LastName);
        var createdBeneficiary = await _mediator.Send(new CreateBeneficiaryCommand { Beneficiary = beneficiary });
        return CreatedAtAction(nameof(GetBeneficiary), new { id = createdBeneficiary.BeneficiaryId }, createdBeneficiary);
    }

    /// <summary>
    /// Updates an existing beneficiary.
    /// </summary>
    /// <param name="id">The beneficiary ID.</param>
    /// <param name="beneficiary">The updated beneficiary data.</param>
    /// <returns>The updated beneficiary.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BeneficiaryDto>> UpdateBeneficiary(Guid id, UpdateBeneficiaryDto beneficiary)
    {
        _logger.LogInformation("Updating beneficiary {BeneficiaryId}", id);
        var updatedBeneficiary = await _mediator.Send(new UpdateBeneficiaryCommand { BeneficiaryId = id, Beneficiary = beneficiary });

        if (updatedBeneficiary == null)
        {
            _logger.LogWarning("Beneficiary {BeneficiaryId} not found for update", id);
            return NotFound();
        }

        return Ok(updatedBeneficiary);
    }

    /// <summary>
    /// Deletes a beneficiary.
    /// </summary>
    /// <param name="id">The beneficiary ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBeneficiary(Guid id)
    {
        _logger.LogInformation("Deleting beneficiary {BeneficiaryId}", id);
        var deleted = await _mediator.Send(new DeleteBeneficiaryCommand { BeneficiaryId = id });

        if (!deleted)
        {
            _logger.LogWarning("Beneficiary {BeneficiaryId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
