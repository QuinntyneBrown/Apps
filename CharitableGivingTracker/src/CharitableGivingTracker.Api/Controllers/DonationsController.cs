// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Api.Features.Donations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharitableGivingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DonationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DonationsController> _logger;

    public DonationsController(IMediator mediator, ILogger<DonationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all donations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DonationDto>>> GetAll()
    {
        _logger.LogInformation("Getting all donations");
        var result = await _mediator.Send(new GetAllDonations.Query());
        return Ok(result);
    }

    /// <summary>
    /// Gets a donation by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DonationDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting donation {DonationId}", id);
        var result = await _mediator.Send(new GetDonationById.Query(id));

        if (result == null)
        {
            _logger.LogWarning("Donation {DonationId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Gets donations by organization ID.
    /// </summary>
    [HttpGet("organization/{organizationId}")]
    [ProducesResponseType(typeof(List<DonationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DonationDto>>> GetByOrganization(Guid organizationId)
    {
        _logger.LogInformation("Getting donations for organization {OrganizationId}", organizationId);
        var result = await _mediator.Send(new GetDonationsByOrganization.Query(organizationId));
        return Ok(result);
    }

    /// <summary>
    /// Creates a new donation.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<DonationDto>> Create([FromBody] CreateDonation.Command command)
    {
        _logger.LogInformation("Creating new donation for organization {OrganizationId}", command.OrganizationId);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.DonationId }, result);
    }

    /// <summary>
    /// Updates an existing donation.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(DonationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DonationDto>> Update(Guid id, [FromBody] UpdateDonation.Command command)
    {
        if (id != command.DonationId)
        {
            _logger.LogWarning("Donation ID mismatch: {RouteId} vs {CommandId}", id, command.DonationId);
            return BadRequest("Donation ID mismatch");
        }

        _logger.LogInformation("Updating donation {DonationId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Donation {DonationId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a donation.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting donation {DonationId}", id);
        var result = await _mediator.Send(new DeleteDonation.Command(id));

        if (!result)
        {
            _logger.LogWarning("Donation {DonationId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
