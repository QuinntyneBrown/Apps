// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Spots;

/// <summary>
/// Query to get all spots for a user.
/// </summary>
public class GetSpotsQuery : IRequest<List<SpotDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter spots.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include only favorites.
    /// </summary>
    public bool? FavoritesOnly { get; set; }
}

/// <summary>
/// Handler for GetSpotsQuery.
/// </summary>
public class GetSpotsQueryHandler : IRequestHandler<GetSpotsQuery, List<SpotDto>>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSpotsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetSpotsQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetSpotsQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>List of spot DTOs.</returns>
    public async Task<List<SpotDto>> Handle(GetSpotsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Spots.Include(s => s.Trips).AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.FavoritesOnly.HasValue && request.FavoritesOnly.Value)
        {
            query = query.Where(s => s.IsFavorite);
        }

        var spots = await query
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);

        return spots.Select(s => new SpotDto
        {
            SpotId = s.SpotId,
            UserId = s.UserId,
            Name = s.Name,
            LocationType = s.LocationType,
            Latitude = s.Latitude,
            Longitude = s.Longitude,
            Description = s.Description,
            WaterBodyName = s.WaterBodyName,
            Directions = s.Directions,
            IsFavorite = s.IsFavorite,
            CreatedAt = s.CreatedAt,
            TripCount = s.Trips?.Count ?? 0
        }).ToList();
    }
}
