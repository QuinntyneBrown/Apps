using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Patterns;

public record GetPatternsQuery : IRequest<IEnumerable<PatternDto>>
{
    public Guid? UserId { get; init; }
    public string? PatternType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool? IsHighConfidence { get; init; }
}

public class GetPatternsQueryHandler : IRequestHandler<GetPatternsQuery, IEnumerable<PatternDto>>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<GetPatternsQueryHandler> _logger;

    public GetPatternsQueryHandler(
        ISleepQualityTrackerContext context,
        ILogger<GetPatternsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PatternDto>> Handle(GetPatternsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting patterns for user {UserId}", request.UserId);

        var query = _context.Patterns.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.PatternType))
        {
            query = query.Where(p => p.PatternType == request.PatternType);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(p => p.StartDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(p => p.EndDate <= request.EndDate.Value);
        }

        var patterns = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        var results = patterns.Select(p => p.ToDto());

        if (request.IsHighConfidence.HasValue)
        {
            results = results.Where(p => p.IsHighConfidence == request.IsHighConfidence.Value);
        }

        return results;
    }
}
