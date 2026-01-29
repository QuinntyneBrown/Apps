using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record GetSkillByIdQuery(Guid SkillId, Guid TenantId) : IRequest<SkillDto?>;

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillDto?>
{
    private readonly ISkillsDbContext _context;

    public GetSkillByIdQueryHandler(ISkillsDbContext context) => _context = context;

    public async Task<SkillDto?> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == request.SkillId && s.TenantId == request.TenantId, cancellationToken);
        return skill == null ? null : new SkillDto(skill.SkillId, skill.TenantId, skill.UserId, skill.Name, skill.Description, skill.ProficiencyLevel, skill.CreatedAt, skill.UpdatedAt);
    }
}
