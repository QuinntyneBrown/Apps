using PersonalMissionStatementBuilder.Api.Features.Values;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalMissionStatementBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(IMediator mediator, ILogger<ValuesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ValueDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ValueDto>>> GetValues(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? missionStatementId)
    {
        _logger.LogInformation("Getting values for user {UserId}", userId);

        var result = await _mediator.Send(new GetValuesQuery
        {
            UserId = userId,
            MissionStatementId = missionStatementId,
        });

        return Ok(result);
    }

    [HttpGet("{valueId:guid}")]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueDto>> GetValueById(Guid valueId)
    {
        _logger.LogInformation("Getting value {ValueId}", valueId);

        var result = await _mediator.Send(new GetValueByIdQuery { ValueId = valueId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ValueDto>> CreateValue([FromBody] CreateValueCommand command)
    {
        _logger.LogInformation("Creating value for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/values/{result.ValueId}", result);
    }

    [HttpPut("{valueId:guid}")]
    [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueDto>> UpdateValue(Guid valueId, [FromBody] UpdateValueCommand command)
    {
        if (valueId != command.ValueId)
        {
            return BadRequest("Value ID mismatch");
        }

        _logger.LogInformation("Updating value {ValueId}", valueId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{valueId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteValue(Guid valueId)
    {
        _logger.LogInformation("Deleting value {ValueId}", valueId);

        var result = await _mediator.Send(new DeleteValueCommand { ValueId = valueId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
