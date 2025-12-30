using StressMoodTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StressMoodTracker.Api.Features.Triggers;

public record GetTriggersQuery : IRequest<IEnumerable<TriggerDto>>
{
    public Guid? UserId { get; init; }
    public string? TriggerType { get; init; }
    public int? MinImpactLevel { get; init; }
}

public class GetTriggersQueryHandler : IRequestHandler<GetTriggersQuery, IEnumerable<TriggerDto>>
{
    private readonly IStressMoodTrackerContext _context;
    private readonly ILogger<GetTriggersQueryHandler> _logger;

    public GetTriggersQueryHandler(
        IStressMoodTrackerContext context,
        ILogger<GetTriggersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TriggerDto>> Handle(GetTriggersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting triggers for user {UserId}", request.UserId);

        var query = _context.Triggers.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.TriggerType))
        {
            query = query.Where(t => t.TriggerType == request.TriggerType);
        }

        if (request.MinImpactLevel.HasValue)
        {
            query = query.Where(t => t.ImpactLevel >= request.MinImpactLevel.Value);
        }

        var triggers = await query
            .OrderByDescending(t => t.ImpactLevel)
            .ThenBy(t => t.Name)
            .ToListAsync(cancellationToken);

        return triggers.Select(t => t.ToDto());
    }
}
