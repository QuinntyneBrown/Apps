// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeBrewingTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeBrewingTracker.Api.Features.Batches.Commands;

/// <summary>
/// Command to delete a batch.
/// </summary>
public class DeleteBatchCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the batch ID.
    /// </summary>
    public Guid BatchId { get; set; }
}

/// <summary>
/// Handler for DeleteBatchCommand.
/// </summary>
public class DeleteBatchCommandHandler : IRequestHandler<DeleteBatchCommand, Unit>
{
    private readonly IHomeBrewingTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBatchCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteBatchCommandHandler(IHomeBrewingTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Unit> Handle(DeleteBatchCommand request, CancellationToken cancellationToken)
    {
        var batch = await _context.Batches
            .FirstOrDefaultAsync(b => b.BatchId == request.BatchId, cancellationToken)
            ?? throw new InvalidOperationException($"Batch with ID {request.BatchId} not found.");

        _context.Batches.Remove(batch);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
