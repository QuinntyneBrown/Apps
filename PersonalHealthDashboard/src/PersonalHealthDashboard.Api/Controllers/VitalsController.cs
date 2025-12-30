using PersonalHealthDashboard.Api.Features.Vitals;
using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalHealthDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VitalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VitalsController> _logger;

    public VitalsController(IMediator mediator, ILogger<VitalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VitalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VitalDto>>> GetVitals(
        [FromQuery] Guid? userId,
        [FromQuery] VitalType? vitalType,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] string? source)
    {
        _logger.LogInformation("Getting vitals for user {UserId}", userId);

        var result = await _mediator.Send(new GetVitalsQuery
        {
            UserId = userId,
            VitalType = vitalType,
            StartDate = startDate,
            EndDate = endDate,
            Source = source,
        });

        return Ok(result);
    }

    [HttpGet("{vitalId:guid}")]
    [ProducesResponseType(typeof(VitalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VitalDto>> GetVitalById(Guid vitalId)
    {
        _logger.LogInformation("Getting vital {VitalId}", vitalId);

        var result = await _mediator.Send(new GetVitalByIdQuery { VitalId = vitalId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VitalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VitalDto>> CreateVital([FromBody] CreateVitalCommand command)
    {
        _logger.LogInformation("Creating vital for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/vitals/{result.VitalId}", result);
    }

    [HttpPut("{vitalId:guid}")]
    [ProducesResponseType(typeof(VitalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VitalDto>> UpdateVital(Guid vitalId, [FromBody] UpdateVitalCommand command)
    {
        if (vitalId != command.VitalId)
        {
            return BadRequest("Vital ID mismatch");
        }

        _logger.LogInformation("Updating vital {VitalId}", vitalId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{vitalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVital(Guid vitalId)
    {
        _logger.LogInformation("Deleting vital {VitalId}", vitalId);

        var result = await _mediator.Send(new DeleteVitalCommand { VitalId = vitalId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
