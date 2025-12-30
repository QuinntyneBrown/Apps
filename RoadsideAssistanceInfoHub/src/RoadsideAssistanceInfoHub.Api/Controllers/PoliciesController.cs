using RoadsideAssistanceInfoHub.Api.Features.Policies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RoadsideAssistanceInfoHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PoliciesController> _logger;

    public PoliciesController(IMediator mediator, ILogger<PoliciesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PolicyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PolicyDto>>> GetPolicies(
        [FromQuery] Guid? vehicleId,
        [FromQuery] string? provider)
    {
        _logger.LogInformation("Getting policies");

        var result = await _mediator.Send(new GetPoliciesQuery
        {
            VehicleId = vehicleId,
            Provider = provider,
        });

        return Ok(result);
    }

    [HttpGet("{policyId:guid}")]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PolicyDto>> GetPolicyById(Guid policyId)
    {
        _logger.LogInformation("Getting policy {PolicyId}", policyId);

        var result = await _mediator.Send(new GetPolicyByIdQuery { PolicyId = policyId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PolicyDto>> CreatePolicy([FromBody] CreatePolicyCommand command)
    {
        _logger.LogInformation("Creating policy");

        var result = await _mediator.Send(command);

        return Created($"/api/policies/{result.PolicyId}", result);
    }

    [HttpPut("{policyId:guid}")]
    [ProducesResponseType(typeof(PolicyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PolicyDto>> UpdatePolicy(Guid policyId, [FromBody] UpdatePolicyCommand command)
    {
        if (policyId != command.PolicyId)
        {
            return BadRequest("Policy ID mismatch");
        }

        _logger.LogInformation("Updating policy {PolicyId}", policyId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{policyId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePolicy(Guid policyId)
    {
        _logger.LogInformation("Deleting policy {PolicyId}", policyId);

        var result = await _mediator.Send(new DeletePolicyCommand { PolicyId = policyId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
