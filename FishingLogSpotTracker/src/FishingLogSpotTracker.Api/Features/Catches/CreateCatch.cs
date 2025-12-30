// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;

namespace FishingLogSpotTracker.Api.Features.Catches;

/// <summary>
/// Command to create a new catch.
/// </summary>
public class CreateCatchCommand : IRequest<CatchDto>
{
    /// <summary>
    /// Gets or sets the user ID who made this catch.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the trip ID associated with this catch.
    /// </summary>
    public Guid TripId { get; set; }

    /// <summary>
    /// Gets or sets the fish species.
    /// </summary>
    public FishSpecies Species { get; set; }

    /// <summary>
    /// Gets or sets the length of the fish in inches.
    /// </summary>
    public decimal? Length { get; set; }

    /// <summary>
    /// Gets or sets the weight of the fish in pounds.
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Gets or sets the time when the fish was caught.
    /// </summary>
    public DateTime CatchTime { get; set; }

    /// <summary>
    /// Gets or sets the bait or lure used.
    /// </summary>
    public string? BaitUsed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the fish was released.
    /// </summary>
    public bool WasReleased { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the catch.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the photo URL of the catch.
    /// </summary>
    public string? PhotoUrl { get; set; }
}

/// <summary>
/// Validator for CreateCatchCommand.
/// </summary>
public class CreateCatchCommandValidator : AbstractValidator<CreateCatchCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCatchCommandValidator"/> class.
    /// </summary>
    public CreateCatchCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.TripId).NotEmpty();
        RuleFor(x => x.Species).IsInEnum();
        RuleFor(x => x.CatchTime).NotEmpty();
        RuleFor(x => x.Length).GreaterThan(0).When(x => x.Length.HasValue);
        RuleFor(x => x.Weight).GreaterThan(0).When(x => x.Weight.HasValue);
    }
}

/// <summary>
/// Handler for CreateCatchCommand.
/// </summary>
public class CreateCatchCommandHandler : IRequestHandler<CreateCatchCommand, CatchDto>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateCatchCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the CreateCatchCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created catch DTO.</returns>
    public async Task<CatchDto> Handle(CreateCatchCommand request, CancellationToken cancellationToken)
    {
        var catchEntity = new Catch
        {
            CatchId = Guid.NewGuid(),
            UserId = request.UserId,
            TripId = request.TripId,
            Species = request.Species,
            Length = request.Length,
            Weight = request.Weight,
            CatchTime = request.CatchTime,
            BaitUsed = request.BaitUsed,
            WasReleased = request.WasReleased,
            Notes = request.Notes,
            PhotoUrl = request.PhotoUrl,
            CreatedAt = DateTime.UtcNow
        };

        _context.Catches.Add(catchEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CatchDto
        {
            CatchId = catchEntity.CatchId,
            UserId = catchEntity.UserId,
            TripId = catchEntity.TripId,
            Species = catchEntity.Species,
            Length = catchEntity.Length,
            Weight = catchEntity.Weight,
            CatchTime = catchEntity.CatchTime,
            BaitUsed = catchEntity.BaitUsed,
            WasReleased = catchEntity.WasReleased,
            Notes = catchEntity.Notes,
            PhotoUrl = catchEntity.PhotoUrl,
            CreatedAt = catchEntity.CreatedAt
        };
    }
}
