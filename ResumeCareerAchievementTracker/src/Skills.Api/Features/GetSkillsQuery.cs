using Skills.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Skills.Api.Features;

public record GetSkillsQuery : IRequest<IEnumerable<SkillDto>>;

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, IEnumerable<SkillDto>>
{
    private readonly ISkillsDbContext _context;

    public GetSkillsQueryHandler(ISkillsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Skills
            .AsNoTracking()
            .Select(s => s.ToDto())
            .ToListAsync(cancellationToken);
    }
}
