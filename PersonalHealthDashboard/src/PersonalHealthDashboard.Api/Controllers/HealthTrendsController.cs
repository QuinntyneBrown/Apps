using PersonalHealthDashboard.Api.Features.HealthTrends;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalHealthDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthTrendsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<HealthTrendsController> _logger;

    public HealthTrendsController(IMediator mediator, ILogger<HealthTrendsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<HealthTrendDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<HealthTrendDto>>> GetHealthTrends(
        [FromQuery] Guid? userId,
        [FromQuery] string? metricName,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? trendDirection)
    {
        _logger.LogInformation("Getting health trends for user {UserId}", userId);

        var result = await _mediator.Send(new GetHealthTrendsQuery
        {
            UserId = userId,
            MetricName = metricName,
            StartDate = startDate,
            EndDate = endDate,
            TrendDirection = trendDirection,
        });

        return Ok(result);
    }

    [HttpGet("{healthTrendId:guid}")]
    [ProducesResponseType(typeof(HealthTrendDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HealthTrendDto>> GetHealthTrendById(Guid healthTrendId)
    {
        _logger.LogInformation("Getting health trend {HealthTrendId}", healthTrendId);

        var result = await _mediator.Send(new GetHealthTrendByIdQuery { HealthTrendId = healthTrendId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(HealthTrendDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HealthTrendDto>> CreateHealthTrend([FromBody] CreateHealthTrendCommand command)
    {
        _logger.LogInformation("Creating health trend for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/healthtrends/{result.HealthTrendId}", result);
    }

    [HttpPut("{healthTrendId:guid}")]
    [ProducesResponseType(typeof(HealthTrendDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HealthTrendDto>> UpdateHealthTrend(Guid healthTrendId, [FromBody] UpdateHealthTrendCommand command)
    {
        if (healthTrendId != command.HealthTrendId)
        {
            return BadRequest("Health trend ID mismatch");
        }

        _logger.LogInformation("Updating health trend {HealthTrendId}", healthTrendId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{healthTrendId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHealthTrend(Guid healthTrendId)
    {
        _logger.LogInformation("Deleting health trend {HealthTrendId}", healthTrendId);

        var result = await _mediator.Send(new DeleteHealthTrendCommand { HealthTrendId = healthTrendId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
