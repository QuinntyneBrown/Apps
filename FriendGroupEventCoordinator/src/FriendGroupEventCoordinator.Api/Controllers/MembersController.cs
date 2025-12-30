// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Features.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FriendGroupEventCoordinator.Api.Controllers;

/// <summary>
/// Controller for managing members.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MembersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MembersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public MembersController(IMediator mediator, ILogger<MembersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets a member by ID.
    /// </summary>
    /// <param name="id">The member ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The member.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> GetMember(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting member with ID: {MemberId}", id);
        var memberDto = await _mediator.Send(new GetMemberQuery(id), cancellationToken);

        if (memberDto == null)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return NotFound();
        }

        return Ok(memberDto);
    }

    /// <summary>
    /// Gets all members for a specific group.
    /// </summary>
    /// <param name="groupId">The group ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of members for the group.</returns>
    [HttpGet("group/{groupId}")]
    [ProducesResponseType(typeof(List<MemberDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MemberDto>>> GetMembersByGroup(Guid groupId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting members for group with ID: {GroupId}", groupId);
        var members = await _mediator.Send(new GetMembersByGroupQuery(groupId), cancellationToken);
        return Ok(members);
    }

    /// <summary>
    /// Creates a new member.
    /// </summary>
    /// <param name="createMemberDto">The member to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created member.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto createMemberDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new member: {Name}", createMemberDto.Name);
        var memberDto = await _mediator.Send(new CreateMemberCommand(createMemberDto), cancellationToken);
        return CreatedAtAction(nameof(GetMember), new { id = memberDto.MemberId }, memberDto);
    }

    /// <summary>
    /// Updates an existing member.
    /// </summary>
    /// <param name="id">The member ID.</param>
    /// <param name="updateMemberDto">The updated member data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated member.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> UpdateMember(Guid id, UpdateMemberDto updateMemberDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating member with ID: {MemberId}", id);
        var memberDto = await _mediator.Send(new UpdateMemberCommand(id, updateMemberDto), cancellationToken);

        if (memberDto == null)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return NotFound();
        }

        return Ok(memberDto);
    }

    /// <summary>
    /// Removes a member (sets IsActive to false).
    /// </summary>
    /// <param name="id">The member ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The removed member.</returns>
    [HttpPost("{id}/remove")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> RemoveMember(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Removing member with ID: {MemberId}", id);
        var memberDto = await _mediator.Send(new RemoveMemberCommand(id), cancellationToken);

        if (memberDto == null)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return NotFound();
        }

        return Ok(memberDto);
    }

    /// <summary>
    /// Promotes a member to admin.
    /// </summary>
    /// <param name="id">The member ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The promoted member.</returns>
    [HttpPost("{id}/promote")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberDto>> PromoteToAdmin(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Promoting member with ID: {MemberId} to admin", id);
        var memberDto = await _mediator.Send(new PromoteToAdminCommand(id), cancellationToken);

        if (memberDto == null)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return NotFound();
        }

        return Ok(memberDto);
    }

    /// <summary>
    /// Deletes a member.
    /// </summary>
    /// <param name="id">The member ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMember(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting member with ID: {MemberId}", id);
        var result = await _mediator.Send(new DeleteMemberCommand(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Member with ID {MemberId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
