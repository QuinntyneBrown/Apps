using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Skills;

public record UpdateSkillCommand : IRequest<SkillDto?>
{
    public Guid SkillId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public ProficiencyLevel ProficiencyLevel { get; init; }
    public ProficiencyLevel? TargetLevel { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? TargetDate { get; init; }
    public decimal HoursSpent { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<UpdateSkillCommandHandler> _logger;

    public UpdateSkillCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<UpdateSkillCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto?> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating skill {SkillId}", request.SkillId);

        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null)
        {
            _logger.LogWarning("Skill {SkillId} not found", request.SkillId);
            return null;
        }

        skill.Name = request.Name;
        skill.Category = request.Category;
        skill.ProficiencyLevel = request.ProficiencyLevel;
        skill.TargetLevel = request.TargetLevel;
        skill.StartDate = request.StartDate;
        skill.TargetDate = request.TargetDate;
        skill.HoursSpent = request.HoursSpent;
        skill.Notes = request.Notes;
        skill.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated skill {SkillId}", request.SkillId);

        return skill.ToDto();
    }
}
