using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record UpdateSkillCommand(Guid SkillId, Guid TenantId, string? Name, string? Description, int? ProficiencyLevel) : IRequest<SkillDto?>;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillDto?>
{
    private readonly ISkillsDbContext _context;

    public UpdateSkillCommandHandler(ISkillsDbContext context) => _context = context;

    public async Task<SkillDto?> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == request.SkillId && s.TenantId == request.TenantId, cancellationToken);
        if (skill == null) return null;
        skill.Update(request.Name, request.Description, request.ProficiencyLevel);
        await _context.SaveChangesAsync(cancellationToken);
        return new SkillDto(skill.SkillId, skill.TenantId, skill.UserId, skill.Name, skill.Description, skill.ProficiencyLevel, skill.CreatedAt, skill.UpdatedAt);
    }
}
