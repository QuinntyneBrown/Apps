// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Api.Features.Groups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FriendGroupEventCoordinator.Api.Controllers;

/// <summary>
/// Controller for managing groups.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GroupsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public GroupsController(IMediator mediator, ILogger<GroupsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all groups.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of groups.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<GroupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GroupDto>>> GetGroups(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all groups");
        var groups = await _mediator.Send(new GetGroupsQuery(), cancellationToken);
        return Ok(groups);
    }

    /// <summary>
    /// Gets a group by ID.
    /// </summary>
    /// <param name="id">The group ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The group.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroupDto>> GetGroup(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting group with ID: {GroupId}", id);
        var groupDto = await _mediator.Send(new GetGroupQuery(id), cancellationToken);

        if (groupDto == null)
        {
            _logger.LogWarning("Group with ID {GroupId} not found", id);
            return NotFound();
        }

        return Ok(groupDto);
    }

    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="createGroupDto">The group to create.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created group.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GroupDto>> CreateGroup(CreateGroupDto createGroupDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new group: {Name}", createGroupDto.Name);
        var groupDto = await _mediator.Send(new CreateGroupCommand(createGroupDto), cancellationToken);
        return CreatedAtAction(nameof(GetGroup), new { id = groupDto.GroupId }, groupDto);
    }

    /// <summary>
    /// Updates an existing group.
    /// </summary>
    /// <param name="id">The group ID.</param>
    /// <param name="updateGroupDto">The updated group data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated group.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroupDto>> UpdateGroup(Guid id, UpdateGroupDto updateGroupDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating group with ID: {GroupId}", id);
        var groupDto = await _mediator.Send(new UpdateGroupCommand(id, updateGroupDto), cancellationToken);

        if (groupDto == null)
        {
            _logger.LogWarning("Group with ID {GroupId} not found", id);
            return NotFound();
        }

        return Ok(groupDto);
    }

    /// <summary>
    /// Deactivates a group.
    /// </summary>
    /// <param name="id">The group ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deactivated group.</returns>
    [HttpPost("{id}/deactivate")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroupDto>> DeactivateGroup(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deactivating group with ID: {GroupId}", id);
        var groupDto = await _mediator.Send(new DeactivateGroupCommand(id), cancellationToken);

        if (groupDto == null)
        {
            _logger.LogWarning("Group with ID {GroupId} not found", id);
            return NotFound();
        }

        return Ok(groupDto);
    }

    /// <summary>
    /// Deletes a group.
    /// </summary>
    /// <param name="id">The group ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGroup(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting group with ID: {GroupId}", id);
        var result = await _mediator.Send(new DeleteGroupCommand(id), cancellationToken);

        if (!result)
        {
            _logger.LogWarning("Group with ID {GroupId} not found", id);
            return NotFound();
        }

        return NoContent();
    }
}
