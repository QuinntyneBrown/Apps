using ProfessionalNetworkCRM.Api.Features.Introductions;
using ProfessionalNetworkCRM.Core.Model.IntroductionAggregate.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IntroductionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IntroductionsController> _logger;

    public IntroductionsController(IMediator mediator, ILogger<IntroductionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IntroductionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IntroductionDto>>> GetIntroductions(
        [FromQuery] Guid? contactId,
        [FromQuery] IntroductionStatus? status)
    {
        _logger.LogInformation("Getting introductions");

        var result = await _mediator.Send(new GetIntroductionsQuery
        {
            ContactId = contactId,
            Status = status,
        });

        return Ok(result);
    }

    [HttpPost("request")]
    [ProducesResponseType(typeof(IntroductionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IntroductionDto>> RequestIntroduction([FromBody] RequestIntroductionCommand command)
    {
        _logger.LogInformation("Requesting introduction from {FromContactId} to {ToContactId}",
            command.FromContactId, command.ToContactId);

        var result = await _mediator.Send(command);

        return Created($"/api/introductions/{result.IntroductionId}", result);
    }

    [HttpPost("make")]
    [ProducesResponseType(typeof(IntroductionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IntroductionDto>> MakeIntroduction([FromBody] MakeIntroductionCommand command)
    {
        _logger.LogInformation("Making introduction {IntroductionId}", command.IntroductionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Created($"/api/introductions/{result.IntroductionId}", result);
    }
}
