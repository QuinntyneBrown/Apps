using JobSearchOrganizer.Api.Features.Applications;
using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchOrganizer.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications(
        [FromQuery] Guid? userId,
        [FromQuery] Guid? companyId,
        [FromQuery] ApplicationStatus? status,
        [FromQuery] bool? isRemote)
    {
        _logger.LogInformation("Getting applications for user {UserId}", userId);

        var result = await _mediator.Send(new GetApplicationsQuery
        {
            UserId = userId,
            CompanyId = companyId,
            Status = status,
            IsRemote = isRemote,
        });

        return Ok(result);
    }

    [HttpGet("{applicationId:guid}")]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplicationDto>> GetApplicationById(Guid applicationId)
    {
        _logger.LogInformation("Getting application {ApplicationId}", applicationId);

        var result = await _mediator.Send(new GetApplicationByIdQuery { ApplicationId = applicationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApplicationDto>> CreateApplication([FromBody] CreateApplicationCommand command)
    {
        _logger.LogInformation("Creating application for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/applications/{result.ApplicationId}", result);
    }

    [HttpPut("{applicationId:guid}")]
    [ProducesResponseType(typeof(ApplicationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApplicationDto>> UpdateApplication(Guid applicationId, [FromBody] UpdateApplicationCommand command)
    {
        if (applicationId != command.ApplicationId)
        {
            return BadRequest("Application ID mismatch");
        }

        _logger.LogInformation("Updating application {ApplicationId}", applicationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteApplication(Guid applicationId)
    {
        _logger.LogInformation("Deleting application {ApplicationId}", applicationId);

        var result = await _mediator.Send(new DeleteApplicationCommand { ApplicationId = applicationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
