// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Command to create an assignment.
/// </summary>
public class CreateAssignment : IRequest<AssignmentDto?>
{
    /// <summary>
    /// Gets or sets the assignment data.
    /// </summary>
    public CreateAssignmentDto Assignment { get; set; } = null!;
}

/// <summary>
/// Handler for CreateAssignment command.
/// </summary>
public class CreateAssignmentHandler : IRequestHandler<CreateAssignment, AssignmentDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAssignmentHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateAssignmentHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the CreateAssignment command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created assignment DTO or null if chore or family member not found.</returns>
    public async Task<AssignmentDto?> Handle(CreateAssignment request, CancellationToken cancellationToken)
    {
        // Verify chore and family member exist
        var chore = await _context.Chores
            .FirstOrDefaultAsync(c => c.ChoreId == request.Assignment.ChoreId, cancellationToken);
        var familyMember = await _context.FamilyMembers
            .FirstOrDefaultAsync(f => f.FamilyMemberId == request.Assignment.FamilyMemberId, cancellationToken);

        if (chore == null || familyMember == null)
        {
            return null;
        }

        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            ChoreId = request.Assignment.ChoreId,
            FamilyMemberId = request.Assignment.FamilyMemberId,
            AssignedDate = request.Assignment.AssignedDate,
            DueDate = request.Assignment.DueDate,
            Notes = request.Assignment.Notes,
            IsCompleted = false,
            IsVerified = false,
            PointsEarned = 0,
            CreatedAt = DateTime.UtcNow
        };

        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);

        return new AssignmentDto
        {
            AssignmentId = assignment.AssignmentId,
            ChoreId = assignment.ChoreId,
            ChoreName = chore.Name,
            FamilyMemberId = assignment.FamilyMemberId,
            FamilyMemberName = familyMember.Name,
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
