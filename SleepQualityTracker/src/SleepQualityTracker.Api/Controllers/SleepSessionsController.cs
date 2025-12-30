using SleepQualityTracker.Api.Features.SleepSessions;
using SleepQualityTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SleepQualityTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SleepSessionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SleepSessionsController> _logger;

    public SleepSessionsController(IMediator mediator, ILogger<SleepSessionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SleepSessionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SleepSessionDto>>> GetSleepSessions(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] SleepQuality? sleepQuality,
        [FromQuery] bool? meetsRecommendedDuration)
    {
        _logger.LogInformation("Getting sleep sessions for user {UserId}", userId);

        var result = await _mediator.Send(new GetSleepSessionsQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            SleepQuality = sleepQuality,
            MeetsRecommendedDuration = meetsRecommendedDuration,
        });

        return Ok(result);
    }

    [HttpGet("{sleepSessionId:guid}")]
    [ProducesResponseType(typeof(SleepSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SleepSessionDto>> GetSleepSessionById(Guid sleepSessionId)
    {
        _logger.LogInformation("Getting sleep session {SleepSessionId}", sleepSessionId);

        var result = await _mediator.Send(new GetSleepSessionByIdQuery { SleepSessionId = sleepSessionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SleepSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SleepSessionDto>> CreateSleepSession([FromBody] CreateSleepSessionCommand command)
    {
        _logger.LogInformation("Creating sleep session for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/sleepsessions/{result.SleepSessionId}", result);
    }

    [HttpPut("{sleepSessionId:guid}")]
    [ProducesResponseType(typeof(SleepSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SleepSessionDto>> UpdateSleepSession(Guid sleepSessionId, [FromBody] UpdateSleepSessionCommand command)
    {
        if (sleepSessionId != command.SleepSessionId)
        {
            return BadRequest("Sleep session ID mismatch");
        }

        _logger.LogInformation("Updating sleep session {SleepSessionId}", sleepSessionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{sleepSessionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSleepSession(Guid sleepSessionId)
    {
        _logger.LogInformation("Deleting sleep session {SleepSessionId}", sleepSessionId);

        var result = await _mediator.Send(new DeleteSleepSessionCommand { SleepSessionId = sleepSessionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
