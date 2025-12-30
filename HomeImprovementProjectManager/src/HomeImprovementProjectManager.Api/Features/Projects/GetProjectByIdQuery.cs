// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Query to get a project by ID.
/// </summary>
public record GetProjectByIdQuery : IRequest<ProjectDto?>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for GetProjectByIdQuery.
/// </summary>
public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<GetProjectByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetProjectByIdQueryHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<GetProjectByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting project {ProjectId}",
            request.ProjectId);

        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogInformation(
                "Project {ProjectId} not found",
                request.ProjectId);
            return null;
        }

        return project.ToDto();
    }
}
