using SleepQualityTracker.Api.Features.Patterns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SleepQualityTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatternsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PatternsController> _logger;

    public PatternsController(IMediator mediator, ILogger<PatternsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatternDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PatternDto>>> GetPatterns(
        [FromQuery] Guid? userId,
        [FromQuery] string? patternType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] bool? isHighConfidence)
    {
        _logger.LogInformation("Getting patterns for user {UserId}", userId);

        var result = await _mediator.Send(new GetPatternsQuery
        {
            UserId = userId,
            PatternType = patternType,
            StartDate = startDate,
            EndDate = endDate,
            IsHighConfidence = isHighConfidence,
        });

        return Ok(result);
    }

    [HttpGet("{patternId:guid}")]
    [ProducesResponseType(typeof(PatternDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatternDto>> GetPatternById(Guid patternId)
    {
        _logger.LogInformation("Getting pattern {PatternId}", patternId);

        var result = await _mediator.Send(new GetPatternByIdQuery { PatternId = patternId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PatternDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PatternDto>> CreatePattern([FromBody] CreatePatternCommand command)
    {
        _logger.LogInformation("Creating pattern for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/patterns/{result.PatternId}", result);
    }

    [HttpPut("{patternId:guid}")]
    [ProducesResponseType(typeof(PatternDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatternDto>> UpdatePattern(Guid patternId, [FromBody] UpdatePatternCommand command)
    {
        if (patternId != command.PatternId)
        {
            return BadRequest("Pattern ID mismatch");
        }

        _logger.LogInformation("Updating pattern {PatternId}", patternId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{patternId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePattern(Guid patternId)
    {
        _logger.LogInformation("Deleting pattern {PatternId}", patternId);

        var result = await _mediator.Send(new DeletePatternCommand { PatternId = patternId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
