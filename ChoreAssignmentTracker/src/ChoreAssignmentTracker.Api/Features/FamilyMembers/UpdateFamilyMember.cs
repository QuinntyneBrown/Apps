// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Command to update a family member.
/// </summary>
public class UpdateFamilyMember : IRequest<FamilyMemberDto?>
{
    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }

    /// <summary>
    /// Gets or sets the family member data.
    /// </summary>
    public UpdateFamilyMemberDto FamilyMember { get; set; } = null!;
}

/// <summary>
/// Handler for UpdateFamilyMember command.
/// </summary>
public class UpdateFamilyMemberHandler : IRequestHandler<UpdateFamilyMember, FamilyMemberDto?>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFamilyMemberHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateFamilyMemberHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the UpdateFamilyMember command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated family member DTO or null if not found.</returns>
    public async Task<FamilyMemberDto?> Handle(UpdateFamilyMember request, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers
            .Include(f => f.Assignments)
            .FirstOrDefaultAsync(f => f.FamilyMemberId == request.FamilyMemberId, cancellationToken);

        if (familyMember == null)
        {
            return null;
        }

        familyMember.Name = request.FamilyMember.Name;
        familyMember.Age = request.FamilyMember.Age;
        familyMember.Avatar = request.FamilyMember.Avatar;
        familyMember.IsActive = request.FamilyMember.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

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
