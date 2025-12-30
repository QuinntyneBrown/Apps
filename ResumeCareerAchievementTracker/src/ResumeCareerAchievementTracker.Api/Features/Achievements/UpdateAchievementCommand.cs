using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record UpdateAchievementCommand : IRequest<AchievementDto?>
{
    public Guid AchievementId { get; init; }
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

public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, AchievementDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<UpdateAchievementCommandHandler> _logger;

    public UpdateAchievementCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<UpdateAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto?> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating achievement {AchievementId}", request.AchievementId);

        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null)
        {
            _logger.LogWarning("Achievement {AchievementId} not found", request.AchievementId);
            return null;
        }

        achievement.Title = request.Title;
        achievement.Description = request.Description;
        achievement.AchievementType = request.AchievementType;
        achievement.AchievedDate = request.AchievedDate;
        achievement.Organization = request.Organization;
        achievement.Impact = request.Impact;
        achievement.SkillIds = request.SkillIds ?? new List<Guid>();
        achievement.ProjectIds = request.ProjectIds ?? new List<Guid>();
        achievement.Tags = request.Tags ?? new List<string>();
        achievement.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated achievement {AchievementId}", request.AchievementId);

        return achievement.ToDto();
    }
}
