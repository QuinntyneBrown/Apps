using RoadsideAssistanceInfoHub.Api.Features.EmergencyContacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RoadsideAssistanceInfoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmergencyContactsController> _logger;

    public EmergencyContactsController(IMediator mediator, ILogger<EmergencyContactsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmergencyContactDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmergencyContactDto>>> GetEmergencyContacts(
        [FromQuery] string? name,
        [FromQuery] string? contactType,
        [FromQuery] bool? isPrimaryContact,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting emergency contacts");

        var result = await _mediator.Send(new GetEmergencyContactsQuery
        {
            Name = name,
            ContactType = contactType,
            IsPrimaryContact = isPrimaryContact,
            IsActive = isActive,
        });

        return Ok(result);
    }

    [HttpGet("{emergencyContactId:guid}")]
    [ProducesResponseType(typeof(EmergencyContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmergencyContactDto>> GetEmergencyContactById(Guid emergencyContactId)
    {
        _logger.LogInformation("Getting emergency contact {EmergencyContactId}", emergencyContactId);

        var result = await _mediator.Send(new GetEmergencyContactByIdQuery { EmergencyContactId = emergencyContactId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmergencyContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmergencyContactDto>> CreateEmergencyContact([FromBody] CreateEmergencyContactCommand command)
    {
        _logger.LogInformation("Creating emergency contact");

        var result = await _mediator.Send(command);

        return Created($"/api/emergencycontacts/{result.EmergencyContactId}", result);
    }

    [HttpPut("{emergencyContactId:guid}")]
    [ProducesResponseType(typeof(EmergencyContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmergencyContactDto>> UpdateEmergencyContact(Guid emergencyContactId, [FromBody] UpdateEmergencyContactCommand command)
    {
        if (emergencyContactId != command.EmergencyContactId)
        {
            return BadRequest("Emergency contact ID mismatch");
        }

        _logger.LogInformation("Updating emergency contact {EmergencyContactId}", emergencyContactId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{emergencyContactId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmergencyContact(Guid emergencyContactId)
    {
        _logger.LogInformation("Deleting emergency contact {EmergencyContactId}", emergencyContactId);

        var result = await _mediator.Send(new DeleteEmergencyContactCommand { EmergencyContactId = emergencyContactId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
