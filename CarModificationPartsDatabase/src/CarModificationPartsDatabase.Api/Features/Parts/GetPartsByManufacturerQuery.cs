// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get parts by manufacturer.
/// </summary>
public record GetPartsByManufacturerQuery : IRequest<IEnumerable<PartDto>>
{
    /// <summary>
    /// Gets or sets the manufacturer.
    /// </summary>
    public string Manufacturer { get; init; } = string.Empty;
}

/// <summary>
/// Handler for GetPartsByManufacturerQuery.
/// </summary>
public class GetPartsByManufacturerQueryHandler : IRequestHandler<GetPartsByManufacturerQuery, IEnumerable<PartDto>>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetPartsByManufacturerQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPartsByManufacturerQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetPartsByManufacturerQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetPartsByManufacturerQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PartDto>> Handle(GetPartsByManufacturerQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting parts by manufacturer {Manufacturer}", request.Manufacturer);

        var parts = await _context.Parts
            .AsNoTracking()
            .Where(p => p.Manufacturer.ToLower() == request.Manufacturer.ToLower())
            .ToListAsync(cancellationToken);

        return parts.Select(p => p.ToDto());
    }
}
