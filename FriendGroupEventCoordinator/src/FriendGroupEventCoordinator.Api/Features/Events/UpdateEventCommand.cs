// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Command to update an existing event.
/// </summary>
public record UpdateEventCommand(Guid EventId, UpdateEventDto Event) : IRequest<EventDto?>;

/// <summary>
/// Handler for updating an existing event.
/// </summary>
public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateEventCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateEventCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the update event command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated event DTO or null if not found.</returns>
    public async Task<EventDto?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
            .Include(e => e.RSVPs)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            return null;
        }

        eventEntity.Title = request.Event.Title;
        eventEntity.Description = request.Event.Description;
        eventEntity.EventType = request.Event.EventType;
        eventEntity.StartDateTime = request.Event.StartDateTime;
        eventEntity.EndDateTime = request.Event.EndDateTime;
        eventEntity.Location = request.Event.Location;
        eventEntity.MaxAttendees = request.Event.MaxAttendees;
        eventEntity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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
