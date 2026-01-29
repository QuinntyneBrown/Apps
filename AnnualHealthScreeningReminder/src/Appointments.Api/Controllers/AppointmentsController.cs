using Appointments.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IMediator mediator, ILogger<AppointmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all appointments");
        var result = await _mediator.Send(new GetAppointmentsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> GetAppointmentById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting appointment {AppointmentId}", id);
        var result = await _mediator.Send(new GetAppointmentByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<AppointmentDto>> CreateAppointment(
        [FromBody] CreateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating appointment for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAppointmentById), new { id = result.AppointmentId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> UpdateAppointment(
        Guid id,
        [FromBody] UpdateAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.AppointmentId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating appointment {AppointmentId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAppointment(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting appointment {AppointmentId}", id);
        var result = await _mediator.Send(new DeleteAppointmentCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
