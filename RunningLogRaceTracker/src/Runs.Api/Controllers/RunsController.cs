using MediatR;
using Microsoft.AspNetCore.Mvc;
using Runs.Api.Features;

namespace Runs.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RunsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RunsController> _logger;

    public RunsController(IMediator mediator, ILogger<RunsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RunDto>>> GetRuns(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRunsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RunDto>> GetRunById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetRunByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RunDto>> CreateRun([FromBody] CreateRunCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetRunById), new { id = result.RunId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RunDto>> UpdateRun(Guid id, [FromBody] UpdateRunCommand command, CancellationToken cancellationToken)
    {
        if (id != command.RunId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRun(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteRunCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
