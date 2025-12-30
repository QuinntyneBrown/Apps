// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Command to delete an RSVP.
/// </summary>
public record DeleteRSVPCommand(Guid RSVPId) : IRequest<bool>;

/// <summary>
/// Handler for deleting an RSVP.
/// </summary>
public class DeleteRSVPCommandHandler : IRequestHandler<DeleteRSVPCommand, bool>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteRSVPCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteRSVPCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the delete RSVP command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteRSVPCommand request, CancellationToken cancellationToken)
    {
        var rsvp = await _context.RSVPs
            .FirstOrDefaultAsync(r => r.RSVPId == request.RSVPId, cancellationToken);

        if (rsvp == null)
        {
            return false;
        }

        _context.RSVPs.Remove(rsvp);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
