using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record ToggleSkillFeaturedCommand : IRequest<SkillDto?>
{
    public Guid SkillId { get; init; }
}

public class ToggleSkillFeaturedCommandHandler : IRequestHandler<ToggleSkillFeaturedCommand, SkillDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<ToggleSkillFeaturedCommandHandler> _logger;

    public ToggleSkillFeaturedCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<ToggleSkillFeaturedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto?> Handle(ToggleSkillFeaturedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Toggling featured status for skill {SkillId}", request.SkillId);

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null)
        {
            _logger.LogWarning("Skill {SkillId} not found", request.SkillId);
            return null;
        }

        skill.ToggleFeatured();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Toggled featured status for skill {SkillId} to {IsFeatured}",
            request.SkillId,
            skill.IsFeatured);

        return skill.ToDto();
    }
}
