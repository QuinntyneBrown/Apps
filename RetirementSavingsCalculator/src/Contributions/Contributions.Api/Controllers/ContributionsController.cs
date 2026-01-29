using Contributions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contributions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContributionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContributionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContributionDto>>> GetContributions(CancellationToken ct) => Ok(await _mediator.Send(new GetContributionsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ContributionDto>> GetContributionById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetContributionByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ContributionDto>> CreateContribution([FromBody] CreateContributionCommand cmd, CancellationToken ct)
    {
        var result = await _mediator.Send(cmd, ct);
        return CreatedAtAction(nameof(GetContributionById), new { id = result.ContributionId }, result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteContribution(Guid id, CancellationToken ct) => await _mediator.Send(new DeleteContributionCommand(id), ct) ? NoContent() : NotFound();
}
