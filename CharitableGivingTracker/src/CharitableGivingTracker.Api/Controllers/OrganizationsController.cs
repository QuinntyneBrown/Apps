// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CharitableGivingTracker.Api.Features.Organizations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharitableGivingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(IMediator mediator, ILogger<OrganizationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all organizations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationDto>>> GetAll()
    {
        _logger.LogInformation("Getting all organizations");
        var result = await _mediator.Send(new GetAllOrganizations.Query());
        return Ok(result);
    }

    /// <summary>
    /// Gets an organization by ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> GetById(Guid id)
    {
        _logger.LogInformation("Getting organization {OrganizationId}", id);
        var result = await _mediator.Send(new GetOrganizationById.Query(id));

        if (result == null)
        {
            _logger.LogWarning("Organization {OrganizationId} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<OrganizationDto>> Create([FromBody] CreateOrganization.Command command)
    {
        _logger.LogInformation("Creating new organization {OrganizationName}", command.Name);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.OrganizationId }, result);
    }

    /// <summary>
    /// Updates an existing organization.
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(OrganizationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrganizationDto>> Update(Guid id, [FromBody] UpdateOrganization.Command command)
    {
        if (id != command.OrganizationId)
        {
            _logger.LogWarning("Organization ID mismatch: {RouteId} vs {CommandId}", id, command.OrganizationId);
            return BadRequest("Organization ID mismatch");
        }

        _logger.LogInformation("Updating organization {OrganizationId}", id);
        var result = await _mediator.Send(command);

        if (result == null)
        {
            _logger.LogWarning("Organization {OrganizationId} not found for update", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an organization.
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting organization {OrganizationId}", id);
        var result = await _mediator.Send(new DeleteOrganization.Command(id));

        if (!result)
        {
            _logger.LogWarning("Organization {OrganizationId} not found for deletion", id);
            return NotFound();
        }

        return NoContent();
    }
}
