// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Query to get an RSVP by ID.
/// </summary>
public record GetRSVPQuery(Guid RSVPId) : IRequest<RSVPDto?>;

/// <summary>
/// Handler for getting an RSVP by ID.
/// </summary>
public class GetRSVPQueryHandler : IRequestHandler<GetRSVPQuery, RSVPDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRSVPQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRSVPQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get RSVP query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The RSVP DTO or null if not found.</returns>
    public async Task<RSVPDto?> Handle(GetRSVPQuery request, CancellationToken cancellationToken)
    {
        var rsvp = await _context.RSVPs
            .FirstOrDefaultAsync(r => r.RSVPId == request.RSVPId, cancellationToken);

        if (rsvp == null)
        {
            return null;
        }

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
