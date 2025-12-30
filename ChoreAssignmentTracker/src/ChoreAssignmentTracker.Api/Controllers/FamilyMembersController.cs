// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Api.Features.FamilyMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChoreAssignmentTracker.Api.Controllers;

/// <summary>
/// Controller for managing family members.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FamilyMembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FamilyMembersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FamilyMembersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public FamilyMembersController(IMediator mediator, ILogger<FamilyMembersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all family members.
    /// </summary>
    /// <param name="userId">Optional user ID to filter by.</param>
    /// <returns>A list of family members.</returns>
    [HttpGet]
    public async Task<ActionResult<List<FamilyMemberDto>>> GetFamilyMembers([FromQuery] Guid? userId)
    {
        _logger.LogInformation("Getting family members{Filter}", userId.HasValue ? $" for user {userId.Value}" : string.Empty);

        var result = await _mediator.Send(new GetFamilyMembers { UserId = userId });
        return Ok(result);
    }

    /// <summary>
    /// Gets a family member by ID.
    /// </summary>
    /// <param name="id">The family member ID.</param>
    /// <returns>The family member.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<FamilyMemberDto>> GetFamilyMember(Guid id)
    {
        _logger.LogInformation("Getting family member {FamilyMemberId}", id);

        var result = await _mediator.Send(new GetFamilyMemberById { FamilyMemberId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new family member.
    /// </summary>
    /// <param name="dto">The family member data.</param>
    /// <returns>The created family member.</returns>
    [HttpPost]
    public async Task<ActionResult<FamilyMemberDto>> CreateFamilyMember(CreateFamilyMemberDto dto)
    {
        _logger.LogInformation("Creating family member {Name}", dto.Name);

        var result = await _mediator.Send(new CreateFamilyMember { FamilyMember = dto });
        return CreatedAtAction(nameof(GetFamilyMember), new { id = result.FamilyMemberId }, result);
    }

    /// <summary>
    /// Updates a family member.
    /// </summary>
    /// <param name="id">The family member ID.</param>
    /// <param name="dto">The family member data.</param>
    /// <returns>The updated family member.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<FamilyMemberDto>> UpdateFamilyMember(Guid id, UpdateFamilyMemberDto dto)
    {
        _logger.LogInformation("Updating family member {FamilyMemberId}", id);

        var result = await _mediator.Send(new UpdateFamilyMember { FamilyMemberId = id, FamilyMember = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a family member.
    /// </summary>
    /// <param name="id">The family member ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFamilyMember(Guid id)
    {
        _logger.LogInformation("Deleting family member {FamilyMemberId}", id);

        var result = await _mediator.Send(new DeleteFamilyMember { FamilyMemberId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
