using SleepQualityTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SleepQualityTracker.Api.Features.Patterns;

public record GetPatternByIdQuery : IRequest<PatternDto?>
{
    public Guid PatternId { get; init; }
}

public class GetPatternByIdQueryHandler : IRequestHandler<GetPatternByIdQuery, PatternDto?>
{
    private readonly ISleepQualityTrackerContext _context;
    private readonly ILogger<GetPatternByIdQueryHandler> _logger;

    public GetPatternByIdQueryHandler(
        ISleepQualityTrackerContext context,
        ILogger<GetPatternByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PatternDto?> Handle(GetPatternByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting pattern {PatternId}", request.PatternId);

        var pattern = await _context.Patterns
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PatternId == request.PatternId, cancellationToken);

        if (pattern == null)
        {
            _logger.LogWarning("Pattern {PatternId} not found", request.PatternId);
            return null;
        }

        return pattern.ToDto();
    }
}
