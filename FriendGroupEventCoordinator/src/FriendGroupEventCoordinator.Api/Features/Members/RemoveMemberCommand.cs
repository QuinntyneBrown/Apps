// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Command to remove a member (set IsActive to false).
/// </summary>
public record RemoveMemberCommand(Guid MemberId) : IRequest<MemberDto?>;

/// <summary>
/// Handler for removing a member.
/// </summary>
public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, MemberDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveMemberCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public RemoveMemberCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the remove member command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The removed member DTO or null if not found.</returns>
    public async Task<MemberDto?> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        if (member == null)
        {
            return null;
        }

        member.Remove();
        await _context.SaveChangesAsync(cancellationToken);

        return new MemberDto
        {
            MemberId = member.MemberId,
            GroupId = member.GroupId,
            UserId = member.UserId,
            Name = member.Name,
            Email = member.Email,
            IsAdmin = member.IsAdmin,
            IsActive = member.IsActive,
            JoinedAt = member.JoinedAt,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt
        };
    }
}
