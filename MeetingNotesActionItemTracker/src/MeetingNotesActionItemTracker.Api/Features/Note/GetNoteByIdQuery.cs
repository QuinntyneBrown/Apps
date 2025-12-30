// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record GetNoteByIdQuery(Guid NoteId) : IRequest<NoteDto?>;

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public GetNoteByIdQueryHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<NoteDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        return note?.ToDto();
    }
}
