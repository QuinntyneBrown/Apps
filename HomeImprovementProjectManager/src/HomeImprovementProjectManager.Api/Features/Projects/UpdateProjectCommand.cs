// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to update an existing project.
/// </summary>
public record UpdateProjectCommand : IRequest<ProjectDto?>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the project status.
    /// </summary>
    public ProjectStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Gets or sets the estimated cost.
    /// </summary>
    public decimal? EstimatedCost { get; init; }

    /// <summary>
    /// Gets or sets the actual cost.
    /// </summary>
    public decimal? ActualCost { get; init; }
}

/// <summary>
/// Handler for UpdateProjectCommand.
/// </summary>
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<UpdateProjectCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateProjectCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<UpdateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating project {ProjectId}",
            request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogInformation(
                "Project {ProjectId} not found",
                request.ProjectId);
            return null;
        }

        project.Name = request.Name;
        project.Description = request.Description;
        project.Status = request.Status;
        project.StartDate = request.StartDate;
        project.EndDate = request.EndDate;
        project.EstimatedCost = request.EstimatedCost;
        project.ActualCost = request.ActualCost;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated project {ProjectId}",
            project.ProjectId);

        return project.ToDto();
    }
}
