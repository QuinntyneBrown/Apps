// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Spots;

/// <summary>
/// Command to update an existing spot.
/// </summary>
public class UpdateSpotCommand : IRequest<SpotDto?>
{
    /// <summary>
    /// Gets or sets the spot ID.
    /// </summary>
    public Guid SpotId { get; set; }

    /// <summary>
    /// Gets or sets the name of the spot.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the location type.
    /// </summary>
    public LocationType LocationType { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate.
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate.
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Gets or sets the description of the spot.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the water body name (lake, river, etc.).
    /// </summary>
    public string? WaterBodyName { get; set; }

    /// <summary>
    /// Gets or sets optional directions to the spot.
    /// </summary>
    public string? Directions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is a favorite spot.
    /// </summary>
    public bool IsFavorite { get; set; }
}

/// <summary>
/// Validator for UpdateSpotCommand.
/// </summary>
public class UpdateSpotCommandValidator : AbstractValidator<UpdateSpotCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSpotCommandValidator"/> class.
    /// </summary>
    public UpdateSpotCommandValidator()
    {
        RuleFor(x => x.SpotId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LocationType).IsInEnum();
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);
    }
}

/// <summary>
/// Handler for UpdateSpotCommand.
/// </summary>
public class UpdateSpotCommandHandler : IRequestHandler<UpdateSpotCommand, SpotDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSpotCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateSpotCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the UpdateSpotCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated spot DTO or null if not found.</returns>
    public async Task<SpotDto?> Handle(UpdateSpotCommand request, CancellationToken cancellationToken)
    {
        var spot = await _context.Spots
            .Include(s => s.Trips)
            .FirstOrDefaultAsync(s => s.SpotId == request.SpotId, cancellationToken);

        if (spot == null)
        {
            return null;
        }

        spot.Name = request.Name;
        spot.LocationType = request.LocationType;
        spot.Latitude = request.Latitude;
        spot.Longitude = request.Longitude;
        spot.Description = request.Description;
        spot.WaterBodyName = request.WaterBodyName;
        spot.Directions = request.Directions;
        spot.IsFavorite = request.IsFavorite;

        await _context.SaveChangesAsync(cancellationToken);

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
