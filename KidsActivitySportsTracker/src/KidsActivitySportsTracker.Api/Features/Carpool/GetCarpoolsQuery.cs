// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Query to get all carpools.
/// </summary>
public record GetCarpoolsQuery : IRequest<List<CarpoolDto>>
{
    public Guid? UserId { get; init; }
}

/// <summary>
/// Handler for getting all carpools.
/// </summary>
public class GetCarpoolsQueryHandler : IRequestHandler<GetCarpoolsQuery, List<CarpoolDto>>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public GetCarpoolsQueryHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<CarpoolDto>> Handle(GetCarpoolsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Carpools.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        var carpools = await query
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        return carpools.Select(c => c.ToDto()).ToList();
    }
}
