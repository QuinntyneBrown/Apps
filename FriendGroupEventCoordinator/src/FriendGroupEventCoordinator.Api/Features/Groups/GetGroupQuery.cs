// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Query to get a group by ID.
/// </summary>
public record GetGroupQuery(Guid GroupId) : IRequest<GroupDto?>;

/// <summary>
/// Handler for getting a group by ID.
/// </summary>
public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetGroupQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get group query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The group DTO or null if not found.</returns>
    public async Task<GroupDto?> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            return null;
        }

        return new GroupDto
        {
            GroupId = group.GroupId,
            CreatedByUserId = group.CreatedByUserId,
            Name = group.Name,
            Description = group.Description,
            IsActive = group.IsActive,
            CreatedAt = group.CreatedAt,
            UpdatedAt = group.UpdatedAt,
            ActiveMemberCount = group.GetActiveMemberCount()
        };
    }
}
