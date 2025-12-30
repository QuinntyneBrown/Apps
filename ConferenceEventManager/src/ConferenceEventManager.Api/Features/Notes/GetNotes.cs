// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Query to get all notes.
/// </summary>
public class GetNotes
{
    /// <summary>
    /// Query to get all notes, optionally filtered by event ID or user ID.
    /// </summary>
    public class Query : IRequest<List<NoteDto>>
    {
        public Guid? EventId { get; set; }
        public Guid? UserId { get; set; }
    }

    /// <summary>
    /// Handler for GetNotes query.
    /// </summary>
    public class Handler : IRequestHandler<Query, List<NoteDto>>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<List<NoteDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Notes.AsQueryable();

            if (request.EventId.HasValue)
            {
                query = query.Where(n => n.EventId == request.EventId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(n => n.UserId == request.UserId.Value);
            }

            var notes = await query
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);

            return notes.Select(NoteDto.FromNote).ToList();
        }
    }
}
