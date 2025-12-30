// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.TastingNotes.Queries;

/// <summary>
/// Query to get all tasting notes.
/// </summary>
public class GetTastingNotesQuery : IRequest<List<TastingNoteDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional batch ID filter.
    /// </summary>
    public Guid? BatchId { get; set; }
}

/// <summary>
/// Handler for GetTastingNotesQuery.
/// </summary>
public class GetTastingNotesQueryHandler : IRequestHandler<GetTastingNotesQuery, List<TastingNoteDto>>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTastingNotesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTastingNotesQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<TastingNoteDto>> Handle(GetTastingNotesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TastingNotes.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.BatchId.HasValue)
        {
            query = query.Where(t => t.BatchId == request.BatchId.Value);
        }

        var tastingNotes = await query
            .OrderByDescending(t => t.TastingDate)
            .ToListAsync(cancellationToken);

        return tastingNotes.Select(t => new TastingNoteDto
        {
            TastingNoteId = t.TastingNoteId,
            UserId = t.UserId,
            BatchId = t.BatchId,
            TastingDate = t.TastingDate,
            Rating = t.Rating,
            Appearance = t.Appearance,
            Aroma = t.Aroma,
            Flavor = t.Flavor,
            Mouthfeel = t.Mouthfeel,
            OverallImpression = t.OverallImpression,
            CreatedAt = t.CreatedAt,
        }).ToList();
    }
}
