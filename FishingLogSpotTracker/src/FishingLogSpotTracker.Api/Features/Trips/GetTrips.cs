// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Query to get all trips for a user.
/// </summary>
public class GetTripsQuery : IRequest<List<TripDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter trips.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the spot ID to filter trips.
    /// </summary>
    public Guid? SpotId { get; set; }
}

/// <summary>
/// Handler for GetTripsQuery.
/// </summary>
public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, List<TripDto>>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTripsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTripsQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetTripsQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>List of trip DTOs.</returns>
    public async Task<List<TripDto>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Trips
            .Include(t => t.Spot)
            .Include(t => t.Catches)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.SpotId.HasValue)
        {
            query = query.Where(t => t.SpotId == request.SpotId.Value);
        }

        var trips = await query
            .OrderByDescending(t => t.TripDate)
            .ToListAsync(cancellationToken);

        return trips.Select(t => new TripDto
        {
            TripId = t.TripId,
            UserId = t.UserId,
            SpotId = t.SpotId,
            SpotName = t.Spot?.Name,
            TripDate = t.TripDate,
            StartTime = t.StartTime,
            EndTime = t.EndTime,
            WeatherConditions = t.WeatherConditions,
            WaterTemperature = t.WaterTemperature,
            AirTemperature = t.AirTemperature,
            Notes = t.Notes,
            CreatedAt = t.CreatedAt,
            CatchCount = t.Catches?.Count ?? 0,
            DurationInHours = t.GetDurationInHours()
        }).ToList();
    }
}
