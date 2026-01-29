using MediatR;
using Microsoft.AspNetCore.Mvc;
using Races.Api.Features;

namespace Races.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RacesController> _logger;

    public RacesController(IMediator mediator, ILogger<RacesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RaceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RaceDto>>> GetRaces(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all races");
        var result = await _mediator.Send(new GetRacesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RaceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RaceDto>> GetRaceById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting race {RaceId}", id);
        var result = await _mediator.Send(new GetRaceByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RaceDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<RaceDto>> CreateRace(
        [FromBody] CreateRaceCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating race {RaceName}", command.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRaceById), new { id = result.RaceId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(RaceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RaceDto>> UpdateRace(
        Guid id,
        [FromBody] UpdateRaceCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.RaceId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating race {RaceId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRace(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting race {RaceId}", id);
        var result = await _mediator.Send(new DeleteRaceCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
