using Projects.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Features;

public record DeleteProjectCommand(Guid ProjectId) : IRequest<bool>;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectsDbContext _context;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    public DeleteProjectCommandHandler(IProjectsDbContext context, ILogger<DeleteProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Project deleted: {ProjectId}", request.ProjectId);

        return true;
    }
}
