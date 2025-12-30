// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Command to delete an event.
/// </summary>
public record DeleteEventCommand(Guid EventId) : IRequest<bool>;

/// <summary>
/// Handler for deleting an event.
/// </summary>
public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEventCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteEventCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the delete event command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            return false;
        }

        _context.Events.Remove(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
