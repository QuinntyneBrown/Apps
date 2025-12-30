// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Command to update an existing RSVP.
/// </summary>
public record UpdateRSVPCommand(Guid RSVPId, UpdateRSVPDto RSVP) : IRequest<RSVPDto?>;

/// <summary>
/// Handler for updating an existing RSVP.
/// </summary>
public class UpdateRSVPCommandHandler : IRequestHandler<UpdateRSVPCommand, RSVPDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRSVPCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateRSVPCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the update RSVP command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated RSVP DTO or null if not found.</returns>
    public async Task<RSVPDto?> Handle(UpdateRSVPCommand request, CancellationToken cancellationToken)
    {
        var rsvp = await _context.RSVPs
            .FirstOrDefaultAsync(r => r.RSVPId == request.RSVPId, cancellationToken);

        if (rsvp == null)
        {
            return null;
        }

        rsvp.UpdateResponse(request.RSVP.Response);
        rsvp.AdditionalGuests = request.RSVP.AdditionalGuests;
        rsvp.Notes = request.RSVP.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new RSVPDto
        {
            RSVPId = rsvp.RSVPId,
            EventId = rsvp.EventId,
            MemberId = rsvp.MemberId,
            UserId = rsvp.UserId,
            Response = rsvp.Response,
            AdditionalGuests = rsvp.AdditionalGuests,
            Notes = rsvp.Notes,
            CreatedAt = rsvp.CreatedAt,
            UpdatedAt = rsvp.UpdatedAt
        };
    }
}
