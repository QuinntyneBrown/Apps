using Intakes.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Intakes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntakesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IntakesController> _logger;

    public IntakesController(IMediator mediator, ILogger<IntakesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IntakeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IntakeDto>>> GetIntakes(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetIntakesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IntakeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IntakeDto>> GetIntakeById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetIntakeByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(IntakeDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<IntakeDto>> CreateIntake(
        [FromBody] CreateIntakeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetIntakeById), new { id = result.IntakeId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(IntakeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IntakeDto>> UpdateIntake(
        Guid id,
        [FromBody] UpdateIntakeCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.IntakeId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteIntake(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteIntakeCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
