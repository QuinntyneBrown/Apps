// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to delete a project.
/// </summary>
public record DeleteProjectCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for DeleteProjectCommand.
/// </summary>
public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<DeleteProjectCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteProjectCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<DeleteProjectCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting project {ProjectId}",
            request.ProjectId);

        var project = await _context.Projects
            .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId, cancellationToken);

        if (project == null)
        {
            _logger.LogInformation(
                "Project {ProjectId} not found",
                request.ProjectId);
            return false;
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted project {ProjectId}",
            request.ProjectId);

        return true;
    }
}
