using VehicleValueTracker.Api.Features.ValueAssessments;
using VehicleValueTracker.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VehicleValueTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValueAssessmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ValueAssessmentsController> _logger;

    public ValueAssessmentsController(IMediator mediator, ILogger<ValueAssessmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ValueAssessmentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ValueAssessmentDto>>> GetValueAssessments(
        [FromQuery] Guid? vehicleId,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] ConditionGrade? conditionGrade)
    {
        _logger.LogInformation("Getting value assessments for vehicle {VehicleId}", vehicleId);

        var result = await _mediator.Send(new GetValueAssessmentsQuery
        {
            VehicleId = vehicleId,
            FromDate = fromDate,
            ToDate = toDate,
            ConditionGrade = conditionGrade,
        });

        return Ok(result);
    }

    [HttpGet("{valueAssessmentId:guid}")]
    [ProducesResponseType(typeof(ValueAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueAssessmentDto>> GetValueAssessmentById(Guid valueAssessmentId)
    {
        _logger.LogInformation("Getting value assessment {ValueAssessmentId}", valueAssessmentId);

        var result = await _mediator.Send(new GetValueAssessmentByIdQuery { ValueAssessmentId = valueAssessmentId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ValueAssessmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ValueAssessmentDto>> CreateValueAssessment([FromBody] CreateValueAssessmentCommand command)
    {
        _logger.LogInformation("Creating value assessment for vehicle {VehicleId}", command.VehicleId);

        var result = await _mediator.Send(command);

        return Created($"/api/valueassessments/{result.ValueAssessmentId}", result);
    }

    [HttpPut("{valueAssessmentId:guid}")]
    [ProducesResponseType(typeof(ValueAssessmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValueAssessmentDto>> UpdateValueAssessment(Guid valueAssessmentId, [FromBody] UpdateValueAssessmentCommand command)
    {
        if (valueAssessmentId != command.ValueAssessmentId)
        {
            return BadRequest("Value assessment ID mismatch");
        }

        _logger.LogInformation("Updating value assessment {ValueAssessmentId}", valueAssessmentId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{valueAssessmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteValueAssessment(Guid valueAssessmentId)
    {
        _logger.LogInformation("Deleting value assessment {ValueAssessmentId}", valueAssessmentId);

        var result = await _mediator.Send(new DeleteValueAssessmentCommand { ValueAssessmentId = valueAssessmentId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
