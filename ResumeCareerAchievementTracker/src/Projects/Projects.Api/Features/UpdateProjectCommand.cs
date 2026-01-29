using Projects.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Projects.Api.Features;

public record UpdateProjectCommand(
    Guid ProjectId,
    string Name,
    string Description,
    string? Organization,
    string? Role,
    DateTime StartDate,
    DateTime? EndDate,
    bool IsFeatured) : IRequest<ProjectDto?>;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
{
    private readonly IProjectsDbContext _context;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(IProjectsDbContext context, ILogger<UpdateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null) return null;

        project.Name = request.Name;
        project.Description = request.Description;
        project.Organization = request.Organization;
        project.Role = request.Role;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.IsFeatured = request.IsFeatured;
        project.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Project updated: {ProjectId}", project.ProjectId);

        return project.ToDto();
    }
}
