using Warranties.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Warranties.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarrantiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WarrantiesController> _logger;

    public WarrantiesController(IMediator mediator, ILogger<WarrantiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WarrantyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WarrantyDto>>> GetWarranties(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all warranties");
        var result = await _mediator.Send(new GetWarrantiesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WarrantyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarrantyDto>> GetWarrantyById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting warranty {WarrantyId}", id);
        var result = await _mediator.Send(new GetWarrantyByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WarrantyDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<WarrantyDto>> CreateWarranty(
        [FromBody] CreateWarrantyCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating warranty for appliance {ApplianceId}", command.ApplianceId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetWarrantyById), new { id = result.WarrantyId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WarrantyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WarrantyDto>> UpdateWarranty(
        Guid id,
        [FromBody] UpdateWarrantyCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.WarrantyId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating warranty {WarrantyId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWarranty(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting warranty {WarrantyId}", id);
        var result = await _mediator.Send(new DeleteWarrantyCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
