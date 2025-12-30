// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Command to delete a note.
/// </summary>
public class DeleteNote
{
    /// <summary>
    /// Command to delete a note.
    /// </summary>
    public class Command : IRequest<Unit>
    {
        public Guid NoteId { get; set; }
    }

    /// <summary>
    /// Handler for DeleteNote command.
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
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Note with ID {request.NoteId} not found.");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
