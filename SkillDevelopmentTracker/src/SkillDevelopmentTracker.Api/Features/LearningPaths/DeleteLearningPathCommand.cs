using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record DeleteLearningPathCommand : IRequest<bool>
{
    public Guid LearningPathId { get; init; }
}

public class DeleteLearningPathCommandHandler : IRequestHandler<DeleteLearningPathCommand, bool>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<DeleteLearningPathCommandHandler> _logger;

    public DeleteLearningPathCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<DeleteLearningPathCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteLearningPathCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting learning path {LearningPathId}", request.LearningPathId);

        var learningPath = await _context.LearningPaths
            .FirstOrDefaultAsync(lp => lp.LearningPathId == request.LearningPathId, cancellationToken);

        if (learningPath == null)
        {
            _logger.LogWarning("Learning path {LearningPathId} not found", request.LearningPathId);
            return false;
        }

        _context.LearningPaths.Remove(learningPath);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted learning path {LearningPathId}", request.LearningPathId);

        return true;
    }
}
