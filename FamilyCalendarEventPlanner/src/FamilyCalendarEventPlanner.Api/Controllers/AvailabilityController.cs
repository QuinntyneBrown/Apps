using FamilyCalendarEventPlanner.Api.Features.Availability;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyCalendarEventPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailabilityController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AvailabilityController> _logger;

    public AvailabilityController(IMediator mediator, ILogger<AvailabilityController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AvailabilityBlockDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AvailabilityBlockDto>>> GetAvailabilityBlocks([FromQuery] Guid memberId)
    {
        _logger.LogInformation("Getting availability blocks for member {MemberId}", memberId);

        var result = await _mediator.Send(new GetAvailabilityBlocksQuery { MemberId = memberId });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AvailabilityBlockDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AvailabilityBlockDto>> CreateAvailabilityBlock([FromBody] CreateAvailabilityBlockCommand command)
    {
        _logger.LogInformation("Creating availability block for member {MemberId}", command.MemberId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    [HttpDelete("{blockId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAvailabilityBlock(Guid blockId)
    {
        _logger.LogInformation("Deleting availability block {BlockId}", blockId);

        var result = await _mediator.Send(new DeleteAvailabilityBlockCommand { BlockId = blockId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
