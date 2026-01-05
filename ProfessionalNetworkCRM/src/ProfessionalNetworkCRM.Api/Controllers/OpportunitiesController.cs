using ProfessionalNetworkCRM.Api.Features.Opportunities;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProfessionalNetworkCRM.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpportunitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OpportunitiesController> _logger;

    public OpportunitiesController(IMediator mediator, ILogger<OpportunitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OpportunityDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OpportunityDto>>> GetOpportunities(
        [FromQuery] Guid? contactId,
        [FromQuery] OpportunityType? opportunityType,
        [FromQuery] OpportunityStatus? status)
    {
        _logger.LogInformation("Getting opportunities");

        var result = await _mediator.Send(new GetOpportunitiesQuery
        {
            ContactId = contactId,
            OpportunityType = opportunityType,
            Status = status,
        });

        return Ok(result);
    }

    [HttpGet("{opportunityId:guid}")]
    [ProducesResponseType(typeof(OpportunityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OpportunityDto>> GetOpportunityById(Guid opportunityId)
    {
        _logger.LogInformation("Getting opportunity {OpportunityId}", opportunityId);

        var result = await _mediator.Send(new GetOpportunityByIdQuery { OpportunityId = opportunityId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OpportunityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OpportunityDto>> CreateOpportunity([FromBody] CreateOpportunityCommand command)
    {
        _logger.LogInformation("Creating opportunity for contact {ContactId}", command.ContactId);

        var result = await _mediator.Send(command);

        return Created($"/api/opportunities/{result.OpportunityId}", result);
    }

    [HttpPut("{opportunityId:guid}")]
    [ProducesResponseType(typeof(OpportunityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OpportunityDto>> UpdateOpportunity(Guid opportunityId, [FromBody] UpdateOpportunityCommand command)
    {
        if (opportunityId != command.OpportunityId)
        {
            return BadRequest("Opportunity ID mismatch");
        }

        _logger.LogInformation("Updating opportunity {OpportunityId}", opportunityId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{opportunityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOpportunity(Guid opportunityId)
    {
        _logger.LogInformation("Deleting opportunity {OpportunityId}", opportunityId);

        var result = await _mediator.Send(new DeleteOpportunityCommand { OpportunityId = opportunityId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
