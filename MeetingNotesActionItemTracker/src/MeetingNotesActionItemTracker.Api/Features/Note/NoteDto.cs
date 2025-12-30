// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;

namespace MeetingNotesActionItemTracker.Api.Features.Note;

public record NoteDto(
    Guid NoteId,
    Guid UserId,
    Guid MeetingId,
    string Content,
    string? Category,
    bool IsImportant,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public static class NoteExtensions
{
    public static NoteDto ToDto(this Core.Note note)
    {
        return new NoteDto(
            note.NoteId,
            note.UserId,
            note.MeetingId,
            note.Content,
            note.Category,
            note.IsImportant,
            note.CreatedAt,
            note.UpdatedAt
        );
    }
}
