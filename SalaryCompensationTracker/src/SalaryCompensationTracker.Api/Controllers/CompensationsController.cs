using SalaryCompensationTracker.Api.Features.Compensations;
using SalaryCompensationTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SalaryCompensationTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompensationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CompensationsController> _logger;

    public CompensationsController(IMediator mediator, ILogger<CompensationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompensationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CompensationDto>>> GetCompensations(
        [FromQuery] Guid? userId,
        [FromQuery] CompensationType? compensationType,
        [FromQuery] bool? activeOnly)
    {
        _logger.LogInformation("Getting compensations for user {UserId}", userId);

        var result = await _mediator.Send(new GetCompensationsQuery
        {
            UserId = userId,
            CompensationType = compensationType,
            ActiveOnly = activeOnly,
        });

        return Ok(result);
    }

    [HttpGet("{compensationId:guid}")]
    [ProducesResponseType(typeof(CompensationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompensationDto>> GetCompensationById(Guid compensationId)
    {
        _logger.LogInformation("Getting compensation {CompensationId}", compensationId);

        var result = await _mediator.Send(new GetCompensationByIdQuery { CompensationId = compensationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompensationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompensationDto>> CreateCompensation([FromBody] CreateCompensationCommand command)
    {
        _logger.LogInformation("Creating compensation for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/compensations/{result.CompensationId}", result);
    }

    [HttpPut("{compensationId:guid}")]
    [ProducesResponseType(typeof(CompensationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompensationDto>> UpdateCompensation(Guid compensationId, [FromBody] UpdateCompensationCommand command)
    {
        if (compensationId != command.CompensationId)
        {
            return BadRequest("Compensation ID mismatch");
        }

        _logger.LogInformation("Updating compensation {CompensationId}", compensationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{compensationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompensation(Guid compensationId)
    {
        _logger.LogInformation("Deleting compensation {CompensationId}", compensationId);

        var result = await _mediator.Send(new DeleteCompensationCommand { CompensationId = compensationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
