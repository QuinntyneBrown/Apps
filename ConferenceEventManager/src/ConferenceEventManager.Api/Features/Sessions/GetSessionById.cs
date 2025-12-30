// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Query to get a session by ID.
/// </summary>
public class GetSessionById
{
    /// <summary>
    /// Query to get a session by ID.
    /// </summary>
    public class Query : IRequest<SessionDto>
    {
        public Guid SessionId { get; set; }
    }

    /// <summary>
    /// Handler for GetSessionById query.
    /// </summary>
    public class Handler : IRequestHandler<Query, SessionDto>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<SessionDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken)
                ?? throw new KeyNotFoundException($"Session with ID {request.SessionId} not found.");

            return SessionDto.FromSession(session);
        }
    }
}
