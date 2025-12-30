// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Query to get an event by ID.
/// </summary>
public class GetEventById
{
    /// <summary>
    /// Query to get an event by ID.
    /// </summary>
    public class Query : IRequest<EventDto>
    {
        public Guid EventId { get; set; }
    }

    /// <summary>
    /// Handler for GetEventById query.
    /// </summary>
    public class Handler : IRequestHandler<Query, EventDto>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<EventDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var evt = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken)
                ?? throw new KeyNotFoundException($"Event with ID {request.EventId} not found.");

            return EventDto.FromEvent(evt);
        }
    }
}
