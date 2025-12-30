// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Query to get all projects for a user.
/// </summary>
public record GetProjectsByUserIdQuery : IRequest<IEnumerable<ProjectDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetProjectsByUserIdQuery.
/// </summary>
public class GetProjectsByUserIdQueryHandler : IRequestHandler<GetProjectsByUserIdQuery, IEnumerable<ProjectDto>>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<GetProjectsByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetProjectsByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetProjectsByUserIdQueryHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<GetProjectsByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting projects for user {UserId}",
            request.UserId);

        var projects = await _context.Projects
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return projects.Select(p => p.ToDto());
    }
}
