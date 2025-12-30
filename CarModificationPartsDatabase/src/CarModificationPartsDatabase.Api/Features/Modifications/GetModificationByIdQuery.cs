// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Query to get a modification by ID.
/// </summary>
public record GetModificationByIdQuery : IRequest<ModificationDto?>
{
    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }
}

/// <summary>
/// Handler for GetModificationByIdQuery.
/// </summary>
public class GetModificationByIdQueryHandler : IRequestHandler<GetModificationByIdQuery, ModificationDto?>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<GetModificationByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetModificationByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetModificationByIdQueryHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<GetModificationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ModificationDto?> Handle(GetModificationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting modification {ModificationId}", request.ModificationId);

        var modification = await _context.Modifications
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ModificationId == request.ModificationId, cancellationToken);

        if (modification == null)
        {
            _logger.LogInformation("Modification {ModificationId} not found", request.ModificationId);
            return null;
        }

        return modification.ToDto();
    }
}
