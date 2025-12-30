// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record UpdateNoteCommand(
    Guid NoteId,
    string Content,
    string? Category,
    bool IsImportant
) : IRequest<NoteDto>;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public UpdateNoteCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<NoteDto> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        if (note == null)
        {
            throw new InvalidOperationException($"Note with ID {request.NoteId} not found");
        }

        note.Content = request.Content;
        note.Category = request.Category;
        note.IsImportant = request.IsImportant;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return note.ToDto();
    }
}
