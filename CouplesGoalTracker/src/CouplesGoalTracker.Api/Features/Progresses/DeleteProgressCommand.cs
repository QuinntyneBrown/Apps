// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CouplesGoalTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CouplesGoalTracker.Api.Features.Progresses;

/// <summary>
/// Command to delete a progress entry.
/// </summary>
public class DeleteProgressCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the progress ID to delete.
    /// </summary>
    public Guid ProgressId { get; set; }
}

/// <summary>
/// Handler for DeleteProgressCommand.
/// </summary>
public class DeleteProgressCommandHandler : IRequestHandler<DeleteProgressCommand, bool>
{
    private readonly ICouplesGoalTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteProgressCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteProgressCommandHandler(ICouplesGoalTrackerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteProgressCommand request, CancellationToken cancellationToken)
    {
        var progress = await _context.Progresses
            .FirstOrDefaultAsync(p => p.ProgressId == request.ProgressId, cancellationToken);

        if (progress == null)
        {
            return false;
        }

        _context.Progresses.Remove(progress);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
