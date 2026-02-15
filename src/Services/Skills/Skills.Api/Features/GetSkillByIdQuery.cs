using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record GetSkillByIdQuery(Guid SkillId) : IRequest<SkillDto?>;

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillDto?>
{
    private readonly ISkillsDbContext _context;

    public GetSkillByIdQueryHandler(ISkillsDbContext context)
    {
        _context = context;
    }

    public async Task<SkillDto?> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SkillId == request.SkillId, cancellationToken);

        return skill?.ToDto();
    }
}
