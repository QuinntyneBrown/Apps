using VehicleValueTracker.Api.Features.MarketComparisons;
using VehicleValueTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VehicleValueTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketComparisonsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MarketComparisonsController> _logger;

    public MarketComparisonsController(IMediator mediator, ILogger<MarketComparisonsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MarketComparisonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MarketComparisonDto>>> GetMarketComparisons(
        [FromQuery] Guid? vehicleId,
        [FromQuery] string? listingSource,
        [FromQuery] bool? isActive,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate)
    {
        _logger.LogInformation("Getting market comparisons for vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new GetMarketComparisonsQuery
        {
            VehicleId = vehicleId,
            ListingSource = listingSource,
            IsActive = isActive,
            FromDate = fromDate,
            ToDate = toDate,
        });

        return Ok(result);
    }

    [HttpGet("{marketComparisonId:guid}")]
    [ProducesResponseType(typeof(MarketComparisonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MarketComparisonDto>> GetMarketComparisonById(Guid marketComparisonId)
    {
        _logger.LogInformation("Getting market comparison {MarketComparisonId}", marketComparisonId);

        var result = await _mediator.Send(new GetMarketComparisonByIdQuery { MarketComparisonId = marketComparisonId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MarketComparisonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MarketComparisonDto>> CreateMarketComparison([FromBody] CreateMarketComparisonCommand command)
    {
        _logger.LogInformation("Creating market comparison for vehicle {VehicleId}", command.VehicleId);

        var result = await _mediator.Send(command);

        return Created($"/api/marketcomparisons/{result.MarketComparisonId}", result);
    }

    [HttpPut("{marketComparisonId:guid}")]
    [ProducesResponseType(typeof(MarketComparisonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MarketComparisonDto>> UpdateMarketComparison(Guid marketComparisonId, [FromBody] UpdateMarketComparisonCommand command)
    {
        if (marketComparisonId != command.MarketComparisonId)
        {
            return BadRequest("Market comparison ID mismatch");
        }

        _logger.LogInformation("Updating market comparison {MarketComparisonId}", marketComparisonId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{marketComparisonId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMarketComparison(Guid marketComparisonId)
    {
        _logger.LogInformation("Deleting market comparison {MarketComparisonId}", marketComparisonId);

        var result = await _mediator.Send(new DeleteMarketComparisonCommand { MarketComparisonId = marketComparisonId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
