// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Query to get all events for a specific group.
/// </summary>
public record GetEventsByGroupQuery(Guid GroupId) : IRequest<List<EventDto>>;

/// <summary>
/// Handler for getting all events for a specific group.
/// </summary>
public class GetEventsByGroupQueryHandler : IRequestHandler<GetEventsByGroupQuery, List<EventDto>>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEventsByGroupQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetEventsByGroupQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get events by group query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A list of event DTOs for the specified group.</returns>
    public async Task<List<EventDto>> Handle(GetEventsByGroupQuery request, CancellationToken cancellationToken)
    {
        var events = await _context.Events
            .Include(e => e.RSVPs)
            .Where(e => e.GroupId == request.GroupId)
            .OrderByDescending(e => e.StartDateTime)
            .ToListAsync(cancellationToken);

        return events.Select(e => new EventDto
        {
            EventId = e.EventId,
            GroupId = e.GroupId,
            CreatedByUserId = e.CreatedByUserId,
            Title = e.Title,
            Description = e.Description,
            EventType = e.EventType,
            StartDateTime = e.StartDateTime,
            EndDateTime = e.EndDateTime,
            Location = e.Location,
            MaxAttendees = e.MaxAttendees,
            IsCancelled = e.IsCancelled,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            ConfirmedAttendeeCount = e.GetConfirmedAttendeeCount()
        }).ToList();
    }
}
