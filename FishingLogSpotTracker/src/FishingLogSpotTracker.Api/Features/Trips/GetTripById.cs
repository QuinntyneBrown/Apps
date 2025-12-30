// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Query to get a trip by ID.
/// </summary>
public class GetTripByIdQuery : IRequest<TripDto?>
{
    /// <summary>
    /// Gets or sets the trip ID.
    /// </summary>
    public Guid TripId { get; set; }
}

/// <summary>
/// Handler for GetTripByIdQuery.
/// </summary>
public class GetTripByIdQueryHandler : IRequestHandler<GetTripByIdQuery, TripDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTripByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTripByIdQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetTripByIdQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The trip DTO or null if not found.</returns>
    public async Task<TripDto?> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .Include(t => t.Spot)
            .Include(t => t.Catches)
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            return null;
        }

        return new TripDto
        {
            TripId = trip.TripId,
            UserId = trip.UserId,
            SpotId = trip.SpotId,
            SpotName = trip.Spot?.Name,
            TripDate = trip.TripDate,
            StartTime = trip.StartTime,
            EndTime = trip.EndTime,
            WeatherConditions = trip.WeatherConditions,
            WaterTemperature = trip.WaterTemperature,
            AirTemperature = trip.AirTemperature,
            Notes = trip.Notes,
            CreatedAt = trip.CreatedAt,
            CatchCount = trip.Catches?.Count ?? 0,
            DurationInHours = trip.GetDurationInHours()
        };
    }
}
