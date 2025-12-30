// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Query to get an event by ID.
/// </summary>
public record GetEventQuery(Guid EventId) : IRequest<EventDto?>;

/// <summary>
/// Handler for getting an event by ID.
/// </summary>
public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEventQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetEventQueryHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the get event query.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The event DTO or null if not found.</returns>
    public async Task<EventDto?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
            .Include(e => e.RSVPs)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            return null;
        }

        return new EventDto
        {
            EventId = eventEntity.EventId,
            GroupId = eventEntity.GroupId,
            CreatedByUserId = eventEntity.CreatedByUserId,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            EventType = eventEntity.EventType,
            StartDateTime = eventEntity.StartDateTime,
            EndDateTime = eventEntity.EndDateTime,
            Location = eventEntity.Location,
            MaxAttendees = eventEntity.MaxAttendees,
            IsCancelled = eventEntity.IsCancelled,
            CreatedAt = eventEntity.CreatedAt,
            UpdatedAt = eventEntity.UpdatedAt,
            ConfirmedAttendeeCount = eventEntity.GetConfirmedAttendeeCount()
        };
    }
}
