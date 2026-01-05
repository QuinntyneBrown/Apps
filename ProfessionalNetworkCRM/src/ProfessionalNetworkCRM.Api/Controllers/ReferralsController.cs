using ProfessionalNetworkCRM.Api.Features.Referrals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReferralsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReferralsController> _logger;

    public ReferralsController(IMediator mediator, ILogger<ReferralsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReferralDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReferralDto>>> GetReferrals(
        [FromQuery] Guid? sourceContactId,
        [FromQuery] bool? thankYouSent)
    {
        _logger.LogInformation("Getting referrals");

        var result = await _mediator.Send(new GetReferralsQuery
        {
            SourceContactId = sourceContactId,
            ThankYouSent = thankYouSent,
        });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReferralDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReferralDto>> CreateReferral([FromBody] CreateReferralCommand command)
    {
        _logger.LogInformation("Creating referral from contact {SourceContactId}", command.SourceContactId);

        var result = await _mediator.Send(command);

        return Created($"/api/referrals/{result.ReferralId}", result);
    }

    [HttpPut("{referralId:guid}")]
    [ProducesResponseType(typeof(ReferralDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReferralDto>> UpdateReferral(Guid referralId, [FromBody] UpdateReferralCommand command)
    {
        if (referralId != command.ReferralId)
        {
            return BadRequest("Referral ID mismatch");
        }

        _logger.LogInformation("Updating referral {ReferralId}", referralId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
