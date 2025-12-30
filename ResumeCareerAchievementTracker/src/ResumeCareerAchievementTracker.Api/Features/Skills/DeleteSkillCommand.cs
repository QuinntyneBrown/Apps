using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record DeleteSkillCommand : IRequest<bool>
{
    public Guid SkillId { get; init; }
}

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, bool>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<DeleteSkillCommandHandler> _logger;

    public DeleteSkillCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<DeleteSkillCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting skill {SkillId}", request.SkillId);

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null)
        {
            _logger.LogWarning("Skill {SkillId} not found", request.SkillId);
            return false;
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted skill {SkillId}", request.SkillId);

        return true;
    }
}
