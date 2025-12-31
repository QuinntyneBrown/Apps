using GiftRegistryOrganizer.Api.Features.Contributions;
using Microsoft.AspNetCore.Mvc;

namespace GiftRegistryOrganizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContributionsController : ControllerBase
{
    private readonly ILogger<ContributionsController> _logger;

    public ContributionsController(ILogger<ContributionsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContributionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContributionDto>>> GetContributions(
        [FromQuery] Guid? registryItemId)
    {
        _logger.LogInformation("Getting contributions for registry item {RegistryItemId}", registryItemId);
        return Ok(new List<ContributionDto>());
    }

    [HttpGet("{contributionId:guid}")]
    [ProducesResponseType(typeof(ContributionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContributionDto>> GetContributionById(Guid contributionId)
    {
        _logger.LogInformation("Getting contribution {ContributionId}", contributionId);
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContributionDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ContributionDto>> CreateContribution([FromBody] CreateContributionCommand command)
    {
        _logger.LogInformation("Creating contribution");
        return Created($"/api/contributions/{Guid.NewGuid()}", null);
    }

    [HttpDelete("{contributionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContribution(Guid contributionId)
    {
        _logger.LogInformation("Deleting contribution {ContributionId}", contributionId);
        return NotFound();
    }
}

public record CreateContributionCommand
{
    public Guid RegistryItemId { get; init; }
    public string ContributorName { get; init; } = string.Empty;
    public string? ContributorEmail { get; init; }
    public int Quantity { get; init; }
}
