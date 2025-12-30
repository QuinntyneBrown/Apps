// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Query to get all projects for a user.
/// </summary>
public class GetProjectsQuery : IRequest<List<ProjectDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for getting projects.
/// </summary>
public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectsHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetProjectsHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Where(p => p.UserId == request.UserId)
            .Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                UserId = p.UserId,
                ClientId = p.ClientId,
                Name = p.Name,
                Description = p.Description,
                Status = p.Status,
                StartDate = p.StartDate,
                DueDate = p.DueDate,
                CompletionDate = p.CompletionDate,
                HourlyRate = p.HourlyRate,
                FixedBudget = p.FixedBudget,
                Currency = p.Currency,
                Notes = p.Notes,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
