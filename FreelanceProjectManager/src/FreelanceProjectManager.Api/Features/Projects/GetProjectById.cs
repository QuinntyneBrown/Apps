// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Query to get a project by ID.
/// </summary>
public class GetProjectByIdQuery : IRequest<ProjectDto?>
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
/// Handler for getting a project by ID.
/// </summary>
public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetProjectByIdHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Where(p => p.ProjectId == request.ProjectId && p.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project == null)
        {
            return null;
        }

        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            ClientId = project.ClientId,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            DueDate = project.DueDate,
            CompletionDate = project.CompletionDate,
            HourlyRate = project.HourlyRate,
            FixedBudget = project.FixedBudget,
            Currency = project.Currency,
            Notes = project.Notes,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
}
