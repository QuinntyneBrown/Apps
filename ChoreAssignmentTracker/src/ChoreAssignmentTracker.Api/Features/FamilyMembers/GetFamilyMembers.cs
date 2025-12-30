// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Query to get all family members.
/// </summary>
public class GetFamilyMembers : IRequest<List<FamilyMemberDto>>
{
    /// <summary>
    /// Gets or sets the user ID to filter by.
    /// </summary>
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetFamilyMembers query.
/// </summary>
public class GetFamilyMembersHandler : IRequestHandler<GetFamilyMembers, List<FamilyMemberDto>>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFamilyMembersHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetFamilyMembersHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetFamilyMembers query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of family member DTOs.</returns>
    public async Task<List<FamilyMemberDto>> Handle(GetFamilyMembers request, CancellationToken cancellationToken)
    {
        var query = _context.FamilyMembers
            .Include(f => f.Assignments)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(f => f.UserId == request.UserId.Value);
        }

        var familyMembers = await query.ToListAsync(cancellationToken);

        return familyMembers.Select(f => new FamilyMemberDto
        {
            FamilyMemberId = f.FamilyMemberId,
            UserId = f.UserId,
            Name = f.Name,
            Age = f.Age,
            Avatar = f.Avatar,
            TotalPoints = f.TotalPoints,
            AvailablePoints = f.AvailablePoints,
            IsActive = f.IsActive,
            CreatedAt = f.CreatedAt,
            CompletionRate = f.GetCompletionRate()
        }).ToList();
    }
}
