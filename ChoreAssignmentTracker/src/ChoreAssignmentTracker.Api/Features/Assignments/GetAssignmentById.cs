// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Query to get an assignment by ID.
/// </summary>
public class GetAssignmentById : IRequest<AssignmentDto?>
{
    /// <summary>
    /// Gets or sets the assignment ID.
    /// </summary>
    public Guid AssignmentId { get; set; }
}

/// <summary>
/// Handler for GetAssignmentById query.
/// </summary>
public class GetAssignmentByIdHandler : IRequestHandler<GetAssignmentById, AssignmentDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAssignmentByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAssignmentByIdHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetAssignmentById query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The assignment DTO or null if not found.</returns>
    public async Task<AssignmentDto?> Handle(GetAssignmentById request, CancellationToken cancellationToken)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Chore)
            .Include(a => a.FamilyMember)
            .FirstOrDefaultAsync(a => a.AssignmentId == request.AssignmentId, cancellationToken);

        if (assignment == null)
        {
            return null;
        }

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
