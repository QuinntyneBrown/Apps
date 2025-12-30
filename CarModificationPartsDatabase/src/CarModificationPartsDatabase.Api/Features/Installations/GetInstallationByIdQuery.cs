// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get an installation by ID.
/// </summary>
public record GetInstallationByIdQuery : IRequest<InstallationDto?>
{
    /// <summary>
    /// Gets or sets the installation ID.
    /// </summary>
    public Guid InstallationId { get; init; }
}

/// <summary>
/// Handler for GetInstallationByIdQuery.
/// </summary>
public class GetInstallationByIdQueryHandler : IRequestHandler<GetInstallationByIdQuery, InstallationDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetInstallationByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInstallationByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetInstallationByIdQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetInstallationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InstallationDto?> Handle(GetInstallationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting installation {InstallationId}", request.InstallationId);

        var installation = await _context.Installations
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InstallationId == request.InstallationId, cancellationToken);

        if (installation == null)
        {
            _logger.LogInformation("Installation {InstallationId} not found", request.InstallationId);
            return null;
        }

        return installation.ToDto();
    }
}
