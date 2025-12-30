using WoodworkingProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WoodworkingProjectManager.Api.Features.Projects;

public record CreateProjectCommand : IRequest<ProjectDto>
{
    public Guid UserId { get; init; }
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

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IWoodworkingProjectManagerContext _context;
    private readonly ILogger<CreateProjectCommandHandler> _logger;

    public CreateProjectCommandHandler(
        IWoodworkingProjectManagerContext context,
        ILogger<CreateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating project for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
            WoodType = request.WoodType,
            StartDate = request.StartDate,
            CompletionDate = request.CompletionDate,
            EstimatedCost = request.EstimatedCost,
            ActualCost = request.ActualCost,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created project {ProjectId} for user {UserId}",
            project.ProjectId,
            request.UserId);

        return project.ToDto();
    }
}
