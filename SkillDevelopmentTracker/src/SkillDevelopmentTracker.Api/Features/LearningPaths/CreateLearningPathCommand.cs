using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.LearningPaths;

public record CreateLearningPathCommand : IRequest<LearningPathDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateLearningPathCommandHandler : IRequestHandler<CreateLearningPathCommand, LearningPathDto>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<CreateLearningPathCommandHandler> _logger;

    public CreateLearningPathCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<CreateLearningPathCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<LearningPathDto> Handle(CreateLearningPathCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating learning path for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var learningPath = new LearningPath
        {
            LearningPathId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            TargetDate = request.TargetDate,
            Notes = request.Notes,
            ProgressPercentage = 0,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.LearningPaths.Add(learningPath);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created learning path {LearningPathId} for user {UserId}",
            learningPath.LearningPathId,
            request.UserId);

        return learningPath.ToDto();
    }
}
