// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Reminder;

public record DeleteReminderCommand(Guid ReminderId) : IRequest<Unit>;

public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, Unit>
{
    private readonly IHydrationTrackerContext _context;

    public DeleteReminderCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken)
            ?? throw new InvalidOperationException($"Reminder with ID {request.ReminderId} not found.");

        _context.Reminders.Remove(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
