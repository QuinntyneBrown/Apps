// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Spots;

/// <summary>
/// Query to get a spot by ID.
/// </summary>
public class GetSpotByIdQuery : IRequest<SpotDto?>
{
    /// <summary>
    /// Gets or sets the spot ID.
    /// </summary>
    public Guid SpotId { get; set; }
}

/// <summary>
/// Handler for GetSpotByIdQuery.
/// </summary>
public class GetSpotByIdQueryHandler : IRequestHandler<GetSpotByIdQuery, SpotDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSpotByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetSpotByIdQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetSpotByIdQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The spot DTO or null if not found.</returns>
    public async Task<SpotDto?> Handle(GetSpotByIdQuery request, CancellationToken cancellationToken)
    {
        var spot = await _context.Spots
            .Include(s => s.Trips)
            .FirstOrDefaultAsync(s => s.SpotId == request.SpotId, cancellationToken);

        if (spot == null)
        {
            return null;
        }

        return new SpotDto
        {
            SpotId = spot.SpotId,
            UserId = spot.UserId,
            Name = spot.Name,
            LocationType = spot.LocationType,
            Latitude = spot.Latitude,
            Longitude = spot.Longitude,
            Description = spot.Description,
            WaterBodyName = spot.WaterBodyName,
            Directions = spot.Directions,
            IsFavorite = spot.IsFavorite,
            CreatedAt = spot.CreatedAt,
            TripCount = spot.Trips?.Count ?? 0
        };
    }
}
