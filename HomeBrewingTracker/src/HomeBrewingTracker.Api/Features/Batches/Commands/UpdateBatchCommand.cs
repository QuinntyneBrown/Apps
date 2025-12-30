// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Batches.Commands;

/// <summary>
/// Command to update an existing batch.
/// </summary>
public class UpdateBatchCommand : IRequest<BatchDto>
{
    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }

    /// <summary>
    /// Gets or sets the batch number.
    /// </summary>
    public string BatchNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public BatchStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the brew date.
    /// </summary>
    public DateTime BrewDate { get; set; }

    /// <summary>
    /// Gets or sets the bottling date.
    /// </summary>
    public DateTime? BottlingDate { get; set; }

    /// <summary>
    /// Gets or sets the actual original gravity.
    /// </summary>
    public decimal? ActualOriginalGravity { get; set; }

    /// <summary>
    /// Gets or sets the actual final gravity.
    /// </summary>
    public decimal? ActualFinalGravity { get; set; }

    /// <summary>
    /// Gets or sets the actual ABV.
    /// </summary>
    public decimal? ActualABV { get; set; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateBatchCommand.
/// </summary>
public class UpdateBatchCommandHandler : IRequestHandler<UpdateBatchCommand, BatchDto>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateBatchCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<BatchDto> Handle(UpdateBatchCommand request, CancellationToken cancellationToken)
    {
        var batch = await _context.Batches
            .FirstOrDefaultAsync(b => b.BatchId == request.BatchId, cancellationToken)
            ?? throw new InvalidOperationException($"Batch with ID {request.BatchId} not found.");

        batch.BatchNumber = request.BatchNumber;
        batch.Status = request.Status;
        batch.BrewDate = request.BrewDate;
        batch.BottlingDate = request.BottlingDate;
        batch.ActualOriginalGravity = request.ActualOriginalGravity;
        batch.ActualFinalGravity = request.ActualFinalGravity;
        batch.ActualABV = request.ActualABV;
        batch.Notes = request.Notes;

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
