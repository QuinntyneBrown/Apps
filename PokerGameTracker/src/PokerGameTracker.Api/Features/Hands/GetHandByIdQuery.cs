// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Hands;

public class GetHandByIdQuery : IRequest<HandDto?>
{
    public Guid HandId { get; set; }
}

public class GetHandByIdQueryHandler : IRequestHandler<GetHandByIdQuery, HandDto?>
{
    private readonly IPokerGameTrackerContext _context;

    public GetHandByIdQueryHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<HandDto?> Handle(GetHandByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Hands
            .Where(h => h.HandId == request.HandId)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
