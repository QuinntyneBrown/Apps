using Projects.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Features;

public record GetProjectByIdQuery(Guid ProjectId) : IRequest<ProjectDto?>;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IProjectsDbContext _context;

    public GetProjectByIdQueryHandler(IProjectsDbContext context)
    {
        _context = context;
    }

    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        return project?.ToDto();
    }
}
