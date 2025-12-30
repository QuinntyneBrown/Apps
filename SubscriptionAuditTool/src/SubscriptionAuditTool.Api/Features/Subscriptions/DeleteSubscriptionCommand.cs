using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record DeleteSubscriptionCommand : IRequest<bool>
{
    public Guid SubscriptionId { get; init; }
}

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, bool>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<DeleteSubscriptionCommandHandler> _logger;

    public DeleteSubscriptionCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<DeleteSubscriptionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting subscription {SubscriptionId}",
            request.SubscriptionId);

        var subscription = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.SubscriptionId == request.SubscriptionId, cancellationToken);

        if (subscription == null)
        {
            _logger.LogWarning(
                "Subscription {SubscriptionId} not found",
                request.SubscriptionId);
            return false;
        }

        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted subscription {SubscriptionId}",
            request.SubscriptionId);

        return true;
    }
}
