using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record UpdateSubscriptionCommand : IRequest<SubscriptionDto?>
{
    public Guid SubscriptionId { get; init; }
    public string ServiceName { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public BillingCycle BillingCycle { get; init; }
    public DateTime NextBillingDate { get; init; }
    public SubscriptionStatus Status { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? CancellationDate { get; init; }
    public Guid? CategoryId { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, SubscriptionDto?>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<UpdateSubscriptionCommandHandler> _logger;

    public UpdateSubscriptionCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<UpdateSubscriptionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SubscriptionDto?> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating subscription {SubscriptionId}",
            request.SubscriptionId);

        var subscription = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.SubscriptionId == request.SubscriptionId, cancellationToken);

        if (subscription == null)
        {
            _logger.LogWarning(
                "Subscription {SubscriptionId} not found",
                request.SubscriptionId);
            return null;
        }

        subscription.ServiceName = request.ServiceName;
        subscription.Cost = request.Cost;
        subscription.BillingCycle = request.BillingCycle;
        subscription.NextBillingDate = request.NextBillingDate;
        subscription.Status = request.Status;
        subscription.StartDate = request.StartDate;
        subscription.CancellationDate = request.CancellationDate;
        subscription.CategoryId = request.CategoryId;
        subscription.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated subscription {SubscriptionId}",
            request.SubscriptionId);

        return subscription.ToDto();
    }
}
