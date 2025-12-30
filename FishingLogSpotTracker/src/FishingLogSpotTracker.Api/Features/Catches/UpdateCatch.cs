// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Catches;

/// <summary>
/// Command to update an existing catch.
/// </summary>
public class UpdateCatchCommand : IRequest<CatchDto?>
{
    /// <summary>
    /// Gets or sets the catch ID.
    /// </summary>
    public Guid CatchId { get; set; }

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
/// Validator for UpdateCatchCommand.
/// </summary>
public class UpdateCatchCommandValidator : AbstractValidator<UpdateCatchCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCatchCommandValidator"/> class.
    /// </summary>
    public UpdateCatchCommandValidator()
    {
        RuleFor(x => x.CatchId).NotEmpty();
        RuleFor(x => x.Species).IsInEnum();
        RuleFor(x => x.CatchTime).NotEmpty();
        RuleFor(x => x.Length).GreaterThan(0).When(x => x.Length.HasValue);
        RuleFor(x => x.Weight).GreaterThan(0).When(x => x.Weight.HasValue);
    }
}

/// <summary>
/// Handler for UpdateCatchCommand.
/// </summary>
public class UpdateCatchCommandHandler : IRequestHandler<UpdateCatchCommand, CatchDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateCatchCommandHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the UpdateCatchCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated catch DTO or null if not found.</returns>
    public async Task<CatchDto?> Handle(UpdateCatchCommand request, CancellationToken cancellationToken)
    {
        var catchEntity = await _context.Catches
            .FirstOrDefaultAsync(c => c.CatchId == request.CatchId, cancellationToken);

        if (catchEntity == null)
        {
            return null;
        }

        catchEntity.Species = request.Species;
        catchEntity.Length = request.Length;
        catchEntity.Weight = request.Weight;
        catchEntity.CatchTime = request.CatchTime;
        catchEntity.BaitUsed = request.BaitUsed;
        catchEntity.WasReleased = request.WasReleased;
        catchEntity.Notes = request.Notes;
        catchEntity.PhotoUrl = request.PhotoUrl;

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
