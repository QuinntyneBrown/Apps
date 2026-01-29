using GiftPlanning.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GiftPlanning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftPlansController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GiftPlansController> _logger;

    public GiftPlansController(IMediator mediator, ILogger<GiftPlansController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GiftPlanDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GiftPlanDto>>> GetGiftPlans(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all gift plans");
        var result = await _mediator.Send(new GetGiftPlansQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GiftPlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftPlanDto>> GetGiftPlanById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting gift plan {GiftPlanId}", id);
        var result = await _mediator.Send(new GetGiftPlanByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GiftPlanDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<GiftPlanDto>> CreateGiftPlan(
        [FromBody] CreateGiftPlanCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating gift plan for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetGiftPlanById), new { id = result.GiftPlanId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(GiftPlanDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GiftPlanDto>> UpdateGiftPlan(
        Guid id,
        [FromBody] UpdateGiftPlanCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.GiftPlanId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating gift plan {GiftPlanId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGiftPlan(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting gift plan {GiftPlanId}", id);
        var result = await _mediator.Send(new DeleteGiftPlanCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
