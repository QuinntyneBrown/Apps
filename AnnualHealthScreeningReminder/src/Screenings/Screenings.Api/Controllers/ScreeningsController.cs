using Screenings.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Screenings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreeningsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScreeningsController> _logger;

    public ScreeningsController(IMediator mediator, ILogger<ScreeningsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ScreeningDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ScreeningDto>>> GetScreenings(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all screenings");
        var result = await _mediator.Send(new GetScreeningsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ScreeningDto>> GetScreeningById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting screening {ScreeningId}", id);
        var result = await _mediator.Send(new GetScreeningByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ScreeningDto>> CreateScreening(
        [FromBody] CreateScreeningCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating screening for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetScreeningById), new { id = result.ScreeningId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ScreeningDto>> UpdateScreening(
        Guid id,
        [FromBody] UpdateScreeningCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ScreeningId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating screening {ScreeningId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteScreening(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting screening {ScreeningId}", id);
        var result = await _mediator.Send(new DeleteScreeningCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
