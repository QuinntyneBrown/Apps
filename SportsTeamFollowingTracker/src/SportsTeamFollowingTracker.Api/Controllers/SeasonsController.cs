using SportsTeamFollowingTracker.Api.Features.Seasons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SportsTeamFollowingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeasonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SeasonsController> _logger;

    public SeasonsController(IMediator mediator, ILogger<SeasonsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SeasonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SeasonDto>>> GetSeasons(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? teamId,
        [FromQuery] int? year)
    {
        _logger.LogInformation("Getting seasons for user {UserId}, team {TeamId}", userId, teamId);

        var result = await _mediator.Send(new GetSeasonsQuery
        {
            UserId = userId,
            TeamId = teamId,
            Year = year,
        });

        return Ok(result);
    }

    [HttpGet("{seasonId:guid}")]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonDto>> GetSeasonById(Guid seasonId)
    {
        _logger.LogInformation("Getting season {SeasonId}", seasonId);

        var result = await _mediator.Send(new GetSeasonByIdQuery { SeasonId = seasonId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SeasonDto>> CreateSeason([FromBody] CreateSeasonCommand command)
    {
        _logger.LogInformation("Creating season for team {TeamId}", command.TeamId);

        var result = await _mediator.Send(command);

        return Created($"/api/seasons/{result.SeasonId}", result);
    }

    [HttpPut("{seasonId:guid}")]
    [ProducesResponseType(typeof(SeasonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SeasonDto>> UpdateSeason(Guid seasonId, [FromBody] UpdateSeasonCommand command)
    {
        if (seasonId != command.SeasonId)
        {
            return BadRequest("Season ID mismatch");
        }

        _logger.LogInformation("Updating season {SeasonId}", seasonId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{seasonId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSeason(Guid seasonId)
    {
        _logger.LogInformation("Deleting season {SeasonId}", seasonId);

        var result = await _mediator.Send(new DeleteSeasonCommand { SeasonId = seasonId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
