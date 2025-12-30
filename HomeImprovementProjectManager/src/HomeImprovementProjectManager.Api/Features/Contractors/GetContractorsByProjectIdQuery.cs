// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Query to get all contractors for a project.
/// </summary>
public record GetContractorsByProjectIdQuery : IRequest<IEnumerable<ContractorDto>>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }
}

/// <summary>
/// Handler for GetContractorsByProjectIdQuery.
/// </summary>
public class GetContractorsByProjectIdQueryHandler : IRequestHandler<GetContractorsByProjectIdQuery, IEnumerable<ContractorDto>>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<GetContractorsByProjectIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetContractorsByProjectIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetContractorsByProjectIdQueryHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<GetContractorsByProjectIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ContractorDto>> Handle(GetContractorsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting contractors for project {ProjectId}",
            request.ProjectId);

        var contractors = await _context.Contractors
            .AsNoTracking()
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return contractors.Select(c => c.ToDto());
    }
}
