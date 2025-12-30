// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Spots;

/// <summary>
/// Command to create a new spot.
/// </summary>
public class CreateSpotCommand : IRequest<SpotDto>
{
    /// <summary>
    /// Gets or sets the user ID who owns this spot.
    /// </summary>
    public Guid UserId { get; set; }

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
/// Validator for CreateSpotCommand.
/// </summary>
public class CreateSpotCommandValidator : AbstractValidator<CreateSpotCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSpotCommandValidator"/> class.
    /// </summary>
    public CreateSpotCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LocationType).IsInEnum();
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);
    }
}

/// <summary>
/// Handler for CreateSpotCommand.
/// </summary>
public class CreateSpotCommandHandler : IRequestHandler<CreateSpotCommand, SpotDto>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSpotCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateSpotCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the CreateSpotCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created spot DTO.</returns>
    public async Task<SpotDto> Handle(CreateSpotCommand request, CancellationToken cancellationToken)
    {
        var spot = new Spot
        {
            SpotId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            LocationType = request.LocationType,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Description = request.Description,
            WaterBodyName = request.WaterBodyName,
            Directions = request.Directions,
            IsFavorite = request.IsFavorite,
            CreatedAt = DateTime.UtcNow
        };

        _context.Spots.Add(spot);
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
            TripCount = 0
        };
    }
}
