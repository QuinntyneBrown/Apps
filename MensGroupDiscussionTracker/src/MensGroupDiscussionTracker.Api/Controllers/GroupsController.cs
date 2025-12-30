using MensGroupDiscussionTracker.Api.Features.Groups;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MensGroupDiscussionTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GroupsController> _logger;

    public GroupsController(IMediator mediator, ILogger<GroupsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups(
        [FromQuery] Guid? createdByUserId,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting groups for user {UserId}", createdByUserId);

        var result = await _mediator.Send(new GetGroupsQuery
        {
            CreatedByUserId = createdByUserId,
            IsActive = isActive,
        });

        return Ok(result);
    }

    [HttpGet("{groupId:guid}")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroupDto>> GetGroupById(Guid groupId)
    {
        _logger.LogInformation("Getting group {GroupId}", groupId);

        var result = await _mediator.Send(new GetGroupByIdQuery { GroupId = groupId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GroupDto>> CreateGroup([FromBody] CreateGroupCommand command)
    {
        _logger.LogInformation("Creating group for user {UserId}", command.CreatedByUserId);

        var result = await _mediator.Send(command);

        return Created($"/api/groups/{result.GroupId}", result);
    }

    [HttpPut("{groupId:guid}")]
    [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GroupDto>> UpdateGroup(Guid groupId, [FromBody] UpdateGroupCommand command)
    {
        if (groupId != command.GroupId)
        {
            return BadRequest("Group ID mismatch");
        }

        _logger.LogInformation("Updating group {GroupId}", groupId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{groupId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        _logger.LogInformation("Deleting group {GroupId}", groupId);

        var result = await _mediator.Send(new DeleteGroupCommand { GroupId = groupId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
