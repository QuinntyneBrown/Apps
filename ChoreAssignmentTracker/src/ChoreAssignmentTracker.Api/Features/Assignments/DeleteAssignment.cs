// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Command to delete an assignment.
/// </summary>
public class DeleteAssignment : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; set; }
}

/// <summary>
/// Handler for DeleteAssignment command.
/// </summary>
public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignment, bool>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteAssignmentHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteAssignmentHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the DeleteAssignment command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteAssignment request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .FirstOrDefaultAsync(a => a.AssignmentId == request.AssignmentId, cancellationToken);

        if (assignment == null)
        {
            return false;
        }

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
