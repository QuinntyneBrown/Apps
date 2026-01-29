using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record GetSkillsQuery(Guid TenantId, Guid UserId) : IRequest<IEnumerable<SkillDto>>;

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, IEnumerable<SkillDto>>
{
    private readonly ISkillsDbContext _context;

    public GetSkillsQueryHandler(ISkillsDbContext context) => _context = context;

    public async Task<IEnumerable<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Skills
            .Where(s => s.TenantId == request.TenantId && s.UserId == request.UserId)
            .Select(s => new SkillDto(s.SkillId, s.TenantId, s.UserId, s.Name, s.Description, s.ProficiencyLevel, s.CreatedAt, s.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
