using SideHustleIncomeTracker.Api.Features.TaxEstimates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SideHustleIncomeTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxEstimatesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TaxEstimatesController> _logger;

    public TaxEstimatesController(IMediator mediator, ILogger<TaxEstimatesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaxEstimateDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaxEstimateDto>>> GetTaxEstimates(
        [FromQuery] Guid? businessId,
        [FromQuery] int? taxYear,
        [FromQuery] bool? isPaid)
    {
        _logger.LogInformation("Getting tax estimates");

        var result = await _mediator.Send(new GetTaxEstimatesQuery
        {
            BusinessId = businessId,
            TaxYear = taxYear,
            IsPaid = isPaid,
        });

        return Ok(result);
    }

    [HttpGet("{taxEstimateId:guid}")]
    [ProducesResponseType(typeof(TaxEstimateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxEstimateDto>> GetTaxEstimateById(Guid taxEstimateId)
    {
        _logger.LogInformation("Getting tax estimate {TaxEstimateId}", taxEstimateId);

        var result = await _mediator.Send(new GetTaxEstimateByIdQuery { TaxEstimateId = taxEstimateId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TaxEstimateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaxEstimateDto>> CreateTaxEstimate([FromBody] CreateTaxEstimateCommand command)
    {
        _logger.LogInformation("Creating tax estimate for business {BusinessId}", command.BusinessId);

        var result = await _mediator.Send(command);

        return Created($"/api/taxestimates/{result.TaxEstimateId}", result);
    }

    [HttpPut("{taxEstimateId:guid}")]
    [ProducesResponseType(typeof(TaxEstimateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxEstimateDto>> UpdateTaxEstimate(Guid taxEstimateId, [FromBody] UpdateTaxEstimateCommand command)
    {
        if (taxEstimateId != command.TaxEstimateId)
        {
            return BadRequest("Tax estimate ID mismatch");
        }

        _logger.LogInformation("Updating tax estimate {TaxEstimateId}", taxEstimateId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{taxEstimateId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTaxEstimate(Guid taxEstimateId)
    {
        _logger.LogInformation("Deleting tax estimate {TaxEstimateId}", taxEstimateId);

        var result = await _mediator.Send(new DeleteTaxEstimateCommand { TaxEstimateId = taxEstimateId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
