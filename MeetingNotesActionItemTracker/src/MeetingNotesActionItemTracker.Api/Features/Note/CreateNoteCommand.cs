// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record CreateNoteCommand(
    Guid UserId,
    Guid MeetingId,
    string Content,
    string? Category,
    bool IsImportant
) : IRequest<NoteDto>;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IMeetingNotesActionItemTrackerContext _context;

    public CreateNoteCommandHandler(IMeetingNotesActionItemTrackerContext context)
    {
        _context = context;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Core.Note
        {
            NoteId = Guid.NewGuid(),
            UserId = request.UserId,
            MeetingId = request.MeetingId,
            Content = request.Content,
            Category = request.Category,
            IsImportant = request.IsImportant,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        return note.ToDto();
    }
}
