using GiftRegistryOrganizer.Api.Features.Registries;
using Microsoft.AspNetCore.Mvc;

namespace GiftRegistryOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistriesController : ControllerBase
{
    private readonly ILogger<RegistriesController> _logger;

    public RegistriesController(ILogger<RegistriesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RegistryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RegistryDto>>> GetRegistries(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isActive)
    {
        _logger.LogInformation("Getting registries for user {UserId}", userId);
        return Ok(new List<RegistryDto>());
    }

    [HttpGet("{registryId:guid}")]
    [ProducesResponseType(typeof(RegistryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistryDto>> GetRegistryById(Guid registryId)
    {
        _logger.LogInformation("Getting registry {RegistryId}", registryId);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegistryDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegistryDto>> CreateRegistry([FromBody] CreateRegistryCommand command)
    {
        _logger.LogInformation("Creating registry");
        return Created($"/api/registries/{Guid.NewGuid()}", null);
    }

    [HttpPut("{registryId:guid}")]
    [ProducesResponseType(typeof(RegistryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistryDto>> UpdateRegistry(Guid registryId, [FromBody] UpdateRegistryCommand command)
    {
        _logger.LogInformation("Updating registry {RegistryId}", registryId);
        return NotFound();
    }

    [HttpDelete("{registryId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRegistry(Guid registryId)
    {
        _logger.LogInformation("Deleting registry {RegistryId}", registryId);
        return NotFound();
    }
}

public record CreateRegistryCommand
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public GiftRegistryOrganizer.Core.RegistryType Type { get; init; }
    public DateTime EventDate { get; init; }
}

public record UpdateRegistryCommand
{
    public Guid RegistryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public GiftRegistryOrganizer.Core.RegistryType Type { get; init; }
    public DateTime EventDate { get; init; }
    public bool IsActive { get; init; }
}
