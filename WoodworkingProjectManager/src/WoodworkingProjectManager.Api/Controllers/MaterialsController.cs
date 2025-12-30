using WoodworkingProjectManager.Api.Features.Materials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WoodworkingProjectManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaterialsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MaterialsController> _logger;

    public MaterialsController(IMediator mediator, ILogger<MaterialsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaterialDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterials(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? projectId)
    {
        _logger.LogInformation("Getting materials for user {UserId}", userId);

        var result = await _mediator.Send(new GetMaterialsQuery
        {
            UserId = userId,
            ProjectId = projectId,
        });

        return Ok(result);
    }

    [HttpGet("{materialId:guid}")]
    [ProducesResponseType(typeof(MaterialDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaterialDto>> GetMaterialById(Guid materialId)
    {
        _logger.LogInformation("Getting material {MaterialId}", materialId);

        var result = await _mediator.Send(new GetMaterialByIdQuery { MaterialId = materialId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MaterialDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaterialDto>> CreateMaterial([FromBody] CreateMaterialCommand command)
    {
        _logger.LogInformation("Creating material for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/materials/{result.MaterialId}", result);
    }

    [HttpPut("{materialId:guid}")]
    [ProducesResponseType(typeof(MaterialDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaterialDto>> UpdateMaterial(Guid materialId, [FromBody] UpdateMaterialCommand command)
    {
        if (materialId != command.MaterialId)
        {
            return BadRequest("Material ID mismatch");
        }

        _logger.LogInformation("Updating material {MaterialId}", materialId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{materialId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMaterial(Guid materialId)
    {
        _logger.LogInformation("Deleting material {MaterialId}", materialId);

        var result = await _mediator.Send(new DeleteMaterialCommand { MaterialId = materialId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
