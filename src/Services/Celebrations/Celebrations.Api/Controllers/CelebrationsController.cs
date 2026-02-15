using Celebrations.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Celebrations.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CelebrationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CelebrationsController> _logger;

    public CelebrationsController(IMediator mediator, ILogger<CelebrationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CelebrationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CelebrationDto>>> GetCelebrations(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all celebrations");
        var result = await _mediator.Send(new GetCelebrationsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CelebrationDto>> GetCelebrationById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting celebration {CelebrationId}", id);
        var result = await _mediator.Send(new GetCelebrationByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CelebrationDto>> CreateCelebration(
        [FromBody] CreateCelebrationCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating celebration for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCelebrationById), new { id = result.CelebrationId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CelebrationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CelebrationDto>> UpdateCelebration(
        Guid id,
        [FromBody] UpdateCelebrationCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CelebrationId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating celebration {CelebrationId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCelebration(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting celebration {CelebrationId}", id);
        var result = await _mediator.Send(new DeleteCelebrationCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
