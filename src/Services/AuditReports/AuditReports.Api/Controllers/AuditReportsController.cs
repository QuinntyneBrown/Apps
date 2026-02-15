using AuditReports.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuditReports.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<AuditReportDto>>> GetReports(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuditReportsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuditReportDto>> GetReportById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuditReportByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuditReportDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<AuditReportDto>> GenerateReport(
        [FromBody] GenerateAuditReportCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetReportById), new { id = result.ReportId }, result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReport(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteAuditReportCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
