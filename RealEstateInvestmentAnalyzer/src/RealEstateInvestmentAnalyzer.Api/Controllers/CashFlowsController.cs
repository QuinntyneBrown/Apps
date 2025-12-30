using RealEstateInvestmentAnalyzer.Api.Features.CashFlows;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateInvestmentAnalyzer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CashFlowsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CashFlowsController> _logger;

    public CashFlowsController(IMediator mediator, ILogger<CashFlowsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CashFlowDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CashFlowDto>>> GetCashFlows(
        [FromQuery] Guid? propertyId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting cash flows");

        var result = await _mediator.Send(new GetCashFlowsQuery
        {
            PropertyId = propertyId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{cashFlowId:guid}")]
    [ProducesResponseType(typeof(CashFlowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CashFlowDto>> GetCashFlowById(Guid cashFlowId)
    {
        _logger.LogInformation("Getting cash flow {CashFlowId}", cashFlowId);

        var result = await _mediator.Send(new GetCashFlowByIdQuery { CashFlowId = cashFlowId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CashFlowDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CashFlowDto>> CreateCashFlow([FromBody] CreateCashFlowCommand command)
    {
        _logger.LogInformation("Creating cash flow");

        var result = await _mediator.Send(command);

        return Created($"/api/cashflows/{result.CashFlowId}", result);
    }

    [HttpPut("{cashFlowId:guid}")]
    [ProducesResponseType(typeof(CashFlowDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CashFlowDto>> UpdateCashFlow(Guid cashFlowId, [FromBody] UpdateCashFlowCommand command)
    {
        if (cashFlowId != command.CashFlowId)
        {
            return BadRequest("Cash flow ID mismatch");
        }

        _logger.LogInformation("Updating cash flow {CashFlowId}", cashFlowId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{cashFlowId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCashFlow(Guid cashFlowId)
    {
        _logger.LogInformation("Deleting cash flow {CashFlowId}", cashFlowId);

        var result = await _mediator.Send(new DeleteCashFlowCommand { CashFlowId = cashFlowId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
