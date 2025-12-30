// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Query to get all groups.
/// </summary>
public record GetGroupsQuery : IRequest<List<GroupDto>>;

/// <summary>
/// Handler for getting all groups.
/// </summary>
public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GroupDto>>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGroupsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetGroupsQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get groups query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of group DTOs.</returns>
    public async Task<List<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.Groups
            .Include(g => g.Members)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync(cancellationToken);

        return groups.Select(g => new GroupDto
        {
            GroupId = g.GroupId,
            CreatedByUserId = g.CreatedByUserId,
            Name = g.Name,
            Description = g.Description,
            IsActive = g.IsActive,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt,
            ActiveMemberCount = g.GetActiveMemberCount()
        }).ToList();
    }
}
