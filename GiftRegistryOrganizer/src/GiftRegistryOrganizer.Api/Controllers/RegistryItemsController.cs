using GiftRegistryOrganizer.Api.Features.RegistryItems;
using Microsoft.AspNetCore.Mvc;

namespace GiftRegistryOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistryItemsController : ControllerBase
{
    private readonly ILogger<RegistryItemsController> _logger;

    public RegistryItemsController(ILogger<RegistryItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RegistryItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RegistryItemDto>>> GetRegistryItems(
        [FromQuery] Guid? registryId,
        [FromQuery] bool? isFulfilled)
    {
        _logger.LogInformation("Getting registry items for registry {RegistryId}", registryId);
        return Ok(new List<RegistryItemDto>());
    }

    [HttpGet("{registryItemId:guid}")]
    [ProducesResponseType(typeof(RegistryItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistryItemDto>> GetRegistryItemById(Guid registryItemId)
    {
        _logger.LogInformation("Getting registry item {RegistryItemId}", registryItemId);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegistryItemDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegistryItemDto>> CreateRegistryItem([FromBody] CreateRegistryItemCommand command)
    {
        _logger.LogInformation("Creating registry item");
        return Created($"/api/registryitems/{Guid.NewGuid()}", null);
    }

    [HttpPut("{registryItemId:guid}")]
    [ProducesResponseType(typeof(RegistryItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RegistryItemDto>> UpdateRegistryItem(Guid registryItemId, [FromBody] UpdateRegistryItemCommand command)
    {
        _logger.LogInformation("Updating registry item {RegistryItemId}", registryItemId);
        return NotFound();
    }

    [HttpDelete("{registryItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRegistryItem(Guid registryItemId)
    {
        _logger.LogInformation("Deleting registry item {RegistryItemId}", registryItemId);
        return NotFound();
    }
}

public record CreateRegistryItemCommand
{
    public Guid RegistryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public string? Url { get; init; }
    public int QuantityDesired { get; init; }
    public GiftRegistryOrganizer.Core.Priority Priority { get; init; }
}

public record UpdateRegistryItemCommand
{
    public Guid RegistryItemId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public string? Url { get; init; }
    public int QuantityDesired { get; init; }
    public GiftRegistryOrganizer.Core.Priority Priority { get; init; }
    public bool IsFulfilled { get; init; }
}
