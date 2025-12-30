// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Command to deactivate a group.
/// </summary>
public record DeactivateGroupCommand(Guid GroupId) : IRequest<GroupDto?>;

/// <summary>
/// Handler for deactivating a group.
/// </summary>
public class DeactivateGroupCommandHandler : IRequestHandler<DeactivateGroupCommand, GroupDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeactivateGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeactivateGroupCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the deactivate group command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deactivated group DTO or null if not found.</returns>
    public async Task<GroupDto?> Handle(DeactivateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .Include(g => g.Members)
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            return null;
        }

        group.Deactivate();
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
