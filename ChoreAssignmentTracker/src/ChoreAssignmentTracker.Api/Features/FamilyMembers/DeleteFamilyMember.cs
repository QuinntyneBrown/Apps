// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Command to delete a family member.
/// </summary>
public class DeleteFamilyMember : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the family member ID.
    /// </summary>
    public Guid FamilyMemberId { get; set; }
}

/// <summary>
/// Handler for DeleteFamilyMember command.
/// </summary>
public class DeleteFamilyMemberHandler : IRequestHandler<DeleteFamilyMember, bool>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFamilyMemberHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteFamilyMemberHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the DeleteFamilyMember command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    public async Task<bool> Handle(DeleteFamilyMember request, CancellationToken cancellationToken)
    {
        var familyMember = await _context.FamilyMembers
            .FirstOrDefaultAsync(f => f.FamilyMemberId == request.FamilyMemberId, cancellationToken);

        if (familyMember == null)
        {
            return false;
        }

        _context.FamilyMembers.Remove(familyMember);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
