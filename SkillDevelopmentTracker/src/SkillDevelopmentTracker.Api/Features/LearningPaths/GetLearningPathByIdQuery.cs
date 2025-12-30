using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record GetLearningPathByIdQuery : IRequest<LearningPathDto?>
{
    public Guid LearningPathId { get; init; }
}

public class GetLearningPathByIdQueryHandler : IRequestHandler<GetLearningPathByIdQuery, LearningPathDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetLearningPathByIdQueryHandler> _logger;

    public GetLearningPathByIdQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetLearningPathByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LearningPathDto?> Handle(GetLearningPathByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting learning path {LearningPathId}", request.LearningPathId);

        var learningPath = await _context.LearningPaths
            .AsNoTracking()
            .FirstOrDefaultAsync(lp => lp.LearningPathId == request.LearningPathId, cancellationToken);

        if (learningPath == null)
        {
            _logger.LogWarning("Learning path {LearningPathId} not found", request.LearningPathId);
            return null;
        }

        return learningPath.ToDto();
    }
}
