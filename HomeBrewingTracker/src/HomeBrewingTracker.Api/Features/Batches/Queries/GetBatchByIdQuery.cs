// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Batches.Queries;

/// <summary>
/// Query to get a batch by ID.
/// </summary>
public class GetBatchByIdQuery : IRequest<BatchDto?>
{
    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }
}

/// <summary>
/// Handler for GetBatchByIdQuery.
/// </summary>
public class GetBatchByIdQueryHandler : IRequestHandler<GetBatchByIdQuery, BatchDto?>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBatchByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetBatchByIdQueryHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<BatchDto?> Handle(GetBatchByIdQuery request, CancellationToken cancellationToken)
    {
        var batch = await _context.Batches
            .FirstOrDefaultAsync(b => b.BatchId == request.BatchId, cancellationToken);

        if (batch == null)
        {
            return null;
        }

        return new BatchDto
        {
            BatchId = batch.BatchId,
            UserId = batch.UserId,
            RecipeId = batch.RecipeId,
            BatchNumber = batch.BatchNumber,
            Status = batch.Status,
            BrewDate = batch.BrewDate,
            BottlingDate = batch.BottlingDate,
            ActualOriginalGravity = batch.ActualOriginalGravity,
            ActualFinalGravity = batch.ActualFinalGravity,
            ActualABV = batch.ActualABV,
            Notes = batch.Notes,
            CreatedAt = batch.CreatedAt,
        };
    }
}
