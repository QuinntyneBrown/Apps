// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.Assignments;

/// <summary>
/// Query to get all assignments.
/// </summary>
public class GetAssignments : IRequest<List<AssignmentDto>>
{
    /// <summary>
    /// Gets or sets the family member ID to filter by.
    /// </summary>
    public Guid? FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the chore ID to filter by.
    /// </summary>
    public Guid? ChoreId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include only completed assignments.
    /// </summary>
    public bool? IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include only overdue assignments.
    /// </summary>
    public bool? IsOverdue { get; set; }
}

/// <summary>
/// Handler for GetAssignments query.
/// </summary>
public class GetAssignmentsHandler : IRequestHandler<GetAssignments, List<AssignmentDto>>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAssignmentsHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetAssignmentsHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetAssignments query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of assignment DTOs.</returns>
    public async Task<List<AssignmentDto>> Handle(GetAssignments request, CancellationToken cancellationToken)
    {
        var query = _context.Assignments
            .Include(a => a.Chore)
            .Include(a => a.FamilyMember)
            .AsQueryable();

        if (request.FamilyMemberId.HasValue)
        {
            query = query.Where(a => a.FamilyMemberId == request.FamilyMemberId.Value);
        }

        if (request.ChoreId.HasValue)
        {
            query = query.Where(a => a.ChoreId == request.ChoreId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(a => a.IsCompleted == request.IsCompleted.Value);
        }

        var assignments = await query.ToListAsync(cancellationToken);

        // Filter by overdue if requested
        if (request.IsOverdue.HasValue && request.IsOverdue.Value)
        {
            assignments = assignments.Where(a => a.IsOverdue()).ToList();
        }

        return assignments.Select(a => new AssignmentDto
        {
            AssignmentId = a.AssignmentId,
            ChoreId = a.ChoreId,
            ChoreName = a.Chore?.Name,
            FamilyMemberId = a.FamilyMemberId,
            FamilyMemberName = a.FamilyMember?.Name,
            AssignedDate = a.AssignedDate,
            DueDate = a.DueDate,
            CompletedDate = a.CompletedDate,
            IsCompleted = a.IsCompleted,
            IsVerified = a.IsVerified,
            Notes = a.Notes,
            PointsEarned = a.PointsEarned,
            IsOverdue = a.IsOverdue(),
            CreatedAt = a.CreatedAt
        }).ToList();
    }
}
