using Achievements.Core;
using Achievements.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Achievements.Api.Features;

public record UpdateAchievementCommand(
    Guid AchievementId,
    string Title,
    string Description,
    AchievementType AchievementType,
    DateTime AchievedDate,
    string? Organization,
    string? Impact,
    bool IsFeatured) : IRequest<AchievementDto?>;

public class UpdateAchievementCommandHandler : IRequestHandler<UpdateAchievementCommand, AchievementDto?>
{
    private readonly IAchievementsDbContext _context;
    private readonly ILogger<UpdateAchievementCommandHandler> _logger;

    public UpdateAchievementCommandHandler(IAchievementsDbContext context, ILogger<UpdateAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto?> Handle(UpdateAchievementCommand request, CancellationToken cancellationToken)
    {
        var achievement = await _context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementId == request.AchievementId, cancellationToken);

        if (achievement == null) return null;

        achievement.Title = request.Title;
        achievement.Description = request.Description;
        achievement.AchievementType = request.AchievementType;
        achievement.AchievedDate = request.AchievedDate;
        achievement.Organization = request.Organization;
        achievement.Impact = request.Impact;
        achievement.IsFeatured = request.IsFeatured;
        achievement.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Achievement updated: {AchievementId}", achievement.AchievementId);

        return achievement.ToDto();
    }
}
