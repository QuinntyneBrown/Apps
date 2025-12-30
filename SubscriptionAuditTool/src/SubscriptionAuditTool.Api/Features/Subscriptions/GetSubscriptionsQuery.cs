using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Subscriptions;

public record GetSubscriptionsQuery : IRequest<IEnumerable<SubscriptionDto>>
{
    public SubscriptionStatus? Status { get; init; }
    public Guid? CategoryId { get; init; }
    public BillingCycle? BillingCycle { get; init; }
}

public class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsQuery, IEnumerable<SubscriptionDto>>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<GetSubscriptionsQueryHandler> _logger;

    public GetSubscriptionsQueryHandler(
        ISubscriptionAuditToolContext context,
        ILogger<GetSubscriptionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SubscriptionDto>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting subscriptions");

        var query = _context.Subscriptions
            .Include(s => s.Category)
            .AsNoTracking();

        if (request.Status.HasValue)
        {
            query = query.Where(s => s.Status == request.Status.Value);
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(s => s.CategoryId == request.CategoryId.Value);
        }

        if (request.BillingCycle.HasValue)
        {
            query = query.Where(s => s.BillingCycle == request.BillingCycle.Value);
        }

        var subscriptions = await query
            .OrderBy(s => s.ServiceName)
            .ToListAsync(cancellationToken);

        return subscriptions.Select(s => s.ToDto());
    }
}
