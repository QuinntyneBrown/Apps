using PetCareManager.Api.Features.Vaccinations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaccinationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VaccinationsController> _logger;

    public VaccinationsController(IMediator mediator, ILogger<VaccinationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VaccinationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VaccinationDto>>> GetVaccinations([FromQuery] Guid? petId)
    {
        _logger.LogInformation("Getting vaccinations for pet {PetId}", petId);

        var result = await _mediator.Send(new GetVaccinationsQuery { PetId = petId });

        return Ok(result);
    }

    [HttpGet("{vaccinationId:guid}")]
    [ProducesResponseType(typeof(VaccinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VaccinationDto>> GetVaccinationById(Guid vaccinationId)
    {
        _logger.LogInformation("Getting vaccination {VaccinationId}", vaccinationId);

        var result = await _mediator.Send(new GetVaccinationByIdQuery { VaccinationId = vaccinationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VaccinationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VaccinationDto>> CreateVaccination([FromBody] CreateVaccinationCommand command)
    {
        _logger.LogInformation("Creating vaccination for pet {PetId}", command.PetId);

        var result = await _mediator.Send(command);

        return Created($"/api/vaccinations/{result.VaccinationId}", result);
    }

    [HttpPut("{vaccinationId:guid}")]
    [ProducesResponseType(typeof(VaccinationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VaccinationDto>> UpdateVaccination(Guid vaccinationId, [FromBody] UpdateVaccinationCommand command)
    {
        if (vaccinationId != command.VaccinationId)
        {
            return BadRequest("Vaccination ID mismatch");
        }

        _logger.LogInformation("Updating vaccination {VaccinationId}", vaccinationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{vaccinationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVaccination(Guid vaccinationId)
    {
        _logger.LogInformation("Deleting vaccination {VaccinationId}", vaccinationId);

        var result = await _mediator.Send(new DeleteVaccinationCommand { VaccinationId = vaccinationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
