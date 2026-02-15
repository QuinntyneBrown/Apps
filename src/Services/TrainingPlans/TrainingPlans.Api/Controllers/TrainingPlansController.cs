using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrainingPlans.Api.Features;

namespace TrainingPlans.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingPlansController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TrainingPlansController> _logger;

    public TrainingPlansController(IMediator mediator, ILogger<TrainingPlansController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainingPlanDto>>> GetTrainingPlans(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTrainingPlansQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TrainingPlanDto>> GetTrainingPlanById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTrainingPlanByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TrainingPlanDto>> CreateTrainingPlan([FromBody] CreateTrainingPlanCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetTrainingPlanById), new { id = result.TrainingPlanId }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TrainingPlanDto>> UpdateTrainingPlan(Guid id, [FromBody] UpdateTrainingPlanCommand command, CancellationToken cancellationToken)
    {
        if (id != command.TrainingPlanId) return BadRequest("ID mismatch");
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTrainingPlan(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteTrainingPlanCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
