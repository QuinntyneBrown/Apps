using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record GetLearningPathsQuery : IRequest<IEnumerable<LearningPathDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetLearningPathsQueryHandler : IRequestHandler<GetLearningPathsQuery, IEnumerable<LearningPathDto>>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetLearningPathsQueryHandler> _logger;

    public GetLearningPathsQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetLearningPathsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<LearningPathDto>> Handle(GetLearningPathsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting learning paths for user {UserId}", request.UserId);

        var query = _context.LearningPaths.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(lp => lp.UserId == request.UserId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(lp => lp.IsCompleted == request.IsCompleted.Value);
        }

        var learningPaths = await query
            .OrderByDescending(lp => lp.StartDate)
            .ToListAsync(cancellationToken);

        return learningPaths.Select(lp => lp.ToDto());
    }
}
