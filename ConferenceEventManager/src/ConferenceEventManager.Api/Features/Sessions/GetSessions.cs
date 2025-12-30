// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Query to get all sessions.
/// </summary>
public class GetSessions
{
    /// <summary>
    /// Query to get all sessions, optionally filtered by event ID or user ID.
    /// </summary>
    public class Query : IRequest<List<SessionDto>>
    {
        public Guid? EventId { get; set; }
        public Guid? UserId { get; set; }
    }

    /// <summary>
    /// Handler for GetSessions query.
    /// </summary>
    public class Handler : IRequestHandler<Query, List<SessionDto>>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<List<SessionDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Sessions.AsQueryable();

            if (request.EventId.HasValue)
            {
                query = query.Where(s => s.EventId == request.EventId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(s => s.UserId == request.UserId.Value);
            }

            var sessions = await query
                .OrderBy(s => s.StartTime)
                .ToListAsync(cancellationToken);

            return sessions.Select(SessionDto.FromSession).ToList();
        }
    }
}
