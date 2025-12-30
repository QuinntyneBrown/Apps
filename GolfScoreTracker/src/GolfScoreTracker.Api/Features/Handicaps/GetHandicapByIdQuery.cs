// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GolfScoreTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreTracker.Api.Features.Handicaps;

public class GetHandicapByIdQuery : IRequest<HandicapDto?>
{
    public Guid HandicapId { get; set; }
}

public class GetHandicapByIdQueryHandler : IRequestHandler<GetHandicapByIdQuery, HandicapDto?>
{
    private readonly IGolfScoreTrackerContext _context;

    public GetHandicapByIdQueryHandler(IGolfScoreTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandicapDto?> Handle(GetHandicapByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Handicaps
            .Where(h => h.HandicapId == request.HandicapId)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
