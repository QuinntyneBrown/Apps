using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record UpdateLearningPathCommand : IRequest<LearningPathDto?>
{
    public Guid LearningPathId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public int ProgressPercentage { get; init; }
    public string? Notes { get; init; }
}

public class UpdateLearningPathCommandHandler : IRequestHandler<UpdateLearningPathCommand, LearningPathDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<UpdateLearningPathCommandHandler> _logger;

    public UpdateLearningPathCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<UpdateLearningPathCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LearningPathDto?> Handle(UpdateLearningPathCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating learning path {LearningPathId}", request.LearningPathId);

        var learningPath = await _context.LearningPaths
            .FirstOrDefaultAsync(lp => lp.LearningPathId == request.LearningPathId, cancellationToken);

        if (learningPath == null)
        {
            _logger.LogWarning("Learning path {LearningPathId} not found", request.LearningPathId);
            return null;
        }

        learningPath.Title = request.Title;
        learningPath.Description = request.Description;
        learningPath.StartDate = request.StartDate;
        learningPath.TargetDate = request.TargetDate;
        learningPath.Notes = request.Notes;
        learningPath.UpdateProgress(request.ProgressPercentage);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated learning path {LearningPathId}", request.LearningPathId);

        return learningPath.ToDto();
    }
}
