using Appliances.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appliances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppliancesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AppliancesController> _logger;

    public AppliancesController(IMediator mediator, ILogger<AppliancesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ApplianceDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApplianceDto>>> GetAppliances(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all appliances");
        var result = await _mediator.Send(new GetAppliancesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApplianceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplianceDto>> GetApplianceById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting appliance {ApplianceId}", id);
        var result = await _mediator.Send(new GetApplianceByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApplianceDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApplianceDto>> CreateAppliance(
        [FromBody] CreateApplianceCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating appliance: {Name}", command.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetApplianceById), new { id = result.ApplianceId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApplianceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApplianceDto>> UpdateAppliance(
        Guid id,
        [FromBody] UpdateApplianceCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ApplianceId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating appliance {ApplianceId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAppliance(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting appliance {ApplianceId}", id);
        var result = await _mediator.Send(new DeleteApplianceCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
