// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.TastingNotes.Commands;

/// <summary>
/// Command to create a new tasting note.
/// </summary>
public class CreateTastingNoteCommand : IRequest<TastingNoteDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the tasting date.
    /// </summary>
    public DateTime TastingDate { get; set; } = DateTime.UtcNow;

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
/// Handler for CreateTastingNoteCommand.
/// </summary>
public class CreateTastingNoteCommandHandler : IRequestHandler<CreateTastingNoteCommand, TastingNoteDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTastingNoteCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateTastingNoteCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TastingNoteDto> Handle(CreateTastingNoteCommand request, CancellationToken cancellationToken)
    {
        var tastingNote = new TastingNote
        {
            TastingNoteId = Guid.NewGuid(),
            UserId = request.UserId,
            BatchId = request.BatchId,
            TastingDate = request.TastingDate,
            Rating = request.Rating,
            Appearance = request.Appearance,
            Aroma = request.Aroma,
            Flavor = request.Flavor,
            Mouthfeel = request.Mouthfeel,
            OverallImpression = request.OverallImpression,
            CreatedAt = DateTime.UtcNow,
        };

        _context.TastingNotes.Add(tastingNote);
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
