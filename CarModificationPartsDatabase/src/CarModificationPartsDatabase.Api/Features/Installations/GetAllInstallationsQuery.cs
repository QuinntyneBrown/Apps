// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get all installations.
/// </summary>
public record GetAllInstallationsQuery : IRequest<IEnumerable<InstallationDto>>
{
}

/// <summary>
/// Handler for GetAllInstallationsQuery.
/// </summary>
public class GetAllInstallationsQueryHandler : IRequestHandler<GetAllInstallationsQuery, IEnumerable<InstallationDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetAllInstallationsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllInstallationsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetAllInstallationsQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetAllInstallationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<InstallationDto>> Handle(GetAllInstallationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all installations");

        var installations = await _context.Installations
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return installations.Select(i => i.ToDto());
    }
}
