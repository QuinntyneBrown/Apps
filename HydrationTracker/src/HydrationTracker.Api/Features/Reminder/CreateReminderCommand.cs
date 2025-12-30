// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Reminder;

public record CreateReminderCommand(
    Guid UserId,
    TimeSpan ReminderTime,
    string? Message,
    bool IsEnabled) : IRequest<ReminderDto>;

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ReminderDto>
{
    private readonly IHydrationTrackerContext _context;

    public CreateReminderCommandHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReminderDto> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        var reminder = new Core.Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = request.UserId,
            ReminderTime = request.ReminderTime,
            Message = request.Message,
            IsEnabled = request.IsEnabled,
            CreatedAt = DateTime.UtcNow
        };

        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync(cancellationToken);

        return reminder.ToDto();
    }
}
