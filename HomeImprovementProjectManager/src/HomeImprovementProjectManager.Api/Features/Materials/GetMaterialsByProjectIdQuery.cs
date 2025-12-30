// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Query to get all materials for a project.
/// </summary>
public record GetMaterialsByProjectIdQuery : IRequest<IEnumerable<MaterialDto>>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for GetMaterialsByProjectIdQuery.
/// </summary>
public class GetMaterialsByProjectIdQueryHandler : IRequestHandler<GetMaterialsByProjectIdQuery, IEnumerable<MaterialDto>>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<GetMaterialsByProjectIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMaterialsByProjectIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetMaterialsByProjectIdQueryHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<GetMaterialsByProjectIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<MaterialDto>> Handle(GetMaterialsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting materials for project {ProjectId}",
            request.ProjectId);

        var materials = await _context.Materials
            .AsNoTracking()
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return materials.Select(m => m.ToDto());
    }
}
