using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record UpdateAchievementCommand : IRequest<AchievementDto?>
{
    public Guid AchievementId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime AchievedDate { get; init; }
    public string? Impact { get; init; }
    public string? Category { get; init; }
    public bool IsKeyAchievement { get; init; }
}

public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, AchievementDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<UpdateAchievementCommandHandler> _logger;

    public UpdateAchievementCommandHandler(
        IPerformanceReviewPrepToolContext context,
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
        achievement.AchievedDate = request.AchievedDate;
        achievement.Impact = request.Impact;
        achievement.Category = request.Category;
        achievement.IsKeyAchievement = request.IsKeyAchievement;
        achievement.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated achievement {AchievementId}", request.AchievementId);

        return achievement.ToDto();
    }
}
