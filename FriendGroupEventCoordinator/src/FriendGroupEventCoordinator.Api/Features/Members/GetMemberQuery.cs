// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Query to get a member by ID.
/// </summary>
public record GetMemberQuery(Guid MemberId) : IRequest<MemberDto?>;

/// <summary>
/// Handler for getting a member by ID.
/// </summary>
public class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, MemberDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMemberQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetMemberQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get member query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The member DTO or null if not found.</returns>
    public async Task<MemberDto?> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var member = await _context.Members
            .FirstOrDefaultAsync(m => m.MemberId == request.MemberId, cancellationToken);

        if (member == null)
        {
            return null;
        }

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
