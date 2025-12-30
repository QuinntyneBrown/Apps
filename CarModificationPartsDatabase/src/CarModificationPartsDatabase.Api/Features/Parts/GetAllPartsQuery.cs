// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get all parts.
/// </summary>
public record GetAllPartsQuery : IRequest<IEnumerable<PartDto>>
{
}

/// <summary>
/// Handler for GetAllPartsQuery.
/// </summary>
public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, IEnumerable<PartDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetAllPartsQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPartsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetAllPartsQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetAllPartsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PartDto>> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all parts");

        var parts = await _context.Parts
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return parts.Select(p => p.ToDto());
    }
}
