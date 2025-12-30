using ResumeCareerAchievementTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ResumeCareerAchievementTracker.Api.Features.Skills;

public record UpdateProficiencyCommand : IRequest<SkillDto?>
{
    public Guid SkillId { get; init; }
    public string ProficiencyLevel { get; init; } = string.Empty;
}

public class UpdateProficiencyCommandHandler : IRequestHandler<UpdateProficiencyCommand, SkillDto?>
{
    private readonly IResumeCareerAchievementTrackerContext _context;
    private readonly ILogger<UpdateProficiencyCommandHandler> _logger;

    public UpdateProficiencyCommandHandler(
        IResumeCareerAchievementTrackerContext context,
        ILogger<UpdateProficiencyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto?> Handle(UpdateProficiencyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating proficiency for skill {SkillId} to {ProficiencyLevel}",
            request.SkillId,
            request.ProficiencyLevel);

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null)
        {
            _logger.LogWarning("Skill {SkillId} not found", request.SkillId);
            return null;
        }

        skill.UpdateProficiency(request.ProficiencyLevel);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated proficiency for skill {SkillId} to {ProficiencyLevel}",
            request.SkillId,
            request.ProficiencyLevel);

        return skill.ToDto();
    }
}
