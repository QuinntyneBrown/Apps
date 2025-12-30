using StressMoodTracker.Api.Features.Triggers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StressMoodTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TriggersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TriggersController> _logger;

    public TriggersController(IMediator mediator, ILogger<TriggersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TriggerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TriggerDto>>> GetTriggers(
        [FromQuery] Guid? userId,
        [FromQuery] string? triggerType,
        [FromQuery] int? minImpactLevel)
    {
        _logger.LogInformation("Getting triggers for user {UserId}", userId);

        var result = await _mediator.Send(new GetTriggersQuery
        {
            UserId = userId,
            TriggerType = triggerType,
            MinImpactLevel = minImpactLevel,
        });

        return Ok(result);
    }

    [HttpGet("{triggerId:guid}")]
    [ProducesResponseType(typeof(TriggerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TriggerDto>> GetTriggerById(Guid triggerId)
    {
        _logger.LogInformation("Getting trigger {TriggerId}", triggerId);

        var result = await _mediator.Send(new GetTriggerByIdQuery { TriggerId = triggerId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TriggerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TriggerDto>> CreateTrigger([FromBody] CreateTriggerCommand command)
    {
        _logger.LogInformation("Creating trigger for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/triggers/{result.TriggerId}", result);
    }

    [HttpPut("{triggerId:guid}")]
    [ProducesResponseType(typeof(TriggerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TriggerDto>> UpdateTrigger(Guid triggerId, [FromBody] UpdateTriggerCommand command)
    {
        if (triggerId != command.TriggerId)
        {
            return BadRequest("Trigger ID mismatch");
        }

        _logger.LogInformation("Updating trigger {TriggerId}", triggerId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{triggerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrigger(Guid triggerId)
    {
        _logger.LogInformation("Deleting trigger {TriggerId}", triggerId);

        var result = await _mediator.Send(new DeleteTriggerCommand { TriggerId = triggerId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
