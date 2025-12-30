using RealEstateInvestmentAnalyzer.Api.Features.Leases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateInvestmentAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeasesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<LeasesController> _logger;

    public LeasesController(IMediator mediator, ILogger<LeasesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LeaseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LeaseDto>>> GetLeases(
        [FromQuery] Guid? propertyId,
        [FromQuery] bool? activeOnly)
    {
        _logger.LogInformation("Getting leases");

        var result = await _mediator.Send(new GetLeasesQuery
        {
            PropertyId = propertyId,
            ActiveOnly = activeOnly,
        });

        return Ok(result);
    }

    [HttpGet("{leaseId:guid}")]
    [ProducesResponseType(typeof(LeaseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaseDto>> GetLeaseById(Guid leaseId)
    {
        _logger.LogInformation("Getting lease {LeaseId}", leaseId);

        var result = await _mediator.Send(new GetLeaseByIdQuery { LeaseId = leaseId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(LeaseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LeaseDto>> CreateLease([FromBody] CreateLeaseCommand command)
    {
        _logger.LogInformation("Creating lease");

        var result = await _mediator.Send(command);

        return Created($"/api/leases/{result.LeaseId}", result);
    }

    [HttpPut("{leaseId:guid}")]
    [ProducesResponseType(typeof(LeaseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaseDto>> UpdateLease(Guid leaseId, [FromBody] UpdateLeaseCommand command)
    {
        if (leaseId != command.LeaseId)
        {
            return BadRequest("Lease ID mismatch");
        }

        _logger.LogInformation("Updating lease {LeaseId}", leaseId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{leaseId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLease(Guid leaseId)
    {
        _logger.LogInformation("Deleting lease {LeaseId}", leaseId);

        var result = await _mediator.Send(new DeleteLeaseCommand { LeaseId = leaseId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{leaseId:guid}/terminate")]
    [ProducesResponseType(typeof(LeaseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeaseDto>> TerminateLease(Guid leaseId)
    {
        _logger.LogInformation("Terminating lease {LeaseId}", leaseId);

        var result = await _mediator.Send(new TerminateLeaseCommand { LeaseId = leaseId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
