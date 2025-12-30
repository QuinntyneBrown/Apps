// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Reminder;

public record UpdateReminderCommand(
    Guid ReminderId,
    Guid UserId,
    TimeSpan ReminderTime,
    string? Message,
    bool IsEnabled) : IRequest<ReminderDto>;

public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ReminderDto>
{
    private readonly IHydrationTrackerContext _context;

    public UpdateReminderCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReminderDto> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken)
            ?? throw new InvalidOperationException($"Reminder with ID {request.ReminderId} not found.");

        reminder.UserId = request.UserId;
        reminder.ReminderTime = request.ReminderTime;
        reminder.Message = request.Message;
        reminder.IsEnabled = request.IsEnabled;

        await _context.SaveChangesAsync(cancellationToken);

        return reminder.ToDto();
    }
}
