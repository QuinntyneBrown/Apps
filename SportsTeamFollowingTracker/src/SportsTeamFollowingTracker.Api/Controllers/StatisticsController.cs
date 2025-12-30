using SportsTeamFollowingTracker.Api.Features.Statistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SportsTeamFollowingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StatisticsController> _logger;

    public StatisticsController(IMediator mediator, ILogger<StatisticsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<StatisticDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StatisticDto>>> GetStatistics(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? teamId,
        [FromQuery] string? statName,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting statistics for user {UserId}, team {TeamId}", userId, teamId);

        var result = await _mediator.Send(new GetStatisticsQuery
        {
            UserId = userId,
            TeamId = teamId,
            StatName = statName,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{statisticId:guid}")]
    [ProducesResponseType(typeof(StatisticDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StatisticDto>> GetStatisticById(Guid statisticId)
    {
        _logger.LogInformation("Getting statistic {StatisticId}", statisticId);

        var result = await _mediator.Send(new GetStatisticByIdQuery { StatisticId = statisticId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(StatisticDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StatisticDto>> CreateStatistic([FromBody] CreateStatisticCommand command)
    {
        _logger.LogInformation("Creating statistic for team {TeamId}", command.TeamId);

        var result = await _mediator.Send(command);

        return Created($"/api/statistics/{result.StatisticId}", result);
    }

    [HttpPut("{statisticId:guid}")]
    [ProducesResponseType(typeof(StatisticDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StatisticDto>> UpdateStatistic(Guid statisticId, [FromBody] UpdateStatisticCommand command)
    {
        if (statisticId != command.StatisticId)
        {
            return BadRequest("Statistic ID mismatch");
        }

        _logger.LogInformation("Updating statistic {StatisticId}", statisticId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{statisticId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStatistic(Guid statisticId)
    {
        _logger.LogInformation("Deleting statistic {StatisticId}", statisticId);

        var result = await _mediator.Send(new DeleteStatisticCommand { StatisticId = statisticId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
