// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Command to create a new event.
/// </summary>
public record CreateEventCommand(CreateEventDto Event) : IRequest<EventDto>;

/// <summary>
/// Handler for creating a new event.
/// </summary>
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEventCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateEventCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the create event command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created event DTO.</returns>
    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event
        {
            EventId = Guid.NewGuid(),
            GroupId = request.Event.GroupId,
            CreatedByUserId = request.Event.CreatedByUserId,
            Title = request.Event.Title,
            Description = request.Event.Description,
            EventType = request.Event.EventType,
            StartDateTime = request.Event.StartDateTime,
            EndDateTime = request.Event.EndDateTime,
            Location = request.Event.Location,
            MaxAttendees = request.Event.MaxAttendees,
            IsCancelled = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Events.Add(eventEntity);
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
            ConfirmedAttendeeCount = 0
        };
    }
}
