using Sources.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sources.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SourcesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SourcesController> _logger;

    public SourcesController(IMediator mediator, ILogger<SourcesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SourceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SourceDto>>> GetSources(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all sources");
        var result = await _mediator.Send(new GetSourcesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SourceDto>> GetSourceById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting source {SourceId}", id);
        var result = await _mediator.Send(new GetSourceByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<SourceDto>> CreateSource(
        [FromBody] CreateSourceCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating source {Title}", command.Title);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetSourceById), new { id = result.SourceId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SourceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SourceDto>> UpdateSource(
        Guid id,
        [FromBody] UpdateSourceCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.SourceId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating source {SourceId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSource(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting source {SourceId}", id);
        var result = await _mediator.Send(new DeleteSourceCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
