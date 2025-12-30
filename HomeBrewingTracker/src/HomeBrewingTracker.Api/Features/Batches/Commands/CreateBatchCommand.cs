// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Batches.Commands;

/// <summary>
/// Command to create a new batch.
/// </summary>
public class CreateBatchCommand : IRequest<BatchDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the recipe ID.
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    public string BatchNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brew date.
    /// </summary>
    public DateTime BrewDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateBatchCommand.
/// </summary>
public class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, BatchDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateBatchCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<BatchDto> Handle(CreateBatchCommand request, CancellationToken cancellationToken)
    {
        var batch = new Batch
        {
            BatchId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipeId = request.RecipeId,
            BatchNumber = request.BatchNumber,
            Status = BatchStatus.Planned,
            BrewDate = request.BrewDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Batches.Add(batch);
        await _context.SaveChangesAsync(cancellationToken);

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
