using SubscriptionAuditTool.Api.Features.Subscriptions;
using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SubscriptionAuditTool.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SubscriptionsController> _logger;

    public SubscriptionsController(IMediator mediator, ILogger<SubscriptionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SubscriptionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetSubscriptions(
        [FromQuery] SubscriptionStatus? status,
        [FromQuery] Guid? categoryId,
        [FromQuery] BillingCycle? billingCycle)
    {
        _logger.LogInformation("Getting subscriptions with filters");

        var result = await _mediator.Send(new GetSubscriptionsQuery
        {
            Status = status,
            CategoryId = categoryId,
            BillingCycle = billingCycle,
        });

        return Ok(result);
    }

    [HttpGet("{subscriptionId:guid}")]
    [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubscriptionDto>> GetSubscriptionById(Guid subscriptionId)
    {
        _logger.LogInformation("Getting subscription {SubscriptionId}", subscriptionId);

        var result = await _mediator.Send(new GetSubscriptionByIdQuery { SubscriptionId = subscriptionId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubscriptionDto>> CreateSubscription([FromBody] CreateSubscriptionCommand command)
    {
        _logger.LogInformation("Creating subscription for service {ServiceName}", command.ServiceName);

        var result = await _mediator.Send(command);

        return Created($"/api/subscriptions/{result.SubscriptionId}", result);
    }

    [HttpPut("{subscriptionId:guid}")]
    [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubscriptionDto>> UpdateSubscription(
        Guid subscriptionId,
        [FromBody] UpdateSubscriptionCommand command)
    {
        if (subscriptionId != command.SubscriptionId)
        {
            return BadRequest("Subscription ID mismatch");
        }

        _logger.LogInformation("Updating subscription {SubscriptionId}", subscriptionId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{subscriptionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        _logger.LogInformation("Deleting subscription {SubscriptionId}", subscriptionId);

        var result = await _mediator.Send(new DeleteSubscriptionCommand { SubscriptionId = subscriptionId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
