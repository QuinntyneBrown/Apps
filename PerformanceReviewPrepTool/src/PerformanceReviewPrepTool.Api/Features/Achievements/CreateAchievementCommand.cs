using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Achievements;

public record CreateAchievementCommand : IRequest<AchievementDto>
{
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime AchievedDate { get; init; }
    public string? Impact { get; init; }
    public string? Category { get; init; }
    public bool IsKeyAchievement { get; init; }
}

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, AchievementDto>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<CreateAchievementCommandHandler> _logger;

    public CreateAchievementCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<CreateAchievementCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AchievementDto> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating achievement for user {UserId}: {Title}",
            request.UserId,
            request.Title);

        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = request.UserId,
            ReviewPeriodId = request.ReviewPeriodId,
            Title = request.Title,
            Description = request.Description,
            AchievedDate = request.AchievedDate,
            Impact = request.Impact,
            Category = request.Category,
            IsKeyAchievement = request.IsKeyAchievement,
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
