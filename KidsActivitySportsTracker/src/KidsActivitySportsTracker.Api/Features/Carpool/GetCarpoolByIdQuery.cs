// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Query to get a carpool by ID.
/// </summary>
public record GetCarpoolByIdQuery : IRequest<CarpoolDto?>
{
    public Guid CarpoolId { get; init; }
}

/// <summary>
/// Handler for getting a carpool by ID.
/// </summary>
public class GetCarpoolByIdQueryHandler : IRequestHandler<GetCarpoolByIdQuery, CarpoolDto?>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetCarpoolByIdQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<CarpoolDto?> Handle(GetCarpoolByIdQuery request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        return carpool?.ToDto();
    }
}
