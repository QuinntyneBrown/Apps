using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record GetSubscriptionByIdQuery : IRequest<SubscriptionDto?>
{
    public Guid SubscriptionId { get; init; }
}

public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionDto?>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<GetSubscriptionByIdQueryHandler> _logger;

    public GetSubscriptionByIdQueryHandler(
        ISubscriptionAuditToolContext context,
        ILogger<GetSubscriptionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SubscriptionDto?> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting subscription {SubscriptionId}",
            request.SubscriptionId);

        var subscription = await _context.Subscriptions
            .Include(s => s.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SubscriptionId == request.SubscriptionId, cancellationToken);

        if (subscription == null)
        {
            _logger.LogWarning(
                "Subscription {SubscriptionId} not found",
                request.SubscriptionId);
            return null;
        }

        return subscription.ToDto();
    }
}
