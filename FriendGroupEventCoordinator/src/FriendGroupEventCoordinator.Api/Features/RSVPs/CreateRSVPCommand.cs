// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Command to create a new RSVP.
/// </summary>
public record CreateRSVPCommand(CreateRSVPDto RSVP) : IRequest<RSVPDto>;

/// <summary>
/// Handler for creating a new RSVP.
/// </summary>
public class CreateRSVPCommandHandler : IRequestHandler<CreateRSVPCommand, RSVPDto>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRSVPCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateRSVPCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the create RSVP command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created RSVP DTO.</returns>
    public async Task<RSVPDto> Handle(CreateRSVPCommand request, CancellationToken cancellationToken)
    {
        var rsvp = new RSVP
        {
            RSVPId = Guid.NewGuid(),
            EventId = request.RSVP.EventId,
            MemberId = request.RSVP.MemberId,
            UserId = request.RSVP.UserId,
            Response = request.RSVP.Response,
            AdditionalGuests = request.RSVP.AdditionalGuests,
            Notes = request.RSVP.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.RSVPs.Add(rsvp);
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
