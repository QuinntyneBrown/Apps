// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Command to delete an event.
/// </summary>
public class DeleteEvent
{
    /// <summary>
    /// Command to delete an event.
    /// </summary>
    public class Command : IRequest<Unit>
    {
        public Guid EventId { get; set; }
    }

    /// <summary>
    /// Handler for DeleteEvent command.
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
            var evt = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken)
                ?? throw new KeyNotFoundException($"Event with ID {request.EventId} not found.");

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
