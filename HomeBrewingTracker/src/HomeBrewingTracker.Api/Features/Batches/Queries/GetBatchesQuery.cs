// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Batches.Queries;

/// <summary>
/// Query to get all batches.
/// </summary>
public class GetBatchesQuery : IRequest<List<BatchDto>>
{
    /// <summary>
    /// Gets or sets the optional user ID filter.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional recipe ID filter.
    /// </summary>
    public Guid? RecipeId { get; set; }
}

/// <summary>
/// Handler for GetBatchesQuery.
/// </summary>
public class GetBatchesQueryHandler : IRequestHandler<GetBatchesQuery, List<BatchDto>>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBatchesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetBatchesQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<BatchDto>> Handle(GetBatchesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Batches.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(b => b.UserId == request.UserId.Value);
        }

        if (request.RecipeId.HasValue)
        {
            query = query.Where(b => b.RecipeId == request.RecipeId.Value);
        }

        var batches = await query
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);

        return batches.Select(b => new BatchDto
        {
            BatchId = b.BatchId,
            UserId = b.UserId,
            RecipeId = b.RecipeId,
            BatchNumber = b.BatchNumber,
            Status = b.Status,
            BrewDate = b.BrewDate,
            BottlingDate = b.BottlingDate,
            ActualOriginalGravity = b.ActualOriginalGravity,
            ActualFinalGravity = b.ActualFinalGravity,
            ActualABV = b.ActualABV,
            Notes = b.Notes,
            CreatedAt = b.CreatedAt,
        }).ToList();
    }
}
