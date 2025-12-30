// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Chores;

/// <summary>
/// Command to delete a chore.
/// </summary>
public class DeleteChore : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the chore ID.
    /// </summary>
    public Guid ChoreId { get; set; }
}

/// <summary>
/// Handler for DeleteChore command.
/// </summary>
public class DeleteChoreHandler : IRequestHandler<DeleteChore, bool>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteChoreHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteChoreHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the DeleteChore command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteChore request, CancellationToken cancellationToken)
    {
        var chore = await _context.Chores
            .FirstOrDefaultAsync(c => c.ChoreId == request.ChoreId, cancellationToken);

        if (chore == null)
        {
            return false;
        }

        _context.Chores.Remove(chore);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
