// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Query to get a family member by ID.
/// </summary>
public class GetFamilyMemberById : IRequest<FamilyMemberDto?>
{
    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }
}

/// <summary>
/// Handler for GetFamilyMemberById query.
/// </summary>
public class GetFamilyMemberByIdHandler : IRequestHandler<GetFamilyMemberById, FamilyMemberDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFamilyMemberByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetFamilyMemberByIdHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the GetFamilyMemberById query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The family member DTO or null if not found.</returns>
    public async Task<FamilyMemberDto?> Handle(GetFamilyMemberById request, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers
            .Include(f => f.Assignments)
            .FirstOrDefaultAsync(f => f.FamilyMemberId == request.FamilyMemberId, cancellationToken);

        if (familyMember == null)
        {
            return null;
        }

        return new FamilyMemberDto
        {
            FamilyMemberId = familyMember.FamilyMemberId,
            UserId = familyMember.UserId,
            Name = familyMember.Name,
            Age = familyMember.Age,
            Avatar = familyMember.Avatar,
            TotalPoints = familyMember.TotalPoints,
            AvailablePoints = familyMember.AvailablePoints,
            IsActive = familyMember.IsActive,
            CreatedAt = familyMember.CreatedAt,
            CompletionRate = familyMember.GetCompletionRate()
        };
    }
}
