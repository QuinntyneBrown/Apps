using TimeBlocks.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TimeBlocks.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeBlocksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TimeBlocksController> _logger;

    public TimeBlocksController(IMediator mediator, ILogger<TimeBlocksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TimeBlockDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TimeBlockDto>>> GetTimeBlocks(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTimeBlocksQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TimeBlockDto>> GetTimeBlockById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTimeBlockByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<TimeBlockDto>> CreateTimeBlock(
        [FromBody] CreateTimeBlockCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTimeBlockById), new { id = result.TimeBlockId }, result);
    }

    [HttpPost("{id:guid}/end")]
    [ProducesResponseType(typeof(TimeBlockDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TimeBlockDto>> EndTimeBlock(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new EndTimeBlockCommand(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTimeBlock(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTimeBlockCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
