// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Catches;

/// <summary>
/// Query to get all catches for a user or trip.
/// </summary>
public class GetCatchesQuery : IRequest<List<CatchDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter catches.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the trip ID to filter catches.
    /// </summary>
    public Guid? TripId { get; set; }

    /// <summary>
    /// Gets or sets the species to filter catches.
    /// </summary>
    public FishSpecies? Species { get; set; }
}

/// <summary>
/// Handler for GetCatchesQuery.
/// </summary>
public class GetCatchesQueryHandler : IRequestHandler<GetCatchesQuery, List<CatchDto>>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCatchesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetCatchesQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetCatchesQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>List of catch DTOs.</returns>
    public async Task<List<CatchDto>> Handle(GetCatchesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Catches.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.TripId.HasValue)
        {
            query = query.Where(c => c.TripId == request.TripId.Value);
        }

        if (request.Species.HasValue)
        {
            query = query.Where(c => c.Species == request.Species.Value);
        }

        var catches = await query
            .OrderByDescending(c => c.CatchTime)
            .ToListAsync(cancellationToken);

        return catches.Select(c => new CatchDto
        {
            CatchId = c.CatchId,
            UserId = c.UserId,
            TripId = c.TripId,
            Species = c.Species,
            Length = c.Length,
            Weight = c.Weight,
            CatchTime = c.CatchTime,
            BaitUsed = c.BaitUsed,
            WasReleased = c.WasReleased,
            Notes = c.Notes,
            PhotoUrl = c.PhotoUrl,
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}
