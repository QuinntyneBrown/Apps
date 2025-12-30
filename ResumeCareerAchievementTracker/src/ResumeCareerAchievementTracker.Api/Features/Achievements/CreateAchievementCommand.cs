using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record CreateAchievementCommand : IRequest<AchievementDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public AchievementType AchievementType { get; init; }
    public DateTime AchievedDate { get; init; }
    public string? Organization { get; init; }
    public string? Impact { get; init; }
    public List<Guid>? SkillIds { get; init; }
    public List<Guid>? ProjectIds { get; init; }
    public List<string>? Tags { get; init; }
}

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, AchievementDto>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<CreateAchievementCommandHandler> _logger;

    public CreateAchievementCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<CreateAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating achievement for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            AchievementType = request.AchievementType,
            AchievedDate = request.AchievedDate,
            Organization = request.Organization,
            Impact = request.Impact,
            SkillIds = request.SkillIds ?? new List<Guid>(),
            ProjectIds = request.ProjectIds ?? new List<Guid>(),
            Tags = request.Tags ?? new List<string>(),
            IsFeatured = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created achievement {AchievementId} for user {UserId}",
            achievement.AchievementId,
            request.UserId);

        return achievement.ToDto();
    }
}
