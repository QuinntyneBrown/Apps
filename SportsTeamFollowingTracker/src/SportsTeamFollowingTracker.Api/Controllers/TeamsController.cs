using SportsTeamFollowingTracker.Api.Features.Teams;
using SportsTeamFollowingTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SportsTeamFollowingTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TeamsController> _logger;

    public TeamsController(IMediator mediator, ILogger<TeamsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TeamDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams(
        [FromQuery] Guid? userId,
        [FromQuery] Sport? sport,
        [FromQuery] bool? isFavorite)
    {
        _logger.LogInformation("Getting teams for user {UserId}", userId);

        var result = await _mediator.Send(new GetTeamsQuery
        {
            UserId = userId,
            Sport = sport,
            IsFavorite = isFavorite,
        });

        return Ok(result);
    }

    [HttpGet("{teamId:guid}")]
    [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> GetTeamById(Guid teamId)
    {
        _logger.LogInformation("Getting team {TeamId}", teamId);

        var result = await _mediator.Send(new GetTeamByIdQuery { TeamId = teamId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TeamDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] CreateTeamCommand command)
    {
        _logger.LogInformation("Creating team for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/teams/{result.TeamId}", result);
    }

    [HttpPut("{teamId:guid}")]
    [ProducesResponseType(typeof(TeamDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TeamDto>> UpdateTeam(Guid teamId, [FromBody] UpdateTeamCommand command)
    {
        if (teamId != command.TeamId)
        {
            return BadRequest("Team ID mismatch");
        }

        _logger.LogInformation("Updating team {TeamId}", teamId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{teamId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTeam(Guid teamId)
    {
        _logger.LogInformation("Deleting team {TeamId}", teamId);

        var result = await _mediator.Send(new DeleteTeamCommand { TeamId = teamId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
