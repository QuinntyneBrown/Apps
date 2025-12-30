// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record DeleteNoteCommand(Guid NoteId) : IRequest<Unit>;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public DeleteNoteCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        if (note == null)
        {
            throw new InvalidOperationException($"Note with ID {request.NoteId} not found");
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
