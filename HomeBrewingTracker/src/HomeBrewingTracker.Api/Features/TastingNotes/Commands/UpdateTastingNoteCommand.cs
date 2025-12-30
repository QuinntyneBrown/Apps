// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.TastingNotes.Commands;

/// <summary>
/// Command to update an existing tasting note.
/// </summary>
public class UpdateTastingNoteCommand : IRequest<TastingNoteDto>
{
    /// <summary>
    /// Gets or sets the tasting note ID.
    /// </summary>
    public Guid TastingNoteId { get; set; }

    /// <summary>
    /// Gets or sets the tasting date.
    /// </summary>
    public DateTime TastingDate { get; set; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the appearance.
    /// </summary>
    public string? Appearance { get; set; }

    /// <summary>
    /// Gets or sets the aroma.
    /// </summary>
    public string? Aroma { get; set; }

    /// <summary>
    /// Gets or sets the flavor.
    /// </summary>
    public string? Flavor { get; set; }

    /// <summary>
    /// Gets or sets the mouthfeel.
    /// </summary>
    public string? Mouthfeel { get; set; }

    /// <summary>
    /// Gets or sets the overall impression.
    /// </summary>
    public string? OverallImpression { get; set; }
}

/// <summary>
/// Handler for UpdateTastingNoteCommand.
/// </summary>
public class UpdateTastingNoteCommandHandler : IRequestHandler<UpdateTastingNoteCommand, TastingNoteDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTastingNoteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateTastingNoteCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TastingNoteDto> Handle(UpdateTastingNoteCommand request, CancellationToken cancellationToken)
    {
        var tastingNote = await _context.TastingNotes
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken)
            ?? throw new InvalidOperationException($"TastingNote with ID {request.TastingNoteId} not found.");

        tastingNote.TastingDate = request.TastingDate;
        tastingNote.Rating = request.Rating;
        tastingNote.Appearance = request.Appearance;
        tastingNote.Aroma = request.Aroma;
        tastingNote.Flavor = request.Flavor;
        tastingNote.Mouthfeel = request.Mouthfeel;
        tastingNote.OverallImpression = request.OverallImpression;

        await _context.SaveChangesAsync(cancellationToken);

        return new TastingNoteDto
        {
            TastingNoteId = tastingNote.TastingNoteId,
            UserId = tastingNote.UserId,
            BatchId = tastingNote.BatchId,
            TastingDate = tastingNote.TastingDate,
            Rating = tastingNote.Rating,
            Appearance = tastingNote.Appearance,
            Aroma = tastingNote.Aroma,
            Flavor = tastingNote.Flavor,
            Mouthfeel = tastingNote.Mouthfeel,
            OverallImpression = tastingNote.OverallImpression,
            CreatedAt = tastingNote.CreatedAt,
        };
    }
}
