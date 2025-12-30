// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Query to get all events.
/// </summary>
public class GetEvents
{
    /// <summary>
    /// Query to get all events, optionally filtered by user ID.
    /// </summary>
    public class Query : IRequest<List<EventDto>>
    {
        public Guid? UserId { get; set; }
    }

    /// <summary>
    /// Handler for GetEvents query.
    /// </summary>
    public class Handler : IRequestHandler<Query, List<EventDto>>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<List<EventDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Events.AsQueryable();

            if (request.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == request.UserId.Value);
            }

            var events = await query
                .OrderByDescending(e => e.StartDate)
                .ToListAsync(cancellationToken);

            return events.Select(EventDto.FromEvent).ToList();
        }
    }
}
