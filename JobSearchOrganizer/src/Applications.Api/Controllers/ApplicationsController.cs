using Applications.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Applications.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ApplicationsController> _logger;

    public ApplicationsController(IMediator mediator, ILogger<ApplicationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ApplicationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all applications");
        var result = await _mediator.Send(new GetApplicationsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting application {ApplicationId}", id);
        var result = await _mediator.Send(new GetApplicationByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ApplicationDto>> CreateApplication(
        [FromBody] CreateApplicationCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating application for user {UserId}", command.UserId);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetApplicationById), new { id = result.ApplicationId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApplicationDto>> UpdateApplication(
        Guid id,
        [FromBody] UpdateApplicationCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ApplicationId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating application {ApplicationId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteApplication(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting application {ApplicationId}", id);
        var result = await _mediator.Send(new DeleteApplicationCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
