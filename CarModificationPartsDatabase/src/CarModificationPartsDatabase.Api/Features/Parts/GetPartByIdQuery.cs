// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get a part by ID.
/// </summary>
public record GetPartByIdQuery : IRequest<PartDto?>
{
    /// <summary>
    /// Gets or sets the part ID.
    /// </summary>
    public Guid PartId { get; init; }
}

/// <summary>
/// Handler for GetPartByIdQuery.
/// </summary>
public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, PartDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetPartByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPartByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetPartByIdQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetPartByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<PartDto?> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting part {PartId}", request.PartId);

        var part = await _context.Parts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

        if (part == null)
        {
            _logger.LogInformation("Part {PartId} not found", request.PartId);
            return null;
        }

        return part.ToDto();
    }
}
