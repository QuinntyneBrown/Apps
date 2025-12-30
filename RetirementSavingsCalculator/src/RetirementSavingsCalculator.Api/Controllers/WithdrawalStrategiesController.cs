using RetirementSavingsCalculator.Api.Features.WithdrawalStrategies;
using RetirementSavingsCalculator.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RetirementSavingsCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WithdrawalStrategiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WithdrawalStrategiesController> _logger;

    public WithdrawalStrategiesController(IMediator mediator, ILogger<WithdrawalStrategiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WithdrawalStrategyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WithdrawalStrategyDto>>> GetWithdrawalStrategies(
        [FromQuery] Guid? retirementScenarioId,
        [FromQuery] WithdrawalStrategyType? strategyType,
        [FromQuery] bool? adjustForInflation,
        [FromQuery] decimal? minWithdrawalRate,
        [FromQuery] decimal? maxWithdrawalRate)
    {
        _logger.LogInformation("Getting withdrawal strategies for scenario {RetirementScenarioId}", retirementScenarioId);

        var result = await _mediator.Send(new GetWithdrawalStrategiesQuery
        {
            RetirementScenarioId = retirementScenarioId,
            StrategyType = strategyType,
            AdjustForInflation = adjustForInflation,
            MinWithdrawalRate = minWithdrawalRate,
            MaxWithdrawalRate = maxWithdrawalRate,
        });

        return Ok(result);
    }

    [HttpGet("{withdrawalStrategyId:guid}")]
    [ProducesResponseType(typeof(WithdrawalStrategyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WithdrawalStrategyDto>> GetWithdrawalStrategyById(Guid withdrawalStrategyId)
    {
        _logger.LogInformation("Getting withdrawal strategy {WithdrawalStrategyId}", withdrawalStrategyId);

        var result = await _mediator.Send(new GetWithdrawalStrategyByIdQuery { WithdrawalStrategyId = withdrawalStrategyId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WithdrawalStrategyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WithdrawalStrategyDto>> CreateWithdrawalStrategy([FromBody] CreateWithdrawalStrategyCommand command)
    {
        _logger.LogInformation("Creating withdrawal strategy '{Name}' for scenario {RetirementScenarioId}", command.Name, command.RetirementScenarioId);

        var result = await _mediator.Send(command);

        return Created($"/api/withdrawalstrategies/{result.WithdrawalStrategyId}", result);
    }

    [HttpPut("{withdrawalStrategyId:guid}")]
    [ProducesResponseType(typeof(WithdrawalStrategyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WithdrawalStrategyDto>> UpdateWithdrawalStrategy(Guid withdrawalStrategyId, [FromBody] UpdateWithdrawalStrategyCommand command)
    {
        if (withdrawalStrategyId != command.WithdrawalStrategyId)
        {
            return BadRequest("Withdrawal strategy ID mismatch");
        }

        _logger.LogInformation("Updating withdrawal strategy {WithdrawalStrategyId}", withdrawalStrategyId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{withdrawalStrategyId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWithdrawalStrategy(Guid withdrawalStrategyId)
    {
        _logger.LogInformation("Deleting withdrawal strategy {WithdrawalStrategyId}", withdrawalStrategyId);

        var result = await _mediator.Send(new DeleteWithdrawalStrategyCommand { WithdrawalStrategyId = withdrawalStrategyId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
