// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Query to get a note by ID.
/// </summary>
public class GetNoteById
{
    /// <summary>
    /// Query to get a note by ID.
    /// </summary>
    public class Query : IRequest<NoteDto>
    {
        public Guid NoteId { get; set; }
    }

    /// <summary>
    /// Handler for GetNoteById query.
    /// </summary>
    public class Handler : IRequestHandler<Query, NoteDto>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<NoteDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Note with ID {request.NoteId} not found.");

            return NoteDto.FromNote(note);
        }
    }
}
