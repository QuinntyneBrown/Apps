// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PokerGameTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PokerGameTracker.Api.Features.Sessions;

public class GetSessionsQuery : IRequest<List<SessionDto>>
{
}

public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, List<SessionDto>>
{
    private readonly IPokerGameTrackerContext _context;

    public GetSessionsQueryHandler(IPokerGameTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<SessionDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sessions
            .Select(s => new SessionDto
            {
                SessionId = s.SessionId,
                UserId = s.UserId,
                GameType = (int)s.GameType,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                BuyIn = s.BuyIn,
                CashOut = s.CashOut,
                Location = s.Location,
                Notes = s.Notes,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
