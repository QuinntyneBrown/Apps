using SalaryCompensationTracker.Api.Features.Benefits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCompensationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenefitsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BenefitsController> _logger;

    public BenefitsController(IMediator mediator, ILogger<BenefitsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BenefitDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BenefitDto>>> GetBenefits(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? compensationId,
        [FromQuery] string? category)
    {
        _logger.LogInformation("Getting benefits for user {UserId}", userId);

        var result = await _mediator.Send(new GetBenefitsQuery
        {
            UserId = userId,
            CompensationId = compensationId,
            Category = category,
        });

        return Ok(result);
    }

    [HttpGet("{benefitId:guid}")]
    [ProducesResponseType(typeof(BenefitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BenefitDto>> GetBenefitById(Guid benefitId)
    {
        _logger.LogInformation("Getting benefit {BenefitId}", benefitId);

        var result = await _mediator.Send(new GetBenefitByIdQuery { BenefitId = benefitId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BenefitDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BenefitDto>> CreateBenefit([FromBody] CreateBenefitCommand command)
    {
        _logger.LogInformation("Creating benefit for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/benefits/{result.BenefitId}", result);
    }

    [HttpPut("{benefitId:guid}")]
    [ProducesResponseType(typeof(BenefitDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BenefitDto>> UpdateBenefit(Guid benefitId, [FromBody] UpdateBenefitCommand command)
    {
        if (benefitId != command.BenefitId)
        {
            return BadRequest("Benefit ID mismatch");
        }

        _logger.LogInformation("Updating benefit {BenefitId}", benefitId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{benefitId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBenefit(Guid benefitId)
    {
        _logger.LogInformation("Deleting benefit {BenefitId}", benefitId);

        var result = await _mediator.Send(new DeleteBenefitCommand { BenefitId = benefitId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
