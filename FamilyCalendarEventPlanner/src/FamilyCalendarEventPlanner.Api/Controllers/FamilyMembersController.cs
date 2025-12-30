using FamilyCalendarEventPlanner.Api.Features.FamilyMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyMembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FamilyMembersController> _logger;

    public FamilyMembersController(IMediator mediator, ILogger<FamilyMembersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FamilyMemberDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FamilyMemberDto>>> GetFamilyMembers(
        [FromQuery] Guid? familyId,
        [FromQuery] bool? isImmediate)
    {
        _logger.LogInformation(
            "Getting family members for family {FamilyId}, isImmediate: {IsImmediate}",
            familyId,
            isImmediate);

        var result = await _mediator.Send(new GetFamilyMembersQuery
        {
            FamilyId = familyId,
            IsImmediate = isImmediate
        });

        return Ok(result);
    }

    [HttpGet("{memberId:guid}")]
    [ProducesResponseType(typeof(FamilyMemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FamilyMemberDto>> GetFamilyMemberById(Guid memberId)
    {
        _logger.LogInformation("Getting family member {MemberId}", memberId);

        var result = await _mediator.Send(new GetFamilyMemberByIdQuery { MemberId = memberId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FamilyMemberDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FamilyMemberDto>> CreateFamilyMember([FromBody] CreateFamilyMemberCommand command)
    {
        _logger.LogInformation("Creating family member for family {FamilyId}", command.FamilyId);

        var result = await _mediator.Send(command);

        return Created($"/api/familymembers/{result.MemberId}", result);
    }

    [HttpPut("{memberId:guid}")]
    [ProducesResponseType(typeof(FamilyMemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FamilyMemberDto>> UpdateFamilyMember(Guid memberId, [FromBody] UpdateFamilyMemberCommand command)
    {
        if (memberId != command.MemberId)
        {
            return BadRequest("Member ID mismatch");
        }

        _logger.LogInformation("Updating family member {MemberId}", memberId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{memberId:guid}/role")]
    [ProducesResponseType(typeof(FamilyMemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FamilyMemberDto>> ChangeFamilyMemberRole(Guid memberId, [FromBody] ChangeFamilyMemberRoleCommand command)
    {
        if (memberId != command.MemberId)
        {
            return BadRequest("Member ID mismatch");
        }

        _logger.LogInformation("Changing role for family member {MemberId}", memberId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFamilyMember(Guid memberId)
    {
        _logger.LogInformation("Deleting family member {MemberId}", memberId);

        var result = await _mediator.Send(new DeleteFamilyMemberCommand { MemberId = memberId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
