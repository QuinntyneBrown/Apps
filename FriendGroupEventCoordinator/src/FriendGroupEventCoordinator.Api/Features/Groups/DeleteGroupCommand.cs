// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Groups;

/// <summary>
/// Command to delete a group.
/// </summary>
public record DeleteGroupCommand(Guid GroupId) : IRequest<bool>;

/// <summary>
/// Handler for deleting a group.
/// </summary>
public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, bool>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteGroupCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteGroupCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the delete group command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups
            .FirstOrDefaultAsync(g => g.GroupId == request.GroupId, cancellationToken);

        if (group == null)
        {
            return false;
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
