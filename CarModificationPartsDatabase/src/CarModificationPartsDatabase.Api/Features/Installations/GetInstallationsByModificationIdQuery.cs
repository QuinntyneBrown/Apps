// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get installations by modification ID.
/// </summary>
public record GetInstallationsByModificationIdQuery : IRequest<IEnumerable<InstallationDto>>
{
    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }
}

/// <summary>
/// Handler for GetInstallationsByModificationIdQuery.
/// </summary>
public class GetInstallationsByModificationIdQueryHandler : IRequestHandler<GetInstallationsByModificationIdQuery, IEnumerable<InstallationDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetInstallationsByModificationIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInstallationsByModificationIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetInstallationsByModificationIdQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetInstallationsByModificationIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InstallationDto>> Handle(GetInstallationsByModificationIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting installations for modification {ModificationId}", request.ModificationId);

        var installations = await _context.Installations
            .AsNoTracking()
            .Where(i => i.ModificationId == request.ModificationId)
            .ToListAsync(cancellationToken);

        return installations.Select(i => i.ToDto());
    }
}
