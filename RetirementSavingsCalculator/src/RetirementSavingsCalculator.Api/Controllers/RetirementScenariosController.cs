using RetirementSavingsCalculator.Api.Features.RetirementScenarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RetirementSavingsCalculator.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RetirementScenariosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RetirementScenariosController> _logger;

    public RetirementScenariosController(IMediator mediator, ILogger<RetirementScenariosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RetirementScenarioDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RetirementScenarioDto>>> GetRetirementScenarios(
        [FromQuery] int? minCurrentAge,
        [FromQuery] int? maxCurrentAge,
        [FromQuery] int? minRetirementAge,
        [FromQuery] int? maxRetirementAge,
        [FromQuery] decimal? minCurrentSavings,
        [FromQuery] decimal? maxCurrentSavings)
    {
        _logger.LogInformation("Getting retirement scenarios");

        var result = await _mediator.Send(new GetRetirementScenariosQuery
        {
            MinCurrentAge = minCurrentAge,
            MaxCurrentAge = maxCurrentAge,
            MinRetirementAge = minRetirementAge,
            MaxRetirementAge = maxRetirementAge,
            MinCurrentSavings = minCurrentSavings,
            MaxCurrentSavings = maxCurrentSavings,
        });

        return Ok(result);
    }

    [HttpGet("{retirementScenarioId:guid}")]
    [ProducesResponseType(typeof(RetirementScenarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RetirementScenarioDto>> GetRetirementScenarioById(Guid retirementScenarioId)
    {
        _logger.LogInformation("Getting retirement scenario {RetirementScenarioId}", retirementScenarioId);

        var result = await _mediator.Send(new GetRetirementScenarioByIdQuery { RetirementScenarioId = retirementScenarioId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RetirementScenarioDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RetirementScenarioDto>> CreateRetirementScenario([FromBody] CreateRetirementScenarioCommand command)
    {
        _logger.LogInformation("Creating retirement scenario: {Name}", command.Name);

        var result = await _mediator.Send(command);

        return Created($"/api/retirementscenarios/{result.RetirementScenarioId}", result);
    }

    [HttpPut("{retirementScenarioId:guid}")]
    [ProducesResponseType(typeof(RetirementScenarioDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RetirementScenarioDto>> UpdateRetirementScenario(Guid retirementScenarioId, [FromBody] UpdateRetirementScenarioCommand command)
    {
        if (retirementScenarioId != command.RetirementScenarioId)
        {
            return BadRequest("Retirement scenario ID mismatch");
        }

        _logger.LogInformation("Updating retirement scenario {RetirementScenarioId}", retirementScenarioId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{retirementScenarioId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRetirementScenario(Guid retirementScenarioId)
    {
        _logger.LogInformation("Deleting retirement scenario {RetirementScenarioId}", retirementScenarioId);

        var result = await _mediator.Send(new DeleteRetirementScenarioCommand { RetirementScenarioId = retirementScenarioId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
