// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Handicaps;

public class GetHandicapsQuery : IRequest<List<HandicapDto>>
{
    public Guid? UserId { get; set; }
}

public class GetHandicapsQueryHandler : IRequestHandler<GetHandicapsQuery, List<HandicapDto>>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetHandicapsQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<HandicapDto>> Handle(GetHandicapsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Handicaps.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(h => h.UserId == request.UserId.Value);
        }

        return await query
            .Select(h => new HandicapDto
            {
                HandicapId = h.HandicapId,
                UserId = h.UserId,
                HandicapIndex = h.HandicapIndex,
                CalculatedDate = h.CalculatedDate,
                RoundsUsed = h.RoundsUsed,
                Notes = h.Notes,
                CreatedAt = h.CreatedAt
            })
            .OrderByDescending(h => h.CalculatedDate)
            .ToListAsync(cancellationToken);
    }
}
