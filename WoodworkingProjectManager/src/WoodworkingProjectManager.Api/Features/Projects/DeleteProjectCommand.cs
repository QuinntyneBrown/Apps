using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record DeleteProjectCommand : IRequest<bool>
{
    public Guid ProjectId { get; init; }
}

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    public DeleteProjectCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<DeleteProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting project {ProjectId}", request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", request.ProjectId);
            return false;
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted project {ProjectId}", request.ProjectId);

        return true;
    }
}
