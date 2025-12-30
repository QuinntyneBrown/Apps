using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarrantyReturnPeriodTracker.Api.Features.Warranties;

namespace WarrantyReturnPeriodTracker.Api.Controllers;

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
    public async Task<ActionResult<List<WarrantyDto>>> GetWarranties(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all warranties");
        var warranties = await _mediator.Send(new GetWarrantiesQuery(), cancellationToken);
        return Ok(warranties);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WarrantyDto>> GetWarrantyById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting warranty with ID: {WarrantyId}", id);
        var warranty = await _mediator.Send(new GetWarrantyByIdQuery { WarrantyId = id }, cancellationToken);

        if (warranty == null)
        {
            _logger.LogWarning("Warranty with ID {WarrantyId} not found", id);
            return NotFound();
        }

        return Ok(warranty);
    }

    [HttpPost]
    public async Task<ActionResult<WarrantyDto>> CreateWarranty(
        CreateWarrantyCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new warranty for purchase: {PurchaseId}", command.PurchaseId);
        var warranty = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetWarrantyById), new { id = warranty.WarrantyId }, warranty);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WarrantyDto>> UpdateWarranty(
        Guid id,
        UpdateWarrantyCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.WarrantyId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Updating warranty with ID: {WarrantyId}", id);

        try
        {
            var warranty = await _mediator.Send(command, cancellationToken);
            return Ok(warranty);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to update warranty with ID {WarrantyId}", id);
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteWarranty(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting warranty with ID: {WarrantyId}", id);

        try
        {
            await _mediator.Send(new DeleteWarrantyCommand { WarrantyId = id }, cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Failed to delete warranty with ID {WarrantyId}", id);
            return NotFound(ex.Message);
        }
    }
}
