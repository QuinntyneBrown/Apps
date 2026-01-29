using Projects.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Features;

public record GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
{
    private readonly IProjectsDbContext _context;

    public GetProjectsQueryHandler(IProjectsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .Select(p => p.ToDto())
            .ToListAsync(cancellationToken);
    }
}
