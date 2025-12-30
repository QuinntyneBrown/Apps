using ProfessionalNetworkCRM.Api.Features.FollowUps;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowUpsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FollowUpsController> _logger;

    public FollowUpsController(IMediator mediator, ILogger<FollowUpsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FollowUpDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FollowUpDto>>> GetFollowUps(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? contactId,
        [FromQuery] bool? isCompleted,
        [FromQuery] bool? isOverdue)
    {
        _logger.LogInformation("Getting follow-ups for user {UserId}, contact {ContactId}", userId, contactId);

        var result = await _mediator.Send(new GetFollowUpsQuery
        {
            UserId = userId,
            ContactId = contactId,
            IsCompleted = isCompleted,
            IsOverdue = isOverdue,
        });

        return Ok(result);
    }

    [HttpGet("{followUpId:guid}")]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> GetFollowUpById(Guid followUpId)
    {
        _logger.LogInformation("Getting follow-up {FollowUpId}", followUpId);

        var result = await _mediator.Send(new GetFollowUpByIdQuery { FollowUpId = followUpId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FollowUpDto>> CreateFollowUp([FromBody] CreateFollowUpCommand command)
    {
        _logger.LogInformation("Creating follow-up for contact {ContactId}", command.ContactId);

        var result = await _mediator.Send(command);

        return Created($"/api/followups/{result.FollowUpId}", result);
    }

    [HttpPut("{followUpId:guid}")]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> UpdateFollowUp(Guid followUpId, [FromBody] UpdateFollowUpCommand command)
    {
        if (followUpId != command.FollowUpId)
        {
            return BadRequest("Follow-up ID mismatch");
        }

        _logger.LogInformation("Updating follow-up {FollowUpId}", followUpId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{followUpId:guid}/complete")]
    [ProducesResponseType(typeof(FollowUpDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FollowUpDto>> CompleteFollowUp(Guid followUpId)
    {
        _logger.LogInformation("Completing follow-up {FollowUpId}", followUpId);

        var result = await _mediator.Send(new CompleteFollowUpCommand { FollowUpId = followUpId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{followUpId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFollowUp(Guid followUpId)
    {
        _logger.LogInformation("Deleting follow-up {FollowUpId}", followUpId);

        var result = await _mediator.Send(new DeleteFollowUpCommand { FollowUpId = followUpId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
