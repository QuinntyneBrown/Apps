using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Achievements;

public record ToggleFeaturedCommand : IRequest<AchievementDto?>
{
    public Guid AchievementId { get; init; }
}

public class ToggleFeaturedCommandHandler : IRequestHandler<ToggleFeaturedCommand, AchievementDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<ToggleFeaturedCommandHandler> _logger;

    public ToggleFeaturedCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<ToggleFeaturedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto?> Handle(ToggleFeaturedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling featured status for achievement {AchievementId}", request.AchievementId);

        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null)
        {
            _logger.LogWarning("Achievement {AchievementId} not found", request.AchievementId);
            return null;
        }

        achievement.ToggleFeatured();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled featured status for achievement {AchievementId} to {IsFeatured}",
            request.AchievementId,
            achievement.IsFeatured);

        return achievement.ToDto();
    }
}
