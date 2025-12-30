// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Hands;

public class GetHandsQuery : IRequest<List<HandDto>>
{
}

public class GetHandsQueryHandler : IRequestHandler<GetHandsQuery, List<HandDto>>
{
    private readonly IPokerGameTrackerContext _context;

    public GetHandsQueryHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<HandDto>> Handle(GetHandsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Hands
            .Select(h => new HandDto
            {
                HandId = h.HandId,
                UserId = h.UserId,
                SessionId = h.SessionId,
                StartingCards = h.StartingCards,
                PotSize = h.PotSize,
                WasWon = h.WasWon,
                Notes = h.Notes,
                CreatedAt = h.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
