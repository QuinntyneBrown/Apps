using PetCareManager.Api.Features.VetAppointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VetAppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VetAppointmentsController> _logger;

    public VetAppointmentsController(IMediator mediator, ILogger<VetAppointmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VetAppointmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VetAppointmentDto>>> GetVetAppointments(
        [FromQuery] Guid? petId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting vet appointments for pet {PetId}", petId);

        var result = await _mediator.Send(new GetVetAppointmentsQuery
        {
            PetId = petId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{vetAppointmentId:guid}")]
    [ProducesResponseType(typeof(VetAppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VetAppointmentDto>> GetVetAppointmentById(Guid vetAppointmentId)
    {
        _logger.LogInformation("Getting vet appointment {VetAppointmentId}", vetAppointmentId);

        var result = await _mediator.Send(new GetVetAppointmentByIdQuery { VetAppointmentId = vetAppointmentId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VetAppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VetAppointmentDto>> CreateVetAppointment([FromBody] CreateVetAppointmentCommand command)
    {
        _logger.LogInformation("Creating vet appointment for pet {PetId}", command.PetId);

        var result = await _mediator.Send(command);

        return Created($"/api/vetappointments/{result.VetAppointmentId}", result);
    }

    [HttpPut("{vetAppointmentId:guid}")]
    [ProducesResponseType(typeof(VetAppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VetAppointmentDto>> UpdateVetAppointment(Guid vetAppointmentId, [FromBody] UpdateVetAppointmentCommand command)
    {
        if (vetAppointmentId != command.VetAppointmentId)
        {
            return BadRequest("Vet appointment ID mismatch");
        }

        _logger.LogInformation("Updating vet appointment {VetAppointmentId}", vetAppointmentId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{vetAppointmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVetAppointment(Guid vetAppointmentId)
    {
        _logger.LogInformation("Deleting vet appointment {VetAppointmentId}", vetAppointmentId);

        var result = await _mediator.Send(new DeleteVetAppointmentCommand { VetAppointmentId = vetAppointmentId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
