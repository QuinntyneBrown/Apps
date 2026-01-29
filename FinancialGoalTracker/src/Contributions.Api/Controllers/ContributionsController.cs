using Contributions.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Contributions.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContributionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContributionsController> _logger;

    public ContributionsController(IMediator mediator, ILogger<ContributionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContributionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContributionDto>>> GetContributions([FromQuery] Guid? goalId = null)
    {
        _logger.LogInformation("Getting contributions");
        var result = await _mediator.Send(new GetContributionsQuery { GoalId = goalId });
        return Ok(result);
    }

    [HttpGet("{contributionId:guid}")]
    [ProducesResponseType(typeof(ContributionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContributionDto>> GetContributionById(Guid contributionId)
    {
        _logger.LogInformation("Getting contribution {ContributionId}", contributionId);
        var result = await _mediator.Send(new GetContributionByIdQuery { ContributionId = contributionId });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContributionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContributionDto>> CreateContribution([FromBody] CreateContributionCommand command)
    {
        _logger.LogInformation("Creating contribution for goal {GoalId}", command.GoalId);
        var result = await _mediator.Send(command);
        return Created($"/api/contributions/{result.ContributionId}", result);
    }

    [HttpPut("{contributionId:guid}")]
    [ProducesResponseType(typeof(ContributionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContributionDto>> UpdateContribution(Guid contributionId, [FromBody] UpdateContributionCommand command)
    {
        if (contributionId != command.ContributionId) return BadRequest("Contribution ID mismatch");
        _logger.LogInformation("Updating contribution {ContributionId}", contributionId);
        var result = await _mediator.Send(command);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{contributionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContribution(Guid contributionId)
    {
        _logger.LogInformation("Deleting contribution {ContributionId}", contributionId);
        var result = await _mediator.Send(new DeleteContributionCommand { ContributionId = contributionId });
        if (!result) return NotFound();
        return NoContent();
    }
}
