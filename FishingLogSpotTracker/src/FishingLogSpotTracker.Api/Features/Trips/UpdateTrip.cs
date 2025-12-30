// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Command to update an existing trip.
/// </summary>
public class UpdateTripCommand : IRequest<TripDto?>
{
    /// <summary>
    /// Gets or sets the trip ID.
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Gets or sets the spot ID where the trip took place.
    /// </summary>
    public Guid? SpotId { get; set; }

    /// <summary>
    /// Gets or sets the trip date.
    /// </summary>
    public DateTime TripDate { get; set; }

    /// <summary>
    /// Gets or sets the start time of the trip.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the trip.
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Gets or sets the weather conditions.
    /// </summary>
    public string? WeatherConditions { get; set; }

    /// <summary>
    /// Gets or sets the water temperature.
    /// </summary>
    public decimal? WaterTemperature { get; set; }

    /// <summary>
    /// Gets or sets the air temperature.
    /// </summary>
    public decimal? AirTemperature { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the trip.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Validator for UpdateTripCommand.
/// </summary>
public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTripCommandValidator"/> class.
    /// </summary>
    public UpdateTripCommandValidator()
    {
        RuleFor(x => x.TripId).NotEmpty();
        RuleFor(x => x.TripDate).NotEmpty();
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).When(x => x.EndTime.HasValue);
    }
}

/// <summary>
/// Handler for UpdateTripCommand.
/// </summary>
public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, TripDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTripCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateTripCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the UpdateTripCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated trip DTO or null if not found.</returns>
    public async Task<TripDto?> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = await _context.Trips
            .Include(t => t.Spot)
            .Include(t => t.Catches)
            .FirstOrDefaultAsync(t => t.TripId == request.TripId, cancellationToken);

        if (trip == null)
        {
            return null;
        }

        trip.SpotId = request.SpotId;
        trip.TripDate = request.TripDate;
        trip.StartTime = request.StartTime;
        trip.EndTime = request.EndTime;
        trip.WeatherConditions = request.WeatherConditions;
        trip.WaterTemperature = request.WaterTemperature;
        trip.AirTemperature = request.AirTemperature;
        trip.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

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
