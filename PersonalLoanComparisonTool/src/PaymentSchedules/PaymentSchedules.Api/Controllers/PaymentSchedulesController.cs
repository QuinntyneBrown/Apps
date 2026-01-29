using PaymentSchedules.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentSchedules.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PaymentSchedulesController> _logger;

    public PaymentSchedulesController(IMediator mediator, ILogger<PaymentSchedulesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentScheduleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentScheduleDto>>> GetPaymentSchedules(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPaymentSchedulesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PaymentScheduleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentScheduleDto>> GetPaymentScheduleById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPaymentScheduleByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentScheduleDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<PaymentScheduleDto>> CreatePaymentSchedule([FromBody] CreatePaymentScheduleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPaymentScheduleById), new { id = result.ScheduleId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePaymentSchedule(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeletePaymentScheduleCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
