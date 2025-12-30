using PetCareManager.Api.Features.Medications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MedicationsController> _logger;

    public MedicationsController(IMediator mediator, ILogger<MedicationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MedicationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MedicationDto>>> GetMedications([FromQuery] Guid? petId)
    {
        _logger.LogInformation("Getting medications for pet {PetId}", petId);

        var result = await _mediator.Send(new GetMedicationsQuery { PetId = petId });

        return Ok(result);
    }

    [HttpGet("{medicationId:guid}")]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicationDto>> GetMedicationById(Guid medicationId)
    {
        _logger.LogInformation("Getting medication {MedicationId}", medicationId);

        var result = await _mediator.Send(new GetMedicationByIdQuery { MedicationId = medicationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicationDto>> CreateMedication([FromBody] CreateMedicationCommand command)
    {
        _logger.LogInformation("Creating medication for pet {PetId}", command.PetId);

        var result = await _mediator.Send(command);

        return Created($"/api/medications/{result.MedicationId}", result);
    }

    [HttpPut("{medicationId:guid}")]
    [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicationDto>> UpdateMedication(Guid medicationId, [FromBody] UpdateMedicationCommand command)
    {
        if (medicationId != command.MedicationId)
        {
            return BadRequest("Medication ID mismatch");
        }

        _logger.LogInformation("Updating medication {MedicationId}", medicationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{medicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedication(Guid medicationId)
    {
        _logger.LogInformation("Deleting medication {MedicationId}", medicationId);

        var result = await _mediator.Send(new DeleteMedicationCommand { MedicationId = medicationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
