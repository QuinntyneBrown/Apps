// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Command to delete a project.
/// </summary>
public class DeleteProjectCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for deleting a project.
/// </summary>
public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteProjectHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Where(p => p.ProjectId == request.ProjectId && p.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project == null)
        {
            return false;
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
