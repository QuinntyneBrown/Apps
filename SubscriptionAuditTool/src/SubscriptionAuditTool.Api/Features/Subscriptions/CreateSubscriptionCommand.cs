using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record CreateSubscriptionCommand : IRequest<SubscriptionDto>
{
    public string ServiceName { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public BillingCycle BillingCycle { get; init; }
    public DateTime NextBillingDate { get; init; }
    public DateTime StartDate { get; init; }
    public Guid? CategoryId { get; init; }
    public string? Notes { get; init; }
}

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<CreateSubscriptionCommandHandler> _logger;

    public CreateSubscriptionCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<CreateSubscriptionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SubscriptionDto> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating subscription for service: {ServiceName}",
            request.ServiceName);

        var subscription = new Subscription
        {
            SubscriptionId = Guid.NewGuid(),
            ServiceName = request.ServiceName,
            Cost = request.Cost,
            BillingCycle = request.BillingCycle,
            NextBillingDate = request.NextBillingDate,
            Status = SubscriptionStatus.Active,
            StartDate = request.StartDate,
            CategoryId = request.CategoryId,
            Notes = request.Notes,
        };

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created subscription {SubscriptionId} for service {ServiceName}",
            subscription.SubscriptionId,
            request.ServiceName);

        return subscription.ToDto();
    }
}
