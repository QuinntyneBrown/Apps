// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FriendGroupEventCoordinator.Api.Features.Events;

/// <summary>
/// Command to cancel an event.
/// </summary>
public record CancelEventCommand(Guid EventId) : IRequest<EventDto?>;

/// <summary>
/// Handler for cancelling an event.
/// </summary>
public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand, EventDto?>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelEventCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CancelEventCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the cancel event command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cancelled event DTO or null if not found.</returns>
    public async Task<EventDto?> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _context.Events
            .Include(e => e.RSVPs)
            .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken);

        if (eventEntity == null)
        {
            return null;
        }

        eventEntity.Cancel();
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
