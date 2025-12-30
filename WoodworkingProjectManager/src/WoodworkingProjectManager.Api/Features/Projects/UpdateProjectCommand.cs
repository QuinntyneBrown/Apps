using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record UpdateProjectCommand : IRequest<ProjectDto?>
{
    public Guid ProjectId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public ProjectStatus Status { get; init; }
    public WoodType WoodType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? CompletionDate { get; init; }
    public decimal? EstimatedCost { get; init; }
    public decimal? ActualCost { get; init; }
    public string? Notes { get; init; }
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    public UpdateProjectCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<UpdateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating project {ProjectId}", request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogWarning("Project {ProjectId} not found", request.ProjectId);
            return null;
        }

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status;
        project.WoodType = request.WoodType;
        project.StartDate = request.StartDate;
        project.CompletionDate = request.CompletionDate;
        project.EstimatedCost = request.EstimatedCost;
        project.ActualCost = request.ActualCost;
        project.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated project {ProjectId}", request.ProjectId);

        return project.ToDto();
    }
}
