// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Command to delete a session.
/// </summary>
public class DeleteSession
{
    /// <summary>
    /// Command to delete a session.
    /// </summary>
    public class Command : IRequest<Unit>
    {
        public Guid SessionId { get; set; }
    }

    /// <summary>
    /// Handler for DeleteSession command.
    /// </summary>
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken)
                ?? throw new KeyNotFoundException($"Session with ID {request.SessionId} not found.");

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
