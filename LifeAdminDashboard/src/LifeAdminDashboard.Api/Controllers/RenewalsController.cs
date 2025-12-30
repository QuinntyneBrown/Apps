using LifeAdminDashboard.Api.Features.Renewals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeAdminDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RenewalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RenewalsController> _logger;

    public RenewalsController(IMediator mediator, ILogger<RenewalsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RenewalDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RenewalDto>>> GetRenewals(
        [FromQuery] Guid? userId,
        [FromQuery] string? renewalType,
        [FromQuery] bool? isActive,
        [FromQuery] bool? isDueSoon)
    {
        _logger.LogInformation("Getting renewals for user {UserId}", userId);

        var result = await _mediator.Send(new GetRenewalsQuery
        {
            UserId = userId,
            RenewalType = renewalType,
            IsActive = isActive,
            IsDueSoon = isDueSoon,
        });

        return Ok(result);
    }

    [HttpGet("{renewalId:guid}")]
    [ProducesResponseType(typeof(RenewalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RenewalDto>> GetRenewalById(Guid renewalId)
    {
        _logger.LogInformation("Getting renewal {RenewalId}", renewalId);

        var result = await _mediator.Send(new GetRenewalByIdQuery { RenewalId = renewalId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RenewalDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RenewalDto>> CreateRenewal([FromBody] CreateRenewalCommand command)
    {
        _logger.LogInformation("Creating renewal for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/renewals/{result.RenewalId}", result);
    }

    [HttpPut("{renewalId:guid}")]
    [ProducesResponseType(typeof(RenewalDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RenewalDto>> UpdateRenewal(Guid renewalId, [FromBody] UpdateRenewalCommand command)
    {
        if (renewalId != command.RenewalId)
        {
            return BadRequest("Renewal ID mismatch");
        }

        _logger.LogInformation("Updating renewal {RenewalId}", renewalId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{renewalId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRenewal(Guid renewalId)
    {
        _logger.LogInformation("Deleting renewal {RenewalId}", renewalId);

        var result = await _mediator.Send(new DeleteRenewalCommand { RenewalId = renewalId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
