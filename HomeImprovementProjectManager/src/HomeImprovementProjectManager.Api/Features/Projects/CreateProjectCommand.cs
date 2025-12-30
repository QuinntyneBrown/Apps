// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to create a new project.
/// </summary>
public record CreateProjectCommand : IRequest<ProjectDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

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
/// Handler for CreateProjectCommand.
/// </summary>
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<CreateProjectCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateProjectCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<CreateProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating project for user {UserId}, name {Name}",
            request.UserId,
            request.Name);

        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EstimatedCost = request.EstimatedCost,
            ActualCost = request.ActualCost,
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
