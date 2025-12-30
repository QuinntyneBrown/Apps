// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Command to update an assignment.
/// </summary>
public class UpdateAssignment : IRequest<AssignmentDto?>
{
    /// <summary>
    /// Gets or sets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; set; }

    /// <summary>
    /// Gets or sets the assignment data.
    /// </summary>
    public UpdateAssignmentDto Assignment { get; set; } = null!;
}

/// <summary>
/// Handler for UpdateAssignment command.
/// </summary>
public class UpdateAssignmentHandler : IRequestHandler<UpdateAssignment, AssignmentDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAssignmentHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateAssignmentHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the UpdateAssignment command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated assignment DTO or null if not found.</returns>
    public async Task<AssignmentDto?> Handle(UpdateAssignment request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Chore)
            .Include(a => a.FamilyMember)
            .FirstOrDefaultAsync(a => a.AssignmentId == request.AssignmentId, cancellationToken);

        if (assignment == null)
        {
            return null;
        }

        assignment.DueDate = request.Assignment.DueDate;
        assignment.Notes = request.Assignment.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new AssignmentDto
        {
            AssignmentId = assignment.AssignmentId,
            ChoreId = assignment.ChoreId,
            ChoreName = assignment.Chore?.Name,
            FamilyMemberId = assignment.FamilyMemberId,
            FamilyMemberName = assignment.FamilyMember?.Name,
            AssignedDate = assignment.AssignedDate,
            DueDate = assignment.DueDate,
            CompletedDate = assignment.CompletedDate,
            IsCompleted = assignment.IsCompleted,
            IsVerified = assignment.IsVerified,
            Notes = assignment.Notes,
            PointsEarned = assignment.PointsEarned,
            IsOverdue = assignment.IsOverdue(),
            CreatedAt = assignment.CreatedAt
        };
    }
}
