// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.RSVPs;

/// <summary>
/// Query to get all RSVPs for a specific event.
/// </summary>
public record GetRSVPsByEventQuery(Guid EventId) : IRequest<List<RSVPDto>>;

/// <summary>
/// Handler for getting all RSVPs for a specific event.
/// </summary>
public class GetRSVPsByEventQueryHandler : IRequestHandler<GetRSVPsByEventQuery, List<RSVPDto>>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetRSVPsByEventQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetRSVPsByEventQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get RSVPs by event query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of RSVP DTOs for the specified event.</returns>
    public async Task<List<RSVPDto>> Handle(GetRSVPsByEventQuery request, CancellationToken cancellationToken)
    {
        var rsvps = await _context.RSVPs
            .Where(r => r.EventId == request.EventId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return rsvps.Select(r => new RSVPDto
        {
            RSVPId = r.RSVPId,
            EventId = r.EventId,
            MemberId = r.MemberId,
            UserId = r.UserId,
            Response = r.Response,
            AdditionalGuests = r.AdditionalGuests,
            Notes = r.Notes,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        }).ToList();
    }
}
