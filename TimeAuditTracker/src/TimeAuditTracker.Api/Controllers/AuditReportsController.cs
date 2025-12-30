using TimeAuditTracker.Api.Features.AuditReports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TimeAuditTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuditReportsController> _logger;

    public AuditReportsController(IMediator mediator, ILogger<AuditReportsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuditReportDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuditReportDto>>> GetAuditReports(
        [FromQuery] Guid? userId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        _logger.LogInformation("Getting audit reports for user {UserId}", userId);

        var result = await _mediator.Send(new GetAuditReportsQuery
        {
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
        });

        return Ok(result);
    }

    [HttpGet("{auditReportId:guid}")]
    [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuditReportDto>> GetAuditReportById(Guid auditReportId)
    {
        _logger.LogInformation("Getting audit report {AuditReportId}", auditReportId);

        var result = await _mediator.Send(new GetAuditReportByIdQuery { AuditReportId = auditReportId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuditReportDto>> CreateAuditReport([FromBody] CreateAuditReportCommand command)
    {
        _logger.LogInformation("Creating audit report for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/auditreports/{result.AuditReportId}", result);
    }

    [HttpPut("{auditReportId:guid}")]
    [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuditReportDto>> UpdateAuditReport(Guid auditReportId, [FromBody] UpdateAuditReportCommand command)
    {
        if (auditReportId != command.AuditReportId)
        {
            return BadRequest("Audit report ID mismatch");
        }

        _logger.LogInformation("Updating audit report {AuditReportId}", auditReportId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{auditReportId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuditReport(Guid auditReportId)
    {
        _logger.LogInformation("Deleting audit report {AuditReportId}", auditReportId);

        var result = await _mediator.Send(new DeleteAuditReportCommand { AuditReportId = auditReportId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
