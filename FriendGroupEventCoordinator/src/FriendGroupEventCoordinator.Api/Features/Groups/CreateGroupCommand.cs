// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Command to create a new group.
/// </summary>
public record CreateGroupCommand(CreateGroupDto Group) : IRequest<GroupDto>;

/// <summary>
/// Handler for creating a new group.
/// </summary>
public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupDto>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateGroupCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the create group command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created group DTO.</returns>
    public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Group
        {
            GroupId = Guid.NewGuid(),
            CreatedByUserId = request.Group.CreatedByUserId,
            Name = request.Group.Name,
            Description = request.Group.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Groups.Add(group);
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
            ActiveMemberCount = 0
        };
    }
}
