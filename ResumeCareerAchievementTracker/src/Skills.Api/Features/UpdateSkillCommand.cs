using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record UpdateSkillCommand(
    Guid SkillId,
    string Name,
    string Category,
    string ProficiencyLevel,
    decimal? YearsOfExperience,
    bool IsFeatured) : IRequest<SkillDto?>;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto?>
{
    private readonly ISkillsDbContext _context;
    private readonly ILogger<UpdateSkillCommandHandler> _logger;

    public UpdateSkillCommandHandler(ISkillsDbContext context, ILogger<UpdateSkillCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SkillDto?> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        if (skill == null) return null;

        skill.Name = request.Name;
        skill.Category = request.Category;
        skill.ProficiencyLevel = request.ProficiencyLevel;
        skill.YearsOfExperience = request.YearsOfExperience;
        skill.IsFeatured = request.IsFeatured;
        skill.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Skill updated: {SkillId}", skill.SkillId);

        return skill.ToDto();
    }
}
