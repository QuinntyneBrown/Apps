using PersonalMissionStatementBuilder.Api.Features.MissionStatements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PersonalMissionStatementBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionStatementsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MissionStatementsController> _logger;

    public MissionStatementsController(IMediator mediator, ILogger<MissionStatementsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MissionStatementDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MissionStatementDto>>> GetMissionStatements(
        [FromQuery] Guid? userId,
        [FromQuery] bool? currentVersionOnly)
    {
        _logger.LogInformation("Getting mission statements for user {UserId}", userId);

        var result = await _mediator.Send(new GetMissionStatementsQuery
        {
            UserId = userId,
            CurrentVersionOnly = currentVersionOnly,
        });

        return Ok(result);
    }

    [HttpGet("{missionStatementId:guid}")]
    [ProducesResponseType(typeof(MissionStatementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MissionStatementDto>> GetMissionStatementById(Guid missionStatementId)
    {
        _logger.LogInformation("Getting mission statement {MissionStatementId}", missionStatementId);

        var result = await _mediator.Send(new GetMissionStatementByIdQuery { MissionStatementId = missionStatementId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MissionStatementDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MissionStatementDto>> CreateMissionStatement([FromBody] CreateMissionStatementCommand command)
    {
        _logger.LogInformation("Creating mission statement for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/missionstatements/{result.MissionStatementId}", result);
    }

    [HttpPut("{missionStatementId:guid}")]
    [ProducesResponseType(typeof(MissionStatementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MissionStatementDto>> UpdateMissionStatement(Guid missionStatementId, [FromBody] UpdateMissionStatementCommand command)
    {
        if (missionStatementId != command.MissionStatementId)
        {
            return BadRequest("Mission statement ID mismatch");
        }

        _logger.LogInformation("Updating mission statement {MissionStatementId}", missionStatementId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{missionStatementId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMissionStatement(Guid missionStatementId)
    {
        _logger.LogInformation("Deleting mission statement {MissionStatementId}", missionStatementId);

        var result = await _mediator.Send(new DeleteMissionStatementCommand { MissionStatementId = missionStatementId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
