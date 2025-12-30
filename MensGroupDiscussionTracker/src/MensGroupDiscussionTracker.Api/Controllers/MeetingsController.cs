using MensGroupDiscussionTracker.Api.Features.Meetings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MensGroupDiscussionTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MeetingsController> _logger;

    public MeetingsController(IMediator mediator, ILogger<MeetingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MeetingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MeetingDto>>> GetMeetings(
        [FromQuery] Guid? groupId)
    {
        _logger.LogInformation("Getting meetings for group {GroupId}", groupId);

        var result = await _mediator.Send(new GetMeetingsQuery
        {
            GroupId = groupId,
        });

        return Ok(result);
    }

    [HttpGet("{meetingId:guid}")]
    [ProducesResponseType(typeof(MeetingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MeetingDto>> GetMeetingById(Guid meetingId)
    {
        _logger.LogInformation("Getting meeting {MeetingId}", meetingId);

        var result = await _mediator.Send(new GetMeetingByIdQuery { MeetingId = meetingId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MeetingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MeetingDto>> CreateMeeting([FromBody] CreateMeetingCommand command)
    {
        _logger.LogInformation("Creating meeting for group {GroupId}", command.GroupId);

        var result = await _mediator.Send(command);

        return Created($"/api/meetings/{result.MeetingId}", result);
    }

    [HttpPut("{meetingId:guid}")]
    [ProducesResponseType(typeof(MeetingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MeetingDto>> UpdateMeeting(Guid meetingId, [FromBody] UpdateMeetingCommand command)
    {
        if (meetingId != command.MeetingId)
        {
            return BadRequest("Meeting ID mismatch");
        }

        _logger.LogInformation("Updating meeting {MeetingId}", meetingId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{meetingId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMeeting(Guid meetingId)
    {
        _logger.LogInformation("Deleting meeting {MeetingId}", meetingId);

        var result = await _mediator.Send(new DeleteMeetingCommand { MeetingId = meetingId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
