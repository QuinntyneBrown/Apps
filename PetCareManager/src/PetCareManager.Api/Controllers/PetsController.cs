using PetCareManager.Api.Features.Pets;
using PetCareManager.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetCareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PetsController> _logger;

    public PetsController(IMediator mediator, ILogger<PetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetPets(
        [FromQuery] Guid? userId,
        [FromQuery] PetType? petType)
    {
        _logger.LogInformation("Getting pets for user {UserId}", userId);

        var result = await _mediator.Send(new GetPetsQuery
        {
            UserId = userId,
            PetType = petType,
        });

        return Ok(result);
    }

    [HttpGet("{petId:guid}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> GetPetById(Guid petId)
    {
        _logger.LogInformation("Getting pet {PetId}", petId);

        var result = await _mediator.Send(new GetPetByIdQuery { PetId = petId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> CreatePet([FromBody] CreatePetCommand command)
    {
        _logger.LogInformation("Creating pet for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/pets/{result.PetId}", result);
    }

    [HttpPut("{petId:guid}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> UpdatePet(Guid petId, [FromBody] UpdatePetCommand command)
    {
        if (petId != command.PetId)
        {
            return BadRequest("Pet ID mismatch");
        }

        _logger.LogInformation("Updating pet {PetId}", petId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{petId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePet(Guid petId)
    {
        _logger.LogInformation("Deleting pet {PetId}", petId);

        var result = await _mediator.Send(new DeletePetCommand { PetId = petId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
