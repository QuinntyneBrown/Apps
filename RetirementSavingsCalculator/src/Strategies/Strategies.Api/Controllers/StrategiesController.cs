using Strategies.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Strategies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StrategiesController : ControllerBase
{
    private readonly IMediator _mediator;
    public StrategiesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StrategyDto>>> GetStrategies(CancellationToken ct) => Ok(await _mediator.Send(new GetStrategiesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StrategyDto>> GetStrategyById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStrategyByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StrategyDto>> CreateStrategy([FromBody] CreateStrategyCommand cmd, CancellationToken ct)
    {
        var result = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetStrategyById), new { id = result.WithdrawalStrategyId }, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStrategy(Guid id, CancellationToken ct) => await _mediator.Send(new DeleteStrategyCommand(id), ct) ? NoContent() : NotFound();
}
