// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.TastingNotes.Queries;

/// <summary>
/// Query to get a tasting note by ID.
/// </summary>
public class GetTastingNoteByIdQuery : IRequest<TastingNoteDto?>
{
    /// <summary>
    /// Gets or sets the tasting note ID.
    /// </summary>
    public Guid TastingNoteId { get; set; }
}

/// <summary>
/// Handler for GetTastingNoteByIdQuery.
/// </summary>
public class GetTastingNoteByIdQueryHandler : IRequestHandler<GetTastingNoteByIdQuery, TastingNoteDto?>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTastingNoteByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTastingNoteByIdQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TastingNoteDto?> Handle(GetTastingNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var tastingNote = await _context.TastingNotes
            .FirstOrDefaultAsync(t => t.TastingNoteId == request.TastingNoteId, cancellationToken);

        if (tastingNote == null)
        {
            return null;
        }

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
