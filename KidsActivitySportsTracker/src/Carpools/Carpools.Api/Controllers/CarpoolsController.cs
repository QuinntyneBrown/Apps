using Carpools.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Carpools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarpoolsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CarpoolsController> _logger;

    public CarpoolsController(IMediator mediator, ILogger<CarpoolsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarpoolDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarpoolDto>>> GetCarpools(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all carpools");
        var result = await _mediator.Send(new GetCarpoolsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarpoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarpoolDto>> GetCarpoolById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting carpool {CarpoolId}", id);
        var result = await _mediator.Send(new GetCarpoolByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CarpoolDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CarpoolDto>> CreateCarpool(
        [FromBody] CreateCarpoolCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating carpool for schedule {ScheduleId}", command.ScheduleId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCarpoolById), new { id = result.CarpoolId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CarpoolDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarpoolDto>> UpdateCarpool(
        Guid id,
        [FromBody] UpdateCarpoolCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CarpoolId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating carpool {CarpoolId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCarpool(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting carpool {CarpoolId}", id);
        var result = await _mediator.Send(new DeleteCarpoolCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
