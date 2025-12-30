// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get all modifications.
/// </summary>
public record GetAllModificationsQuery : IRequest<IEnumerable<ModificationDto>>
{
}

/// <summary>
/// Handler for GetAllModificationsQuery.
/// </summary>
public class GetAllModificationsQueryHandler : IRequestHandler<GetAllModificationsQuery, IEnumerable<ModificationDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetAllModificationsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllModificationsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetAllModificationsQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetAllModificationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ModificationDto>> Handle(GetAllModificationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all modifications");

        var modifications = await _context.Modifications
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return modifications.Select(m => m.ToDto());
    }
}
