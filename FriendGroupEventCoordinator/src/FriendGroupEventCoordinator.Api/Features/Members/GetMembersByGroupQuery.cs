// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Query to get all members for a specific group.
/// </summary>
public record GetMembersByGroupQuery(Guid GroupId) : IRequest<List<MemberDto>>;

/// <summary>
/// Handler for getting all members for a specific group.
/// </summary>
public class GetMembersByGroupQueryHandler : IRequestHandler<GetMembersByGroupQuery, List<MemberDto>>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMembersByGroupQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetMembersByGroupQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get members by group query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of member DTOs for the specified group.</returns>
    public async Task<List<MemberDto>> Handle(GetMembersByGroupQuery request, CancellationToken cancellationToken)
    {
        var members = await _context.Members
            .Where(m => m.GroupId == request.GroupId)
            .OrderByDescending(m => m.JoinedAt)
            .ToListAsync(cancellationToken);

        return members.Select(m => new MemberDto
        {
            MemberId = m.MemberId,
            GroupId = m.GroupId,
            UserId = m.UserId,
            Name = m.Name,
            Email = m.Email,
            IsAdmin = m.IsAdmin,
            IsActive = m.IsActive,
            JoinedAt = m.JoinedAt,
            CreatedAt = m.CreatedAt,
            UpdatedAt = m.UpdatedAt
        }).ToList();
    }
}
