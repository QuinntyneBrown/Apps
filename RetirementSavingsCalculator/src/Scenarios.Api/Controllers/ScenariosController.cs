using Scenarios.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Scenarios.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScenariosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ScenariosController> _logger;

    public ScenariosController(IMediator mediator, ILogger<ScenariosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScenarioDto>>> GetScenarios(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScenariosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScenarioDto>> GetScenarioById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScenarioByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ScenarioDto>> CreateScenario([FromBody] CreateScenarioCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetScenarioById), new { id = result.RetirementScenarioId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ScenarioDto>> UpdateScenario(Guid id, [FromBody] UpdateScenarioCommand command, CancellationToken cancellationToken)
    {
        if (id != command.RetirementScenarioId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteScenario(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteScenarioCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
