// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.TastingNotes.Commands;

/// <summary>
/// Command to delete a tasting note.
/// </summary>
public class DeleteTastingNoteCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the tasting note ID.
    /// </summary>
    public Guid TastingNoteId { get; set; }
}

/// <summary>
/// Handler for DeleteTastingNoteCommand.
/// </summary>
public class DeleteTastingNoteCommandHandler : IRequestHandler<DeleteTastingNoteCommand, Unit>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTastingNoteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteTastingNoteCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Unit> Handle(DeleteTastingNoteCommand request, CancellationToken cancellationToken)
    {
        var tastingNote = await _context.TastingNotes
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken)
            ?? throw new InvalidOperationException($"TastingNote with ID {request.TastingNoteId} not found.");

        _context.TastingNotes.Remove(tastingNote);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
