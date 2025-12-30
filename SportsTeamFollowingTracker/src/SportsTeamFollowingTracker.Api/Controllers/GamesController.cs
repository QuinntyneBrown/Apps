using SportsTeamFollowingTracker.Api.Features.Games;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SportsTeamFollowingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GamesController> _logger;

    public GamesController(IMediator mediator, ILogger<GamesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? teamId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting games for user {UserId}, team {TeamId}", userId, teamId);

        var result = await _mediator.Send(new GetGamesQuery
        {
            UserId = userId,
            TeamId = teamId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{gameId:guid}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> GetGameById(Guid gameId)
    {
        _logger.LogInformation("Getting game {GameId}", gameId);

        var result = await _mediator.Send(new GetGameByIdQuery { GameId = gameId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDto>> CreateGame([FromBody] CreateGameCommand command)
    {
        _logger.LogInformation("Creating game for team {TeamId}", command.TeamId);

        var result = await _mediator.Send(command);

        return Created($"/api/games/{result.GameId}", result);
    }

    [HttpPut("{gameId:guid}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> UpdateGame(Guid gameId, [FromBody] UpdateGameCommand command)
    {
        if (gameId != command.GameId)
        {
            return BadRequest("Game ID mismatch");
        }

        _logger.LogInformation("Updating game {GameId}", gameId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{gameId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGame(Guid gameId)
    {
        _logger.LogInformation("Deleting game {GameId}", gameId);

        var result = await _mediator.Send(new DeleteGameCommand { GameId = gameId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
