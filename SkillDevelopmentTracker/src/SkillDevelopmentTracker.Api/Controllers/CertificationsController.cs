using SkillDevelopmentTracker.Api.Features.Certifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SkillDevelopmentTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CertificationsController> _logger;

    public CertificationsController(IMediator mediator, ILogger<CertificationsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CertificationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CertificationDto>>> GetCertifications(
        [FromQuery] Guid? userId,
        [FromQuery] bool? isActive,
        [FromQuery] string? issuingOrganization)
    {
        _logger.LogInformation("Getting certifications for user {UserId}", userId);

        var result = await _mediator.Send(new GetCertificationsQuery
        {
            UserId = userId,
            IsActive = isActive,
            IssuingOrganization = issuingOrganization,
        });

        return Ok(result);
    }

    [HttpGet("{certificationId:guid}")]
    [ProducesResponseType(typeof(CertificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CertificationDto>> GetCertificationById(Guid certificationId)
    {
        _logger.LogInformation("Getting certification {CertificationId}", certificationId);

        var result = await _mediator.Send(new GetCertificationByIdQuery { CertificationId = certificationId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CertificationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CertificationDto>> CreateCertification([FromBody] CreateCertificationCommand command)
    {
        _logger.LogInformation("Creating certification for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/certifications/{result.CertificationId}", result);
    }

    [HttpPut("{certificationId:guid}")]
    [ProducesResponseType(typeof(CertificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CertificationDto>> UpdateCertification(Guid certificationId, [FromBody] UpdateCertificationCommand command)
    {
        if (certificationId != command.CertificationId)
        {
            return BadRequest("Certification ID mismatch");
        }

        _logger.LogInformation("Updating certification {CertificationId}", certificationId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{certificationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCertification(Guid certificationId)
    {
        _logger.LogInformation("Deleting certification {CertificationId}", certificationId);

        var result = await _mediator.Send(new DeleteCertificationCommand { CertificationId = certificationId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
