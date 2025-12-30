// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FishingLogSpotTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FishingLogSpotTracker.Api.Features.Catches;

/// <summary>
/// Query to get a catch by ID.
/// </summary>
public class GetCatchByIdQuery : IRequest<CatchDto?>
{
    /// <summary>
    /// Gets or sets the catch ID.
    /// </summary>
    public Guid CatchId { get; set; }
}

/// <summary>
/// Handler for GetCatchByIdQuery.
/// </summary>
public class GetCatchByIdQueryHandler : IRequestHandler<GetCatchByIdQuery, CatchDto?>
{
    private readonly IFishingLogSpotTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCatchByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetCatchByIdQueryHandler(IFishingLogSpotTrackerContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the GetCatchByIdQuery.
    /// </summary>
    /// <param name="request">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The catch DTO or null if not found.</returns>
    public async Task<CatchDto?> Handle(GetCatchByIdQuery request, CancellationToken cancellationToken)
    {
        var catchEntity = await _context.Catches
            .FirstOrDefaultAsync(c => c.CatchId == request.CatchId, cancellationToken);

        if (catchEntity == null)
        {
            return null;
        }

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
