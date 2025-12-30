// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Command to verify an assignment.
/// </summary>
public class VerifyAssignment : IRequest<AssignmentDto?>
{
    /// <summary>
    /// Gets or sets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; set; }

    /// <summary>
    /// Gets or sets the verification data.
    /// </summary>
    public VerifyAssignmentDto VerificationData { get; set; } = null!;
}

/// <summary>
/// Handler for VerifyAssignment command.
/// </summary>
public class VerifyAssignmentHandler : IRequestHandler<VerifyAssignment, AssignmentDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="VerifyAssignmentHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public VerifyAssignmentHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the VerifyAssignment command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The verified assignment DTO or null if not found.</returns>
    public async Task<AssignmentDto?> Handle(VerifyAssignment request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Chore)
            .Include(a => a.FamilyMember)
            .FirstOrDefaultAsync(a => a.AssignmentId == request.AssignmentId, cancellationToken);

        if (assignment == null || assignment.FamilyMember == null)
        {
            return null;
        }

        // Verify the assignment and award points
        assignment.Verify(request.VerificationData.Points);
        assignment.FamilyMember.AwardPoints(request.VerificationData.Points);

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
