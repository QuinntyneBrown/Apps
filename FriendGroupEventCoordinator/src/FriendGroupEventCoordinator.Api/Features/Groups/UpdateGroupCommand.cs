// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Command to update an existing group.
/// </summary>
public record UpdateGroupCommand(Guid GroupId, UpdateGroupDto Group) : IRequest<GroupDto?>;

/// <summary>
/// Handler for updating an existing group.
/// </summary>
public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateGroupCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the update group command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated group DTO or null if not found.</returns>
    public async Task<GroupDto?> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            return null;
        }

        group.Name = request.Group.Name;
        group.Description = request.Group.Description;
        group.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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
