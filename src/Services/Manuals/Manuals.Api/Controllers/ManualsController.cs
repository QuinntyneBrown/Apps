using Manuals.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Manuals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManualsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ManualsController> _logger;

    public ManualsController(IMediator mediator, ILogger<ManualsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ManualDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ManualDto>>> GetManuals(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all manuals");
        var result = await _mediator.Send(new GetManualsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ManualDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ManualDto>> GetManualById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting manual {ManualId}", id);
        var result = await _mediator.Send(new GetManualByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ManualDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ManualDto>> CreateManual(
        [FromBody] CreateManualCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating manual for appliance {ApplianceId}", command.ApplianceId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetManualById), new { id = result.ManualId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ManualDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ManualDto>> UpdateManual(
        Guid id,
        [FromBody] UpdateManualCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ManualId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating manual {ManualId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteManual(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting manual {ManualId}", id);
        var result = await _mediator.Send(new DeleteManualCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
