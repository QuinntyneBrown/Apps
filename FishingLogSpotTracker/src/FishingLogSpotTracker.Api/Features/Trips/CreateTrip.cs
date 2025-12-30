// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Trips;

/// <summary>
/// Command to create a new trip.
/// </summary>
public class CreateTripCommand : IRequest<TripDto>
{
    /// <summary>
    /// Gets or sets the user ID who owns this trip.
    /// </summary>
    public Guid UserId { get; set; }

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
/// Validator for CreateTripCommand.
/// </summary>
public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTripCommandValidator"/> class.
    /// </summary>
    public CreateTripCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TripDate).NotEmpty();
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).When(x => x.EndTime.HasValue);
    }
}

/// <summary>
/// Handler for CreateTripCommand.
/// </summary>
public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, TripDto>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTripCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateTripCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the CreateTripCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created trip DTO.</returns>
    public async Task<TripDto> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        var trip = new Trip
        {
            TripId = Guid.NewGuid(),
            UserId = request.UserId,
            SpotId = request.SpotId,
            TripDate = request.TripDate,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            WeatherConditions = request.WeatherConditions,
            WaterTemperature = request.WaterTemperature,
            AirTemperature = request.AirTemperature,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync(cancellationToken);

        var spot = request.SpotId.HasValue
            ? await _context.Spots.FirstOrDefaultAsync(s => s.SpotId == request.SpotId.Value, cancellationToken)
            : null;

        return new TripDto
        {
            TripId = trip.TripId,
            UserId = trip.UserId,
            SpotId = trip.SpotId,
            SpotName = spot?.Name,
            TripDate = trip.TripDate,
            StartTime = trip.StartTime,
            EndTime = trip.EndTime,
            WeatherConditions = trip.WeatherConditions,
            WaterTemperature = trip.WaterTemperature,
            AirTemperature = trip.AirTemperature,
            Notes = trip.Notes,
            CreatedAt = trip.CreatedAt,
            CatchCount = 0,
            DurationInHours = trip.GetDurationInHours()
        };
    }
}
